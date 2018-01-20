namespace MVChain.Lib
{
    using System;
    public class MineService
    {
        internal void MineGenesisBlock(string keypairjsonpath, string genesisblockpath)
        {
            if (string.IsNullOrWhiteSpace(keypairjsonpath))
            {
                throw new ArgumentException(nameof(keypairjsonpath));
            }

            if (string.IsNullOrWhiteSpace(genesisblockpath))
            {
                throw new ArgumentException(nameof(genesisblockpath));
            }

            
        }
    }
}