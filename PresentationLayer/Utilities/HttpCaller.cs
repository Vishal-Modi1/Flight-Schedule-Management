using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ViewModels.VM;
using Configuration;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace PresentationLayer.Utilities
{
    public class HttpCaller
    {
        private HttpClient _httpClient;
        private readonly ConfigurationSettings _configurationSettings;
        private readonly HttpContext _httpContext;

        public HttpCaller(HttpContext httpContext)
        {
            _configurationSettings = ConfigurationSettings.Instance;
            _httpContext = httpContext;
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
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetClaimValue("AcessToken"));

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

                    _httpClient.DefaultRequestHeaders.Authorization =  new AuthenticationHeaderValue("Bearer", GetClaimValue("AcessToken"));

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


        public async Task<CurrentResponse> PostFileAsync(string url, MultipartFormDataContent fileContent)
        {
            using (_httpClient = new HttpClient())
            {
                try
                {
                    string apiURL = $"{_configurationSettings.APIURL}{url}";

                    _httpClient.BaseAddress = new Uri(apiURL);
                    _httpClient.DefaultRequestHeaders.Accept.Clear();

                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetClaimValue("AcessToken"));

                    HttpResponseMessage httpResponseMessage = await _httpClient.PostAsync(apiURL, fileContent);
                    CurrentResponse response = JsonConvert.DeserializeObject<CurrentResponse>(httpResponseMessage.Content.ReadAsStringAsync().Result);

                    return response;
                }
                catch (Exception exc)
                {
                    return null;
                }
            }
        }
        public string GetClaimValue(string claimType)
        {
            ClaimsPrincipal cp = _httpContext.User;

            string token = cp.Claims.Where(c => c.Type == claimType)
                               .Select(c => c.Value).SingleOrDefault();

            return token;
        }
    }
}