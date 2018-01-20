using System.Collections.Generic;
using MessagePack;
using Newtonsoft.Json;
using System;
using MVChain.Immutables;
using System.Linq;

namespace MVChain.Lib.Model
{
    [MessagePackObject]
    public class Transaction
    {
        [IgnoreMember, JsonIgnore]
        public byte[] Original { get; set; }

        [IgnoreMember, JsonProperty(PropertyName = "id")]
        public ReadonlyBytes Id { get; set; }

        [Key(0), JsonProperty(PropertyName = "timestamp")]
        public virtual DateTime Timestamp { get; set; }

        [Key(1), JsonProperty(PropertyName = "in")]
        public virtual IList<InEntry> InEntries { get; set; }

        [Key(2), JsonProperty(PropertyName = "out")]
        public virtual IList<OutEntry> OutEntries { get; set; }

        [IgnoreMember, JsonIgnore]
        public TransactionExecInformation ExecInfo { get; set; }

        public Transaction Clone() =>
            new Transaction
            {
                Original = Original,
                Id = Id,
                Timestamp = Timestamp,
                InEntries = InEntries?.Select(x => x.Clone()).ToList(),
                OutEntries = OutEntries?.Select(x => x.Clone()).ToList(),
                ExecInfo = ExecInfo,
            };
    }

}