﻿using Xahau.BinaryCodec.Enums;

namespace Xahau.BinaryCodec.Types
{
    public class TransactionType : SerializedEnumItem<ushort>
    {
        public class Enumeration : SerializedEnumeration<TransactionType, ushort> { }
        public static Enumeration Values = new Enumeration();
        private TransactionType(string reference, int ordinal) : base(reference, ordinal) { }

        private static TransactionType Add(string name, int ordinal)
        {
            return Values.AddEnum(new TransactionType(name, ordinal));
        }

        // https://github.com/XRPLF/rippled/blob/develop/src/ripple/protocol/TxFormats.h

        public static readonly TransactionType AccountDelete = Add(nameof(AccountDelete), 21);
        public static readonly TransactionType AccountSet = Add(nameof(AccountSet), 3);
        public static readonly TransactionType CheckCancel = Add(nameof(CheckCancel), 18);
        public static readonly TransactionType CheckCash = Add(nameof(CheckCash), 17);
        public static readonly TransactionType CheckCreate = Add(nameof(CheckCreate), 16);
        public static readonly TransactionType ClaimReward = Add(nameof(ClaimReward), 98);
        public static readonly TransactionType Contract = Add(nameof(Contract), 9);
        public static readonly TransactionType DepositPreauth = Add(nameof(DepositPreauth), 19);
        public static readonly TransactionType EmitFailure = Add(nameof(EmitFailure), 103);
        public static readonly TransactionType EnableAmendment = Add(nameof(EnableAmendment), 100);
        public static readonly TransactionType EscrowCancel = Add(nameof(EscrowCancel), 4);
        public static readonly TransactionType EscrowCreate = Add(nameof(EscrowCreate), 1);
        public static readonly TransactionType EscrowFinish = Add(nameof(EscrowFinish), 2);
        public static readonly TransactionType GenesisMint = Add(nameof(GenesisMint), 96);
        public static readonly TransactionType Import = Add(nameof(Import), 97);
        public static readonly TransactionType Invalid = Add(nameof(Invalid), -1);
        public static readonly TransactionType Invoke = Add(nameof(Invoke), 99);
        public static readonly TransactionType NFTokenAcceptOffer = Add(nameof(NFTokenAcceptOffer), 29);
        public static readonly TransactionType NFTokenBurn = Add(nameof(NFTokenBurn), 26);
        public static readonly TransactionType NFTokenCancelOffer = Add(nameof(NFTokenCancelOffer), 28);
        public static readonly TransactionType NFTokenCreateOffer = Add(nameof(NFTokenCreateOffer), 27);
        public static readonly TransactionType NFTokenMint = Add(nameof(NFTokenMint), 25);
        public static readonly TransactionType NickNameSet = Add(nameof(NickNameSet), 6);
        public static readonly TransactionType OfferCancel = Add(nameof(OfferCancel), 8);
        public static readonly TransactionType OfferCreate = Add(nameof(OfferCreate), 7);
        public static readonly TransactionType Payment = Add(nameof(Payment), 0);
        public static readonly TransactionType PaymentChannelClaim = Add(nameof(PaymentChannelClaim), 15);
        public static readonly TransactionType PaymentChannelCreate = Add(nameof(PaymentChannelCreate), 13);
        public static readonly TransactionType PaymentChannelFund = Add(nameof(PaymentChannelFund), 14);
        public static readonly TransactionType Remit = Add(nameof(Remit), 95);
        public static readonly TransactionType SetFee = Add(nameof(SetFee), 101);
        public static readonly TransactionType SetHook = Add(nameof(SetHook), 22);
        public static readonly TransactionType SetRegularKey = Add(nameof(SetRegularKey), 5);
        public static readonly TransactionType SignerListSet = Add(nameof(SignerListSet), 12);
        public static readonly TransactionType TicketCancel = Add(nameof(TicketCancel), 11);
        public static readonly TransactionType TicketCreate = Add(nameof(TicketCreate), 10);
        public static readonly TransactionType TrustSet = Add(nameof(TrustSet), 20);
        public static readonly TransactionType UNLModify = Add(nameof(UNLModify), 102);
        public static readonly TransactionType UNLReport = Add(nameof(UNLReport), 104);
        public static readonly TransactionType URITokenBurn = Add(nameof(URITokenBurn), 46);
        public static readonly TransactionType URITokenBuy = Add(nameof(URITokenBuy), 47);
        public static readonly TransactionType URITokenCancelSellOffer = Add(nameof(URITokenCancelSellOffer), 49);
        public static readonly TransactionType URITokenCreateSellOffer = Add(nameof(URITokenCreateSellOffer), 48);
        public static readonly TransactionType URITokenMint = Add(nameof(URITokenMint), 45);
    }
}