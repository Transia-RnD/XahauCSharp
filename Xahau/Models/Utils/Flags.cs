﻿using System;
using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json.Linq;

using Xahau.Client.Exceptions;
using Xahau.Models.Transactions;

// https://github.com/XRPLF/xrpl.js/blob/main/packages/xrpl/src/models/utils/flags.ts

namespace Xahau.Models.Utils
{
    public class Flags
    {
        /// <summary>
        /// Sets a transaction's flags to its numeric representation.
        /// </summary>
        /// <param name="tx"> A transaction to set its flags to its numeric representation.</param>
        public static void SetTransactionFlagsToNumber(Dictionary<string, dynamic> tx)
        {
            if (!tx.TryGetValue("Flags", out var Flags))
            {
                tx.Add("Flags", 0u);
                return;
            }
            switch (Flags)
            {
                case Enum:
                    tx["Flags"] = (uint)Flags;
                    return;
                case null:
                    tx["Flags"] = 0u;
                    return;
                case uint:
                    return;
                default:
                    tx["Flags"] = tx["TransactionType"] switch
                    {
                        "AccountSet" => ConvertAccountSetFlagsToNumber(Flags),
                        //TransactionType.AccountDelete => expr,
                        //TransactionType.CheckCancel => expr,
                        //TransactionType.CheckCash => expr,
                        //TransactionType.CheckCreate => expr,
                        //TransactionType.DepositPreauth => expr,
                        //TransactionType.EscrowCancel => expr,
                        //TransactionType.EscrowCreate => expr,
                        //TransactionType.EscrowFinish => expr,
                        //TransactionType.NFTokenAcceptOffer => expr,
                        //TransactionType.NFTokenBurn => expr,
                        //TransactionType.NFTokenCancelOffer => expr,
                        //TransactionType.NFTokenCreateOffer => expr,
                        //TransactionType.NFTokenMint => expr,
                        //TransactionType.OfferCancel => expr,
                        "OfferCreate" => ConvertOfferCreateFlagsToNumber(Flags),
                        "Payment" => ConvertPaymentTransactionFlagsToNumber(Flags),
                        "PaymentChannelClaim" => ConvertPaymentChannelClaimFlagsToNumber(Flags),
                        //TransactionType.PaymentChannelCreate => expr,
                        //TransactionType.PaymentChannelFund => expr,
                        //TransactionType.SetRegularKey => expr,
                        //TransactionType.SignerListSet => expr,
                        //TransactionType.TicketCreate => expr,
                        "TrustSet" => ConvertTrustSetFlagsToNumber(Flags),
                        "AMMDeposit" => ConvertAMMDepositFlagsToNumber(Flags),
                        "AMMWithdraw" => ConvertAMMWithdrawFlagsToNumber(Flags),
                        _ => 0
                    };
                    break;
            }
        }
        public static uint ConvertAMMDepositFlagsToNumber(dynamic flags)
        {
            if (flags is not Dictionary<string, dynamic> flag)
                return 0;
            return ReduceFlags(flag, Enum.GetValues<AMMDepositFlags>().ToDictionary(c => c.ToString(), c => (uint)c));
        }
        public static uint ConvertAMMWithdrawFlagsToNumber(dynamic flags)
        {
            if (flags is not Dictionary<string, dynamic> flag)
                return 0;
            return ReduceFlags(flag, Enum.GetValues<AMMWithdrawFlags>().ToDictionary(c => c.ToString(), c => (uint)c));
        }
        public static uint ConvertAccountSetFlagsToNumber(dynamic flags)
        {
            if (flags is not Dictionary<string, dynamic> flag)
                return 0;
            return ReduceFlags(flag, Enum.GetValues<AccountSetAsfFlags>().ToDictionary(c => c.ToString(), c => (uint)c));
        }

        public static uint ConvertOfferCreateFlagsToNumber(dynamic flags)
        {
            if (flags is not Dictionary<string, dynamic> flag)
                return 0;
            return ReduceFlags(flag, Enum.GetValues<OfferCreateFlags>().ToDictionary(c => c.ToString(), c => (uint)c) );
        }

        public static uint ConvertPaymentChannelClaimFlagsToNumber(dynamic flags)
        {
            if (flags is not Dictionary<string, dynamic> flag)
                return 0;
            return ReduceFlags(flag, Enum.GetValues<PaymentChannelClaimFlags>().ToDictionary(c => c.ToString(), c => (uint)c));
        }

        public static uint ConvertPaymentTransactionFlagsToNumber(dynamic flags)
        {
            if (flags is not Dictionary<string, dynamic> flag)
                return 0;
            return ReduceFlags(flag, Enum.GetValues<PaymentFlags>().ToDictionary(c => c.ToString(), c => (uint)c));
        }

        public static uint ConvertTrustSetFlagsToNumber(dynamic flags)
        {
            if (flags is not Dictionary<string, dynamic> flag)
                return 0;
            return ReduceFlags(flag, Enum.GetValues<TrustSetFlags>().ToDictionary(c => c.ToString(), c => (uint)c));
        }

        public static uint ReduceFlags(Dictionary<string, dynamic> flags, Dictionary<string, uint> flagEnum)
        {
            return flags.Select(p => p.Key).Aggregate(0u, (resultFlags, f) =>
            {
                if (!flagEnum.TryGetValue(f, out var e))
                {
                    throw new ValidationException($"flag {flags} doesn't exist in flagEnum: {flagEnum}");
                }

                flagEnum.TryGetValue(f, out uint flag);

                return flags[f] == true ? resultFlags | flag : resultFlags;
            });
        }
    }
}