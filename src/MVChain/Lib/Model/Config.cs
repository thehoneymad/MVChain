using System.Net;

namespace MVChain.Lib.Model
{
    internal class Config
    {
        public IPEndPoint ListenOn { get; set; }
        public IPEndPoint[] InitialEndpoints { get; set; }
        public string KeyPairPath { get; set; }
        public string GenesisPath { get; set; }
        public bool ShouldMine { get; set; }
    }
}