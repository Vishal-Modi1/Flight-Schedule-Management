using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace FlightScheduleManagement
{
    public class ConfigurationManager
    {
        private static ConfigurationManager _instance = null;
        private static readonly object padlock = new object();
        public static string _apiURL = System.Configuration.ConfigurationManager.AppSettings["APIURL"].ToString();

        private ConfigurationManager()
        {
        }

        public static ConfigurationManager Instance
        {
            get
            {
                lock (padlock)
                {
                    if (_instance == null)
                    {
                        _instance = new ConfigurationManager();
                    }
                    return _instance;
                }
            }
        }
    }
}