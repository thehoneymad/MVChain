using global::MessagePack;

namespace MVChain.Lib.Util
{
    using System.Security.Cryptography;
    using Lib.Model;

    internal static class ECPointExtensions
    {
        internal static byte[] ToBytes(this ECPoint ecpoint)
        => MessagePackSerializer.Serialize(
            new SerializableEcPoint { X = ecpoint.X, Y = ecpoint.Y });

        internal static ECPoint ToEcPoint(this byte[] bytes)
        {
            var point = MessagePackSerializer.Deserialize<SerializableEcPoint>(bytes);
            return new ECPoint { X = point.X, Y = point.Y };
        }
    }
}