using CoreCmd.Attributes;
using CoreCmdPlayground.Http.Models;
using NetCoreUtils.Text.Table;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace CoreCmdPlayground.Http.Commands
{
    [Alias("rest")]
    class RestSharpCommand
    {
        public void GithubRepos(string orgName)
        {
            try
            {
                var client = new RestClient("https://api.github.com");
                client.UserAgent = ".NET Foundation Repository Reporter";

                var request = new RestRequest($"orgs/{orgName}/repos", DataFormat.Json);

                var response = client.Get(request);
                var repos = JsonSerializer.Deserialize<List<Repository>>(response.Content);

                ConsoleTable table = new ConsoleTable();
                table.AddHeaderCell("Repository Name", false)
                     .AddHeaderCell("Stars")
                     .AddHeaderCell("Last Push")
                     .AddHeaderCell("Github Home URL")
                     ;

                foreach (var repo in repos)
                {
                    var row = new ConsoleTableRow()
                                  .AddCell(repo.Name)
                                  .AddCell(repo.Watchers.ToString())
                                  .AddCell(repo.LastPush.ToString())
                                  .AddCell(repo.GitHubHomeUrl.ToString());
                    table.AddRow(row);
                }

                table.Print();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
