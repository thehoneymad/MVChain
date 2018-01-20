namespace MVChain.Immutables.Util
{
    using System;
    using System.Linq;

    internal static class HexConverter
    {
        internal static byte[] ToBytes(string hex)
        {
            var ret = new byte[hex.Length / 2];
            for (var i = 0; i < hex.Length; i += 2)
                ret[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return ret;
        }

        internal static string FromBytes(byte[] bytes) =>
            string.Join("", bytes.Select(b => $"{b:x2}"));
    }
}