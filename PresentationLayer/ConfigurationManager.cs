using Microsoft.Extensions.Configuration;
using System.IO;

namespace PresentationLayer
{
    public class ConfigurationManager
    {
        private static ConfigurationManager _instance = null;
        private static readonly object padlock = new object();
        private static IConfiguration configuration;
        public static string _apiURL;

        private ConfigurationManager()
        {
            configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

            _apiURL = configuration.GetValue<string>("APIURL");

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