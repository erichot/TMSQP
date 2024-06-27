using iText.IO.Image;
using iText.IO.Font;
using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.Extensions.Hosting;
using System.Text;

namespace TMSQPWeb.Services
{
    public class PDFExportService
    {
        private IWebHostEnvironment m_Environment;

        public PDFExportService(IWebHostEnvironment environment)
        {
            m_Environment = environment;
        }









        public string GeneratePdfFile(PayslipImportViewModel _payslip, PayslipImportHeadViewModel _info)
        {
            var employeeName = _info.EmployeeName;
            var employeeId = _info.EmployeeId;
            var period = _payslip.Period.Trim();
            var salaryYear = _payslip.SalaryYear;
            var salaryMonth = _payslip.SalaryMonth;

            
            // 需要與 WebSystemService 的 GetPayslipPdfFileName 保持一致
            var physicalFullPath = Path.Combine(
                m_Environment.ContentRootPath
                , AppSystem.GetRelativePathPayslipPdfRepository(_payslip.SalaryYear, _payslip.SalaryMonth)
                );

            if (!Directory.Exists(physicalFullPath))
            {
                Directory.CreateDirectory(physicalFullPath);
            }

            // ------------------------------------------------------------
            var fileName = BusinessSystem.GetPayslipPdfFileName(salaryYear, salaryMonth, employeeId);
            var pathFileName = Path.Combine(physicalFullPath, fileName);
            

            var imageSrcLogo = Path.Combine(m_Environment.WebRootPath, @"images\logo_90.png");
            var deviceGrayTableFooter = new DeviceGray(0.8f);

            
            
            var departmentName =  _info.DepartmentName;
            var paymentMethod = _info.PaymentMethod;
            var bankAccount = _info.BankAccount;
            var incomeSubTotal = _info.IncomeSubTotal;
            var deductionSubTotal = _info.DeductionSubTotal;
            var nettPayString = _info.NettPay?.ToString("#,##0.00");

            var epfAmountString = _info.EPFAmount?.ToString("#,##0.00");
            var SOCSOAmountString = _info.SOCSOAmount?.ToString("#,##0.00");
            var EISAmountString = _info.EISAmount?.ToString("#,##0.00");

            var nettPayColumnHeader = $"EMPLOYER [EPF: RM {epfAmountString}] [SOCSO: RM {SOCSOAmountString}] [EIS: RM {EISAmountString}]";

            //const float marginLeftHeaderRowCol2 = 260;
            //const int FontSizeDefaultContent = 10;
            var details = _info.PayslipImportDetails
               .OrderBy(c => c.RowNo).ThenBy(c => c.DetailTypeNo);

            // 整個表格的高度
            //var tableBodyRowHeight = 180f;

            var tableBodyRowPadding = 0f;

            WriterProperties writerProperties = new WriterProperties();
            writerProperties.SetStandardEncryption(
                Encoding.UTF8.GetBytes(employeeId),
                Encoding.UTF8.GetBytes(employeeId), EncryptionConstants.ALLOW_PRINTING | EncryptionConstants.ALLOW_COPY, EncryptionConstants.ENCRYPTION_AES_128);



            // -----------------------------------------------
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(pathFileName, writerProperties));
            Document document = new Document(pdfDocument);


            //FontProgram fontProgramTableTitle = FontProgramFactory.CreateFont(StandardFonts.COURIER_BOLD);
            //var fontTableTitle = PdfFontFactory.CreateFont(fontProgramTableTitle, PdfEncodings.WINANSI);

            //FontProgram fontProgramHeaderRow = FontProgramFactory.CreateFont(StandardFonts.TIMES_ROMAN);
            //var fontHeaderRow = PdfFontFactory.CreateFont(fontProgramHeaderRow, PdfEncodings.WINANSI);

            // ==========================================================
            //var paraCompanyName = new Paragraph(TMSQPBusiness.Models.BusinessSystem.CompanyName).SetFont(fontTableTitle);
            //paraCompanyName.SetFontSize(12);
            //document.Add(paraCompanyName);


            //var paraPeriod = new Paragraph(periodPhase).SetFont(fontTableTitle);
            //paraPeriod.SetMarginLeft(marginLeftHeaderRowCol2);
            //paraPeriod.SetFontSize(FontSizeDefaultContent);
            //document.Add(paraPeriod);


            // =====================================================================
            #region "Header"
            var imageLogo = new Image(ImageDataFactory.Create(imageSrcLogo));

            // Company Titel & Logo
            Table tblPagerHeader = new Table(
                UnitValue.CreatePercentArray(new float[] { 5,1})
                ).UseAllAvailableWidth();


            tblPagerHeader.AddCell(
                new Cell()
                    .Add(
                        new Paragraph(TMSQPBusiness.Models.BusinessSystem.CompanyName)
                        )
                    .SetFont(
                        PdfFontFactory.CreateFont(StandardFonts.COURIER_BOLD)
                        )
                    .SetFontSize(16)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetBorder(Border.NO_BORDER)
                )
                .SetBorder(Border.NO_BORDER);

            imageLogo.SetProperty(Property.FLOAT, FloatPropertyValue.RIGHT);

            tblPagerHeader.AddCell(
                new Cell().Add(
                   imageLogo
                )
                .SetVerticalAlignment(VerticalAlignment.TOP)
                .SetHorizontalAlignment(HorizontalAlignment.RIGHT)
                .SetBorder(Border.NO_BORDER)
            )
            .SetBorder(Border.NO_BORDER);
            tblPagerHeader.SetBorder(Border.NO_BORDER);


            var borderHeader = Border.NO_BORDER;
            var fontPdfHeader = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            var fontSizeHeader = 12;
            var deviceGrayColumnHeader = new DeviceGray(0.95f);
            float[] columnWidthsHeader = { 5, 5, 5, 5 };
            Table tblHeader = new Table(
                UnitValue.CreatePercentArray(columnWidthsHeader)
                ).UseAllAvailableWidth();


            //tblHeader.AddHeaderCell(
            //    new Cell(1, 20)
            //        .Add(
            //            new Paragraph(TMSQPBusiness.Models.BusinessSystem.CompanyName)
            //            )
            //        .SetFont(
            //            PdfFontFactory.CreateFont(StandardFonts.COURIER_BOLD)
            //            )
            //        .SetFontSize(16)
            //        //.SetFontColor(DeviceGray.WHITE)
            //        //.SetBackgroundColor(DeviceGray.BLACK)
            //        .SetTextAlignment(TextAlignment.CENTER)
            //    )
            //    .SetBorder(Border.NO_BORDER);
                

            tblHeader.AddCell(
                   new Cell(1, 2).Add(
                       new Paragraph(employeeName)
                   )
                   .SetFont(fontPdfHeader)
                   .SetFontSize(fontSizeHeader)
                   .SetBorder(borderHeader)
               );


            //imageLogo.SetProperty(Property.FLOAT, FloatPropertyValue.RIGHT);
            ////imageLogo.SetMarginRight(20);
            //tblHeader.AddCell(
            //    new Cell(2, 1).SetBorder(borderHeader)
            //);
            //tblHeader.AddCell(
            //    new Cell(2, 1).Add(
            //       imageLogo
            //    )
            //    //.SetTextAlignment(TextAlignment.JUSTIFIED)
            //    .SetVerticalAlignment(VerticalAlignment.MIDDLE)
            //    //.SetHorizontalAlignment(HorizontalAlignment.CENTER)
            //    .SetBorder(borderHeader)
            //);


            tblHeader.AddCell(
                    new Cell().Add(
                        new Paragraph("E/NO")
                    )
                    .SetFont(fontPdfHeader)
                    .SetFontSize(fontSizeHeader)
                    .SetBorder(borderHeader)
                    //.SetBackgroundColor(deviceGrayColumnHeader)
                );
            tblHeader.AddCell(
                    new Cell().Add(
                        new Paragraph(employeeId)
                    )
                    .SetFont(fontPdfHeader)
                    .SetFontSize(fontSizeHeader)
                    .SetBorder(borderHeader)
                );


            tblHeader.AddCell(
                    new Cell().Add(
                        new Paragraph("DEPARTMENT")
                    )
                    .SetFont(fontPdfHeader)
                    .SetFontSize(fontSizeHeader)
                    .SetBorder(borderHeader)
                    //.SetBackgroundColor(deviceGrayColumnHeader)
                );
            tblHeader.AddCell(
                    new Cell().Add(
                        new Paragraph(departmentName)
                    )
                    .SetFont(fontPdfHeader)
                    .SetFontSize(fontSizeHeader)
                    .SetBorder(borderHeader)
                );


            tblHeader.AddCell(
                new Cell().Add(
                    new Paragraph("PERIOD")
                )
                .SetFont(fontPdfHeader)
                .SetFontSize(fontSizeHeader)
                .SetBorder(borderHeader)
                //.SetBackgroundColor(deviceGrayColumnHeader)
              );
            tblHeader.AddCell(
                new Cell().Add(
                    new Paragraph(period)
                )
                .SetFont(fontPdfHeader)
                .SetFontSize(fontSizeHeader)
                .SetBorder(borderHeader)
             );




            tblHeader.AddCell(
                new Cell().Add(
                     new Paragraph("PAYMENT")
                )
                .SetFont(fontPdfHeader)
                .SetFontSize(fontSizeHeader)
                .SetBorder(borderHeader)
                //.SetBackgroundColor(deviceGrayColumnHeader)
             );
            tblHeader.AddCell(
                new Cell().Add(
                    new Paragraph(paymentMethod)
                )
                .SetFont(fontPdfHeader)
                .SetFontSize(fontSizeHeader)
                .SetBorder(borderHeader)
                
             );



            tblHeader.AddCell(
                new Cell().Add(
                    new Paragraph("BANK A/C")
                )
                .SetFont(fontPdfHeader)
                .SetFontSize(fontSizeHeader)
                .SetBorder(borderHeader)
                //.SetBackgroundColor(deviceGrayColumnHeader)
             );
            tblHeader.AddCell(
                 new Cell().Add(
                     new Paragraph(bankAccount)
                 )
                 .SetFont(fontPdfHeader)
                   .SetFontSize(fontSizeHeader)
                 .SetBorder(borderHeader)
             );
            #endregion




            // -----------------------------------------------------------------
            #region "tblMain Body"
            var borderBody = new SolidBorder(ColorConstants.BLACK, 1);
            var fontPdfBodyColumnHeader = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);
            var fontPdfBodyColumnItem = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);
            var fontPdfBodyColumnAmount = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);
            var fontSizeBody = 12;
           

            float paddingRightBodyLeft = 3;
            float paddingRightBodyRight = 3;


            float[] columnWidthsBody = { 6, 2, 6, 2};
            Table tblMain = new Table(
                UnitValue.CreatePercentArray(columnWidthsBody)
                ).UseAllAvailableWidth();


            tblMain.AddHeaderCell(
                new Cell().Add(
                    new Paragraph("INCOME")
                    )
                .SetPaddingLeft(paddingRightBodyLeft)
                .SetFont(fontPdfBodyColumnHeader)
                .SetTextAlignment(TextAlignment.LEFT)
                .SetBackgroundColor(new DeviceGray(0.8f))
                );
            tblMain.AddHeaderCell(
                new Cell().Add(
                    new Paragraph("RM")
                    )
                .SetFont(fontPdfBodyColumnHeader)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBackgroundColor(new DeviceGray(0.8f))
                );
            tblMain.AddHeaderCell(
                new Cell().Add(
                    new Paragraph("DEDUCTION")
                    )
                .SetPaddingLeft(paddingRightBodyLeft)
                .SetFont(fontPdfBodyColumnHeader)
                .SetTextAlignment(TextAlignment.LEFT)
                .SetBackgroundColor(new DeviceGray(0.8f))
                );
            tblMain.AddHeaderCell(
               new Cell().Add(
                   new Paragraph("RM")
                   )
               .SetFont(fontPdfBodyColumnHeader)
               .SetTextAlignment(TextAlignment.CENTER)
               .SetBackgroundColor(new DeviceGray(0.8f))
               );


            var detailTypeNo_previous = default(byte);
            var detailTypeNo = default(byte);
            var isBeginNewRow = true;
            var isWaitingForEndRow = false;

            foreach (var item in details)
            {   
                var itemName = item.ItemName;
                var amountText = item.Amount?.ToString("#,##0.00");
                detailTypeNo_previous = detailTypeNo;
                detailTypeNo = item.DetailTypeNo;

                if (isBeginNewRow == true && detailTypeNo == 1)
                    isWaitingForEndRow = true;
                else if (isBeginNewRow == true && isWaitingForEndRow == false && detailTypeNo == 2)
                {  
                    tblMain.AddCell(
                        new Cell().Add(
                            new Paragraph(string.Empty)
                            )
                        .SetFont(fontPdfBodyColumnItem)
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetBorderTop(Border.NO_BORDER)
                        .SetBorderBottom(Border.NO_BORDER)
                         //.SetPaddingTop(-10f).SetPaddingBottom(-10f)
                         .SetPaddingTop(tableBodyRowPadding)
                        .SetPaddingBottom(tableBodyRowPadding)
                    )
                   
                    ;
                    tblMain.AddCell(
                        new Cell().Add(
                            new Paragraph(string.Empty)
                            )
                        .SetFont(fontPdfBodyColumnAmount)
                        .SetTextAlignment(TextAlignment.RIGHT)
                        .SetBorderTop(Border.NO_BORDER)
                        .SetBorderBottom(Border.NO_BORDER)
                        //.SetPaddingTop(-10f).SetPaddingBottom(-10f)
                        .SetPaddingTop(tableBodyRowPadding)
                        .SetPaddingBottom(tableBodyRowPadding)
                    )
                    
                    ;
                    isBeginNewRow = true;
                    isWaitingForEndRow = false;
                }

                tblMain.AddCell(
                    new Cell().Add(
                        new Paragraph(itemName)
                        )
                    .SetPaddingLeft(paddingRightBodyLeft)
                    .SetFont(fontPdfBodyColumnItem)
                    .SetTextAlignment(TextAlignment.LEFT)
                    .SetBorderTop(Border.NO_BORDER)
                    .SetBorderBottom(Border.NO_BORDER)
                     .SetPaddingTop(tableBodyRowPadding)
                    .SetPaddingBottom(tableBodyRowPadding)
                )
                
                ;

                tblMain.AddCell(
                   new Cell().Add(
                       new Paragraph(amountText)
                       )
                   .SetPaddingRight(paddingRightBodyRight)
                   .SetFont(fontPdfBodyColumnAmount)
                   .SetTextAlignment(TextAlignment.RIGHT)
                   .SetBorderTop(Border.NO_BORDER)
                   .SetBorderBottom(Border.NO_BORDER)
                    .SetPaddingTop(tableBodyRowPadding)
                    .SetPaddingBottom(tableBodyRowPadding)
               )
                    
                    ;

               
                
                if (detailTypeNo_previous == 1 && detailTypeNo == 2)
                {
                    isBeginNewRow = true;
                    isWaitingForEndRow = false;
                }
                
            }

           
            for (var i = 1;i <= 12; i++)
            {
                tblMain.AddCell(
                    new Cell().Add(
                         new Paragraph(" ")
                        )
                    .SetPaddingTop(10f)
                    .SetPaddingBottom(10f)
                    .SetBorderTop(Border.NO_BORDER)
                    .SetBorderBottom(Border.NO_BORDER)
                     .SetPaddingTop(tableBodyRowPadding)
                    .SetPaddingBottom(tableBodyRowPadding)
                    .SetHeight(30f)
                );
                                    
            }

            #endregion








            // -----------------------------------------------------------------
            #region "tblMain Footer"
            tblMain.AddFooterCell(
                new Cell().SetBackgroundColor(deviceGrayTableFooter)
                    .Add(new Paragraph("TOTAL")                    
                    )
                    .SetPaddingLeft(paddingRightBodyLeft)
                    .SetTextAlignment(TextAlignment.RIGHT)
                );

            tblMain.AddFooterCell(
                new Cell().SetBackgroundColor(deviceGrayTableFooter)
                    .Add(
                        new Paragraph(
                            incomeSubTotal?.ToString("#,##0.00")
                            )
                    )
                    .SetTextAlignment(TextAlignment.RIGHT)
                    .SetPaddingRight(paddingRightBodyRight)
                );

            tblMain.AddFooterCell(
                new Cell().SetBackgroundColor(deviceGrayTableFooter)
                    .Add(new Paragraph("TOTAL"))
                    .SetPaddingLeft(paddingRightBodyLeft)
                    .SetTextAlignment(TextAlignment.RIGHT)
                );

            tblMain.AddFooterCell(
               new Cell().Add(
                    new Paragraph(
                        deductionSubTotal?.ToString("#,##0.00"))
                    )
                    .SetTextAlignment(TextAlignment.RIGHT)
                   .SetBackgroundColor(deviceGrayTableFooter)
                   .SetPaddingRight(paddingRightBodyRight)
               ) ;


            Table nestedTable = new Table(2);
            nestedTable.AddCell(
                new Cell().Add(
                    new Paragraph(nettPayColumnHeader)
                    )
                .SetBorderTop(Border.NO_BORDER)
                .SetBorderBottom(Border.NO_BORDER)
                .SetBorderLeft(Border.NO_BORDER)
                .SetBorderRight(Border.NO_BORDER)
                );
            nestedTable.AddCell(
                new Cell().Add(
                    new Paragraph("NETT PAY")
                    )
                .SetTextAlignment(TextAlignment.RIGHT)
                .SetBorderTop(Border.NO_BORDER)
                .SetBorderBottom(Border.NO_BORDER)
                .SetBorderLeft(Border.NO_BORDER)
                .SetBorderRight(Border.NO_BORDER)
                );
            nestedTable.SetWidth(UnitValue.CreatePercentValue(100));
            //tblMain.AddFooterCell(
            //   new Cell(1,3).Add(
            //        new Paragraph(nettPayColumnHeader)
            //        )
            //        .SetFontSize(11)
            //        .SetTextAlignment(TextAlignment.RIGHT)
            //        .SetBackgroundColor(deviceGrayTableFooter)
            //        .SetBorderBottom(Border.NO_BORDER)
            //        .SetBorderLeft(Border.NO_BORDER)
            //   );
            tblMain.AddFooterCell(
                new Cell(1,3).Add(nestedTable)
               );           
            tblMain.AddFooterCell(
                new Cell().Add(
                    new Paragraph(nettPayString)
                    )
                .SetTextAlignment(TextAlignment.RIGHT)
                .SetBackgroundColor(deviceGrayTableFooter)
                .SetPaddingRight(paddingRightBodyRight)
                );
            #endregion



            document.Add(tblPagerHeader);
            document.Add(tblHeader);
            document.Add(tblMain);
            document.Close();

            return pathFileName;
        }

















        private static Paragraph CreateParagraphWithSpaces(PdfFont font, string value1, string value2)
        {
            Paragraph p = new Paragraph();
            p.SetFont(font);
            p.Add(string.Format("{0, -35}", value1));
            p.Add(value2);
            return p;
        }



        private static Paragraph CreateParagraphWithTab(string key, string value1, string value2)
        {
            Paragraph p = new Paragraph();
            p.AddTabStops(new TabStop(200f, TabAlignment.LEFT));
            p.Add(key);
            p.Add(value1);
            p.Add(new Tab());
            p.Add(value2);
            return p;
        }


    }
}
