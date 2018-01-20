namespace MVChain.Lib.MessagePack
{
    using global::MessagePack;

    [MessagePackObject]
    public class SerializableEcPoint
    {
        [Key(0)]
        public virtual byte[] X { get; set; }

        [Key(1)]
        public virtual byte[] Y { get; set; }
    }
}