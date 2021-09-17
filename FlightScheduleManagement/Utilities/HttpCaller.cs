using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FlightScheduleManagement.Utilities
{
    public class HttpCaller
    {
        private HttpClient _httpClient; 

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            using (_httpClient = new HttpClient())
            {
                try
                {
                    string apiURL = $"{ConfigurationManager._apiURL}{url}";

                    _httpClient.BaseAddress = new Uri(apiURL);
                    _httpClient.DefaultRequestHeaders.Accept.Clear();
                    _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await _httpClient.GetAsync(apiURL);

                    return response;
                }
                catch(Exception exc)
                {
                    return null;
                }
            }
        }
    }
}