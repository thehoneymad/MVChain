using System;
using System.Collections.Generic;
using System.Linq;
using MVChain.Lib.Model;

namespace MVChain.Lib.Util
{
    internal static class BlockUtils
    {
        internal static ulong GetCoinbaseAmount(int height)
        {
            /* INFO: This is essentially trying to portray the
             * exponentially declining function of bitcoin so 
             * you have one function that portrays back straight 
             * to bitcoin. 
             */
            // 1000000 = 0b11110100001001000000
            return height >= 2000 ? 0 :
                1000000ul >> (height / 100);
        }

        internal static byte[] ComputeTransactionId(byte[] data)
        {
            return CryptoUtils.ComputeDoubleSHA256(data);
        }

        internal static byte[] ComputeBlockId(byte[] data)
        {
            var block = MessagePack.MessagePackSerializer.Deserialize<Block>(data).Clone();
            block.TransactionIds = null;
            block.Transactions = null;
            var bytes = MessagePack.MessagePackSerializer.Serialize(block);
            return CryptoUtils.ComputeDoubleSHA256(bytes);
        }
    }
}