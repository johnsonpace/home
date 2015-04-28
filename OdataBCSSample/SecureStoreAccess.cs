using Microsoft.BusinessData.Infrastructure.SecureStore;
using Microsoft.Office.SecureStoreService.Server;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace OdataBCSSample
{
    public class SecureStoreAccess
    {


        public static Dictionary<string, string> GetCredentialsFromSecureApp(string applicationId)
        {
            var credentialMap = new Dictionary<string, string>();

            // Get the default Secure Store Service provider.
            ISecureStoreProvider provider = SecureStoreProviderFactory.Create();
            if (provider == null)
            {
                throw new InvalidOperationException("Unable to get an ISecureStoreProvider");
            }

            var providerContext = provider as ISecureStoreServiceContext;
            if (providerContext != null)
                providerContext.Context = SPServiceContext.GetContext(GetCentralAdminSite());

            var secureStoreProvider = new SecureStoreProvider
            {
                Context = SPServiceContext.GetContext(GetCentralAdminSite())
            };

            using (var credentials = secureStoreProvider.GetCredentials(applicationId))
            {
                var fields = secureStoreProvider.GetTargetApplicationFields(applicationId);
                for (int i = 0; i < fields.Count; i++)
                {
                    var field = fields[i];
                    var credential = credentials[i];

                    var decryptedCredential = GetStringFromSecureString(credential.Credential);

                    credentialMap.Add(field.Name, decryptedCredential);
                }
            }

            return credentialMap;
        }


        public static string GetSecureStoreFieldValue(string secureStoreAppId, string fieldName)
        {
            try
            {
                Dictionary<string, string> dic = GetCredentialsFromSecureApp(secureStoreAppId);

                if (dic.ContainsKey(fieldName))
                    return dic[fieldName].ToString();
                else
                    return null;
            }
            catch (Exception ex)
            {
                ULSLoggingService.LogError("Failed to retrieve the credentials from the secure store:" + ex.ToString());
                return null;
            }

        }

        private static string GetStringFromSecureString(SecureString secStr)
        {
            if (secStr != null)
            {
                IntPtr pPlainText = IntPtr.Zero;
                try
                {
                    pPlainText = Marshal.SecureStringToBSTR(secStr);
                    return Marshal.PtrToStringBSTR(pPlainText);
                }
                finally
                {
                    if (pPlainText != IntPtr.Zero)
                    {
                        Marshal.FreeBSTR(pPlainText);
                    }
                }
            }

            return null;
        }


        private static SPSite GetCentralAdminSite()
        {
            SPAdministrationWebApplication adminWebApp = SPAdministrationWebApplication.Local;
            if (adminWebApp == null)
            {
                throw new InvalidProgramException("Unable to get the admin web app");
            }

            SPSite adminSite = null;
            Uri adminSiteUri = adminWebApp.GetResponseUri(SPUrlZone.Default);
            if (adminSiteUri != null)
            {
                adminSite = adminWebApp.Sites[adminSiteUri.AbsoluteUri];
            }
            else
            {
                throw new InvalidProgramException("Unable to get Central Admin Site.");
            }

            return adminSite;
        }


    }
}
