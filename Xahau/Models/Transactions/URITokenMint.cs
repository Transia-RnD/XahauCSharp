//https://xrpl.org/uritokencreateselloffer.html

using System;
using Newtonsoft.Json;

using Xahau.Client.Json.Converters;
using Xahau.Models.Common;

namespace Xahau.Models.Transactions
{
    [Flags]
    public enum URITokenMintFlags : uint
    {
        tfBurnable = 1,
    }

    public class URITokenMint : TransactionCommon, IURITokenMint
    {
        public URITokenMint()
        {
            TransactionType = TransactionType.URITokenMint;
        }

        public string URI { get; set; }

        public new URITokenMintFlags? Flags { get; set; }

        [JsonConverter(typeof(CurrencyConverter))]
        public Currency Amount { get; set; }
        
        public string? Destination { get; set; }
        
        public string? Digest { get; set; }
    }

    public interface IURITokenMint : ITransactionCommon
    {
        string URI { get; set; }

        new URITokenMintFlags? Flags { get; set; }
        
        Currency Amount { get; set; }
        
        string? Destination { get; set; }
        
        string? Digest { get; set; }
    }

    public class URITokenMintResponse : TransactionResponseCommon, IURITokenMint
    {
        public string URI { get; set; }

        public new URITokenMintFlags? Flags { get; set; }

        [JsonConverter(typeof(CurrencyConverter))]
        public Currency Amount { get; set; }
        
        public string? Destination { get; set; }
        
        public string? Digest { get; set; }
    }

}
