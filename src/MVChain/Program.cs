using System;
using Microsoft.Extensions.CommandLineUtils;

namespace MVChain
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new CommandLineApplication();
            app.Name = "mvchain";
            app.HelpOption("-h|--help");

            app.OnExecute(()=> {
                Console.WriteLine("Hello World!");
                return 0;
            });

            app.Execute(args);
        }
    }
}
