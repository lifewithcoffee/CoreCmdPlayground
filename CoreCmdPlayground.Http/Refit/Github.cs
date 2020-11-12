using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreCmdPlayground.Http.Refit
{
    [Headers("User-Agent: .NET Foundation Repository Reporter")]    // must have, otherwise will return 403 forbidden
    public interface IGitHubApi
    {
        [Get("/users/{user}")]
        Task<GithubUser> GetUser(string user);
    }

    /// <summary>
    /// Doc: https://developer.github.com/v3/users/
    /// </summary>
    public class GithubUser
    {
        public string login { get; set; }

        public string name { get; set; }

        public string email { get; set; }

        public string blog { get; set; }

        public string company { get; set; }

        public string location { get; set; }

        public string bio { get; set; }

        public string avatar_url { get; set; }
    }
}
