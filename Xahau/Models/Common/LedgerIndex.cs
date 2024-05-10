using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Xahau.BinaryCodec.Types;
using Xahau.Models.Ledger;
using Xahau.Client.Extensions;

//https://xrpl.org/ledger-header.html#ledger-index
//https://github.com/XRPLF/xrpl.js/blob/76b73e16a97e1a371261b462ee1a24f1c01dbb0c/packages/xrpl/src/models/common/index.ts


namespace Xahau.Models.Common
{

    public class LedgerIndex
    {
        public LedgerIndex(uint index)
        {
            Index = index;
        }

        public LedgerIndex(LedgerIndexType ledgerIndexType)
        {
            LedgerIndexType = ledgerIndexType;
        }

        public uint? Index { get; set; }
        /// <summary>
        /// Index type<br/>
        /// validated<br/>
        /// closed<br/>
        /// current<br/>
        /// </summary>
        public LedgerIndexType LedgerIndexType { get; set; }
    }
}
