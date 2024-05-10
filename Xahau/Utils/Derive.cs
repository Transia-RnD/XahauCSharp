﻿

// https://github.com/XRPLF/xrpl.js/blob/main/packages/xrpl/src/utils/derive.ts

//todo DO

using Xahau.AddressCodec;
using Xahau.Keypairs;

namespace Xahau.Utils
{
    public static class Derive
    {
        /// <summary>
        /// Derive an X-Address from a public key and a destination tag.<br/>
        /// Options - Public key and destination tag to encode as an X-Address.
        /// </summary>
        /// <param name="publicKey">The public key corresponding to an address.</param>
        /// <param name="tag">A destination tag to encode into an X-address. False indicates no destination tag.</param>
        /// <param name="test">Whether this address is for use in Testnet.</param>
        /// <returns>X-Address.</returns>
        /// <category>Utilities</category>
        public static string DeriveXAddress(string publicKey, int? tag, bool test)
        {
            var classicAddress = XahauKeypairs.DeriveAddress(publicKey);
            return XahauAddressCodec.ClassicAddressToXAddress(classicAddress, tag, test);
        }
    }
}

