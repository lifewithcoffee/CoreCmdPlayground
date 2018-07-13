using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace CoreCmdPlayground.Services
{
    class CsprojFileService
    {
        /// <param name="csprojFilePath">
        ///     The full absolute path of a .csproj file.
        /// </param>
        /// <returns>
        ///     Find the <RootNamespace> element in a .csproj file and return its InnerText value;
        ///     If the element is not found, return the .csproj file's name.
        /// </returns>
        public string GetRootNamespace(string csprojFilePath)
        {
            string result = null;
            try { 
                var doc = new XmlDocument();
                doc.Load(csprojFilePath);

                var root_namespace_node = doc.SelectSingleNode("//RootNamespace");
                if (root_namespace_node != null)
                    result = root_namespace_node.InnerText;
                else
                    result = Path.GetFileNameWithoutExtension(csprojFilePath);
            } catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }

    }
}
