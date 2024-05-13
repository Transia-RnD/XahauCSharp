using Newtonsoft.Json;

using System;
using Xahau.Client.Json.Converters;
using Xahau.Models.Common;
using Xahau.Models.Methods;

namespace Xahau.Models.Ledger
{
    public class LOHookState : BaseLedgerEntry
    {
        public LOHookState()
        {
            LedgerEntryType = LedgerEntryType.HookState;
        }

        public ulong OwnerNode { get; set; }
        public string HookStateKey { get; set; }
        public string HookStateData { get; set; }
    }
}