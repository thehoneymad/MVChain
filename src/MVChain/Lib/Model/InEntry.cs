using MessagePack;
using MVChain.Immutables;
using Newtonsoft.Json;

namespace MVChain.Lib.Model
{
    [MessagePackObject]
    public class InEntry : ISerializable
    {
        [Key(0), JsonProperty(PropertyName = "tx")]
        public virtual ReadonlyBytes TransactionId { get; set; }

        [Key(1), JsonProperty(PropertyName = "i")]
        public virtual ushort OutEntryIndex { get; set; }

        [Key(2), JsonProperty(PropertyName = "pub")]
        public virtual byte[] PublicKey { get; set; }

        [Key(3), JsonProperty(PropertyName = "sig")]
        public virtual byte[] Signature { get; set; }

        public InEntry Clone() =>
            new InEntry
            {
                TransactionId = TransactionId,
                OutEntryIndex = OutEntryIndex,
                PublicKey = PublicKey,
                Signature = Signature,
            };
    }
}