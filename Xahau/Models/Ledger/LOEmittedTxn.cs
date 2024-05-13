using Newtonsoft.Json;

using System;
using Xahau.Client.Json.Converters;
using Xahau.Models.Common;
using Xahau.Models.Methods;

namespace Xahau.Models.Ledger
{
    public class LOEmittedTxn : BaseLedgerEntry
    {
        public LOEmittedTxn()
        {
            LedgerEntryType = LedgerEntryType.EmittedTxn;
        }

        public ulong OwnerNode { get; set; }
        public object Emittedtxn { get; set; }
    }
}