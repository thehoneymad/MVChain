namespace MVChain
{
    using System;
    using McMaster.Extensions.CommandLineUtils;
    using MessagePack.Resolvers;
    using MVChain.Lib;
    using MVChain.Lib.Model;
    using MVChain.Lib.Util;

    class Program
    {
        private const string helpOption = "-h|--help";
        private const string appTitle = "MVChain: The minimum viable blockchain";
        static void Main(string[] args)
        {
            var app = new CommandLineApplication();

            CompositeResolver.RegisterAndSetAsDefault(
                BuiltinResolver.Instance,
                DynamicEnumResolver.Instance,
                DynamicGenericResolver.Instance,
                DynamicObjectResolver.Instance
            );

            var cryptoService = new CryptoService();

            app.Name = "mvchain";
            app.HelpOption(helpOption);

            app.OnExecute(() =>
            {
                Printer.Print(appTitle);
                app.ShowHelp();
                return 0;
            });

            app.Command("keygen", (command) =>
            {
                command.Description = "Generate a set of public and private pair keys";
                command.HelpOption(helpOption);

                command.OnExecute(() =>
                {
                    var keypair = cryptoService.GenerateKeypair();
                    Printer.Print(KeyPair.From(keypair));
                    return 0;
                });
            });

            app.Execute(args);
        }
    }
}
