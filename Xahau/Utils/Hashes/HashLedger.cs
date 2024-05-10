using Newtonsoft.Json.Linq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xahau.BinaryCodec;
using Xahau.BinaryCodec.Hashing;
using Xahau.BinaryCodec.ShaMapTree;
using Xahau.BinaryCodec.Util;
using Xahau.Client.Exceptions;
using Xahau.Models.Ledger;
using Xahau.Models.Transactions;
using Xahau.Utils.Hashes.ShaMap;

using static Xahau.AddressCodec.Utils;

// https://github.com/XRPLF/xrpl.js/blob/main/packages/xrpl/src/utils/hashes/hashLedger.ts

namespace Xahau.Utils.Hashes
{
    public interface HashLedgerHeaderOptions
    {
        public bool? ComputeTreeHashes { get; set; }

    }

    public class HashLedger
    {
        public static string HashSignedTx(string tx)
        {
            string txBlob = tx;
            Dictionary<string, dynamic> txObject = XahauBinaryCodec.Decode(tx).ToObject<Dictionary<string, dynamic>>();
            if (!txObject.ContainsKey("TxnSignature") && !txObject.ContainsKey("Signers"))
            {
                new ValidationException("The transaction must be signed to hash it.");
            }

            return B16.Encode(Sha512.Half(input: txBlob.FromHexToBytes(), prefix: (uint)Xahau.BinaryCodec.Hashing.HashPrefix.TransactionId));
        }

        public static string HashSignedTx(JToken tx)
        {
            string txBlob = XahauBinaryCodec.Encode(tx);
            Dictionary<string, dynamic> txObject = tx.ToObject<Dictionary<string, dynamic>>();
            if (!txObject.ContainsKey("TxnSignature") && !txObject.ContainsKey("Signers"))
            {
                new ValidationException("The transaction must be signed to hash it.");
            }

            return B16.Encode(Sha512.Half(input: txBlob.FromHexToBytes(), prefix: (uint)Xahau.BinaryCodec.Hashing.HashPrefix.TransactionId));
        }
    }
}

