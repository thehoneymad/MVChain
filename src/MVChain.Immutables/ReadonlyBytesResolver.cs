namespace MVChain.Immutables
{
    using global::MessagePack;
    using global::MessagePack.Formatters;
    using global::MVChain.Immutables;

    public class ReadonlyBytesResolver : IFormatterResolver
    {
        public static IFormatterResolver Instance = new ReadonlyBytesResolver();

        ReadonlyBytesResolver() { }

        public IMessagePackFormatter<T> GetFormatter<T>()
        {
            return typeof(T) ==
                typeof(ReadonlyBytes) ?
                ReadonlyBytesFormatter.Instance as IMessagePackFormatter<T> : null;
        }
    }
}