using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSQPBusiness.Helpers
{
    public static class BusinessCultureHelper
    {
        public static int ConvertMonthAbbrNameToMonthN(string _monthAbbrName)
        {
            if (string.IsNullOrEmpty(_monthAbbrName))
                return 0;

            //var monthAbbrNames = new string[] { "JAN", "FEB", "MAR", "Ar", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            var monthAbbrNames = new string[] { "JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC" };
            var monthN = Array.IndexOf(monthAbbrNames, _monthAbbrName) + 1;
            return monthN;
        }


        public static DateOnly GetDateFromPeriodString(string _period)
        {
            if (string.IsNullOrEmpty(_period))
                return DateOnly.MinValue;

            var period = _period.Trim().Replace("END-", string.Empty);
            var monthAbbrName = period.Substring(0, 3);
            var year = int.Parse(period.Substring(4, 4));
            var month = ConvertMonthAbbrNameToMonthN(monthAbbrName);
            return new DateOnly(year, month, 1);
        }

    }
}
