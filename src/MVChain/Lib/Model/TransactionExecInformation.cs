using System.Collections.Generic;

namespace MVChain.Lib.Model
{
    public class TransactionExecInformation
    {
        public bool Coinbase { get; set; }
        public List<TransactionOutput> RedeemedOutputs { get; set; }
        public List<TransactionOutput> GeneratedOutputs { get; set; }
        public ulong TransactionFee { get; set; }
    }
}