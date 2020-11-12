using HttpClientLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CoreCmdPlayground.Http.Commands
{
    public class HttpCommand
    {
        private HttpClient _httpClient;
        private IHttpClientService _httpClientService;

        public HttpCommand(IHttpClientFactory httpClientFactory, IHttpClientService httpClientService)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClientService = httpClientService;
        }

        public async Task DownloadImage(string url)
        {

            //string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            //string localFilename = "favicon.ico";
            //string localPath = Path.Combine(documentsPath, localFilename);

            _httpClientService.ResetHttpClient(_httpClient);
            await _httpClientService.DownloadImageFile(url, @"C:\_temp");
        }

        public async Task GithubRepos(string orgName)   // e.g. dotnet, redwoodteq
        {
            _httpClientService.ResetHttpClient(
                o => {
                    o.ByPassCertificate = true;
                    o.MimeType = "application/vnd.github.v3+json";
                }
            );

            List<Repository> repositories = await _httpClientService.GetStringAsync<Repository>($"https://api.github.com/orgs/{orgName}/repos");

            /** Alternative approach:
             * var streamTask = _httpClient.GetStreamAsync($"https://api.github.com/orgs/{orgName}/repos");
             * var repositories = await JsonSerializer.DeserializeAsync<List<Repository>>(await streamTask);
             **/

            int maxNameLength = repositories.Select(r => r.Name.Length).Max() + 1;
            const string name = "Name";
            const string stars = "Stars";
            const string lastPush = "Last Push";
            const string url = "Github Home URL";
            const string seperator = " ";
            Console.WriteLine($"{name.PadLeft(maxNameLength)} : {stars,-6} | {lastPush,-22} | {url}");
            Console.WriteLine($"{seperator.PadRight(maxNameLength, '-')} : {seperator.PadLeft(6, '-')} | {seperator.PadLeft(22, '-')} | {seperator.PadLeft(30, '-')}");
            foreach (var repo in repositories)
            {
                Console.WriteLine($"{repo.Name.PadLeft(maxNameLength)} : {repo.Watchers,-6} | {repo.LastPush,-22} | {repo.GitHubHomeUrl}");
            }

            Console.WriteLine("\nNOTE: _httpClient was replaced in this demo, i.e. _httpClient was not created from a http client factory");
            Console.WriteLine($"Console width: {Console.WindowWidth}");
        }
    }
}
