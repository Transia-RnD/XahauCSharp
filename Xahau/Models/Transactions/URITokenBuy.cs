//https://xrpl.org/uritokenburn.html

using System;
using Newtonsoft.Json;

using Xahau.Client.Json.Converters;
using Xahau.Models.Common;

namespace Xahau.Models.Transactions
{
    public class URITokenBuy : TransactionCommon, IURITokenBuy
    {
        public URITokenBuy()
        {
            TransactionType = TransactionType.URITokenBuy;
        }

        public string URITokenID { get; set; }
        
        [JsonConverter(typeof(CurrencyConverter))]
        public Currency Amount { get; set; }
    }

    public interface IURITokenBuy : ITransactionCommon
    {
        string URITokenID { get; set; }
        Currency Amount { get; set; }
    }

    public class URITokenBuyResponse : TransactionResponseCommon, IURITokenBuy
    {
        public string URITokenID { get; set; }

        [JsonConverter(typeof(CurrencyConverter))]
        public Currency Amount { get; set; }
    }

}
