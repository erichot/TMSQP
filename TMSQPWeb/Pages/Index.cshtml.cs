
//using MathNet.Numerics.Optimization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


//using Org.BouncyCastle.Bcpg.OpenPgp;
//using System.Data;


namespace TMSQPWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly PDFExportService m_PDFExportService;
        private readonly PayslipImportBindingService m_PayslipImportService;


        public IndexModel(ILogger<IndexModel> logger, PayslipImportBindingService payslipImportService, PDFExportService pDFExportService)
        {
            _logger = logger;
            m_PayslipImportService = payslipImportService;
            m_PDFExportService = pDFExportService;
        }

        public async Task OnGetAsync()
        {

            //using (var package = new ExcelPackage(@"D:\Project\1-成交中\Thermos_QPAY7\Importing\20240515\test.xls"))
            //{
            //    ExcelWorksheet sheet = package.Workbook.Worksheets[1];//取得Sheet1
            //    Console.Write(sheet.Cells[3, 2].Value);
            //}



        }

      




    }
}
