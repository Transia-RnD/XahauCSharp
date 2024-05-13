//https://xrpl.org/remit.html
using System.Collections.Generic;
using static Xahau.Models.Common.Common;

namespace Xahau.Models.Transactions
{
    public class Remit : TransactionCommon, IRemit
    {
        public Remit()
        {
            TransactionType = TransactionType.Remit;
        }

        public string Destination { get; set; }
        public uint? DestinationTag { get; set; }
        public List<AmountEntry>? Amounts { get; set; }
        public MintURIToken? MintURIToken { get; set; }
        public string? InvoiceID { get; set; }
        public string? Blob { get; set; }
        public string? Inform { get; set; }
    }

    public interface IRemit : ITransactionCommon
    {
        string Destination { get; set; }
        uint? DestinationTag { get; set; }
        List<AmountEntry>? Amounts { get; set; }
        MintURIToken? MintURIToken { get; set; }
        string? InvoiceID { get; set; }
        string? Blob { get; set; }
        string? Inform { get; set; }
    }

    public class RemitResponse : TransactionResponseCommon, IRemit
    {
        public string Destination { get; set; }
        public uint? DestinationTag { get; set; }
        public List<AmountEntry>? Amounts { get; set; }
        public MintURIToken? MintURIToken { get; set; }
        public string? InvoiceID { get; set; }
        public string? Blob { get; set; }
        public string? Inform { get; set; }
    }

}
