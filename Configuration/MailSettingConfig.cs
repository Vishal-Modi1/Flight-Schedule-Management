using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration
{
    public class MailSettingConfig
    {
        private static MailSettingConfig _instance = null;
        private static readonly object padlock = new object();
        private static IConfiguration configuration;

        #region Object Creation
        private MailSettingConfig()
        {
            configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("configuration.json", optional: true, reloadOnChange: true)
                    .Build().GetSection("MailSetting");
        }

        public static MailSettingConfig Instance
        {
            get
            {
                lock (padlock)
                {
                    if (_instance == null)
                    {
                        _instance = new MailSettingConfig();
                    }
                    return _instance;
                }
            }
        }

        #endregion
 
        public string SMTP_HOST
        {
            get => configuration.GetValue<string>("SMTP_HOST");
        }
        public int SMTP_PORT
        {
            get => Convert.ToInt32(configuration.GetValue<string>("SMTP_PORT"));
        }
        public string SMTP_CREDENTIALS
        {
            get => configuration.GetValue<string>("SMTP_CREDENTIALS");
        }
        public string SMTP_CREDENTIALS_PASSKEY
        {
            get => configuration.GetValue<string>("SMTP_CREDENTIALS_PASSKEY");
        }
        public bool SMTP_ENABLE_SSL
        {
            get => Convert.ToBoolean(configuration.GetValue<string>("SMTP_ENABLE_SSL"));
        }
    }
}
