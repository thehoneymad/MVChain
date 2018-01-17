namespace MVChain.Lib
{
    using System.Security.Cryptography;
    public class CryptoService
    {
        public static readonly ECCurve ecCurve = ECCurve.NamedCurves.nistP256;
        public (byte[] privateKey, byte[] publicKey) GenerateKeypair()
        {
            using (var ecdsa = ECDsa.Create(ecCurve))
            {
                var parameters = ecdsa.ExportParameters(includePrivateParameters: true);
                return (parameters.D, parameters.Q);
            }
        }
    }
}