using HttpClientLib;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CoreCmdPlayground.Commands
{
    public class HttpCommand : HttpDownloadService
    {
        public HttpCommand(IHttpClientFactory httpClientFactory):base(httpClientFactory)
        { }

        //IHttpDownloadService _httpDownloadService;

        //public HttpCommand(IHttpDownloadService httpDownloadService)
        //{
        //    _httpDownloadService = httpDownloadService;
        //}

        //public async Task DownloadPicture(string url)
        //{
        //    Console.WriteLine("DownloadPicture() called");
        //    await _httpDownloadService.DownloadImage(url);
        //}
    }
}
