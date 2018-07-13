using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CoreCmdPlayground.Services
{
    class ScaffoldingService
    {
        internal void GenerateClassFile(string className, string path)
        {
            Directory.CreateDirectory(path);    // always create the path recursively if not exist

            string filePath = $@"{path}\{className}.cs";
            using(var textWriter = File.AppendText(filePath))
            {
                string src = $@"namespace SomeNS
{{
    class {className}
    {{
        void Bar(){{ }}
    }}
}}";
                textWriter.Write(src);
            }
        }
    }
}
