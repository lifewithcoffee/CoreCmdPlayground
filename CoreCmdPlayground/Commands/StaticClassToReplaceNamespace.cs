using System;
using System.Collections.Generic;
using System.Text;

static class StaticClassToReplaceNamespace
{
    static public Action Foo()
    {
        return foo = () => Console.WriteLine("hello");
    }

    static Action foo = () => Console.WriteLine("hello");

    class Class2
    {

    }
}
