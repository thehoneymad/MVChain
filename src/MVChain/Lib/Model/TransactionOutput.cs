using System;
using MVChain.Immutables;

namespace MVChain.Lib.Model
{
    public class TransactionOutput : IComparable<TransactionOutput>, IEquatable<TransactionOutput>
    {
        public ReadonlyBytes TransactionId { get; set; }
        public ushort OutIndex { get; set; }
        public ReadonlyBytes Recipient { get; set; }
        public ulong Amount { get; set; }

        public override int GetHashCode() =>
            TransactionId.GetHashCode() + OutIndex;

        public override bool Equals(object obj) =>
            Equals(obj as TransactionOutput);

        public bool Equals(TransactionOutput other) =>
            other != null &&
            OutIndex == other.OutIndex &&
            TransactionId.Equals(other.TransactionId);

        public int CompareTo(TransactionOutput other)
        {
            if (other == null) return 1;

            var comp = TransactionId.CompareTo(other.TransactionId);
            return comp != 0 ? comp : OutIndex - other.OutIndex;
        }
    }
}