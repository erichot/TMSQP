using Microsoft.Office.Interop.Excel;
using TMSQPWin.BindingModels;
using Excel = Microsoft.Office.Interop.Excel;

namespace TMSQPWin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var info = new PaySlipBindingModel();

            var excelApp = new Excel.Application();
            excelApp.Visible = true;
            
            //excelApp.ActiveProtectedViewWindow.Edit();
            // 	HKEY_CURRENT_USER\software\policies\microsoft\office\16.0\excel\security\filevalidation

            var pathFileName = "D:\\Project\\1-成交中\\Thermos_QPAY7\\Importing\\payslip0424excel.xls";
            var workbook = excelApp.Workbooks.Open(pathFileName
                //, CorruptLoad: XlCorruptLoad.xlRepairFile
                , ReadOnly: true);


            Worksheet ws = workbook.ActiveSheet;
            info.EmployeeNo = ws.Cells[4, 11].value;
            
            var bp = Convert.ToString(ws.Cells[7, 7].value);
            if (decimal.TryParse(bp, out decimal _dBp))
                info.BasicPay = _dBp;

            /*
             System.Runtime.InteropServices.COMException
              HResult=0x80070BBC
              Message=Office 偵測到此檔案有問題。為保護您的電腦，此檔案無法開啟。
              Source=<Cannot evaluate the exception source>
              StackTrace:
            <Cannot evaluate the exception stack trace>

            */

        }
    }
}
