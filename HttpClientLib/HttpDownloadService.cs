using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HttpClientLib
{
    public interface IHttpDownloadService
    {
        Task DownloadImage(string url);
    }

    public class HttpDownloadService : IHttpDownloadService
    {
        private readonly HttpClient _httpClient;

        public HttpDownloadService(IHttpClientFactory httpClientFactory)
        {
            //var serviceProvider = new ServiceCollection().AddHttpClient().BuildServiceProvider();
            //var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();

            _httpClient = httpClientFactory.CreateClient();
            Console.WriteLine("_httpClient initialized");
        }

        public async Task DownloadImage(string url)
        {
            byte[] imageBytes = await _httpClient.GetByteArrayAsync(url);

            //string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            //string localFilename = "favicon.ico";
            //string localPath = Path.Combine(documentsPath, localFilename);

            string filename = Path.GetFileName(new Uri(url).LocalPath);
            File.WriteAllBytes(@$"C:\_temp\{filename}", imageBytes);
        }

        public async Task Get()
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            _httpClient.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            //var stringTask = _httpClient.GetStringAsync("https://api.github.com/orgs/dotnet/repos");
            var stringTask = _httpClient.GetStringAsync("http://localhost:5000/healthz");

            var msg = await stringTask;
            Console.Write(msg);
        }
    }

}
