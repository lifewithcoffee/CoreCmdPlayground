﻿using CefSharp.OffScreen;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CefSharp.MinimalExample.OffScreen
{
    public class Screenshot
    {
        ChromiumWebBrowser browser;

        public int GetScreenshot(string url)
        {

            try
            {
                string[] args = { }; // fake args

                Console.WriteLine("This example application will load {0}, take a screenshot, and save it to your desktop.", url);
                Console.WriteLine("You may see Chromium debugging output, please wait...");
                Console.WriteLine();

                var settings = new CefSettings()
                {
                    //By default CefSharp will use an in-memory cache, you need to specify a Cache Folder to persist data
                    CachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CefSharp\\Cache")
                };

                //Perform dependency check to make sure all relevant resources are in our output directory.
                Cef.Initialize(settings, performDependencyCheck: true, browserProcessHandler: null);

                // Create the offscreen Chromium browser.
                browser = new ChromiumWebBrowser(url);

                // An event that is fired when the first page is finished loading.
                // This returns to us from another thread.
                browser.LoadingStateChanged += BrowserLoadingStateChanged;

                // We have to wait for something, otherwise the process will exit too soon.
                Console.ReadKey();

                // Clean up Chromium objects.  You need to call this in your application otherwise
                // you will get a crash when closing.
                Cef.Shutdown();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

            return 0;
        }

        private void BrowserLoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            // Check to see if loading is complete - this event is called twice, one when loading starts
            // second time when it's finished
            // (rather than an iframe within the main frame).
            if (!e.IsLoading)
            {
                // Remove the load event handler, because we only want one snapshot of the initial page.
                browser.LoadingStateChanged -= BrowserLoadingStateChanged;

                var scriptTask = browser.EvaluateScriptAsync("document.querySelector('[name=q]').value = 'CefSharp Was Here!'");

                scriptTask.ContinueWith(t =>
                {
                    //Give the browser a little time to render
                    Thread.Sleep(5000);

                    browser.GetSourceAsync().ContinueWith(s =>
                    {
                        string src = s.Result;
                        //Console.WriteLine(src);

                        string srcFilePath = @"C:\_temp\cef.txt";
                        File.WriteAllText(srcFilePath, src);
                        Console.WriteLine($"Source saved to {srcFilePath}");

                        Process.Start(new ProcessStartInfo(srcFilePath)
                        {
                            UseShellExecute = true      // UseShellExecute is false by default on .NET Core.
                        });

                    });

                    // Get full page viewport size
                    var contentSize = browser.GetContentSizeAsync().Result;
                    var fullPageViewPort = new DevTools.Page.Viewport
                    {
                        Width = contentSize.Width,
                        Height = contentSize.Height,
                    };

                    // Wait for the screenshot to be taken.
                    var task = browser.CaptureScreenshotAsync(null, null, fullPageViewPort);
                    task.ContinueWith(x =>
                    {
                        // Make a file to save it to (e.g. C:\Users\jan\Desktop\CefSharp screenshot.png)
                        var screenshotPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "CefSharp screenshot.png");

                        Console.WriteLine();
                        Console.WriteLine("Screenshot ready. Saving to {0}", screenshotPath);

                        // Save the Bitmap to the path.
                        // The image type is auto-detected via the ".png" extension.
                        File.WriteAllBytes(screenshotPath, task.Result);

                        Console.WriteLine("Screenshot saved.  Launching your default image viewer...");

                        // Tell Windows to launch the saved image.
                        Process.Start(new ProcessStartInfo(screenshotPath)
                        {
                            // UseShellExecute is false by default on .NET Core.
                            UseShellExecute = true
                        });

                        Console.WriteLine("Image viewer launched.  Press any key to exit.");
                    }, TaskScheduler.Default);

                });
            }
        }
    }
}