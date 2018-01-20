using MVChain.Lib.Util;
using System;
using System.IO;
using Newtonsoft.Json;

namespace MVChain.Lib.Model
{
    public class KeyPair : ISerializable
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

        internal static KeyPair From((byte[] privateKey, byte[] publicKey) bytekeypair)
        {
            return new KeyPair
            {
                PrivateKey = bytekeypair.privateKey,
                PublicKey = bytekeypair.publicKey,
                Address = Util.CryptoUtils.ToAddress(bytekeypair.publicKey),
            };
        }

        internal static KeyPair LoadFrom(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException(nameof(path));
            }

            var keyContent = File.ReadAllText(path);
            var keyPair = JsonConvert.DeserializeObject<KeyPair>(keyContent);
            return keyPair;
        }
    }
}