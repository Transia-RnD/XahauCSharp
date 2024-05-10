using Newtonsoft.Json.Linq;

using System;

using Xahau.AddressCodec;
using Xahau.BinaryCodec;
using Xahau.BinaryCodec.Ledger;
using Xahau.Keypairs;
using Xahau.Models.Subscriptions;
using Xahau.Models.Transactions;

//https://github.com/XRPLF/xrpl.js/blob/45963b70356f4609781a6396407e2211fd15bcf1/packages/xrpl/src/utils/index.ts

namespace Xahau.Utils
{
    public static class Utilities
    {
        public static bool IsValidSecret(string secret)
        {
            try
            {
                XahauKeypairs.DeriveKeypair(secret);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    
        public static string Encode(this TransactionCommon transactionOrLedgerEntry)
        {
            return XahauBinaryCodec.Encode(transactionOrLedgerEntry);
        }
    
        public static string EncodeForSigning(this TransactionCommon transaction)
        {
            return XahauBinaryCodec.EncodeForSigning(transaction);
        }
    
        public static string EncodeForSigningClaim(this PaymentChannelClaim paymentChannelClaim)
        {
            return XahauBinaryCodec.EncodeForSigningClaim(paymentChannelClaim);
        }
    
        public static string EncodeForMultiSigning(this TransactionCommon transaction, string signer)
        {
            return XahauBinaryCodec.EncodeForMulitSigning(transaction, signer);
        }
    
        public static JToken Decode(string hex)
        {
            return XahauBinaryCodec.Decode(hex);
        }
    
        public static bool IsValidAddress(string address)
        {
            return XahauAddressCodec.IsValidXAddress(address) || XahauCodec.IsValidClassicAddress(address);
        }
    
        public static bool HasNextPage(this BaseResponse response)
        {
            return response.Result.ContainsKey("marker");
        }
    }
}
