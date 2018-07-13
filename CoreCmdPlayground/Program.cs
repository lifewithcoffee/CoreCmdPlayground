using System;
using CoreCmd;

namespace CoreCmdPlayground
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                new CommandExecutor().Execute(args);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
