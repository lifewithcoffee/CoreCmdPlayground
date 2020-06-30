using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace HttpClientLib
{
    static public class ServiceExt
    {
        static public void AddHttpServices(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddTransient<IHttpDownloadService, HttpDownloadService>();
        }
    }
}
