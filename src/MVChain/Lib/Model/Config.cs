using System.Net;

namespace MVChain.Lib.Model
{
    internal class Config: ISerializable
    {
        public IPEndPoint ListenOn { get; set; }
        public IPEndPoint[] InitialEndpoints { get; set; }
        public string KeyPairPath { get; set; }
        public string GenesisPath { get; set; }
        public bool ShouldMine { get; set; }
    }
}