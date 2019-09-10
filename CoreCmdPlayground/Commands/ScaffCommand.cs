using CoreCmd.Attributes;
using CoreCmd.Help;
using HandlebarsDotNet;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCmdPlayground.Commands
{
    [Help("Project scaffolding")]
    class ScafCommand
    {

        // try out Handlebars.Net
        public void TestHandlebars()
        {
            string source =
@"<div class=""entry"">
  <h1>{{title}}</h1>
  <div class=""body"">
    {{body}}
  </div>
</div>";
            var template = Handlebars.Compile(source);

            var data = new
            {
                title = "My new post",
                body = "This is my first post!"
            };

            var result = template(data);

            Console.WriteLine(result);
        }
    }
}
