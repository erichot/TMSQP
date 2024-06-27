using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSQPBusiness.Extensions
{
    public static class UserCultureExtension
    {
        //public static string ToShortDateString(this DateTime _value)
        //{
        //    return _value.ToString(BusinessSystem.ShortDateStringFormat);
        //}
        //public static string ToShortDateString(this DateTime? _value)
        //{
        //    if (_value == null)
        //        return string.Empty;

        //    return ((DateTime)_value).ToString(BusinessSystem.ShortDateStringFormat);
        //}


        public static string ToShortDateTimeString(this DateTime _value)
        {
            return _value.ToString(BusinessSystem.ShortDateTimeStringFormat);
        }
        public static string ToShortDateTimeString(this DateTime? _value)
        {
            if (_value == null)
                return string.Empty;

            return ((DateTime)_value).ToString(BusinessSystem.ShortDateTimeStringFormat);
        }


        public static string ToFullDateString(this DateTime _value)
        {
            //return ToBopShortDateString(_value);
            //return _value.ToString(BusinessSystem.DateStringFormat);
            if (_value == DateTime.MinValue)
                return string.Empty;

            return _value.ToString(BusinessSystem.DateStringFormat);
        }
        public static string ToFullDateString(this DateTime? _value)
        {
            //return ToBopShortDateString(_value);
            if (_value == null)
                return string.Empty;

            return ((DateTime)_value).ToString(BusinessSystem.DateStringFormat);
        }



        public static string ToDateStringForUrl(this DateTime _value)
        {
            return _value.ToString(BusinessSystem.DateStringFormatForUrl);
        }
        public static string ToDateStringForUrl(this DateTime? _value)
        {
            if (_value == null)
                return string.Empty;

            return ((DateTime)_value).ToString(BusinessSystem.DateStringFormatForUrl);
        }

        public static DateTime ToDateForUrl(this string? _value)
        {
            //if (string.IsNullOrEmpty(_value))
            //    return DateTime.MinValue;
            if (DateTime.TryParseExact(_value, BusinessSystem.DateStringFormatForUrl, null, System.Globalization.DateTimeStyles.None, out var _date))
                return _date.Date;

            return DateTime.MinValue;
        }
        public static DateTime? ToDateOrNullForUrl(this string? _value)
        {
            //if (string.IsNullOrEmpty(_value))
            //    return DateTime.MinValue;
            if (DateTime.TryParseExact(_value, BusinessSystem.DateStringFormatForUrl, null, System.Globalization.DateTimeStyles.None, out var _date))
                return _date.Date;

            return null;
        }

        public static DateTime? ToDateOrNullFromYearMonth(this string? _value)
        {
            if (string.IsNullOrEmpty(_value))
                return null;

            if (DateTime.TryParseExact("01-" + _value, "dd-" + BusinessSystem.YearMonthStringFormat, null, System.Globalization.DateTimeStyles.None, out var _date))
                return _date.Date;

            return null;
        }
        public static DateTime ToDateFromYearMonth(this string _value)
        {
            if (string.IsNullOrEmpty(_value))
                return DateTime.MinValue;

            if (DateTime.TryParseExact("01-" + _value, "dd-" + BusinessSystem.YearMonthStringFormat, null, System.Globalization.DateTimeStyles.None, out var _date))
                return _date.Date;

            return DateTime.MinValue;
        }

        public static string ToShortDateString(this DateTime _value)
        {
            if (false && _value.Year == DateTime.Now.Year)
                return _value.ToString(BusinessSystem.ShortDateStringFormat);
            else
                return _value.ToString(BusinessSystem.DateStringFormat);
        }
        public static string ToShortDateString(this DateTime? _value)
        {
            if (_value == null)
                return string.Empty;

            var dtValue = (DateTime)_value;
            if (dtValue.Year == DateTime.Now.Year)
                return dtValue.ToString(BusinessSystem.ShortDateStringFormat);
            else
                return dtValue.ToString(BusinessSystem.DateStringFormat);
        }







        public static string ToFullTimeString(this DateTime _value)
        {
            return _value.ToString(BusinessSystem.TimeStringFormat);
        }
        public static string ToFullTimeString(this DateTime? _value)
        {
            if (_value == null)
                return string.Empty;

            return ((DateTime)_value).ToString(BusinessSystem.TimeStringFormat);
        }




        public static string ToFullDateTimeString(this DateTime? _value)
        {
            if (_value == null)
                return string.Empty;

            return ((DateTime)_value).ToString(BusinessSystem.DateTimeStringFormat);
        }
        public static string ToFullDateTimeString(this DateTime _value)
        {
            return _value.ToString(BusinessSystem.DateTimeStringFormat);
        }


        public static string ToYearMonthString(this DateTime _value)
        {
            return _value.ToString(BusinessSystem.YearMonthStringFormat);
        }



        public static string ToMoneyString(this int _value)
        {
            return _value.ToString("#,##0");
        }
        public static string ToMoneyString(this int? _value)
        {
            if (_value == null)
                return string.Empty;

            return _value.ToInt().ToString("#,##0");
        }
        //public static string ToBopMoneyString(this Int64 _value)
        //{
        //    return _value.ToString("###,##0");
        //}
        public static string ToMoneyString(this long _value)
        {
            return _value.ToString("###,##0");
        }
        public static string ToMoneyString(this long? _value)
        {
            return (_value != null) ? ((long)_value).ToString("###,##0") : "0";
        }
        public static string ToMoneyString(this decimal _value)
        {
            return _value.ToString("###,##0");
        }
        public static string ToMoneyString(this decimal? _value)
        {
            return (_value != null) ? ((decimal)_value).ToString("###,##0") : "0";
        }





    }
}
