using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSQPBusiness.Extensions
{
    public static class ExceptionHandlerExtension
    {
        public static string ComposeExceptionMessage(this Exception _ex)
        {
            return ComposeExceptionMessage(_ex, string.Empty);
        }


        public static string ComposeExceptionMessage(this Exception _ex, string _subject)
        {
            return $"作業發生例外： {Environment.NewLine}{Environment.NewLine} " +
                    $"例外來源：{_ex.Source}。 {Environment.NewLine} " +
                    $"例外訊息：{_ex.Message}。 {Environment.NewLine} " +
                    $"例外描述：{_ex.InnerException}。 ";
        }

    }
}
