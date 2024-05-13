//https://xrpl.org/uritokencancelselloffer.html
namespace Xahau.Models.Transactions
{
    public class URITokenCancelSellOffer : TransactionCommon, IURITokenCancelSellOffer
    {
        public URITokenCancelSellOffer()
        {
            TransactionType = TransactionType.URITokenCancelSellOffer;
        }

        public string URITokenID { get; set; }
    }

    public interface IURITokenCancelSellOffer : ITransactionCommon
    {
        string URITokenID { get; set; }
    }

    public class URITokenCancelSellOfferResponse : TransactionResponseCommon, IURITokenCancelSellOffer
    {
        public string URITokenID { get; set; }
    }

}
