
using System.Collections.Generic;
using static Xahau.Models.Common.Common;

namespace Xahau.Models.Ledger
{
    public class LOHook : BaseLedgerEntry
    {
        public LOHook()
        {
            LedgerEntryType = LedgerEntryType.Hook;
        }

        /// <summary>
        /// The identifying (classic) address of this <see href="https://xrpl.org/accounts.html">account</see>.
        /// </summary>
        public string? Account { get; set; }

        /// <summary>
        /// A bit-map of boolean flags enabled for this hook.
        /// </summary>
        public uint Flags { get; set; }

        public List<HookWrapper> Hooks { get; set; }

        public ulong OwnerNode { get; set; }

        /// <summary>
        /// The outbound quality set by the low account, as an integer in the implied ratio LowQualityOut:1,000,000,000. As a special case, the value 0 is equivalent to 1 billion, or face value.
        /// </summary>
        public string PreviousTxnId { get; set; }

        /// <summary>
        /// The <see href="https://xrpl.org/basic-data-types.html#ledger-index">index of the ledger</see> that contains the transaction that most recently modified this object.
        /// </summary>
        public uint PreviousTxnLgrSeq { get; set; }
    }
}