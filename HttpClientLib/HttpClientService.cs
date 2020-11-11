using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HttpClientLib
{
    public interface IHttpClientService
    {
        void ResetHttpClient(Action<HttpClientOptions> setOption);
        Task<List<T>> GetStringAsync<T>(string url);
    }

    class HttpClientService : IHttpClientService
    {
        HttpClient _httpClient;

        public HttpClientService()
        {
            ResetHttpClient();
        }

        public void ResetHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public void ResetHttpClient(Action<HttpClientOptions> setOption = null)
        {
            HttpClientOptions options = new HttpClientOptions();
            if (setOption != null)
                setOption(options);

            HttpClientHandler clientHandler = new HttpClientHandler();
            if(options.ByPassCertificate)
                clientHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true };

            _httpClient = new HttpClient(clientHandler);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(options.MimeType));
            _httpClient.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
        }

        public async Task<List<T>> GetStringAsync<T>(string url)
        {
            var stringTask = _httpClient.GetStringAsync(url);
            return JsonSerializer.Deserialize<List<T>>(await stringTask);
        }
    }
}
