using Newtonsoft.Json;

using System;
using Xahau.Client.Json.Converters;
using Xahau.Models.Common;
using Xahau.Models.Methods;

namespace Xahau.Models.Ledger
{
    public class LOURIToken : BaseLedgerEntry
    {
        public LOURIToken()
        {
            LedgerEntryType = LedgerEntryType.URIToken;
        }

        public string Owner { get; set; }
        public ulong OwnerNode { get; set; }
        public string Issuer { get; set; }
        public string URI { get; set; }
        public string? Digest { get; set; }
        
        [JsonConverter(typeof(CurrencyConverter))]
        public Currency? Amount { get; set; }
        public string? Destination { get; set; }
        
        /// <summary>
        /// The outbound quality set by the low account, as an integer in the implied ratio LowQualityOut:1,000,000,000. As a special case, the value 0 is equivalent to 1 billion, or face value.
        /// </summary>
        public string PreviousTxnId { get; set; }

        /// <summary>
        /// The <see href="https://xrpl.org/basic-data-types.html#ledger-index">index of the ledger</see> that contains the transaction that most recently modified this object.
        /// </summary>
        public uint PreviousTxnLgrSeq { get; set; }
    }
}