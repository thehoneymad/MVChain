namespace MVChain.Immutables
{
    using global::MessagePack;
    using global::MessagePack.Formatters;

    public class ReadonlyBytesFormatter : IMessagePackFormatter<ReadonlyBytes>
    {
        public static ReadonlyBytesFormatter Instance = new ReadonlyBytesFormatter();

        ReadonlyBytesFormatter() { }

        public int Serialize(ref byte[] bytes, int offset, ReadonlyBytes value, IFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                return MessagePackBinary.WriteNil(ref bytes, offset);
            }

            return MessagePackBinary.WriteBytes(ref bytes, offset, value.bytes);
        }

        public ReadonlyBytes Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize)
        {
            if (MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                return null;
            }

            var buffer = MessagePackBinary.ReadBytes(bytes, offset, out readSize);
            return new ReadonlyBytes(buffer);
        }
    }
}