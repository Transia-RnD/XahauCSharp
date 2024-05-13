//https://xrpl.org/uritokenburn.html
namespace Xahau.Models.Transactions
{
    public class URITokenBurn : TransactionCommon, IURITokenBurn
    {
        public URITokenBurn()
        {
            TransactionType = TransactionType.URITokenBurn;
        }

        public string URITokenID { get; set; }
    }

    public interface IURITokenBurn : ITransactionCommon
    {
        string URITokenID { get; set; }
    }

    public class URITokenBurnResponse : TransactionResponseCommon, IURITokenBurn
    {
        public string URITokenID { get; set; }
    }

}
