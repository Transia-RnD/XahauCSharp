using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using Xahau.Client.Json.Converters;
using Xahau.Models.Common;
using Xahau.Models.Methods;

namespace Xahau.Models.Ledger
{
    public class LOUNLReport : BaseLedgerEntry
    {
        public LOUNLReport()
        {
            LedgerEntryType = LedgerEntryType.UNLReport;
        }

        public List<ImportVLKey>? sfImportVLKeys { get; set; }
        public List<ActiveValidator>? sfActiveValidators { get; set; }
        public string sfPreviousTxnID { get; set; }
        public uint sfPreviousTxnLgrSeq { get; set; }

        public interface IImportVLKey
        {
            public string? Account { get; set; }
            public string PublicKey { get; set; }
        }
        public class ImportVLKey : IImportVLKey
        {
            public string? Account { get; set; }
            public string PublicKey { get; set; }
        }
        public interface IActiveValidator
        {
            public string? Account { get; set; }
            public string PublicKey { get; set; }
        }
        public class ActiveValidator : IActiveValidator
        {
            public string? Account { get; set; }
            public string PublicKey { get; set; }
        }
    }
}