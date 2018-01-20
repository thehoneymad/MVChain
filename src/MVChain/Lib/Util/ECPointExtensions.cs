namespace MVChain.Lib.Util
{
    using System;
    using System.Security.Cryptography;
    using global::MessagePack;
    using MVChain.Lib.MessagePack;

    public static class ECPointExtensions
    {
        public static byte[] ToBytes(this ECPoint ecpoint)
        => MessagePackSerializer.Serialize(
            new SerializableEcPoint { X = ecpoint.X, Y = ecpoint.Y });

        public static ECPoint ToEcPoint(this byte[] bytes)
        {
            var point = MessagePackSerializer.Deserialize<SerializableEcPoint>(bytes);
            return new ECPoint { X = point.X, Y = point.Y };
        }
    }
}