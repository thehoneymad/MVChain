using System;
using System.IO;
using MVChain.Lib.Model;
using MVChain.Lib.Util;
using Newtonsoft.Json;

namespace MVChain.Lib
{
    public class NodeService
    {
        private CryptoService cryptoService;
        private MineService mineService;
        private InventoryManager inventoryManager;
        private BlockProcessor blockProcessor;

        public NodeService(CryptoService cryptoService, MineService mineService)
        {
            this.cryptoService = cryptoService ?? throw new System.ArgumentNullException(nameof(cryptoService));
            this.mineService = mineService ?? throw new System.ArgumentNullException(nameof(mineService));
        }

        internal void BootUp(string configFile)
        {
            var loadedConfig = LoadConfig(configFile);
            var genesis = loadedConfig.genesisBlock;

            this.inventoryManager = new InventoryManager();
            this.blockProcessor = new BlockProcessor();

            inventoryManager.AddBlock(genesis);

            Printer.Print("Booted up with our initial genesis block");
            Printer.Print(JsonConvert.SerializeObject(inventoryManager.BlockList, Formatting.Indented));
        }

        internal void AddBlock(Block block) {
            if (block == null)
            {
                throw new ArgumentNullException(nameof(block));
            }
            mineService.Mine(block);

            this.inventoryManager.AddBlock(block);

            Printer.Print("New block added");
            Printer.Print(JsonConvert.SerializeObject(inventoryManager.BlockList, Formatting.Indented));
        }

        private (Config config, KeyPair keypair, Block genesisBlock) LoadConfig(string configFile)
        {
            if (string.IsNullOrWhiteSpace(configFile))
            {
                throw new ArgumentException(nameof(configFile));
            }

            Config config = null;
            string bootStage = null;
            KeyPair keypair = null;
            Block genesisBlock = null;

            try
            {
                bootStage = "Loading Config";
                config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(Path.GetFullPath(configFile)));
                
                bootStage = "Loading KeyPair";
                keypair = KeyPair.LoadFrom(config.KeyPairPath);

                bootStage = "Loading genesis block";
                var bytes = File.ReadAllBytes(config.GenesisPath);
                genesisBlock = BlockUtils.DeserializeBlock(bytes);
            }
            catch (Exception exp) when (exp is IOException || exp is JsonException)
            {
                Printer.Print(string.Join(" ", bootStage, "failed."));
                Printer.Print(exp.Message);
            }

            return (config, keypair, genesisBlock);
        }
    }
}