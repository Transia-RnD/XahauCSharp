﻿

// https://github.com/XRPLF/xrpl.js/blob/main/packages/xrpl/src/models/transactions/setRegularKey.ts

using System.Collections.Generic;
using System.Threading.Tasks;
using Xahau.Client.Exceptions;

namespace Xahau.Models.Transactions
{
    /// <inheritdoc cref="ISetRegularKey" />
    public class SetRegularKey : TransactionCommon, ISetRegularKey
    {
        public SetRegularKey()
        {
            TransactionType = TransactionType.SetRegularKey;
        }


        /// <inheritdoc />
        public string RegularKey { get; set; }
    }

    /// <summary>
    /// A SetRegularKey transaction assigns, changes, or removes the regular key  pair associated with an account.
    /// </summary>
    public interface ISetRegularKey : ITransactionCommon
    {
        /// <summary>
        /// A base-58-encoded Address that indicates the regular key pair to be assigned to the account.<br/>
        /// If omitted, removes any existing regular key pair from the account.<br/>
        /// Must not match the master key pair for the address.
        /// </summary>
        string RegularKey { get; set; }
    }

    /// <inheritdoc cref="ISetRegularKey" />
    public class SetRegularKeyResponse : TransactionResponseCommon, ISetRegularKey
    {
        /// <inheritdoc />
        public string RegularKey { get; set; }
    }

    public partial class Validation
    {
        /// <summary>
        /// Verify the form and type of a SetRegularKey at runtime.
        /// </summary>
        /// <param name="tx"> A SetRegularKey Transaction.</param>
        /// <exception cref="ValidationException">When the SetRegularKey is malformed.</exception>
        public static async Task ValidateSetRegularKey(Dictionary<string, dynamic> tx)
        {
            await Common.ValidateBaseTransaction(tx);

            if (tx.TryGetValue("RegularKey", out var RegularKey) && RegularKey is not string)
                throw new ValidationException("SetRegularKey: RegularKey must be a string");
        }
    }

}
