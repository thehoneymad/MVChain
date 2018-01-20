using MessagePack;
using MVChain.Immutables;
using Newtonsoft.Json;

namespace MVChain.Lib.Model
{
    [MessagePackObject]
    public class OutEntry
    {
        [Key(0), JsonProperty(PropertyName = "to")]
        public virtual ReadonlyBytes RecipientHash { get; set; }

        [Key(1), JsonProperty(PropertyName = "val")]
        public virtual ulong Amount { get; set; }

        public OutEntry Clone() =>
            new OutEntry
            {
                RecipientHash = RecipientHash,
                Amount = Amount,
            };
    }
}