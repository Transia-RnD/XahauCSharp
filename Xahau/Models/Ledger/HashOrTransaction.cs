using Newtonsoft.Json;
using Xahau.Client.Json.Converters;
using Xahau.Models.Transactions;

namespace Xahau.Models.Ledger
{
    [JsonConverter(typeof(TransactionOrHashConverter))]
    public class HashOrTransaction
    {
        /// <summary>
        /// Unique hash of the transaction you are looking up
        /// </summary>
        public string TransactionHash { get; set; }
        /// <summary>
        /// server transaction response
        /// </summary>
        public TransactionResponseCommon Transaction { get; set; }
    }
}
