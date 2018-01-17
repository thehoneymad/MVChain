namespace MVChain
{
    using System;
    using McMaster.Extensions.CommandLineUtils;
    class Program
    {
        private const string helpOption = "-h|--help";
        private const string appTitle = "MVChain: The minimum viable blockchain";
        static void Main(string[] args)
        {
            var app = new CommandLineApplication();

            app.Name = "mvchain";
            app.HelpOption(helpOption);

            app.OnExecute(() =>
            {
                Console.WriteLine(appTitle);
                app.ShowHelp();
                return 0;
            });

            app.Command("keygen", (command) =>
            {
                command.Description = "Generate a set of public and private pair keys";
                command.HelpOption(helpOption);

                command.OnExecute(() =>
                {
                    Console.WriteLine("This should generate a key value pair");
                    return 0;
                });
            });

            app.Execute(args);
        }
    }
}
