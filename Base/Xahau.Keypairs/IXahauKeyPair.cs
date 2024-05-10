using Xahau.Keypairs.Utils;

namespace Xahau.Keypairs
{

    public class rKeypair
    {
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
    }

    public interface IXahauKeyPair
    {
        string Id();
        string Pk();
    }

    public static class KeyPairExtensions
    {
        public static byte[] PubKeyHash(this IXahauKeyPair pair)
        {
            return HashUtils.PublicKeyHash(pair.Id().ToBytes());
        }
    }
}
