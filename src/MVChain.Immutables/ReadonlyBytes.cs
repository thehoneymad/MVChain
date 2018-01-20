namespace MVChain.Immutables
{
    using System;
    using System.Linq;
    using global::MVChain.Immutables.Util;
    using Newtonsoft.Json;

    [JsonConverter(typeof(ReadonlyBytesConverter))]
    public class ReadonlyBytes : IEquatable<ReadonlyBytes>, IComparable<ReadonlyBytes>
    {
        internal readonly byte[] bytes;

        internal ReadonlyBytes(byte[] bytes)
        {
            this.bytes = bytes;
        }

        public static ReadonlyBytes CopyFrom(byte[] bytes) => new ReadonlyBytes((byte[])bytes.Clone());

        public byte[] ToByteArray() => (byte[])bytes.Clone();

        public int Length => bytes.Length;

        public int CompareTo(ReadonlyBytes other)
        {
            if (other == null) return 1;

            var minCount = Math.Min(bytes.Length, other.bytes.Length);
            for (var i = 0; i < minCount; i++)
            {
                var result = bytes[i].CompareTo(other.bytes[i]);
                if (result != 0) return result;
            }

            return bytes.Length.CompareTo(other.bytes.Length);
        }

        public bool Equals(ReadonlyBytes other) =>
            other != null && bytes.SequenceEqual(other.bytes);

        public override bool Equals(object obj) => Equals(obj as ReadonlyBytes);

        public override int GetHashCode() =>
            bytes[0] | (bytes[1] << 8) | (bytes[2] << 16) | (bytes[3] << 24);

        public override string ToString() => HexConverter.FromBytes(bytes);
    }
}