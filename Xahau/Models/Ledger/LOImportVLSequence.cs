using Newtonsoft.Json;

using System;
using Xahau.Client.Json.Converters;
using Xahau.Models.Common;
using Xahau.Models.Methods;

namespace Xahau.Models.Ledger
{
    public class LOImportVLSequence : BaseLedgerEntry
    {
        public LOImportVLSequence()
        {
            LedgerEntryType = LedgerEntryType.ImportVLSequence;
        }

        public string PublicKey { get; set; }
        public ulong ImportSequence { get; set; }
    }
}