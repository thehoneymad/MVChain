using System;
using System.Collections.Generic;
using MVChain.Immutables;
using MVChain.Lib.Model;

namespace MVChain.Lib
{
    internal class InventoryManager
    {
        private Dictionary<ReadonlyBytes, Block> Blocks = new Dictionary<ReadonlyBytes, Block>();
        internal IEnumerable<Block> BlockList => Blocks.Values;

        internal InventoryManager()
        {
        }

        internal void AddBlock(Block block)
        {
            if (block == null)
            {
                throw new ArgumentNullException(nameof(block));
            }

            this.Blocks.Add(block.Id, block);
        }
    }
}