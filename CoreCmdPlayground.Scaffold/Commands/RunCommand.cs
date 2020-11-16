#if DEBUG
using CoreCmd.Attributes;
using CoreCmdPlayground.Scaffold.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCmdPlayground.Scaffold.Commands
{
    class RunCommand
    {
        public void AddClass(string className, string path)
        {
            new ScaffoldingService().GenerateClassFile(className, path);
        }

        [Help("Print .csproj file full path")]
        public void Csproj()
        {
            //new CsprojFileService().GetRootNamespace(@"e:\rp\git\CoreCmdPlayground\CoreCmdPlayground\CoreCmdPlayground.csproj");
            Console.WriteLine("Searching .csproj file...");
            Console.WriteLine(new CsprojFileService().FindCsprojFile());
        }

        [Help("Print version information")]
        public void Version()
        {
            try
            {
                var svc = new CsprojFileService();

                var projFile = svc.FindCsprojFile();
                var ver = svc.GetVersion(projFile);

                Console.WriteLine($"Version from {projFile} : {ver}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [Help("Print the root namespace")]
        public void RootNs()
        {
            try
            {
                var svc = new CsprojFileService();
                Console.WriteLine($"root namespace of {svc.FindCsprojFile()}:");
                Console.WriteLine($" {svc.GetRootNamespace()}");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
#endif
