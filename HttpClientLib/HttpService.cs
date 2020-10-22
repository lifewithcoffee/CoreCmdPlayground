using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace HttpClientLib
{
    public class HttpService
    {
        private HttpClient _httpClient;

        public HttpService(IHttpClientFactory httpClientFactory)
        {
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

        public async Task GithubRepos(string orgName)   // e.g. dotnet, redwoodteq
        {
            HttpClientHandler clientHandler = new HttpClientHandler{ ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true };   // bypass the certificate
            _httpClient = new HttpClient(clientHandler);

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            _httpClient.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var stringTask = _httpClient.GetStringAsync($"https://api.github.com/orgs/{orgName}/repos");
            var repositories = JsonSerializer.Deserialize<List<Repository>>(await stringTask);

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
            Console.WriteLine($"{name.PadLeft(maxNameLength)} : {stars,-6} | {lastPush, -22} | {url}");
            Console.WriteLine($"{seperator.PadRight(maxNameLength, '-')} : {seperator.PadLeft(6,'-')} | {seperator.PadLeft(22, '-')} | {seperator.PadLeft(30, '-')}");
            foreach (var repo in repositories)
            {
                Console.WriteLine($"{repo.Name.PadLeft(maxNameLength)} : {repo.Watchers,-6} | {repo.LastPush, -22} | {repo.GitHubHomeUrl}");
                //Console.WriteLine($"LastPush: {repo.LastPush}");
                //Console.WriteLine(repo.Description);
                //Console.WriteLine($"Address : {repo.GitHubHomeUrl}");
                //Console.WriteLine(repo.Homepage);
                //Console.WriteLine($"Stars   : {repo.Watchers}");
                //Console.WriteLine("-----------------");
            }

            //var msg = await stringTask;
            //Console.WriteLine(msg);
            Console.WriteLine("\nNOTE: _httpClient was replaced in this demo, i.e. _httpClient was not created from a http client factory");
        }
    }

}
