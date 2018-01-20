using System;
using System.Linq;
using System.Security.Cryptography;

namespace MVChain.Lib.Util
{
    internal static class CryptoUtils
    {
        internal static byte[] ToAddress(byte[] publicKey)
        {
            // INFO: In Bitcoin, recipient address is computed by SHA256 and RIPEMD160.
            return ComputeDoubleSHA256(publicKey);
        }

        internal static byte[] ComputeDoubleSHA256(byte[] bytes)
        {
            using (var sha256 = SHA256.Create())
                return sha256.ComputeHash(sha256.ComputeHash(bytes));
        }

        internal static double Difficulty(byte[] hash)
        {
            var bytes = new byte[] { 0x3F, 0xF0 }.Concat(hash).Take(8);
            if (BitConverter.IsLittleEndian) bytes = bytes.Reverse();

            var d = BitConverter.ToDouble(bytes.ToArray(), 0);
            return Math.Pow(2, -35) / (d - 1);
        }
    }
}