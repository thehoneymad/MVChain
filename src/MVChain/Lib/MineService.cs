namespace MVChain.Lib
{
    using System;
    using System.IO;
    using Model;
    using Util;
    using Newtonsoft.Json;
    using MVChain.Immutables;
    using System.Collections.Generic;
    using System.Threading;

    public class MineService
    {
        public static ReadonlyBytes EmptyHash = ReadonlyBytes.CopyFrom(new byte[32]);
        public static double Difficulty = 2e-6;

        private readonly CryptoService cryptoService;
        public MineService(CryptoService cryptoService)
        {
            this.cryptoService = cryptoService;
        }

        internal void MineGenesisBlock(string keypairjsonpath, string genesisblockpath)
        {
            var keyPair = KeyPair.LoadFrom(keypairjsonpath);
            if (!cryptoService.IsValid(keyPair.PrivateKey, keyPair.PublicKey))
            {
                throw new InvalidDataException("Invalid key pair set provided");
            }

            Printer.Print("Generating the genesis block...");

            var coinbaseTx = CreateCoinbaseTransaction(0, CryptoUtils.ToAddress(keyPair.PublicKey));
            var txIds = new List<Immutables.ReadonlyBytes> { coinbaseTx.Id };
            var root = GetRootHashFromTransactionIds(txIds);

            var genesisBlock = new Block
            {
                PreviousHash = EmptyHash,
                Difficulty = Difficulty,
                Nonce = 0,
                Timestamp = DateTime.UtcNow,
                TransactionRootHash = root,
                TransactionIds = txIds,
                Transactions = new List<byte[]> { coinbaseTx.Original },
                ParsedTransactions = new[] { coinbaseTx }
            };

            Mine(genesisBlock);

            var json = JsonConvert.SerializeObject(genesisBlock, Formatting.Indented);

            Printer.Print(json);

            using (var fs = File.OpenWrite(genesisblockpath))
            {
                fs.Write(genesisBlock.Original, 0, genesisBlock.Original.Length);
                fs.Flush();
            }
        }

        internal bool Mine(Block seed, CancellationToken token = default(CancellationToken))
        {
            var rnd = new Random();
            var nonceSeed = new byte[sizeof(ulong)];
            rnd.NextBytes(nonceSeed);

            ulong nonce = BitConverter.ToUInt64(nonceSeed, 0);

            while (!token.IsCancellationRequested)
            {
                seed.Nonce = nonce++;
                seed.Timestamp = DateTime.UtcNow;

                var data = MessagePack.MessagePackSerializer.Serialize(seed);
                var blockId = BlockUtils.ComputeBlockId(data);
                if (CryptoUtils.Difficulty(blockId) > seed.Difficulty)
                {
                    seed.Id = ReadonlyBytes.CopyFrom(blockId);
                    seed.Original = data;
                    return true;
                }
            }

            return false;
        }

        internal Transaction CreateCoinbaseTransaction(int height, byte[] recipient)
        {
            var transaction = new Transaction
            {
                Timestamp = DateTime.UtcNow,
                InEntries = new List<InEntry>(),
                OutEntries = new List<OutEntry>
                {
                    new OutEntry
                    {
                        Amount = BlockUtils.GetCoinbaseAmount(height: 0),
                        RecipientHash = ReadonlyBytes.CopyFrom(recipient),
                    },
                },
            };

            var data = transaction.Original = MessagePack.MessagePackSerializer.Serialize(transaction);
            transaction.Id = ReadonlyBytes.CopyFrom(BlockUtils.ComputeTransactionId(data));
            return transaction;
        }

        private byte[] GetRootHashFromTransactionIds(IList<ReadonlyBytes> txIds)
        {
            const int HashLength = 32;

            var i = 0;
            var ids = new byte[txIds.Count * HashLength];
            foreach (var txId in txIds)
            {
                Buffer.BlockCopy(
                    txId.ToByteArray(), 0, ids, i++ * HashLength, HashLength);
            }

            return CryptoUtils.ComputeDoubleSHA256(ids);
        }
    }
}