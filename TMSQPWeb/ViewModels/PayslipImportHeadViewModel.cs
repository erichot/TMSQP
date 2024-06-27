using NPOI.OpenXmlFormats.Dml.Chart;
using System.Collections.Generic;

namespace TMSQPWeb.ViewModels
{
    public class PayslipImportHeadViewModel
    {
        public PayslipImportHeadViewModel() 
        {
            PayslipImportDetails = new List<PayslipImportDetailViewModel>();
        }

        public PayslipImportHeadViewModel(short _piNo)
        {
            PINo = _piNo;
            PayslipImportDetails = new List<PayslipImportDetailViewModel>();
        }

        public short PINo {  get; set; }    
        public short HeaderNo { get; set; }



        public int RowNo { get; set; }

        public string Period { get; set; } 
        public string EmployeeName { get; set; }


        private string m_EmployeeId;
        public string EmployeeId 
        {
            get { return m_EmployeeId.Trim(); }
            set { m_EmployeeId = value; }
        }

        // 如果在Import的EmployeeId有對應到，則取得EmployeeNo
        // 若無法對應，則為null
        public short? EmployeeNo { get; set; }

        public string DepartmentName { get; set; }

        public string PaymentMethod { get; set; }


        public string BankAccount { get; set; }






        public int FooterRowNo { get;set; }
        public decimal? IncomeSubTotal { get; set; }

        public decimal? DeductionSubTotal { get; set; }

        public decimal? EPFAmount { get; set; }

        public decimal? SOCSOAmount { get; set; }

        public decimal? EISAmount { get; set; }

        public decimal? NettPay { get; set; }




        



        public List<PayslipImportDetailViewModel> PayslipImportDetails { get; set; }




        public EmployeeViewModel? Employee { get; set; }






        public PayslipImportHead ToEntity()
        {
            return new PayslipImportHead()
            {
                PINo = this.PINo,
                HeaderNo = this.HeaderNo,
                RowNo = this.RowNo,
                Period = this.Period,
                EmployeeId = this.EmployeeId,
                EmployeeNo = this.EmployeeNo,
                EmployeeName = this.EmployeeName,
                DepartmentName = this.DepartmentName,
                PaymentMethod = this.PaymentMethod,
                BankAccount = this.BankAccount,
                FooterRowNo = this.FooterRowNo,
                IncomeSubTotal = this.IncomeSubTotal,
                DeductionSubTotal = this.DeductionSubTotal,
                EPFAmount = this.EPFAmount,
                SOCSOAmount = this.SOCSOAmount,
                EISAmount = this.EISAmount,
                NettPay = this.NettPay,
                Employee = this.Employee?.ToEntity()
            };
        }


    }
}
