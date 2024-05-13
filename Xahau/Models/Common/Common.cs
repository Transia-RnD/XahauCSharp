using System.Collections.Generic;
using Newtonsoft.Json;

// https://github.com/XRPLF/xrpl.js/blob/main/packages/xrpl/src/models/common/index.ts

namespace Xahau.Models.Common
{
    ///// <summary>
    ///// Order book currency
    ///// </summary>
    //public class Currency
    //{
    //    /// <summary>
    //    /// Currency code
    //    /// </summary>
    //    [JsonProperty("currency")]
    //    public string Currency { get; set; }
    //    /// <summary>
    //    /// Currency Issuer
    //    /// </summary>
    //    [JsonProperty("issuer")]
    //    public string Issuer { get; set; }
    //}

    /// <summary> common class </summary>
    public class Common
    {
        /// <summary> is XRP currency </summary>
        public class XRP
        {
            /// <summary> XRP currency code </summary>
            [JsonProperty("currency")]
            public string Currency = "XAH";
        }

        /// <summary> currency with issuer </summary>
        public class IssuedCurrency
        {
            /// <summary>
            /// currency code
            /// </summary>
            [JsonProperty("currency")]
            public string Currency { get; set; }

            /// <summary>
            /// currency issuer
            /// </summary>
            [JsonProperty("issuer")]
            public string Issuer { get; set; }
        }

        /// <summary> currency with amount and issuer </summary>
        public class IssuedCurrencyAmount : IssuedCurrency
        {
            /// <summary> currency value </summary>
            [JsonProperty("value")]
            public string Value { get; set; }
        }

        public class HookGrantWrapper
        {
            public HookGrant HookGrant { get; set; }
        }

        public class HookGrant
        {
            public string HookHash { get; set; }
            public string? Authorize { get; set; }
        }

        public class HookParameterWrapper
        {
            public HookParameter HookParameter { get; set; }
        }

        public class HookParameter
        {
            public string HookParameterName { get; set; }
            public string HookParameterValue { get; set; }
        }

        public class HookWrapper
        {
            public Hook Hook { get; set; }
        }

        public class Hook
        {
            public string? HookHash { get; set; }
            public string? CreateCode { get; set; }
            public uint? Flags { get; set; }
            public string? HookOn { get; set; }
            public string? HookNamespace { get; set; }
            public uint? HookApiVersion { get; set; }
            public List<HookParameterWrapper> HookParameters { get; set; }
            public List<HookGrantWrapper> HookGrants { get; set; }
        }

        public class EmitDetails
        {
            public uint EmitBurden { get; set; }
            public uint EmitGeneration { get; set; }
            public string EmitHookHash { get; set; }
            public string EmitParentTxnID { get; set; }
        }

        public class MintURIToken
        {
            public string URI { get; set; }
            public string? Digest { get; set; }
            public uint? Flags { get; set; }
        }
    }
}