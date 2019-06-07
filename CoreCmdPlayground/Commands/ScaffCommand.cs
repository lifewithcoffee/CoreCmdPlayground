using CoreCmd.Help;
using CoreCmdPlayground.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCmdPlayground.Commands
{
    [Help("Project scaffolding")]
    class ScafCommand
    {
        public void AddClass(string className, string path)
        {
            //new Scaffolding().GenerateClassFile("MyTestObj","./Services");
            new ScaffoldingService().GenerateClassFile(className, path);
        }

        public void Test()
        {
            //new CsprojFileService().GetRootNamespace(@"e:\rp\git\CoreCmdPlayground\CoreCmdPlayground\CoreCmdPlayground.csproj");
            Console.WriteLine("Searching .csproj file...");
            Console.WriteLine(new CsprojFileService().FindCsprojFile());
        }
    }

    [Help("Short command of Scaf")]
    class ScCommand : ScafCommand { }
}
