using Microsoft.SharePoint.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdataBCSSample
{
    public class LoggingService : SPDiagnosticsServiceBase
    {

        public enum LoggingCategories
        {
            UI,
            Configuration,
            BDC
        };


        public static string FDAESDiagnosticAreaName = "FDA.ES";
        private static LoggingService _Current;
        public static LoggingService Current
        {
            get
            {
                if (_Current == null)
                {
                    _Current = new LoggingService();
                }

                return _Current;
            }
        }

        private LoggingService()
            : base("FDA.ES.Logging", SPFarm.Local)
        {

        }

        protected override IEnumerable<SPDiagnosticsArea> ProvideAreas()
        {

            List<SPDiagnosticsArea> areas = new List<SPDiagnosticsArea>
        {
            new SPDiagnosticsArea(FDAESDiagnosticAreaName, new List<SPDiagnosticsCategory>
            {
                new SPDiagnosticsCategory(LoggingCategories.UI.ToString(), TraceSeverity.VerboseEx, EventSeverity.Error),
                new SPDiagnosticsCategory(LoggingCategories.BDC.ToString(), TraceSeverity.VerboseEx, EventSeverity.Error),
                 new SPDiagnosticsCategory(LoggingCategories.Configuration.ToString(), TraceSeverity.VerboseEx, EventSeverity.Error)
            })
        };

            return areas;
        }

        public static void LogError(LoggingCategories Category, string errorMessage)
        {
            SPDiagnosticsCategory category = LoggingService.Current.Areas[FDAESDiagnosticAreaName].Categories[Category.ToString()];
            LoggingService.Current.WriteTrace(0, category, TraceSeverity.Unexpected, errorMessage);
        }

        public static void LogError(LoggingCategories Category, string errorMessage, Exception ex)
        {
            SPDiagnosticsCategory category = LoggingService.Current.Areas[FDAESDiagnosticAreaName].Categories[Category.ToString()];
            LoggingService.Current.WriteTrace(0, category, TraceSeverity.Unexpected, string.Format("Message:{0} | Exception{1}", errorMessage, ex.ToString()));
        }


        public static void LogMessage(LoggingCategories Category, string errorMessage)
        {
            SPDiagnosticsCategory category = LoggingService.Current.Areas[FDAESDiagnosticAreaName].Categories[Category.ToString()];
            LoggingService.Current.WriteTrace(0, category, TraceSeverity.Medium, errorMessage);
        }
    }
}
