using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdataBCSSample
{
    public class ULSLoggingService
    {



        public static void LogMessage(string message)
        {

            LoggingService.LogMessage(LoggingService.LoggingCategories.BDC, message);
        }

        public static void LogError(string message)
        {
            LoggingService.LogError(LoggingService.LoggingCategories.BDC, message);
        }

        public static void LogUIError(string message)
        {
            LoggingService.LogError(LoggingService.LoggingCategories.UI, message);
        }


        public static void LogUIException(string message, Exception ex)
        {
            LoggingService.LogError(LoggingService.LoggingCategories.UI, message, ex);
        }

        public static void LogUITrace(string message)
        {
            LoggingService.LogMessage(LoggingService.LoggingCategories.UI, message);
        }

    }
}
