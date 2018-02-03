using System;
using MVChain.Immutables;
using MVChain.Lib.Model;

namespace MVChain.Lib
{
    public class BlockProcessor
    {
        internal void ProcessBlock(byte[] original, ReadonlyBytes previousHash)
        {
            lock(this) {
                
            }
        }
    }
}