using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdataBCSSample
{
    class Common
    {

        public static string GetSQLonnectionString(string secureStoreAppId)
        {

            string connectionString = "";
            try
            {

                Dictionary<string, string> dic = SecureStoreAccess.GetCredentialsFromSecureApp(secureStoreAppId);
                ULSLoggingService.LogMessage(string.Format("secure store app ID {0}  number of items {1}", secureStoreAppId, dic.Count.ToString()));

                foreach (var item in dic)
                {
                     if (item.Key.Equals("ConnectionString"))
                         connectionString = item.Value.ToString();

                }

            }
                
            catch (Exception ex)
            {

                ULSLoggingService.LogError("Inside GetOdbcConnectionString(): Failed to retrieve the fields from the secure store:" + ex.ToString());

            }
            return connectionString;

        }
    }
}
