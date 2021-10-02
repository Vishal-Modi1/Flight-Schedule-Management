﻿using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Configuration
{
    public class ConfigurationSettings
    {
        private static ConfigurationSettings _instance = null;
        private static readonly object padlock = new object();
        private static IConfiguration configuration;

        #region Object Creation
        private ConfigurationSettings()
        {
            configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("configuration.json", optional: true, reloadOnChange: true)
                    .Build();

        }

        public static ConfigurationSettings Instance
        {
            get
            {
                lock (padlock)
                {
                    if (_instance == null)
                    {
                        _instance = new ConfigurationSettings();
                    }
                    return _instance;
                }
            }
        }

        #endregion

        public string APIURL
        {
            get => configuration.GetValue<string>("APIURL");
        }

        public string JWTKey
        {
            get => configuration.GetValue<string>("JWTKey");
        }

        public  double JWTExpireDays
        {
            get => Convert.ToDouble(configuration.GetValue<string>("JWTExpireDays"));
        }

        public  string JWTIssuer
        {
            get => configuration.GetValue<string>("JWTIssuer");
        }

        public string CookieName
        {
            get => configuration.GetValue<string>("CookieName");
        }
        public MailSettingConfig MailSetting
        {
            get => MailSettingConfig.Instance;
        }
    }
}
