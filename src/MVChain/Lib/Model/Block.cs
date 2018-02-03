using System;
using System.Collections.Generic;
using System.Linq;
using MessagePack;
using MVChain.Immutables;
using Newtonsoft.Json;

namespace MVChain.Lib.Model
{
    [MessagePackObject]
    public class Block : ISerializable
    {
        [IgnoreMember, JsonIgnore]
        public byte[] Original { get; set; }

        [IgnoreMember, JsonProperty(PropertyName = "id")]
        public ReadonlyBytes Id { get; set; }

        [Key(0), JsonProperty(PropertyName = "prev")]
        public virtual ReadonlyBytes PreviousHash { get; set; }

        [Key(1), JsonProperty(PropertyName = "difficulty")]
        public virtual double Difficulty { get; set; }

        [Key(2), JsonProperty(PropertyName = "nonce")]
        public virtual ulong Nonce { get; set; }

        [Key(3), JsonProperty(PropertyName = "timestamp")]
        public virtual DateTime Timestamp { get; set; }

        [Key(4), JsonProperty(PropertyName = "root")]
        public virtual byte[] TransactionRootHash { get; set; }

        [Key(5), JsonIgnore]
        public virtual IList<ReadonlyBytes> TransactionIds { get; set; }

        [Key(6), JsonIgnore]
        public virtual IList<byte[]> Transactions { get; set; }

        [IgnoreMember, JsonProperty(PropertyName = "height")]
        public int Height { get; set; }

        [IgnoreMember, JsonProperty(PropertyName = "txs")]
        public Transaction[] ParsedTransactions { get; set; }

        [IgnoreMember, JsonIgnore]
        public double TotalDifficulty { get; set; }

        public Block Clone() =>
            new Block
            {
                Original = Original,
                Id = Id,
                PreviousHash = PreviousHash,
                Difficulty = Difficulty,
                Nonce = Nonce,
                Timestamp = Timestamp,
                TransactionRootHash = TransactionRootHash,
                TransactionIds = TransactionIds?.ToList(),
                Transactions = Transactions?.ToList(),
                Height = Height,
                ParsedTransactions = ParsedTransactions
                    ?.Select(x => x.Clone()).ToArray(),
                TotalDifficulty = TotalDifficulty,
            };
    }
}