using System;
using System.Security.Cryptography;
using MVChain.Lib.Util;

namespace MVChain.Lib
{
    public class CryptoService
    {
        public static readonly ECCurve ecCurve = ECCurve.NamedCurves.nistP256;
        public (byte[] privateKey, byte[] publicKey) GenerateKeypair()
        {
            using (var ecdsa = ECDsa.Create(ecCurve))
            {
                var parameters = ecdsa.ExportParameters(includePrivateParameters: true);
                return (parameters.D, parameters.Q.ToBytes());
            }
        }

        internal bool IsValid(byte[] privateKey, byte[] publicKey)
        {
            byte[] testHash;
            using (var sha = SHA256.Create())
                testHash = sha.ComputeHash(new byte[0]);

            try
            {
                var signature = Sign(testHash, privateKey, publicKey);
                return Verify(testHash, signature, publicKey);
            }
            catch { return false; }
        }

        public byte[] Sign(byte[] hash, byte[] privateKey, byte[] publicKey)
        {
            var param = new ECParameters
            {
                D = privateKey,
                Q = publicKey.ToEcPoint(),
                Curve = ecCurve,
            };

            using (var dsa = ECDsa.Create(param))
                return dsa.SignHash(hash);
        }

        public bool Verify(byte[] hash, byte[] signature, byte[] publicKey)
        {
            var param = new ECParameters
            {
                Q = publicKey.ToEcPoint(),
                Curve = ecCurve,
            };

            using (var dsa = ECDsa.Create(param))
                return dsa.VerifyHash(hash, signature);
        }
    }
}