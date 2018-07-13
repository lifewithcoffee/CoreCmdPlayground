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
            new Scaffolding().GenerateClassFile(className, path);
        }

        public void Test()
        {
            new Scaffolding().GetCsprojFileRootNamespace(@"e:\rp\git\CoreCmdPlayground\CoreCmdPlayground\CoreCmdPlayground.csproj");
        }
    }
}
