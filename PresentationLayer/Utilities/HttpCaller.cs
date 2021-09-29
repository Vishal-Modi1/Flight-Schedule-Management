using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ViewModels.VM;
using Configuration;

namespace PresentationLayer.Utilities
{
    public class HttpCaller
    {
        private HttpClient _httpClient;
        private readonly ConfigurationSettings _configurationSettings;

        public HttpCaller()
        {
            _configurationSettings = ConfigurationSettings.Instance;
        }

        public async Task<CurrentResponse> GetAsync(string url)
        {
            using (_httpClient = new HttpClient())
            {
                try
                {
                    string apiURL = $"{_configurationSettings.APIURL}{url}";

                    _httpClient.BaseAddress = new Uri(apiURL);
                    _httpClient.DefaultRequestHeaders.Accept.Clear();
                    _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(apiURL);
                    CurrentResponse  response = JsonConvert.DeserializeObject<CurrentResponse>(httpResponseMessage.Content.ReadAsStringAsync().Result);

                    return response;
                }
                catch(Exception exc)
                {
                    return null;
                }
            }
        }

        public async Task<CurrentResponse> PostAsync(string url, string jsonData)
        {
            using (_httpClient = new HttpClient())
            {
                try
                {
                    string apiURL = $"{_configurationSettings.APIURL}{url}";

                    _httpClient.BaseAddress = new Uri(apiURL);
                    _httpClient.DefaultRequestHeaders.Accept.Clear();

                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    HttpResponseMessage httpResponseMessage = await _httpClient.PostAsync(apiURL, content);
                    CurrentResponse response = JsonConvert.DeserializeObject<CurrentResponse>(httpResponseMessage.Content.ReadAsStringAsync().Result);

                    return response;
                }
                catch (Exception exc)
                {
                    return null;
                }
            }
        }
    }
}