using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using Xahau.Client.Json.Converters;
using Xahau.Models.Common;
using Xahau.Models.Methods;
using static Xahau.Models.Common.Common;

namespace Xahau.Models.Ledger
{
    public class LOHookDefinition : BaseLedgerEntry
    {
        public LOHookDefinition()
        {
            LedgerEntryType = LedgerEntryType.HookDefinition;
        }
        public string HookHash { get; set; }
        public string HookOn { get; set; }
        public string HookNamespace { get; set; }
        public List<HookParameter> HookParameters { get; set; }
        public uint HookApiVersion { get; set; }
        public string CreateCode { get; set; }
        public string HookSetTxnID { get; set; }
        public uint ReferenceCount { get; set; }
        public string Fee { get; set; }
        public uint? HookCallbackFee { get; set; }
    }
}