using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;


namespace TMSQPWeb.Pages.Payslips
{
    public class EntryDetailModel : PageModel
    {
        private const string m_PageID = "E010011";

        private readonly PDFExportService m_PDFExportService;
        private readonly WebSystemService m_WebSystemService;
        private readonly SendMailService m_SendMailService;

        private readonly EmployeeBindingService m_EmployeeService;
        private readonly NotificationBindingService m_NotificationService;
        private readonly PayslipImportBindingService m_PayslipImportService;

        public EntryDetailModel(PDFExportService pDFExportService, WebSystemService webSystemService, SendMailService sendMailService, EmployeeBindingService employeeService, NotificationBindingService notificationService, PayslipImportBindingService payslipImportService)
        {
            m_PDFExportService = pDFExportService;
            m_WebSystemService = webSystemService;
            m_SendMailService = sendMailService;
            m_EmployeeService = employeeService;
            m_NotificationService = notificationService;
            m_PayslipImportService = payslipImportService;
        }




        public PageHeaderEntity PG_PageHeaderInfo { get; set; }


        public PayslipImportViewModel PG_PayslipImport { get; set; }


        public PayslipImportHeadViewModel PG_Info { get; set; }


        


        public async Task Page_InitialAsync()
        {
            //PG_PageHeaderInfo = await m_WebSystemService.GetPageHeaderAsync(m_PageID);
            //ViewData[AppSystem.VD_PageFullTitle] = PG_PageHeaderInfo.FullTitle;
        }



        public void OnGet(short? _pn, short? _hn)
        {
            if (_pn.IsNullOrDefault() || _hn.IsNullOrDefault())
            {
                return;
            }

         
            var piNo = _pn.ToShort();
            var headerNo = _hn.ToShort();
            PG_PayslipImport =  m_PayslipImportService.GetEntity(piNo);
            PG_Info =  m_PayslipImportService.GetHeadEntity(piNo, headerNo, _enableTracking: false, _includeDetails: true);

        }











        public async Task<IActionResult> OnPostExportToPdfAsync(short? _pn, short? _hn)
        {
            string msg = string.Empty;

            if (_pn.IsNullOrDefault() || _hn.IsNullOrDefault())
            {
                ModelState.AddModelError(string.Empty, WebBaseUI.Form_Msg_ModelStateInValid);
                TempData[AppSystem.TD_UI_OnPageLoad_Message_Notification] = WebBaseUI.Form_Msg_ModelStateInValid;
                return Page();
            }


            // ===========================================================================
            var piNo = _pn.ToShort();
            var headerNo = _hn.ToShort();
            PG_PayslipImport = m_PayslipImportService.GetEntity(piNo);
            PG_Info = m_PayslipImportService.GetHeadEntity(piNo, headerNo, _enableTracking: false, _includeDetails: true);


            if (PG_PayslipImport == null || PG_Info == null)
            {
                ModelState.AddModelError(string.Empty, WebBaseUI.Form_Msg_ModelStateInValid);
                TempData[AppSystem.TD_UI_OnPageLoad_Message_Notification] = WebBaseUI.Form_Msg_ModelStateInValid;
                return Page();
            }

            // =================================================================
            var pathFileName = m_PDFExportService.GeneratePdfFile(PG_PayslipImport, PG_Info);
            if (System.IO.File.Exists(pathFileName) == false)
            {
                msg = $"PDF file does not existed {pathFileName}";
                ModelState.AddModelError(string.Empty, msg);
                TempData[AppSystem.TD_UI_OnPageLoad_Message_Notification] = msg;
                return Page();
            }


            // ========================================================================
            var fileName = System.IO.Path.GetFileName(pathFileName);
            var memoryStream = new MemoryStream();
            using (var stream = new FileStream(pathFileName, FileMode.Open))
            {
                await stream.CopyToAsync(memoryStream);
            }
            memoryStream.Position = 0;


            return File(memoryStream, "application/pdf", fileName);
        }









        public async Task<IActionResult> OnPostSendByMailAsync(short? _pn, short? _hn)
        {
            var msg = string.Empty;
            if (_pn.IsNullOrDefault() || _hn.IsNullOrDefault())
            {
                msg = WebBaseUI.Form_Msg_ModelStateRequired;
                ModelState.AddModelError(string.Empty, msg);
                TempData[AppSystem.TD_UI_OnPageLoad_Message_Notification] = msg;
                return Page();
            }


            // ===========================================================================
            var piNo = _pn.ToShort();
            var headerNo = _hn.ToShort();

            // payslip period
            PG_PayslipImport = m_PayslipImportService.GetEntity(piNo,_enableTracking:false,_includeDetails:false);
            
            // Employee's slip
            PG_Info = m_PayslipImportService.GetHeadEntity(piNo, headerNo, _enableTracking: false, _includeDetails: true);


            if (PG_PayslipImport == null)
            {
                msg = $"Does not found the [PayslipImport] with {piNo}";
                ModelState.AddModelError(string.Empty, msg);
                TempData[AppSystem.TD_UI_OnPageLoad_Message_Notification] = msg;
                return Page();
            }
            else if (PG_Info == null)
            {
                msg = $"Does not found the [PayslipImportHead] with {piNo} & {headerNo}";
                ModelState.AddModelError(string.Empty, msg);
                TempData[AppSystem.TD_UI_OnPageLoad_Message_Notification] = msg;
                return Page();
            }


            // =================================================================
            var employeeName = PG_Info.EmployeeName;
            var employeeId = PG_Info.EmployeeId;
            var salaryYear = PG_PayslipImport.SalaryYear;
            var salaryMonth = PG_PayslipImport.SalaryMonth;
            var fileName = BusinessSystem.GetPayslipPdfFileName(salaryYear, salaryMonth, employeeId);
            var physicalFullPath = m_WebSystemService.GetPayslipPdfRepositoryPath(salaryYear, salaryMonth);

            // 若沒有先前殘留的檔案，則重新建立
            var pathFileName = Path.Combine(physicalFullPath,fileName);
            if (System.IO.File.Exists(pathFileName) == false)
            {
                pathFileName = m_PDFExportService.GeneratePdfFile(PG_PayslipImport, PG_Info);
                if (System.IO.File.Exists(pathFileName) == false)
                {
                    msg = $"PDF file does not existed {pathFileName}";
                    ModelState.AddModelError(string.Empty, msg);
                    TempData[AppSystem.TD_UI_OnPageLoad_Message_Notification] = msg;
                    return Page();
                }
            }

            

            // ===========================================================================
            //var employeeId = PG_Info.EmployeeId;
            //var employee = await m_EmployeeService.GetEntityAsync(employeeId);
            var employeeInfo = PG_Info.Employee;
            if (employeeInfo == null)
            {
                msg = $"Does not found the [Employee]";
                ModelState.AddModelError(string.Empty, msg);
                TempData[AppSystem.TD_UI_OnPageLoad_Message_Notification] = msg;
                return Page();
            }


            // ========================================================
            var inserting = new NotificationHead(PG_Info.ToEntity(), fileName);
            inserting.SetToCreate(101);
            inserting.ComposeMessage("subject text");
            inserting.SenderAddress = "eric.yuh@msa.hinet.net";
            inserting.SenderName= "Eric Yuh";


            var businessResult = await m_NotificationService.ProcessToInsertAsync(inserting);
            if (businessResult.HasError)
            {
                msg = $"NotificationService insert failure";
                ModelState.AddModelError(string.Empty, msg);
                TempData[AppSystem.TD_UI_OnPageLoad_Message_Notification] = msg;
                return Page();
            }


            if (businessResult.ResultNo.IsNullOrDefault())
            {
                msg = $"NotificationService does not return no";
                ModelState.AddModelError(string.Empty, msg);
                TempData[AppSystem.TD_UI_OnPageLoad_Message_Notification] = msg;
                return Page();
            }

            // ========================================================
            var notificationNo = businessResult.ResultNo;
            var notificationHead = await m_NotificationService.GetHeadEntityAsync(notificationNo, _enableTracking: true, _includeDetails: true);
            if (notificationHead == null)
            {
                msg = $"NotificationHead does not found";
                ModelState.AddModelError(string.Empty, msg);
                TempData[AppSystem.TD_UI_OnPageLoad_Message_Notification] = msg;
                return Page();
            }


            // ========================================================
            var notifictionDetailUpdating = await m_SendMailService.SendMailAsync(notificationHead);
            notificationHead.Notified(notifictionDetailUpdating);



            var businessResult2 = await m_NotificationService.UpdateEntityAsync(notificationHead);
            if (businessResult2.HasError)
            {
                msg = $"NotificationDetail update failure";
                ModelState.AddModelError(string.Empty, msg);
                TempData[AppSystem.TD_UI_OnPageLoad_Message_Notification] = msg;
                return Page();
            }




            msg = $"Mail has sent";
            TempData[AppSystem.TD_UI_OnPageLoad_Message_Notification] = msg;
            return Page();
        }








    }
}
