﻿using Newtonsoft.Json.Linq;

using System.Collections.Generic;
using System.Diagnostics;
using Xahau.BinaryCodec.Binary;
using Xahau.BinaryCodec.Hashing;
using Xahau.BinaryCodec.Types;
using Xahau.BinaryCodec.Util;


// https://github.com/XRPLF/xrpl.js/blob/main/packages/ripple-binary-codec/src/index.ts

namespace Xahau.BinaryCodec
{
    public class XahauBinaryCodec
    {
        static uint PAYMENT_CHANNEL_CLAIM_PREFIX = 0x434C4D00u;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="binary"></param>
        /// <returns>JToken</returns>
        public static JToken Decode(string binary)
        {
            var stobject = StObject.FromHex(binary);
            return stobject.ToJson();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns>string</returns>
        public static string Encode(JToken token)
        {
            return SerializeJson(token);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <returns>string</returns>
        public static string Encode(object json)
        {
            JToken token = JToken.FromObject(json);
            return Encode(token);
        }

        /// <summary>
        /// Encode a transaction into binary format in preparation for signing. (Only encodes fields that are intended to be signed.)
        /// </summary>
        /// <param name="json"></param>
        /// <returns>string</returns>
        public static string EncodeForSigning(object json)
        {
            JToken token = JToken.FromObject(json);
            return SerializeJson(token, HashPrefix.TransactionSig.Bytes(), null, true);
        }

        /// <summary>
        /// Encode a `payment channel <a href="https://xrpl.org/payment-channels.html">here</a>`_ Claim to be signed.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>string</returns> The binary-encoded claim, ready to be signed.
        public static string EncodeForSigningClaim(object obj)
        {
            JToken json = JToken.FromObject(obj);

            byte[] prefix = Bits.GetBytes(PAYMENT_CHANNEL_CLAIM_PREFIX);
            byte[] channel = Hash256.FromHex((string)json["channel"]).Buffer;
            byte[] amount = Uint64.FromValue(int.Parse((string)json["amount"])).ToBytes();
            byte[] rv = new byte[prefix.Length + channel.Length + amount.Length];
            System.Buffer.BlockCopy(prefix, 0, rv, 0, prefix.Length);
            System.Buffer.BlockCopy(channel, 0, rv, prefix.Length, channel.Length);
            System.Buffer.BlockCopy(amount, 0, rv, prefix.Length + channel.Length, amount.Length);
            return rv.ToHex();
        }

        /// <summary>
        /// Encode a transaction into binary format in preparation for providing one signature towards a multi-signed transaction. (Only encodes fields that are intended to be signed.)
        /// </summary>
        /// <param name="json"></param>
        /// <param name="signingAccount"></param>
        /// <returns>string</returns>
        public static string EncodeForMulitSigning(object json, string signingAccount)
        {
            string accountID = new AccountId(signingAccount).ToHex();
            JToken token = JToken.FromObject(json);
            return SerializeJson(token, HashPrefix.TransactionMultiSig.Bytes(), accountID.FromHex(), true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <returns>string</returns>
        public static string SerializeJson(JToken json, byte[]? prefix = null, byte[]? suffix = null, bool signingOnly = false)
        {
            var list = new BytesList();
            if (prefix != null)
            {
                list.Put(prefix);
            }

            StObject so = StObject.FromJson(json, signingOnly);
            list.Put(so.ToBytes());

            if (suffix != null)
            {
                list.Put(suffix);
            }
            return list.BytesHex();
        }
    }
}