﻿using Xahau.BinaryCodec.Hashing;
using Xahau.BinaryCodec.Util;

namespace Xahau.Wallet
{
    internal static class Extensions
    {
        internal static byte[] Bytes(this HashPrefix hp)
        {
            return Bits.GetBytes((uint)hp);
        }
    }
}