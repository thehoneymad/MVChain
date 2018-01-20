namespace MVChain
{
    using System;
    using McMaster.Extensions.CommandLineUtils;
    using McMaster.Extensions.CommandLineUtils.Validation;
    using MessagePack.Resolvers;
    using MVChain.Lib;
    using MVChain.Lib.Model;
    using MVChain.Lib.Util;

    class Program
    {
        private const string helpOptionString = "-h|--help";
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
            var mineService = new MineService();

            app.Name = "mvchain";
            app.HelpOption(helpOptionString);

            app.OnExecute(() =>
            {
                Printer.Print(appTitle);
                app.ShowHelp();
                return 0;
            });


            app.Command("keygen", (command) =>
            {
                command.Description = "Generate a set of public and private pair keys";
                command.HelpOption(helpOptionString);

                command.OnExecute(() =>
                {
                    var keypair = cryptoService.GenerateKeypair();
                    Printer.Print(KeyPair.From(keypair));
                    return 0;
                });
            });

            app.Command("genesis", (command) =>
            {
                command.Description = "Generate the genesis block";
                command.HelpOption(helpOptionString);

                var keypairopt = command.Option(
                    "--keypair <FILE>",
                    "Files that contains keypair json",
                    CommandOptionType.SingleValue)
                    .IsRequired(allowEmptyStrings: false, errorMessage: "keypair file needed");

                keypairopt.ShortName = "-kp";

                var genesisblockopt = command.Option(
                    "--genesis <FILE>",
                    "The genesis block file that will be created",
                    CommandOptionType.SingleValue)
                    .IsRequired(allowEmptyStrings: false, errorMessage: "Genesis block file name required");

                genesisblockopt.ShortName = "-g";
                
                command.OnExecute(() =>
                {
                    mineService.MineGenesisBlock(keypairopt.Value(), genesisblockopt.Value());
                    return 0;
                });
            });

            app.Execute(args);
        }
    }
}
