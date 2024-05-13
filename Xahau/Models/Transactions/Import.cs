//https://xrpl.org/import.html
namespace Xahau.Models.Transactions
{
    public class Import : TransactionCommon, IImport
    {
        public Import()
        {
            TransactionType = TransactionType.Import;
        }

        public string? Issuer { get; set; }
        public string? Blob { get; set; }
    }

    public interface IImport : ITransactionCommon
    {
        string? Issuer { get; set; }
        string? Blob { get; set; }
    }

    public class ImportResponse : TransactionResponseCommon, IImport
    {
        public string? Issuer { get; set; }
        public string? Blob { get; set; }
    }

}
