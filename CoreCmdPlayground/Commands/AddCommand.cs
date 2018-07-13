using CoreCmdPlayground.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCmdPlayground.Commands
{
    class AddCommand
    {
        public void Class(string className, string path)
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
}
