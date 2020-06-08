using HttpClientLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCmdPlayground.Commands
{
    public class HttpCommand
    {
        public void DownloadPicture(string url)
        {
            Console.WriteLine("DownloadPicture() called");
            new HttpService().DownloadImage(url).Wait();
        }
    }
}
