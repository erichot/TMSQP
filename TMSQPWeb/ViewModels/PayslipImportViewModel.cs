

using TMSQPBusiness.Extensions;

namespace TMSQPWeb.ViewModels
{
    public class PayslipImportViewModel:PayslipImport
    {

        public string Period 
        { 
            get
            {
                //return 
                //    (new System.Globalization.CultureInfo(BusinessSystem.LanguageCultureCode))
                //        .DateTimeFormat.GetAbbreviatedMonthName(this.SalaryMonth) 
                //    + "-" + this.SalaryYear.ToString();

                //return this.SalaryMonth.ToString("00") + "-" + this.SalaryYear.ToString();
                return BusinessSystem.GetPeriodNumberString(this.SalaryYear, this.SalaryMonth);
            }
        }


        public string CreatedShortDateString => this.CreatedDate.ToDateStringForUrl();









        public  List<PayslipImportHeadViewModel> PayslipImportHeads { get; set; }

    }
}
