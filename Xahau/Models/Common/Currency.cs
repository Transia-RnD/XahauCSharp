﻿using Newtonsoft.Json;

using System;
using System.Globalization;
using System.Text.RegularExpressions;

using Xahau.Client.Extensions;

//https://xrpl.org/currency-formats.html#currency-formats
//https://github.com/XRPLF/xrpl.js/blob/main/packages/xrpl/src/models/common/index.ts

namespace Xahau.Models.Common;

/// <summary>
/// The XRP Ledger has two kinds of digital asset: XRP and tokens.<br/>
/// Both types have high precision, although their formats are different
/// </summary>
public class Currency
{
    /// <summary>
    /// base constructor.<br/>
    /// base currency code = XRP
    /// </summary>
    public Currency() { CurrencyCode = "XAH"; }

    /// <summary>
    /// The standard format for currency codes is a three-character string such as USD.<br/>
    /// This is intended for use with ISO 4217 Currency Codes <br/>
    /// As a 160-bit hexadecimal string, such as "0158415500000000C1F76FF6ECB0BAC600000000".<br/>
    /// The following characters are permitted:<br/>
    /// all uppercase and lowercase letters, digits, as well as the symbols ? ! @ # $ % ^ * ( ) { } [ ] | and symbols ampersand, less, greater<br/>
    /// Currency codes are case-sensitive.
    /// </summary>
    [JsonProperty(propertyName: "currency")]
    public string CurrencyCode { get; set; }

    /// <summary>
    /// Quoted decimal representation of the amount of the token.<br/>
    /// This can include scientific notation, such as 1.23e11 meaning 123,000,000,000.<br/>
    /// Both e and E may be used.<br/>
    /// This can be negative when displaying balances, but negative values are disallowed in other contexts such as specifying how much to send.
    /// </summary>
    [JsonProperty(propertyName: "value")]
    public string Value { get; set; }

    /// <summary>
    /// Generally, the account that issues this token.<br/>
    /// In special cases, this can refer to the account that holds the token instead.
    /// </summary>
    [JsonProperty(propertyName: "issuer")]
    public string Issuer { get; set; }

    /// <summary>
    /// Readable currency name 
    /// </summary>
    [JsonIgnore]
    public string CurrencyValidName => CurrencyCode is { Length: > 0, } row
        ? row.Length > 3
            ? IsHexCurrencyCode(row)
                ? row.StartsWith(value: "03") ? $"LP {row[2..6]}.." 
                    : row.FromHexString().Trim(trimChar: '\0')
                : row
            : row
        : string.Empty;

    /// <summary>
    /// decimal currency amount (drops for XRP)
    /// </summary>
    [JsonIgnore]
    public decimal ValueAsNumber
    {
        get
        {
            try
            {
                return string.IsNullOrWhiteSpace(Value)
                    ? 0
                    : decimal.Parse(
                        Value,
                        NumberStyles.AllowLeadingSign
                        | (NumberStyles.AllowLeadingSign & NumberStyles.AllowDecimalPoint)
                        | (NumberStyles.AllowLeadingSign & NumberStyles.AllowExponent)
                        | (NumberStyles.AllowLeadingSign & NumberStyles.AllowExponent & NumberStyles.AllowDecimalPoint)
                        | (NumberStyles.AllowExponent & NumberStyles.AllowDecimalPoint)
                        | NumberStyles.AllowExponent
                        | NumberStyles.AllowDecimalPoint,
                        CultureInfo.InvariantCulture);
            }
            catch (Exception e)
            {
                try
                {
                    var num = double.Parse(
                        Value,
                        (NumberStyles.Float & NumberStyles.AllowExponent) | NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint,
                        CultureInfo.InvariantCulture);
                    var valid = $"{num:#########e00}";
                    if (valid.Contains(value: "e-"))
                    {
                        return 0;
                    }

                    if (valid.Contains(value: '-'))
                    {
                        return decimal.MinValue;
                    }

                    return decimal.MaxValue;
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    throw;
                }
            }
        }
        set => Value = value.ToString(
            CurrencyCode == "XAH"
                ? "G0"
                : "G15",
            CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// XAH token amount (non drops value)
    /// </summary>
    [JsonIgnore]
    public decimal? ValueAsXrp
    {
        get
        {
            if (CurrencyCode != "XAH" || string.IsNullOrWhiteSpace(Value))
            {
                return null;
            }

            return ValueAsNumber / 1000000;
        }
        set
        {
            if (value.HasValue)
            {
                CurrencyCode = "XAH";
                var val = value.Value * 1000000;
                Value = val.ToString(format: "G0", CultureInfo.InvariantCulture);
            }
            else
            {
                Value = "0";
            }
        }
    }

    /// <summary>
    /// check currency code for HEX 
    /// </summary>
    /// <param name="code">currency code</param>
    /// <returns></returns>
    public static bool IsHexCurrencyCode(string code) { return Regex.IsMatch(code, pattern: @"[0-9a-fA-F]{40}", RegexOptions.IgnoreCase); }

    #region Overrides of Object

    public override string ToString()
    {
        return CurrencyValidName == "XAH" ? $"XAH: {ValueAsXrp:0.######}" : $"{CurrencyValidName}: {ValueAsNumber:0.###############}";
    }

    public override bool Equals(object o) { return o is Currency model && model.Issuer == Issuer && model.CurrencyCode == CurrencyCode; }

    public static bool operator ==(Currency c1, Currency c2) { return c1.Equals(c2); }

    public static bool operator !=(Currency c1, Currency c2) { return !c1.Equals(c2); }

    #endregion
}