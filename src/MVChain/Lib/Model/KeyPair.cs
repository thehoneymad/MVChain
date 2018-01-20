namespace MVChain.Lib.Model
{
    using System.IO;
    using MVChain.Lib.Util;
    using Newtonsoft.Json;
    public class KeyPair: IPrintableModel
    {
        [JsonProperty(PropertyName = "pub")]
        public byte[] PublicKey { get; set; }

        [JsonProperty(PropertyName = "prv")]
        public byte[] PrivateKey { get; set; }

        [JsonProperty(PropertyName = "addr")]
        public byte[] Address { get; set; }

        public bool ShouldSerializePublicKey() => !PublicKey.IsNullOrEmpty();
        public bool ShouldSerializePrivateKey() => !PrivateKey.IsNullOrEmpty();
        public bool ShouldSerializeAddress() => !Address.IsNullOrEmpty();

        public static KeyPair From((byte[] privateKey, byte[] publicKey) bytekeypair)
        {
            return new KeyPair
            {
                PrivateKey = bytekeypair.privateKey,
                PublicKey = bytekeypair.publicKey,
                Address = CryptoUtils.ToAddress(bytekeypair.publicKey),
            };
        }
    }
}