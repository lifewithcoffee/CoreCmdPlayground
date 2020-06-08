﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpClientLib
{
    public class HttpService
    {
        private readonly HttpClient _httpClient;

        public HttpService()
        {
            var serviceProvider = new ServiceCollection().AddHttpClient().BuildServiceProvider();
            var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();

            _httpClient = httpClientFactory.CreateClient();
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
