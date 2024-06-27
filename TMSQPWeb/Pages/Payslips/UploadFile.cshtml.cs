using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;



using Microsoft.Office.Interop;
using Microsoft.Office.Interop.Excel;
using NPOI.HPSF;


namespace TMSQPWeb.Pages.Payslips
{
    public class UploadFileModel : PageModel
    {
        const int iColB = 2;
        const int iColC = 3;
        const int iColD = 4;
        const int iColE = 5;
        const int iColF = 6;
        const int iColG = 7;
        const int iColH = 8;
        const int iColI = 9;
        const int iColJ = 10;
        const int iColK = 11;
        const int iColL = 12;
        const int iColM = 13;
        const int iColN = 14;
        const int iColO = 15;
        const int iColP = 16;


        private readonly PayslipImportBindingService m_PayslipImportService;
        private readonly EmployeeBindingService m_EmployeeService;

        private readonly WebSystemService m_WebSystemService;
        private readonly FileManagementService m_FileManagementService;

        public UploadFileModel(PayslipImportBindingService payslipImportService, EmployeeBindingService employeeService, WebSystemService webSystemService, FileManagementService fileManagementService)
        {
            m_PayslipImportService = payslipImportService;
            m_EmployeeService = employeeService;
            m_WebSystemService = webSystemService;
            m_FileManagementService = fileManagementService;
        }







        public async Task OnGetAsync()
        {
            
        }











        public async Task Page_InitialAsync()
        {
            //PG_PageHeaderInfo = await m_WebSystemService.GetPageHeaderAsync(m_PageID);
            //ViewData[AppSystem.VD_PageFullTitle] = PG_PageHeaderInfo.FullTitle;
        }


        public async Task Page_LoadAsync()
        {

        }






        public async Task<IActionResult> OnPostUploadAsync(IFormFile _formFile)
        {
            if (_formFile == null)
            {
                TempData[AppSystem.TD_UI_OnPageLoad_Message_Notification] = "Upload failure (does not existed), please try again";
                await Page_LoadAsync();
                return Page();
            }

           
            var uploadPathName = m_WebSystemService.GetTargetFolderForUploadFile();
            var saveFileName = m_FileManagementService.AddNewSaveFileName(_formFile.FileName);
            TempData[AppSystem.TD_UP_PaySlipFileName] = saveFileName;


            var processResult = await m_FileManagementService.SaveFileAsync(_formFile, uploadPathName, saveFileName);
            if (processResult.HasError)
            {
                ModelState.AddModelError(string.Empty, processResult.Message);
                await Page_LoadAsync();
                return Page();
            }



            var info = await ImportByExcelAppAsync(_uploadFileName: _formFile.FileName
                , _importPathFileName: saveFileName);
            if (info == null)
            {
                ModelState.AddModelError(string.Empty, "Parse by Excel failure!");
                await Page_LoadAsync();
                return Page();
            }


            var result = await m_PayslipImportService.ProcessToImportAsync(info);
            if (result.HasError)
            {
                ModelState.AddModelError(string.Empty, "Import to DB failure!");
                await Page_LoadAsync();
                return Page();
            }


            TempData[AppSystem.TD_UI_OnPageLoad_Message_Notification] = "Import complete!";
            return new RedirectToPageResult("List");
        }









        private async Task<PayslipImportViewModel> ImportByExcelAppAsync(string _uploadFileName, string _importPathFileName)
        {
            var uploadPathName = m_WebSystemService.GetTargetFolderForUploadFile();
            var importPathFileName = Path.Combine(uploadPathName, _importPathFileName);

            

            Application app = new Application();
            var wb = app.ProtectedViewWindows.Open(importPathFileName);
            
            //var wb = app.Workbooks.Open(importPathFileName,ReadOnly:true);
            Worksheet ws = wb.Workbook.ActiveSheet;

            var info = new PayslipImportViewModel() { Remark = _uploadFileName };
            var importOperation = await m_PayslipImportService.AddEntityAsync(info);


            var iCurrentHeaderBasedRow = default(int);
            var iCurrentRow = default(int);
            var iSpaceRowsAfterFooter = default(int);
            var payslips = new List<PayslipImportHeadViewModel>();
            var currentHeaderNo = default(short);

            

            var piNo = importOperation.PINo;

            // 估計Body至Footer的間隔不會超過10列
            while (iSpaceRowsAfterFooter < 10)
            {
                iCurrentRow++;
                string currentSectionHeader = ws.Cells[iCurrentRow, iColB].Value;
                if (string.IsNullOrEmpty(currentSectionHeader) == false
                    && currentSectionHeader.Trim().ToUpper() == "TOP THERMO MFG.(MALAYSIA) SDN BHD")
                {
                    iCurrentHeaderBasedRow = iCurrentRow;
                    iSpaceRowsAfterFooter = default;
                    currentHeaderNo++;

                    //if (iCurrentHeaderBasedRow == 222)
                    //{
                    //    var ddddd = "dfasdf";
                    //}

                    var payslipHead = new PayslipImportHeadViewModel(piNo);
                    var iCurrentRowFooter = default(int);
                    var currentDetailNo = default(short);


                    payslipHead.HeaderNo = currentHeaderNo;
                    payslipHead.RowNo = iCurrentHeaderBasedRow;
                    payslipHead.Period = GetPeriodWithColumnOffset(ws, iCurrentHeaderBasedRow, iColH);


                    payslipHead.EmployeeName = ws.Cells[iCurrentHeaderBasedRow + 1, iColB].Value;
                    payslipHead.EmployeeId = GetEmployeeIdWithColumnOffset(ws, iCurrentHeaderBasedRow + 1, iColH);
                    
                    var employeeInfo = await m_EmployeeService.GetEntityAsync(payslipHead.EmployeeId, _enableTracking:false, _includeDetails:false);
                    payslipHead.EmployeeNo = employeeInfo?.EmployeeNo;
                   


                    payslipHead.DepartmentName = GetDepartmentNameWithColumnOffset(ws, iCurrentHeaderBasedRow + 2, iColC);
                    payslipHead.PaymentMethod = GetValueWithColumnOffset(ws, iCurrentHeaderBasedRow + 2, iColH);
                    payslipHead.BankAccount = GetBankAccountWithColumnOffset(ws, iCurrentHeaderBasedRow + 2, iColL);


                    #region "明細模式"
                    // 明細模式------------------------------------------------------
                    // 明細列的第一行
                    bool bIsStartSpaceRowBeforeFooter = false;
                    var iCurrentRowDetail = iCurrentHeaderBasedRow + 4;
                    // 假設明細列不超過16列
                    for (int j = 0; j < 15; j++)
                    {
                        iCurrentRow = iCurrentRowDetail + j;
                        var currentDetailRowNo = iCurrentRow;
                        // Income進項
                        var incomeName = ws.Cells[currentDetailRowNo, iColB].Value;

                        // Deduction扣項
                        var deductionName = GetValueWithColumnOffset(ws, currentDetailRowNo, iColH, _endColOffsetNum: 2);

                        if (string.IsNullOrEmpty(incomeName) && string.IsNullOrEmpty(deductionName))
                            bIsStartSpaceRowBeforeFooter = true;


                        if (bIsStartSpaceRowBeforeFooter == false)
                        {
                            var incomeAmount = GetDecimalWithColumnOffset(ws, currentDetailRowNo, iColF, _startColOffsetNum: null, _endColOffsetNum: 2, out _);
                            if (incomeAmount != null)
                            {
                                currentDetailNo++;
                                var currentPayslipDetail = new PayslipImportDetailViewModel(
                                        _piNo: piNo, _headerNo: currentHeaderNo, _detailNo: currentDetailNo, _detailTypeNo: 1, currentDetailRowNo, incomeName, incomeAmount);
                                payslipHead.PayslipImportDetails.Add(currentPayslipDetail);
                            }


                            var deductionAmount = GetDecimalWithColumnOffset(ws, currentDetailRowNo, iColL, _startColOffsetNum: null, _endColOffsetNum: 4, out _);
                            if (deductionAmount != null)
                            {
                                currentDetailNo++;
                                var currentPayslipDetail = new PayslipImportDetailViewModel(
                                    _piNo: piNo, _headerNo: currentHeaderNo, _detailNo: currentDetailNo, _detailTypeNo: 2, currentDetailRowNo, deductionName, deductionAmount);
                                payslipHead.PayslipImportDetails.Add(currentPayslipDetail);
                            }
                        }



                        if (bIsStartSpaceRowBeforeFooter)
                        {
                            string total = GetValueWithColumnOffset(ws, currentDetailRowNo, iColD, _endColOffsetNum: 2);
                            if (string.IsNullOrEmpty(total) == false && total.ToUpper().Trim() == "TOTAL :")
                            {
                                iCurrentRowFooter = currentDetailRowNo;
                                payslipHead.FooterRowNo = iCurrentRowFooter;
                                break; // 離開明細模式
                            }
                        }
                    }
                    #endregion   // end of 明細模式




                    #region "表尾模式"    
                    var socsoAmountOffset = default(int);
                    var incomeTotal = GetDecimalWithColumnOffset(ws, iCurrentRowFooter, iColD, _startColOffsetNum: null, _endColOffsetNum: 3, out _);
                    var epfAmount = GetDecimalWithColumnOffset(ws, iCurrentRowFooter + 1, iColD, _startColOffsetNum: null, _endColOffsetNum: 2, out _);
                    var socsoAmount = GetDecimalWithColumnOffset(ws, iCurrentRowFooter + 1, iColG, _startColOffsetNum: null, _endColOffsetNum: 2, out socsoAmountOffset);

                    var deductionTotal = GetDecimalWithColumnOffset(ws, iCurrentRowFooter, iColL, _startColOffsetNum: null, _endColOffsetNum: 4, out _);
                    var eisAmountOffset = default(int);
                    var eisAmount = GetDecimalWithColumnOffset(ws, iCurrentRowFooter + 1, iColI, _startColOffsetNum: socsoAmountOffset, _endColOffsetNum: 4, out eisAmountOffset);
                    var nettPay = GetDecimalWithColumnOffset(ws, iCurrentRowFooter + 1, iColL, _startColOffsetNum: eisAmountOffset, _endColOffsetNum: 4, out _);

                    if (incomeTotal != null)
                        payslipHead.IncomeSubTotal = incomeTotal;

                    if (epfAmount != null)
                        payslipHead.EPFAmount = epfAmount;

                    if (socsoAmount != null)
                        payslipHead.SOCSOAmount = socsoAmount;

                    if (deductionTotal != null)
                        payslipHead.DeductionSubTotal = deductionTotal;

                    if (eisAmount != null)
                        payslipHead.EISAmount = eisAmount;

                    if (nettPay != null)
                        payslipHead.NettPay = nettPay;
                    #endregion // end of 表尾模式

                    payslips.Add(payslipHead);
                    iCurrentRow = iCurrentRowFooter + 2;
                }

                // 紀錄
                if (string.IsNullOrEmpty(currentSectionHeader))
                    iSpaceRowsAfterFooter++;

                //Console.Write("iCurrentRow=" + iCurrentRow.ToString());
            }
            wb.Close();
            //wb.Close(false);
            app.Application.Quit();
            app.Quit();
            app = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();

            info.PayslipImportHeads = payslips;
            return info;
        }








        protected string GetPeriodWithColumnOffset(Worksheet _ws, int _row, int _basedColPosition, int _startColOffsetNum = 0, int _endColOffsetNum = 5)
        {
            return GetValueWithColumnOffset(_ws, _row
                , _basedColPosition: _basedColPosition
                , _startColOffsetNum: _startColOffsetNum
                , _endColOffsetNum: _endColOffsetNum
                , _excludeKeyword: "PERIOD");
        }


        protected string GetEmployeeIdWithColumnOffset(Worksheet _ws, int _row, int _basedColPosition, int _startColOffsetNum = 0, int _endColOffsetNum = 4)
        {
            return GetValueWithColumnOffset(_ws, _row
                , _basedColPosition: _basedColPosition
                , _startColOffsetNum: _startColOffsetNum
                , _endColOffsetNum: _endColOffsetNum
                , _excludeKeyword: "E/NO");
        }

        protected string GetDepartmentNameWithColumnOffset(Worksheet _ws, int _row, int _basedColPosition, int _startColOffsetNum = 0, int _endColOffsetNum = 2)
        {
            return GetValueWithColumnOffset(_ws, _row
                , _basedColPosition: _basedColPosition
                , _startColOffsetNum: _startColOffsetNum
                , _endColOffsetNum: _endColOffsetNum
                , _excludeKeyword: "DEPARTMENT");
        }


        protected string GetBankAccountWithColumnOffset(Worksheet _ws, int _row, int _basedColPosition, int _startColOffsetNum = 0, int _endColOffsetNum = 3)
        {
            return GetValueWithColumnOffset(_ws, _row
                , _basedColPosition: _basedColPosition
                , _startColOffsetNum: _startColOffsetNum
                , _endColOffsetNum: _endColOffsetNum
                , _excludeKeyword: "BANK A/C");
        }


        protected string GetValueWithColumnOffset(Worksheet _ws, int _row, int _basedColPosition, int _startColOffsetNum = 0, int _endColOffsetNum = 4, string _excludeKeyword = "")
        {
            int cycle = 1;
            var startColPosition = _basedColPosition + _startColOffsetNum;
            string valueText = _ws.Cells[_row, startColPosition].Value;
            var excludeKeywordLength = _excludeKeyword.Length;

            while (
                cycle <= _endColOffsetNum
                &&
                    (
                    string.IsNullOrEmpty(valueText) == true
                    ||
                    string.IsNullOrEmpty(_excludeKeyword) == false && valueText.Left(excludeKeywordLength) == _excludeKeyword
                    )
                )

            {
                cycle++;
                valueText = _ws.Cells[_row, ++startColPosition].Value;
            }

            if (cycle > _endColOffsetNum)
                return null;

            return valueText;
        }



        protected decimal? GetDecimal(Worksheet _ws, int _row, int _col)
        {
            var valueText = _ws.Cells[_row, _col].Value;
            if (string.IsNullOrEmpty(valueText))
                return null;


            if (decimal.TryParse(valueText, out decimal result))
                return result;

            return null;
        }


        protected decimal? GetDecimalWithColumnOffset(Worksheet _ws, int _row, int _basedColPosition, int? _startColOffsetNum, int? _endColOffsetNum, out int _actualStartColOffset)
        {
            _actualStartColOffset = 0;
            if (_row == 0 || _basedColPosition == 0)
            {
                return null;
            }



            if (_endColOffsetNum == null) _endColOffsetNum = 3;

            var cycle = 1;
            int startColPosition = _basedColPosition + (_startColOffsetNum ?? 0);
            string valueText = _ws.Cells[_row, startColPosition].Value;
            var result = default(decimal);
            var actualColPosition = startColPosition;

            while (
                cycle <= _endColOffsetNum
                &
                    (
                    string.IsNullOrEmpty(valueText) == true
                    ||
                    decimal.TryParse(valueText?.Replace(",", string.Empty), out result) == false
                    )
                )
            {
                actualColPosition = ++startColPosition;
                cycle++;
                valueText = _ws.Cells[_row, actualColPosition].Value;
            }


            if (cycle <= _endColOffsetNum)
            {
                if (decimal.TryParse(valueText?.Replace(",", string.Empty), out _) == true)
                {
                    _actualStartColOffset = actualColPosition - _basedColPosition;
                }
                return result;
            }


            return null;
        }







    }
}
