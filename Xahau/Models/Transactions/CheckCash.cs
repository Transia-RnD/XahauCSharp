﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xahau.Client.Exceptions;
using Xahau.Client.Json.Converters;
using Xahau.Models.Common;


// https://github.com/XRPLF/xrpl.js/blob/main/packages/xrpl/src/models/transactions/checkCash.ts

namespace Xahau.Models.Transactions
{
    /// <inheritdoc cref="ICheckCash" />
    public class CheckCash : TransactionCommon, ICheckCash
    {
        public CheckCash()
        {
            TransactionType = TransactionType.CheckCash;
        }
        /// <inheritdoc />
        public string CheckID { get; set; }
        /// <inheritdoc />
        [JsonConverter(typeof(CurrencyConverter))]
        public Currency? Amount { get; set; }
        /// <inheritdoc />
        [JsonConverter(typeof(CurrencyConverter))]
        public Currency? DeliverMin { get; set; }
    }

    /// <summary>
    /// Attempts to redeem a Check object in the ledger to receive up to the amount  authorized by the corresponding CheckCreate transaction.<br/>
    /// Only the Destination  address of a Check can cash it with a CheckCash transaction.
    /// </summary>
    public interface ICheckCash : ITransactionCommon
    {

        /// <summary>
        /// The ID of the Check ledger object to cash as a 64-character hexadecimal string.
        /// </summary>
        string CheckID { get; set; }
        /// <summary>
        /// Redeem the Check for exactly this amount, if possible.<br/>
        /// The currency must match that of the SendMax of the corresponding CheckCreate transaction.<br/>
        /// You.<br/>
        /// must provide either this field or DeliverMin.
        /// </summary>
        [JsonConverter(typeof(CurrencyConverter))]
        Currency? Amount { get; set; }
        /// <summary>
        /// Redeem the Check for at least this amount and for as much as possible.<br/>
        /// The currency must match that of the SendMax of the corresponding CheckCreate.<br/>
        /// transaction.<br/>
        /// You must provide either this field or Amount.
        /// </summary>
        [JsonConverter(typeof(CurrencyConverter))]
        Currency? DeliverMin { get; set; }
    }

    /// <inheritdoc cref="ICheckCash" />
    public class CheckCashResponse : TransactionResponseCommon, ICheckCash
    {
        /// <inheritdoc />
        public string CheckID { get; set; }
        /// <inheritdoc />
        [JsonConverter(typeof(CurrencyConverter))]
        public Currency? Amount { get; set; }

        /// <inheritdoc />
        [JsonConverter(typeof(CurrencyConverter))]
        public Currency? DeliverMin { get; set; }  
    }

    public partial class Validation
    {
        /// <summary>
        /// Verify the form and type of a CheckCash at runtime.
        /// </summary>
        /// <param name="tx"> A CheckCash Transaction.</param>
        /// <exception cref="ValidationException">When the CheckCash is malformed.</exception>
        public static async Task ValidateCheckCash(Dictionary<string, dynamic> tx)
        {
            await Common.ValidateBaseTransaction(tx);
            tx.TryGetValue("Amount", out var Amount);
            tx.TryGetValue("DeliverMin", out var DeliverMin);

            if (Amount is null && DeliverMin is null)
                throw new ValidationException("CheckCash: must have either Amount or DeliverMin");

            if (Amount is not null && DeliverMin is not null)
                throw new ValidationException("CheckCash: cannot have both Amount and DeliverMin");
            if (Amount is not null && !Common.IsAmount(Amount))
                throw new ValidationException("CheckCash: invalid Amount");
            if (DeliverMin is not null && !Common.IsAmount(DeliverMin))
                throw new ValidationException("CheckCash: invalid DeliverMin");

            if (tx.TryGetValue("CheckID", out var CheckID) && CheckID is not string { })
                throw new ValidationException("CheckCash: invalid CheckID");
        }
    }

}
