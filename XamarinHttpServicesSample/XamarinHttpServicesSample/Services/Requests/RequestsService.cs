using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace XamarinHttpServicesSample.Services
{
    public class RequestsService : IRequestsService
    {
        private static HttpClient _instance;

        private static HttpClient HttpClientInstance => _instance ?? (_instance = new HttpClient());

        public async Task<TResult> GetAsync<TResult>(string uri, string token = "")
        {
            setupHttpClient(token);

            HttpResponseMessage response = await HttpClientInstance.GetAsync(uri);

            await HandleResponse(response);

            string serialized = await response.Content.ReadAsStringAsync();
            TResult result = await Task.Run(() => JsonConvert.DeserializeObject<TResult>(serialized));

            return result;
        }

        public Task<TResult> PostAsync<TResult>(string uri, TResult data, string token = "")
        {
            return PostAsync<TResult, TResult>(uri, data, token);
        }

        public async Task<TResult> PostAsync<TRequest, TResult>(string uri, TRequest data, string token = "")
        {
            setupHttpClient(token);

            string serialized = await Task.Run(() => JsonConvert.SerializeObject(data));
            HttpResponseMessage response = await HttpClientInstance.PostAsync(uri, new StringContent(serialized, Encoding.UTF8, "application/json"));

            await HandleResponse(response);

            string responseData = await response.Content.ReadAsStringAsync();

            return await Task.Run(() => JsonConvert.DeserializeObject<TResult>(responseData));
        }
        
        public Task<TResult> PutAsync<TResult>(string uri, TResult data, string token = "")
        {
            return PutAsync<TResult, TResult>(uri, data, token);
        }

        public async Task<TResult> PutAsync<TRequest, TResult>(string uri, TRequest data, string token = "")
        {
            setupHttpClient(token);
            
            string serialized = await Task.Run(() => JsonConvert.SerializeObject(data));
            HttpResponseMessage response = await HttpClientInstance.PutAsync(uri, new StringContent(serialized, Encoding.UTF8, "application/json"));

            await HandleResponse(response);

            string responseData = await response.Content.ReadAsStringAsync();

            return await Task.Run(() => JsonConvert.DeserializeObject<TResult>(responseData));
        }

        private void setupHttpClient(string token = "")
        {
            HttpClientInstance.DefaultRequestHeaders.Accept.Clear();
            HttpClientInstance.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (!string.IsNullOrEmpty(token))
            {
                HttpClientInstance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        private async Task HandleResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.Forbidden || response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new Exception(content);
                }

                throw new HttpRequestException(content);
            }
        }

        
    }
}
