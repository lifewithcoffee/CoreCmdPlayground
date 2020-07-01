using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

static class StaticClassToReplaceNamespace
{
    static public Action Foo()
    {
        return () => Console.WriteLine("hello");
    }

    static Action foo1 = () => Console.WriteLine("hello");
    static Action foo2 = () => { Console.WriteLine("hello"); };
    static void foo3() => Console.WriteLine("hello");

    class Class2
    {
        public void Foo() => Console.WriteLine("hello");
        Func<int, int, string> Foo2 = (int x, int y) => { Console.WriteLine($"hello {x}, {y}"); return "done"; };
        Predicate<int> Foo3 = (int x) => { Console.WriteLine($"hello {x}"); return false; };

        string GetText(string path, string filename)
        {
            var sr = File.OpenText(AppendPathSeparator(path) + filename);
            var text = sr.ReadToEnd();
            return text;

            // Declare a local function.
            string AppendPathSeparator(string filepath)
            {
                if (!filepath.EndsWith(@"\"))
                    filepath += @"\";

                return filepath;
            }
        }
    }
}
