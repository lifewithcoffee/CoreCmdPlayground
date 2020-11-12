using CoreCmdPlayground.Http.Refit;
using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CoreCmdPlayground.Http.Commands
{
    class RefitCommand
    {
        public async Task GithubUser(string username)
        {
            try
            {
                var gitHubApi = RestService.For<IGitHubApi>("https://api.github.com");
                var user = await gitHubApi.GetUser(username);

                Console.WriteLine(JsonSerializer.Serialize(user, new JsonSerializerOptions { WriteIndented = true }));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
