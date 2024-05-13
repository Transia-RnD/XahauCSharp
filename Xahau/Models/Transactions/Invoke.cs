//https://xrpl.org/invoke.html
namespace Xahau.Models.Transactions
{
    public class Invoke : TransactionCommon, IInvoke
    {
        public Invoke()
        {
            TransactionType = TransactionType.Invoke;
        }

        public string? Destination { get; set; }
        public string? Blob { get; set; }
    }

    public interface IInvoke : ITransactionCommon
    {
        string? Destination { get; set; }
        string? Blob { get; set; }
    }

    public class InvokeResponse : TransactionResponseCommon, IInvoke
    {
        public string? Destination { get; set; }
        public string? Blob { get; set; }
    }

}
