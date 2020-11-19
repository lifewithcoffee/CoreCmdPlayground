using CoreCmd.Attributes;
using CoreCmdPlayground.CefSharp;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CefSharp.MinimalExample.OffScreen
{

    public class CefCommand
    {
        [Help("Open a URL and save the page to a PNG file")]
        public void Screenshot(string url)
        {
            new Screenshot().GetScreenshot(url);
        }

        [Help("Get a page's source")]
        public async Task Source(string url)
        {
            var browser = new BrowserWrapper();
            browser.OpenUrl(url);
            string pageSrc = await browser.Page.GetSourceAsync();

            Console.WriteLine(pageSrc);
            await File.WriteAllTextAsync(@"C:\_temp\cef.txt", pageSrc);
        }
    }
}