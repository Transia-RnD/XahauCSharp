using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Numerics;
using System.Threading.Tasks;

using Xahau.AddressCodec;
using Xahau.BinaryCodec;
using Xahau.Client;
using Xahau.Client.Exceptions;
using Xahau.Models.Common;
using Xahau.Models.Ledger;
using Xahau.Models.Methods;
using Xahau.Utils;

using static Xahau.AddressCodec.XahauAddressCodec;

// https://github.com/XRPLF/xrpl.js/blob/main/packages/xrpl/src/sugar/autofill.ts

namespace Xahau.Sugar
{
    public class AddressNTag
    {
        public string ClassicAddress { get; set; }
        public int? Tag { get; set; }
    }

    public static class AutofillSugar
    {

        const int LEDGER_OFFSET = 20;


        /// <summary>
        /// Autofills fields in a transaction. This will set `Sequence`, `Fee`,
        /// `lastLedgerSequence` according to the current state of the server this Client
        /// is connected to. It also converts all X-Addresses to classic addresses and
        /// flags interfaces into numbers.
        /// </summary>
        /// <param name="client">A client.</param>
        /// <param name="transaction">A {@link Transaction} in JSON format</param>
        /// <param name="signersCount">The expected number of signers for this transaction. Only used for multisigned transactions.</param>
        // <returns>The autofilled transaction.</returns>
        public static async Task<Dictionary<string, dynamic>> Autofill(this IXahauClient client, Dictionary<string, dynamic> transaction, int? signersCount)
        {

            Dictionary<string, dynamic> tx = transaction;

            tx.SetValidAddresses();

            //Flags.SetTransactionFlagsToNumber(tx);
            List<Task> promises = new List<Task>();
            tx.TryGetValue("TransactionType", out var tt);
            if (!tx.ContainsKey("Sequence"))
            {
                promises.Add(client.SetNextValidSequenceNumber(tx));
            }
            if (!tx.ContainsKey("LastLedgerSequence"))
            {
                promises.Add(client.SetLatestValidatedLedgerSequence(tx));
            }
            if (tt == "AccountDelete")
            {
                //todo error here
                //promises.Add(client.CheckAccountDeleteBlockers(tx));
            }
            await Task.WhenAll(promises);

            if (!tx.ContainsKey("Fee"))
            {
                await client.CalculateFeePerTransactionType(tx, signersCount ?? 0);
            }
            string jsonData = JsonConvert.SerializeObject(tx);
            return tx;
        }


        public static void SetValidAddresses(this Dictionary<string, dynamic> tx)
        {
            tx.ValidateAccountAddress("Account", "SourceTag");
            if (tx.ContainsKey("Destination"))
            {
                tx.ValidateAccountAddress("Destination", "DestinationTag");
            }

            // DepositPreauth:
            tx.ConvertToClassicAddress("Authorize");
            tx.ConvertToClassicAddress("Unauthorize");
            // EscrowCancel, EscrowFinish:
            tx.ConvertToClassicAddress("Owner");
            // SetRegularKey:
            tx.ConvertToClassicAddress("RegularKey");
        }

        public static void ValidateAccountAddress(this Dictionary<string, dynamic> tx, string accountField, string tagField)
        {
            // if X-address is given, convert it to classic address
            tx.TryGetValue(accountField, out var aField);
            
            AddressNTag classicAccount = GetClassicAccountAndTag((string)aField, null);
            tx[accountField] = classicAccount.ClassicAddress;

            tx.TryGetValue(tagField, out var tField);

            // XRPL: Does bool or int. Smells.
            if (classicAccount.Tag != null)
            {
                if (tField != null && (int)tField != classicAccount.Tag)
                {
                    throw new ValidationException($"The { tagField }, if present, must match the tag of the { accountField} X - address");
                }
                // eslint-disable-next-line no-param-reassign -- param reassign is safe
                tx[tagField] = classicAccount.Tag;
            }
        }

        public static AddressNTag GetClassicAccountAndTag(this string account, int? expectedTag)
        {
            if (XahauAddressCodec.IsValidXAddress(account))
            {
                CodecAddress codecAddress = XahauAddressCodec.XAddressToClassicAddress(account);
                if (expectedTag != null && codecAddress.Tag != expectedTag)
                {
                    throw new ValidationException("address includes a tag that does not match the tag specified in the transaction");
                }
                return new AddressNTag { ClassicAddress = codecAddress.ClassicAddress, Tag = codecAddress.Tag };
            }
            return new AddressNTag { ClassicAddress = account, Tag = expectedTag };
        }

        public static void ConvertToClassicAddress(this Dictionary<string, dynamic> tx, string fieldName)
        {
            if (tx.ContainsKey(fieldName))
            {
                string account = (string)tx[fieldName];
                if (account is string)
                {
                    AddressNTag addressntag = account.GetClassicAccountAndTag(null);
                    tx[fieldName] = addressntag.ClassicAddress;
                }
            }
        }

        public static async Task SetNextValidSequenceNumber(this IXahauClient client, Dictionary<string, dynamic> tx)
        {
            LedgerIndex index = new LedgerIndex(LedgerIndexType.Current);
            AccountInfoRequest request = new AccountInfoRequest((string)tx["Account"]) { LedgerIndex = index };
            AccountInfo data = await client.AccountInfo(request);
            tx.TryAdd("Sequence", data.AccountData.Sequence);
        }

        public static async Task CalculateFeePerTransactionType(this IXahauClient client, Dictionary<string, dynamic> tx, int signersCount = 0)
        {
            tx["SigningPubKey"] = "";
            tx["Fee"] = "0";
            string txBlob = XahauBinaryCodec.Encode(tx);
            tx["Fee"] = await client.GetFeeEstimateXrp(txBlob, signersCount);
        }

        public static async Task SetLatestValidatedLedgerSequence(this IXahauClient client, Dictionary<string, dynamic> tx)
        {
            uint ledgerSequence = await client.GetLedgerIndex();
            tx.TryAdd("LastLedgerSequence", ledgerSequence + LEDGER_OFFSET);
        }

        public static async Task CheckAccountDeleteBlockers(this IXahauClient client, Dictionary<string, dynamic> tx)
        {
            LedgerIndex index = new LedgerIndex(LedgerIndexType.Validated);
            AccountObjectsRequest request = new AccountObjectsRequest((string)tx["Account"])
            {
                LedgerIndex = index,
                DeletionBlockersOnly = true,
            };
            AccountObjects response = await client.AccountObjects(request);
            TaskCompletionSource task = new TaskCompletionSource();
            if (response.AccountObjectList.Count > 0)
            {
                task.TrySetException(new XahauException($"Account {(string)tx["Account"]} cannot be deleted; there are Escrows, PayChannels, RippleStates, or Checks associated with the account."));
            }
            task.TrySetResult();
        }
    }
}