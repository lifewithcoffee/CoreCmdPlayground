using HttpClientLib;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreCmdPlayground.Commands
{
    public class HttpCommand
    {
        IHttpDownloadService _httpDownloadService;

        public HttpCommand(IHttpDownloadService httpDownloadService)
        {
            _httpDownloadService = httpDownloadService;
        }

        public async Task DownloadPicture(string url)
        {
            Console.WriteLine("DownloadPicture() called");
            await _httpDownloadService.DownloadImage(url);
        }
    }
}
