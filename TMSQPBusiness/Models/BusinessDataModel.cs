using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSQPBusiness.Models
{
    public class BusinessDataModel
    {
        
    }


    public static class BusinessSystem
    {
        


        public const string LanguageCultureCode = "ms-MY";
        public const string CompanyName = "TOP THERMO MFG (MALAYSIA) SDN BHD";


        public const string DateStringFormat = "dd-MM-yyyy";
        public const string YearMonthStringFormat = "MM-yyyy";
        public const string DateStringFormatForUrl = "dd-MM-yyyy";
        public const string DateNumberFormat = "yyyyMMdd";
        public const string ShortDateStringFormat = "dd-MM";
        public const string ShortDateNumberFormat = "ddMM";
        public const string TimeStringFormat = "HH:mm:ss";
        public const string DateTimeStringFormat = "dd-MM-yyyy HH:mm";
        public const string ShortDateTimeStringFormat = "dd-MM HH:mm";





        public static string GetPeriodNumberString(short _salaryYear, byte _salaryMonth)
        {
            return $"{_salaryMonth.ToString("00")}-{_salaryYear}";
        }



        //public static string GetPayslipPdfFileName(PayslipImport _info)
        //{
        //    return GetPeriodNumberString(_info.SalaryYear, _info.SalaryMonth)
        //        + "_" + _info.PayslipImportHeads.f + ".pdf";
        //}
        public static string GetPayslipPdfFileName(short _salaryYear, byte _salaryMonth, string _employeeId)
        {
            return GetPeriodNumberString(_salaryYear, _salaryMonth) 
                + "_" + _employeeId + ".pdf";
        }



    }

}
