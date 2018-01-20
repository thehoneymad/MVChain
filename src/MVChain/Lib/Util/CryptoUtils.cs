using System.Linq;
using System.Security.Cryptography;

namespace MVChain.Lib.Util
{
    public static class CryptoUtils
    {
        public static byte[] ToAddress(byte[] publicKey)
        {
            // INFO: In Bitcoin, recipient address is computed by SHA256 and RIPEMD160.
            return ComputeDoubleSHA256(publicKey);
        }

        public static byte[] ComputeDoubleSHA256(byte[] bytes)
        {
            using (var sha256 = SHA256.Create())
                return sha256.ComputeHash(sha256.ComputeHash(bytes));
        }
    }
}