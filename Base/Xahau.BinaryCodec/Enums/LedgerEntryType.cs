using Newtonsoft.Json.Linq;

//todo not found doc

namespace Xahau.BinaryCodec.Enums
{
    public class LedgerEntryType : SerializedEnumItem<ushort>
    {
        public class Enumeration : SerializedEnumeration<LedgerEntryType, ushort>{}
        public static Enumeration Values = new Enumeration();
        private LedgerEntryType(string name, int ordinal) : base(name, ordinal){}
        private static LedgerEntryType Add(string reference, int ordinal)
        {
            return Values.AddEnum(new LedgerEntryType(reference, ordinal));
        }
        public static readonly LedgerEntryType AccountRoot = Add(nameof(AccountRoot), 'a');
        public static readonly LedgerEntryType Amendments = Add(nameof(Amendments), 'f');
        public static readonly LedgerEntryType Any = Add(nameof(Any), -3);
        public static readonly LedgerEntryType Check = Add(nameof(Check), 'C');
        public static readonly LedgerEntryType Child = Add(nameof(Child), -2);
        public static readonly LedgerEntryType Contract = Add(nameof(Contract), 'c');
        public static readonly LedgerEntryType DepositPreauth = Add(nameof(DepositPreauth), 'p');
        public static readonly LedgerEntryType DirectoryNode = Add(nameof(DirectoryNode), 'd');
        public static readonly LedgerEntryType EmittedTxn = Add(nameof(EmittedTxn), 'E');
        public static readonly LedgerEntryType Escrow = Add(nameof(Escrow), 'u');
        public static readonly LedgerEntryType FeeSettings = Add(nameof(FeeSettings), 's');
        public static readonly LedgerEntryType GeneratorMap = Add(nameof(GeneratorMap), 'g');
        public static readonly LedgerEntryType Hook = Add(nameof(Hook), 'H');
        public static readonly LedgerEntryType HookDefinition = Add(nameof(HookDefinition), 'D');
        public static readonly LedgerEntryType HookState = Add(nameof(HookState), 'v');
        public static readonly LedgerEntryType ImportVLSequence = Add(nameof(ImportVLSequence), 'I');
        public static readonly LedgerEntryType Invalid = Add(nameof(Invalid), -1);
        public static readonly LedgerEntryType LedgerHashes = Add(nameof(LedgerHashes), 'h');
        public static readonly LedgerEntryType NFTokenOffer = Add(nameof(NFTokenOffer), '7');
        public static readonly LedgerEntryType NFTokenPage = Add(nameof(NFTokenPage), 'P');
        public static readonly LedgerEntryType NegativeUNL = Add(nameof(NegativeUNL), 'N');
        public static readonly LedgerEntryType Nickname = Add(nameof(Nickname), 'n');
        public static readonly LedgerEntryType Offer = Add(nameof(Offer), 'o');
        public static readonly LedgerEntryType PayChannel = Add(nameof(PayChannel), 'x');
        public static readonly LedgerEntryType RippleState = Add(nameof(RippleState), 'r');
        public static readonly LedgerEntryType SignerList = Add(nameof(SignerList), 'S');
        public static readonly LedgerEntryType Ticket = Add(nameof(Ticket), 'T');
        public static readonly LedgerEntryType UNLReport = Add(nameof(UNLReport), 'R');
        public static readonly LedgerEntryType URIToken = Add(nameof(URIToken), 'U');

        public static LedgerEntryType FromJson(JToken jToken)
        {
            return Values.FromJson(jToken);
        }
    }
}
