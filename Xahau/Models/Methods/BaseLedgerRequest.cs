using System;
using Newtonsoft.Json;
using Xahau.Client.Json.Converters;
using Xahau.Models.Common;

namespace Xahau.Models.Methods
{
    public class BaseLedgerRequest : BaseRequest
    {
        public BaseLedgerRequest() { }

        public BaseLedgerRequest(Guid requestId) : base(requestId){ }
        /// <summary>
        /// 20-byte hex string for the ledger version to use. 
        /// </summary>
        [JsonProperty("ledger_hash")]
        public string LedgerHash { get; set; }
        /// <summary>
        /// The ledger index of the ledger to use, or a shortcut string to choose a ledger automatically.
        /// </summary>
        [JsonProperty("ledger_index")]
        [JsonConverter(typeof(LedgerIndexConverter))]
        public LedgerIndex LedgerIndex { get; set; }
    }
}
