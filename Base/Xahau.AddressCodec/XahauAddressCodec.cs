using System;
//using Xahau.KeypairsLib;
using System.Diagnostics;
using System.Linq;

// https://github.com/XRPLF/xrpl.js/blob/main/packages/ripple-address-codec/src/index.ts

namespace Xahau.AddressCodec
{
    public class XahauAddressCodec
    {

        private static byte[] CopyOfRange(byte[] source, int from_, int to)
        {
            var range = new byte[to - from_];
            Array.Copy(source, from_, range, 0, range.Length);
            return range;
        }

        public class CodecAddress
        {
            public string ClassicAddress { get; set; }
            public uint? Tag { get; set; }
            public bool Test { get; set; }
        }

        public class CodecAccountID
        {
            public byte[] AccountID { get; set; }
            public uint? Tag { get; set; }
            public bool Test { get; set; }
        }

        private static uint MAX_32_BIT_UNSIGNED_INT = 4294967295;
        private static byte[] PREFIX_BYTES_MAIN = { 0x05, 0x44 };
        private static byte[] PREFIX_BYTES_TEST = { 0x04, 0x93 };

        private static readonly B58 B58;
        public const string Alphabet = "rpshnaf39wBUDNEGHJKLM4PQRST7VWXYZ2bcdeCg65jkm8oFqi1tuvAxyz";
        static XahauAddressCodec()
        {
            B58 = new B58(Alphabet);
        }

        /// <summary>
        /// Returns the X-Address representation of the data.
        /// </summary>
        /// <param classicAddress="string"></param>
        /// <param tag="int"></param>
        /// <param isTest="boolean"></param>
        /// <returns>The X-Address representation of the data.</returns>
        /// <throws>XRPLAddressCodecException: If the classic address does not have enough bytes
        /// or the tag is invalid.</throws>
        public static string ClassicAddressToXAddress(string classicAddress, uint? tag, bool isTest)
        {
            byte[] accountId = XahauCodec.DecodeAccountID(accountId: classicAddress);
            return EncodeXAddress(accountId, tag, isTest);
        }
        /// <summary>
        /// Encode account ID, tag, and network ID to X-address
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="tag"></param>
        /// <param name="isTest"></param>
        /// <returns></returns>
        /// <exception cref="AddressCodecException">Account ID must be 20 bytes</exception>
        public static string EncodeXAddress(byte[] accountId, uint? tag, bool isTest)
        {
            if (accountId.Length != 20)
            {
                // RIPEMD160 is 160 bits = 20 bytes
                throw new AddressCodecException("Account ID must be 20 bytes");
            }
            if (tag > MAX_32_BIT_UNSIGNED_INT)
            {
                throw new AddressCodecException("Invalid tag");
            }
            uint theTag = tag ?? 0;
            int flags = tag == null ? 0x00 : 0x01;
            byte[] prefix = isTest ? PREFIX_BYTES_TEST : PREFIX_BYTES_MAIN;
            byte[] postbytes = {
                (byte)flags,
                (byte)(theTag & 0xff),
                (byte)((theTag >> 8) & 0xff),
                (byte)((theTag >> 16) & 0xff),
                (byte)((theTag >> 24) & 0xff),
                0,
                0,
                0,
                0
            };
            byte[] bytes = Utils.Combine(prefix, accountId, postbytes);
            byte[] checkSum = CopyOfRange(HashUtils.DoubleDigest(bytes), 0, 4);
            byte[] fbytes = Utils.Combine(bytes, checkSum);
            return B58.EncodeToString(fbytes);
        }

        /// <summary>
        /// Returns a tuple containing the classic address, tag, and whether the address
        /// is on a test network for an X-Address.
        /// </summary>
        /// <param xAddress="string"></param>
        /// <returns>A dict containing: classicAddress: the base58 classic address, tag: the destination tag, isTest: whether the address is on the test network (or main)</returns>
        /// <throws>AddressCodecError: If the base decoded value is invalid or the base58 check is invalid</throws>
        public static CodecAddress XAddressToClassicAddress(string xAddress)
        {
            CodecAccountID account = DecodeXAddress(xAddress);
            string classicAddress = XahauCodec.EncodeAccountID(account.AccountID);
            return new CodecAddress { ClassicAddress = classicAddress, Tag = account.Tag, Test = account.Test };
        }
        /// <summary>
        /// Decode address
        /// </summary>
        /// <param name="xAddress"></param>
        /// <returns></returns>
        public static CodecAccountID DecodeXAddress(string xAddress)
        {
            byte[] decoded = B58.Decode(xAddress);
            bool isTest = IsTestAddress(decoded);
            byte[] accountId = CopyOfRange(decoded, 2, 22);
            uint? tag = TagFromBuffer(decoded);
            return new CodecAccountID { AccountID = accountId, Tag = tag, Test = isTest };
        }

        /// <summary>
        /// Returns whether a decoded X-Address is a test address.
        /// </summary>
        /// <param prefix="string">The first 2 bytes of an X-Address.</param>
        /// <returns>Whether a decoded X-Address is a test address.</returns>
        /// <throws>XRPLAddressCodecException: If the prefix is invalid.</throws>
        public static bool IsTestAddress(byte[] buf)
        {
            byte[] decodedPrefix = CopyOfRange(buf, 0, 2);
            if (PREFIX_BYTES_MAIN.ToHex() == decodedPrefix.ToHex())
            {
                return false;
            }
            if (PREFIX_BYTES_TEST.ToHex() == decodedPrefix.ToHex())
            {
                return true;
            }
            throw new AddressCodecException("Invalid X-address: bad prefix");
        }

        /// <summary>
        /// Returns the destination tag extracted from the suffix of the X-Address.
        /// </summary>
        /// <param buffer="bytes[]"></param>
        /// <returns>The destination tag extracted from the suffix of the X-Address.</returns>
        /// <throws>XRPLAddressCodecException: If the address is unsupported.</throws>
        public static uint? TagFromBuffer(byte[] buf)
        {
            var flag = buf[22];
            if (flag >= 2)
            {
                throw new AddressCodecException("Unsupported X-address");
            }
            if (flag == 1)
            {
                // Little-endian to big-endian
                return (uint?)(buf[23] + buf[24] * 0x100 + buf[25] * 0x10000 + buf[26] * 0x1000000);
            }
            if (flag != 0)
            {
                throw new AddressCodecException("flag must be zero to indicate no tag");
            }
            var remainingBytes = new byte[8];
            Array.Copy(buf, 23, remainingBytes, 0, 8);
            if (!Enumerable.SequenceEqual(remainingBytes, new byte[8]))
            {
                throw new AddressCodecException("remaining bytes must be zero");
            }
            return null;
        }

        /// <summary>
        /// Returns whether `xAddress` is a valid X-Address.
        /// </summary>
        /// <param xAddress="string">The X-Address to check for validity.</param>
        /// <returns>Whether `xAddress` is a valid X-Address.</returns>
        public static bool IsValidXAddress(string xAddress)
        {
            try
            {
                DecodeXAddress(xAddress);

            } catch
            {
                return false;
            }
            return true;
        }
    }
}