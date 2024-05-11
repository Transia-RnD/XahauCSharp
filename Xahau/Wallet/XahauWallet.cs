using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System.Collections.Generic;

using Xahau.AddressCodec;
using Xahau.BinaryCodec;
using Xahau.Client.Exceptions;
using Xahau.Keypairs;
using Xahau.Models.Transactions;
using Xahau.Utils.Hashes;

// https://github.com/XRPLF/xrpl.js/blob/main/packages/xrpl/src/Wallet/index.ts

namespace Xahau.Wallet
{
    public class SignatureResult
    {
        public string TxBlob;
        public string Hash;

        public SignatureResult(string txBlob, string hash)
        {
            TxBlob = txBlob;
            Hash = hash;
        }
    }

    public class XahauWallet
    {

        public static string DEFAULT_ALGORITHM = "ed25519";

        public readonly string PublicKey;
        public readonly string PrivateKey;
        public readonly string ClassicAddress;
        public readonly string Seed;

        /// <summary>
        /// Creates a new Wallet.
        /// </summary>
        /// <param name="publicKey">The public key for the account.</param>
        /// <param name="privateKey">The private key used for signing transactions for the account.</param>
        /// <param name="masterAddress">Include if a Wallet uses a Regular Key Pair. It must be the master address of the account.</param>
        /// <param name="seed">The seed used to derive the account keys.</param>
        public XahauWallet(string publicKey, string privateKey, string? masterAddress = null, string? seed = null)
        {
            this.PublicKey = publicKey;
            this.PrivateKey = privateKey;
            this.ClassicAddress = masterAddress ?? XahauKeypairs.DeriveAddress(publicKey);
            this.Seed = seed;
        }

        /// <summary>
        /// Generates a new Wallet using a generated seed.
        /// </summary>
        /// <param name="algorithm">The digital signature algorithm to generate an address for.</param>
        /// <returns>A new Wallet derived from a generated seed.</returns>
        public static XahauWallet Generate(string algorithm = "ed25519")
        {
            string seed = XahauKeypairs.GenerateSeed(null, algorithm);
            return XahauWallet.FromSeed(seed, null, algorithm);
        }
        /// <summary>
        /// Derives a wallet from a seed.
        /// </summary>
        /// <param name="seed">A string used to generate a keypair (publicKey/privateKey) to derive a wallet.</param>
        /// <param name="algorithm">The digital signature algorithm to generate an address for.</param>
        /// <param name="masterAddress">Include if a Wallet uses a Regular Key Pair. It must be the master address of the account.</param>
        /// <returns>A Wallet derived from a seed.</returns>
        public static XahauWallet FromSeed(string seed, string? masterAddress = null, string? algorithm = null)
        {
            return XahauWallet.DeriveWallet(seed, masterAddress, algorithm);
        }
        /// <summary>
        /// An array of random numbers to generate a seed used to derive a wallet.
        /// </summary>
        /// <param name="algorithm">The digital signature algorithm to generate an address for.</param>
        /// <param name="masterAddress">Include if a Wallet uses a Regular Key Pair. It must be the master address of the account.</param>
        /// <returns>A Wallet derived from an entropy.</returns>
        public static XahauWallet FromEntropy(byte[] entropy, string? masterAddress = null, string? algorithm = null)
        {
            string falgorithm = algorithm ?? XahauWallet.DEFAULT_ALGORITHM;
            string seed = XahauKeypairs.GenerateSeed(entropy, falgorithm);
            return XahauWallet.DeriveWallet(seed, masterAddress, falgorithm);
        }

        /// <summary>
        /// Creates a Wallet from xumm numbers.
        /// </summary>
        /// <returns>A Wallet from xumm numbers.</returns>
        public static XahauWallet FromXummNumbers(string[] numbers)
        {
            byte[] entropy = XummExtension.EntropyFromXummNumbers(numbers);
            return FromEntropy(entropy);
        }



        /// <summary>
        /// Derive a Wallet from a seed.
        /// </summary>
        /// <param name="seed">The seed used to derive the wallet.</param>
        /// <param name="algorithm">The digital signature algorithm to generate an address for.</param>
        /// <param name="masterAddress">Include if a Wallet uses a Regular Key Pair. It must be the master address of the account.</param>
        /// <returns>A Wallet derived from the seed.</returns>
        private static XahauWallet DeriveWallet(string seed, string? masterAddress = null, string? algorithm = null)
        {
            IXahauKeyPair keypair = XahauKeypairs.DeriveKeypair(seed, algorithm);
            return new XahauWallet(keypair.Id(), keypair.Pk(), masterAddress, seed);
        }

        /// <summary>
        /// Signs a transaction offline.
        /// </summary>
        /// <param name="transaction">A transaction to be signed offline.</param>
        /// <param name="multisign">Specify true/false to use multisign or actual address (classic/x-address) to make multisign tx request.</param>
        /// <param name="signingFor"></param>
        /// <returns>A Wallet derived from the seed.</returns>
        public SignatureResult Sign(Dictionary<string, dynamic> transaction, bool multisign = false, string? signingFor = null)
        {
            string multisignAddress = "";
            //if (signingFor != null && signingFor.starts(with: "X"))
            //{
            //    multisignAddress = signingFor;
            //}
            //else if (multisign)
            //{
            //    multisignAddress = this.ClassicAddress;
            //}

            Dictionary<string, dynamic> tx = transaction;

            if (tx.ContainsKey("TxnSignature") || tx.ContainsKey("Signers"))
            {
                new ValidationException("txJSON must not contain `TxnSignature` or `Signers` properties");
            }

            JObject txToSignAndEncode = JToken.FromObject(transaction).ToObject<JObject>();
            txToSignAndEncode["SigningPubKey"] = multisignAddress != "" ? "" : this.PublicKey;

            string signature = ComputeSignature(txToSignAndEncode.ToObject<Dictionary<string, dynamic>>(), this.PrivateKey);
            txToSignAndEncode.Add("TxnSignature", signature);

            string serialized = XahauBinaryCodec.Encode(txToSignAndEncode);
            //this.checkTxSerialization(serialized, tx);
            return new SignatureResult(serialized, HashLedger.HashSignedTx(serialized));
        }
        /// <summary>
        /// Signs a transaction offline.
        /// </summary>
        /// <param name="tx">A transaction to be signed offline.</param>
        /// <param name="multisign">Specify true/false to use multisign or actual address (classic/x-address) to make multisign tx request.</param>
        /// <param name="signingFor"></param>
        /// <returns>A Wallet derived from the seed.</returns>
        public SignatureResult Sign(ITransactionCommon tx, bool multisign = false, string? signingFor = null)
        {
            Dictionary<string, dynamic> txJson = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(tx.ToJson());
            return Sign(txJson, multisign, signingFor);
        }

        /// <summary>
        /// Verifies a signed transaction offline.
        /// </summary>
        /// <param name="signedTransaction">A signed transaction (hex string of signTransaction result) to be verified offline.</param>
        /// <returns>Returns true if a signedTransaction is valid.</returns>
        public bool VerifyTransaction(string signedTransaction)
        {
            JToken tx = XahauBinaryCodec.Decode(signedTransaction);
            string messageHex = XahauBinaryCodec.EncodeForSigning(tx.ToObject<Dictionary<string, dynamic>>());
            string signature = (string)tx["TxnSignature"];
            return XahauKeypairs.Verify(messageHex.FromHex(), signature, this.PublicKey);
        }

        public string GetXAddress(uint tag, bool isTestnet = false)
        {
            return XahauAddressCodec.ClassicAddressToXAddress(this.ClassicAddress, tag, isTestnet);
        }

        public string ComputeSignature(Dictionary<string, dynamic> transaction, string privateKey, string? signAs = null)
        {
            string encoded = XahauBinaryCodec.EncodeForSigning(transaction);
            return XahauKeypairs.Sign(AddressCodec.Utils.FromHexToBytes(encoded), privateKey);
        }
        /// <summary>
        /// Creates a Wallet from xumm numbers.
        /// </summary>
        /// <returns>A Wallet from xumm numbers.</returns>
        public static XahauWallet FromXummNumbers(string[] numbers, string algorithm = "secp256k1")
        {
            byte[] entropy = XummExtension.EntropyFromXummNumbers(numbers);
            return FromEntropy(entropy,null, algorithm);
        }
    }
}