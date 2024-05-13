//https://xrpl.org/setHook.html
using System;
using System.Collections.Generic;
using static Xahau.Models.Common.Common;

namespace Xahau.Models.Transactions
{
    [Flags]
    public enum SetHookFlags : uint
    {
        hsfOverride = 0x00000001,
        hsfNSDelete = 0x0000002,
        hsfCollect = 0x00000004,
    }

    public class SetHook : TransactionCommon, ISetHook
    {
        public SetHook()
        {
            TransactionType = TransactionType.SetHook;
        }

        public List<HookWrapper> Hooks { get; set; }
    }

    public interface ISetHook : ITransactionCommon
    {
        List<HookWrapper> Hooks { get; set; }
    }

    public class SetHookResponse : TransactionResponseCommon, ISetHook
    {
        public List<HookWrapper> Hooks { get; set; }
    }

}
