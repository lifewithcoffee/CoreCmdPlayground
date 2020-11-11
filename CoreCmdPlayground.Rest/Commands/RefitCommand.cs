using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreCmdPlayground.Rest.Commands
{
    public interface IGitHubApi
    {
        [Get("/users/{userid}")]
        Task<User> GetUser(string userid);
    }

    class RefitCommand
    {
    }
}
