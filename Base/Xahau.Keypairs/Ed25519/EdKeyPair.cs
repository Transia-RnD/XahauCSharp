using System;

using static Xahau.AddressCodec.Utils;

using Sha512 = Xahau.Keypairs.Utils.Sha512;

//https://github.com/XRPLF/xrpl.js/blob/8a9a9bcc28ace65cde46eed5010eb8927374a736/packages/ripple-keypairs/src/index.ts#L69

namespace Xahau.Keypairs.Ed25519
{
    public class EdKeyPair : IXahauKeyPair
    {
        string prefix = "ED";
        private readonly byte[] _pubBytes;
        private readonly byte[] _privBytes;

        private EdKeyPair(byte[] publicKey, byte[] expandedPrivateKey)
        {
            _pubBytes = publicKey;
            _privBytes = expandedPrivateKey;
        }

        internal static IXahauKeyPair From128Seed(byte[] seed)
        {
            var edSecret = Sha512.Half(seed);
            byte[] publicKey;
            byte[] expandedPrivateKey;
            Chaos.NaCl.Ed25519.KeyPairFromSeed(out publicKey, out expandedPrivateKey, edSecret);
            return new EdKeyPair(publicKey, expandedPrivateKey);
        }

        public string Id() => prefix + _pubBytes.FromBytesToHex();

        public string Pk() => prefix + _privBytes[0..32].FromBytesToHex();

        public static byte[] Sign(byte[] message, byte[] privateKey) => Chaos.NaCl.Ed25519.Sign(message, privateKey);

        public static bool Verify(byte[] signature, byte[] message, byte[] publicKey) => Chaos.NaCl.Ed25519.Verify(signature, message, publicKey);
    }
}