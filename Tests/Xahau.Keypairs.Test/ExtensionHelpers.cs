﻿using System;
using System.Globalization;
using System.Text;

namespace Xahau.Keypairs.Tests;

internal static class ExtensionHelpers
{
    internal static string ConvertStringToHex(this string input)
    {
        var bytes = Encoding.UTF8.GetBytes(input);
        return Convert.ToHexString(bytes);
    }

    internal static string ConvertHexToString(this string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return null;
        var buffer = new byte[input.Length / 2];
        for (var i = 0; i < input.Length; i += 2)
        {
            var hexadecimal = input.Substring(i, 2);
            buffer[i / 2] = byte.Parse(hexadecimal, NumberStyles.HexNumber);
        }
        return Encoding.UTF8.GetString(buffer);
    }
}