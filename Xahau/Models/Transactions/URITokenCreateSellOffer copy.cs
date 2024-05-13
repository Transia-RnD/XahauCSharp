//https://xrpl.org/uritokencreateselloffer.html

using Newtonsoft.Json;

using Xahau.Client.Json.Converters;
using Xahau.Models.Common;

namespace Xahau.Models.Transactions
{
    public class URITokenCreateSellOffer : TransactionCommon, IURITokenCreateSellOffer
    {
        public URITokenCreateSellOffer()
        {
            TransactionType = TransactionType.URITokenCreateSellOffer;
        }

        public string URITokenID { get; set; }

        [JsonConverter(typeof(CurrencyConverter))]
        public Currency Amount { get; set; }
        
        public string? Destination { get; set; }
    }

    public interface IURITokenCreateSellOffer : ITransactionCommon
    {
        string URITokenID { get; set; }

        Currency Amount { get; set; }
        
        string? Destination { get; set; }
    }

    public class URITokenCreateSellOfferResponse : TransactionResponseCommon, IURITokenCreateSellOffer
    {
        public string URITokenID { get; set; }

        [JsonConverter(typeof(CurrencyConverter))]
        public Currency Amount { get; set; }
        
        public string? Destination { get; set; }
    }

}
