﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System;

using Xahau.Models.Transactions;

//https://xrpl.org/transaction-types.html

namespace Xahau.Client.Json.Converters
{
    /// <summary> Transaction json Converter </summary>
    public class TransactionConverter : JsonConverter
    {
        /// <summary>
        /// write <see cref="ITransactionResponseCommon"/>  to json object
        /// </summary>
        /// <param name="writer">writer</param>
        /// <param name="value"><see cref="ITransactionResponseCommon"/> value</param>
        /// <param name="serializer">json serializer</param>
        /// <exception cref="NotSupportedException">Cannot write this object type</exception>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// create <see cref="ITransactionResponseCommon"/> 
        /// </summary>
        /// <param name="jObject">json object LedgerEntity</param>
        /// <returns><see cref="ITransactionResponseCommon"/> </returns>
        public ITransactionResponseCommon Create(JObject jObject)
        {
            return jObject.Property("TransactionType")?.Value.ToString() switch
            {
                "AccountSet" => new AccountSetResponse(),
                "AccountDelete" => new AccountDeleteResponse(),
                "CheckCancel" => new CheckCancelResponse(),
                "CheckCash" => new CheckCashResponse(),
                "CheckCreate" => new CheckCancelResponse(),
                "DepositPreauth" => new DepositPreauthResponse(),
                "EscrowCancel" => new EscrowCancelResponse(),
                "EscrowCreate" => new EscrowCreateResponse(),
                "EscrowFinish" => new EscrowFinishResponse(),
                "NFTokenAcceptOffer" => new NFTokenAcceptOfferResponse(),
                "NFTokenCancelOffer" => new NFTokenCancelOfferResponse(),
                "NFTokenBurn" => new NFTokenBurnResponse(),
                "NFTokenCreateOffer" => new NFTokenCreateOfferResponse(),
                "NFTokenMint" => new NFTokenMintResponse(),
                "OfferCancel" => new OfferCancelResponse(),
                "OfferCreate" => new OfferCreateResponse(),
                "Payment" => new PaymentResponse(),
                "PaymentChannelClaim" => new PaymentChannelClaimResponse(),
                "PaymentChannelCreate" => new PaymentChannelCreateResponse(),
                "PaymentChannelFund" => new PaymentChannelFundResponse(),
                "SetRegularKey" => new SetRegularKeyResponse(),
                "SignerListSet" => new SignerListSetResponse(),
                "TicketCreate" => new TicketCreateResponse(),
                "TrustSet" => new TrustSetResponse(),
                "EnableAmendment" => new EnableAmendmentResponse(),
                "SetFee" => new SetFeeResponse(),
                "UNLModify" => new UNLModifyResponse(),
                "AMMBid" => new AMMBidResponse(),
                "AMMCreate" => new AMMCreateResponse(),
                "AMMDelete" => new AMMDeleteResponse(),
                "AMMDeposit" => new AMMDepositResponse(),
                "AMMVote" => new AMMVoteResponse(),
                "AMMWithdraw" => new AMMWithdrawResponse(),
                "Clawback" => new ClawBackResponse(),
                //_ => throw new Exception("Can't create transaction type" + transactionType)
                _ => SetUnknownType(jObject),
            };
        }

        static TransactionResponseCommon SetUnknownType(JObject jObject)
        {
            jObject.Property("TransactionType").Value = "Unknown";
            return new TransactionResponseCommon();
        }

        /// <summary> read  <see cref="ITransactionResponseCommon"/>   from json object </summary>
        /// <param name="reader">json reader</param>
        /// <param name="objectType">object type</param>
        /// <param name="existingValue">object value</param>
        /// <param name="serializer">json serializer</param>
        /// <returns><see cref="ITransactionResponseCommon"/> </returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jObject = JObject.Load(reader);
            
            ITransactionResponseCommon transactionCommon = Create(jObject);
            serializer.Populate(jObject.CreateReader(), transactionCommon);
            return transactionCommon;
        }

        /// <inheritdoc />
        public override bool CanConvert(Type objectType) => objectType == typeof(ITransactionResponseCommon);

        /// <inheritdoc />
        public override bool CanWrite => false;
    }
}