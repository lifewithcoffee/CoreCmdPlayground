using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Net.Http;
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
    }

}
