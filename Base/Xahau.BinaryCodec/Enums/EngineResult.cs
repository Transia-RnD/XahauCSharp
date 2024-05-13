using Xahau.BinaryCodec.Enums;

//todo not found doc

namespace Xahau.BinaryCodec.Enums
{
    public class EngineResult : SerializedEnumItem<byte>
    {
        public class EngineResultValues : SerializedEnumeration<EngineResult, byte>{}
        public static EngineResultValues Values = new EngineResultValues();
        private readonly string _description;
        public EngineResult(string name, int ordinal, string description) : base(name, ordinal)
        {
            _description = description;
        }
        private static EngineResult Add(string name, int ordinal, string description)
        {
            return Values.AddEnum(new EngineResult(name, ordinal, description));
        }
        // ReSharper disable InconsistentNaming
        public static readonly EngineResult tecCLAIM = Add(nameof(tecCLAIM), -399, "Fee claimed. Sequence used. No action..");
        public static readonly EngineResult tecDIR_FULL = Add(nameof(tecDIR_FULL), -399, "Can not add entry to full directory..");
        public static readonly EngineResult tecFAILED_PROCESSING = Add(nameof(tecFAILED_PROCESSING), -399, "Failed to correctly process transaction..");
        public static readonly EngineResult tecINSUF_RESERVE_LINE = Add(nameof(tecINSUF_RESERVE_LINE), -399, "Insufficient reserve to add trust line..");
        public static readonly EngineResult tecINSUF_RESERVE_OFFER = Add(nameof(tecINSUF_RESERVE_OFFER), -399, "Insufficient reserve to create offer..");
        public static readonly EngineResult tecNO_DST = Add(nameof(tecNO_DST), -399, "Destination does not exist. Send XAH to create it..");
        public static readonly EngineResult tecNO_DST_INSUF_NATIVE = Add(nameof(tecNO_DST_INSUF_NATIVE), -399, "Destination does not exist. Too little XAH sent to create it..");
        public static readonly EngineResult tecNO_LINE_INSUF_RESERVE = Add(nameof(tecNO_LINE_INSUF_RESERVE), -399, "No such line. Too little reserve to create it..");
        public static readonly EngineResult tecNO_LINE_REDUNDANT = Add(nameof(tecNO_LINE_REDUNDANT), -399, "Can't set non-existent line to default..");
        public static readonly EngineResult tecPATH_DRY = Add(nameof(tecPATH_DRY), -399, "Path could not send partial amount..");
        public static readonly EngineResult tecPATH_PARTIAL = Add(nameof(tecPATH_PARTIAL), -399, "Path could not send full amount..");
        public static readonly EngineResult tecNO_ALTERNATIVE_KEY = Add(nameof(tecNO_ALTERNATIVE_KEY), -399, "The operation would remove the ability to sign transactions with the account..");
        public static readonly EngineResult tecNO_REGULAR_KEY = Add(nameof(tecNO_REGULAR_KEY), -399, "Regular key is not set..");
        public static readonly EngineResult tecOVERSIZE = Add(nameof(tecOVERSIZE), -399, "Object exceeded serialization limits..");
        public static readonly EngineResult tecUNFUNDED = Add(nameof(tecUNFUNDED), -399, "Not enough XAH to satisfy the reserve requirement..");
        public static readonly EngineResult tecUNFUNDED_ADD = Add(nameof(tecUNFUNDED_ADD), -399, "DEPRECATED..");
        public static readonly EngineResult tecUNFUNDED_OFFER = Add(nameof(tecUNFUNDED_OFFER), -399, "Insufficient balance to fund created offer..");
        public static readonly EngineResult tecUNFUNDED_PAYMENT = Add(nameof(tecUNFUNDED_PAYMENT), -399, "Insufficient balance to send..");
        public static readonly EngineResult tecOWNERS = Add(nameof(tecOWNERS), -399, "Non-zero owner count..");
        public static readonly EngineResult tecNO_ISSUER = Add(nameof(tecNO_ISSUER), -399, "Issuer account does not exist..");
        public static readonly EngineResult tecNO_AUTH = Add(nameof(tecNO_AUTH), -399, "Not authorized to hold asset..");
        public static readonly EngineResult tecNO_LINE = Add(nameof(tecNO_LINE), -399, "No such line..");
        public static readonly EngineResult tecINSUFF_FEE = Add(nameof(tecINSUFF_FEE), -399, "Insufficient balance to pay fee..");
        public static readonly EngineResult tecFROZEN = Add(nameof(tecFROZEN), -399, "Asset is frozen..");
        public static readonly EngineResult tecNO_TARGET = Add(nameof(tecNO_TARGET), -399, "Target account does not exist..");
        public static readonly EngineResult tecNO_PERMISSION = Add(nameof(tecNO_PERMISSION), -399, "No permission to perform requested operation..");
        public static readonly EngineResult tecNO_ENTRY = Add(nameof(tecNO_ENTRY), -399, "No matching entry found..");
        public static readonly EngineResult tecINSUFFICIENT_RESERVE = Add(nameof(tecINSUFFICIENT_RESERVE), -399, "Insufficient reserve to complete requested operation..");
        public static readonly EngineResult tecNEED_MASTER_KEY = Add(nameof(tecNEED_MASTER_KEY), -399, "The operation requires the use of the Master Key..");
        public static readonly EngineResult tecDST_TAG_NEEDED = Add(nameof(tecDST_TAG_NEEDED), -399, "A destination tag is required..");
        public static readonly EngineResult tecINTERNAL = Add(nameof(tecINTERNAL), -399, "An internal error has occurred during processing..");
        public static readonly EngineResult tecCRYPTOCONDITION_ERROR = Add(nameof(tecCRYPTOCONDITION_ERROR), -399, "Malformed.");
        public static readonly EngineResult tecINVARIANT_FAILED = Add(nameof(tecINVARIANT_FAILED), -399, "One or more invariants for the transaction were not satisfied..");
        public static readonly EngineResult tecEXPIRED = Add(nameof(tecEXPIRED), -399, "Expiration time is passed..");
        public static readonly EngineResult tecDUPLICATE = Add(nameof(tecDUPLICATE), -399, "Ledger object already exists..");
        public static readonly EngineResult tecKILLED = Add(nameof(tecKILLED), -399, "No funds transferred and no offer created..");
        public static readonly EngineResult tecHAS_OBLIGATIONS = Add(nameof(tecHAS_OBLIGATIONS), -399, "The account cannot be deleted since it has obligations..");
        public static readonly EngineResult tecTOO_SOON = Add(nameof(tecTOO_SOON), -399, "It is too early to attempt the requested operation. Please wait..");
        public static readonly EngineResult tecMAX_SEQUENCE_REACHED = Add(nameof(tecMAX_SEQUENCE_REACHED), -399, "The maximum sequence number was reached..");
        public static readonly EngineResult tecNO_SUITABLE_NFTOKEN_PAGE = Add(nameof(tecNO_SUITABLE_NFTOKEN_PAGE), -399, "A suitable NFToken page could not be located..");
        public static readonly EngineResult tecNFTOKEN_BUY_SELL_MISMATCH = Add(nameof(tecNFTOKEN_BUY_SELL_MISMATCH), -399, "The 'Buy' and 'Sell' NFToken offers are mismatched..");
        public static readonly EngineResult tecNFTOKEN_OFFER_TYPE_MISMATCH = Add(nameof(tecNFTOKEN_OFFER_TYPE_MISMATCH), -399, "The type of NFToken offer is incorrect..");
        public static readonly EngineResult tecCANT_ACCEPT_OWN_NFTOKEN_OFFER = Add(nameof(tecCANT_ACCEPT_OWN_NFTOKEN_OFFER), -399, "An NFToken offer cannot be claimed by its owner..");
        public static readonly EngineResult tecINSUFFICIENT_FUNDS = Add(nameof(tecINSUFFICIENT_FUNDS), -399, "Not enough funds available to complete requested transaction..");
        public static readonly EngineResult tecOBJECT_NOT_FOUND = Add(nameof(tecOBJECT_NOT_FOUND), -399, "A requested object could not be located..");
        public static readonly EngineResult tecINSUFFICIENT_PAYMENT = Add(nameof(tecINSUFFICIENT_PAYMENT), -399, "The payment is not sufficient..");
        public static readonly EngineResult tecHOOK_REJECTED = Add(nameof(tecHOOK_REJECTED), -399, "Rejected by hook on sending or receiving account..");
        public static readonly EngineResult tecREQUIRES_FLAG = Add(nameof(tecREQUIRES_FLAG), -399, "The transaction or part-thereof requires a flag that wasn't set..");
        public static readonly EngineResult tecPRECISION_LOSS = Add(nameof(tecPRECISION_LOSS), -399, "The amounts used by the transaction cannot interact..");
        public static readonly EngineResult tecINSUF_RESERVE_SELLER = Add(nameof(tecINSUF_RESERVE_SELLER), -399, "The seller of an object has insufficient reserves.");
        public static readonly EngineResult tefALREADY = Add(nameof(tefALREADY), -399, "The exact transaction was already in this ledger..");
        public static readonly EngineResult tefBAD_ADD_AUTH = Add(nameof(tefBAD_ADD_AUTH), -399, "Not authorized to add account..");
        public static readonly EngineResult tefBAD_AUTH = Add(nameof(tefBAD_AUTH), -399, "Transaction's public key is not authorized..");
        public static readonly EngineResult tefBAD_LEDGER = Add(nameof(tefBAD_LEDGER), -399, "Ledger in unexpected state..");
        public static readonly EngineResult tefBAD_QUORUM = Add(nameof(tefBAD_QUORUM), -399, "Signatures provided do not meet the quorum..");
        public static readonly EngineResult tefBAD_SIGNATURE = Add(nameof(tefBAD_SIGNATURE), -399, "A signature is provided for a non-signer..");
        public static readonly EngineResult tefCREATED = Add(nameof(tefCREATED), -399, "Can't add an already created account..");
        public static readonly EngineResult tefEXCEPTION = Add(nameof(tefEXCEPTION), -399, "Unexpected program state..");
        public static readonly EngineResult tefFAILURE = Add(nameof(tefFAILURE), -399, "Failed to apply..");
        public static readonly EngineResult tefINTERNAL = Add(nameof(tefINTERNAL), -399, "Internal error..");
        public static readonly EngineResult tefMASTER_DISABLED = Add(nameof(tefMASTER_DISABLED), -399, "Master key is disabled..");
        public static readonly EngineResult tefMAX_LEDGER = Add(nameof(tefMAX_LEDGER), -399, "Ledger sequence too high..");
        public static readonly EngineResult tefNO_AUTH_REQUIRED = Add(nameof(tefNO_AUTH_REQUIRED), -399, "Auth is not required..");
        public static readonly EngineResult tefNOT_MULTI_SIGNING = Add(nameof(tefNOT_MULTI_SIGNING), -399, "Account has no appropriate list of multi-signers..");
        public static readonly EngineResult tefPAST_SEQ = Add(nameof(tefPAST_SEQ), -399, "This sequence number has already passed..");
        public static readonly EngineResult tefPAST_IMPORT_SEQ = Add(nameof(tefPAST_IMPORT_SEQ), -399, "This import sequence number has already been used..");
        public static readonly EngineResult tefPAST_IMPORT_VL_SEQ = Add(nameof(tefPAST_IMPORT_VL_SEQ), -399, "This import validator list sequence number has already been used..");
        public static readonly EngineResult tefWRONG_PRIOR = Add(nameof(tefWRONG_PRIOR), -399, "This previous transaction does not match..");
        public static readonly EngineResult tefBAD_AUTH_MASTER = Add(nameof(tefBAD_AUTH_MASTER), -399, "Auth for unclaimed account needs correct master key..");
        public static readonly EngineResult tefINVARIANT_FAILED = Add(nameof(tefINVARIANT_FAILED), -399, "Fee claim violated invariants for the transaction..");
        public static readonly EngineResult tefTOO_BIG = Add(nameof(tefTOO_BIG), -399, "Transaction affects too many items..");
        public static readonly EngineResult tefNO_TICKET = Add(nameof(tefNO_TICKET), -399, "Ticket is not in ledger..");
        public static readonly EngineResult tefNFTOKEN_IS_NOT_TRANSFERABLE = Add(nameof(tefNFTOKEN_IS_NOT_TRANSFERABLE), -399, "The specified NFToken is not transferable..");
        public static readonly EngineResult tefNONDIR_EMIT = Add(nameof(tefNONDIR_EMIT), -399, "An emitted txn was injected into the ledger without a corresponding directory entry..");
        public static readonly EngineResult telLOCAL_ERROR = Add(nameof(telLOCAL_ERROR), -399, "Local failure..");
        public static readonly EngineResult telBAD_DOMAIN = Add(nameof(telBAD_DOMAIN), -399, "Domain too long..");
        public static readonly EngineResult telBAD_PATH_COUNT = Add(nameof(telBAD_PATH_COUNT), -399, "Malformed: Too many paths..");
        public static readonly EngineResult telBAD_PUBLIC_KEY = Add(nameof(telBAD_PUBLIC_KEY), -399, "Public key is not valid..");
        public static readonly EngineResult telFAILED_PROCESSING = Add(nameof(telFAILED_PROCESSING), -399, "Failed to correctly process transaction..");
        public static readonly EngineResult telINSUF_FEE_P = Add(nameof(telINSUF_FEE_P), -399, "Fee insufficient..");
        public static readonly EngineResult telNO_DST_PARTIAL = Add(nameof(telNO_DST_PARTIAL), -399, "Partial payment to create account not allowed..");
        public static readonly EngineResult telCAN_NOT_QUEUE = Add(nameof(telCAN_NOT_QUEUE), -399, "Can not queue at this time..");
        public static readonly EngineResult telCAN_NOT_QUEUE_BALANCE = Add(nameof(telCAN_NOT_QUEUE_BALANCE), -399, "Can not queue at this time: insufficient balance to pay all queued fees..");
        public static readonly EngineResult telCAN_NOT_QUEUE_BLOCKS = Add(nameof(telCAN_NOT_QUEUE_BLOCKS), -399, "Can not queue at this time: would block later queued transaction(s)..");
        public static readonly EngineResult telCAN_NOT_QUEUE_BLOCKED = Add(nameof(telCAN_NOT_QUEUE_BLOCKED), -399, "Can not queue at this time: blocking transaction in queue..");
        public static readonly EngineResult telCAN_NOT_QUEUE_FEE = Add(nameof(telCAN_NOT_QUEUE_FEE), -399, "Can not queue at this time: fee insufficient to replace queued transaction..");
        public static readonly EngineResult telCAN_NOT_QUEUE_FULL = Add(nameof(telCAN_NOT_QUEUE_FULL), -399, "Can not queue at this time: queue is full..");
        public static readonly EngineResult telWRONG_NETWORK = Add(nameof(telWRONG_NETWORK), -399, "Transaction specifies a Network ID that differs from that of the local node..");
        public static readonly EngineResult telREQUIRES_NETWORK_ID = Add(nameof(telREQUIRES_NETWORK_ID), -399, "Transactions submitted to this node/network must include a correct NetworkID field..");
        public static readonly EngineResult telNETWORK_ID_MAKES_TX_NON_CANONICAL = Add(nameof(telNETWORK_ID_MAKES_TX_NON_CANONICAL), -399, "Transactions submitted to this node/network must NOT include a NetworkID field..");
        public static readonly EngineResult telNON_LOCAL_EMITTED_TXN = Add(nameof(telNON_LOCAL_EMITTED_TXN), -399, "Emitted transaction cannot be applied because it was not generated locally..");
        public static readonly EngineResult telIMPORT_VL_KEY_NOT_RECOGNISED = Add(nameof(telIMPORT_VL_KEY_NOT_RECOGNISED), -399, "Import vl key was not recognized..");
        public static readonly EngineResult telCAN_NOT_QUEUE_IMPORT = Add(nameof(telCAN_NOT_QUEUE_IMPORT), -399, "Import transaction was not able to be directly applied and cannot be queued..");
        public static readonly EngineResult temMALFORMED = Add(nameof(temMALFORMED), -399, "Malformed transaction..");
        public static readonly EngineResult temBAD_AMOUNT = Add(nameof(temBAD_AMOUNT), -399, "Can only send positive amounts..");
        public static readonly EngineResult temBAD_CURRENCY = Add(nameof(temBAD_CURRENCY), -399, "Malformed: Bad currency..");
        public static readonly EngineResult temBAD_EXPIRATION = Add(nameof(temBAD_EXPIRATION), -399, "Malformed: Bad expiration..");
        public static readonly EngineResult temBAD_FEE = Add(nameof(temBAD_FEE), -399, "Invalid fee.");
        public static readonly EngineResult temBAD_ISSUER = Add(nameof(temBAD_ISSUER), -399, "Malformed: Bad issuer..");
        public static readonly EngineResult temBAD_LIMIT = Add(nameof(temBAD_LIMIT), -399, "Limits must be non-negative..");
        public static readonly EngineResult temBAD_OFFER = Add(nameof(temBAD_OFFER), -399, "Malformed: Bad offer..");
        public static readonly EngineResult temBAD_PATH = Add(nameof(temBAD_PATH), -399, "Malformed: Bad path..");
        public static readonly EngineResult temBAD_PATH_LOOP = Add(nameof(temBAD_PATH_LOOP), -399, "Malformed: Loop in path..");
        public static readonly EngineResult temBAD_QUORUM = Add(nameof(temBAD_QUORUM), -399, "Malformed: Quorum is unreachable..");
        public static readonly EngineResult temBAD_REGKEY = Add(nameof(temBAD_REGKEY), -399, "Malformed: Regular key cannot be same as master key..");
        public static readonly EngineResult temBAD_SEND_NATIVE_LIMIT = Add(nameof(temBAD_SEND_NATIVE_LIMIT), -399, "Malformed: Limit quality is not allowed for XAH to XAH..");
        public static readonly EngineResult temBAD_SEND_NATIVE_MAX = Add(nameof(temBAD_SEND_NATIVE_MAX), -399, "Malformed: Send max is not allowed for XAH to XAH..");
        public static readonly EngineResult temBAD_SEND_NATIVE_NO_DIRECT = Add(nameof(temBAD_SEND_NATIVE_NO_DIRECT), -399, "Malformed: No Ripple direct is not allowed for XAH to XAH..");
        public static readonly EngineResult temBAD_SEND_NATIVE_PARTIAL = Add(nameof(temBAD_SEND_NATIVE_PARTIAL), -399, "Malformed: Partial payment is not allowed for XAH to XAH..");
        public static readonly EngineResult temBAD_SEND_NATIVE_PATHS = Add(nameof(temBAD_SEND_NATIVE_PATHS), -399, "Malformed: Paths are not allowed for XAH to XAH..");
        public static readonly EngineResult temBAD_SEQUENCE = Add(nameof(temBAD_SEQUENCE), -399, "Malformed: Sequence is not in the past..");
        public static readonly EngineResult temBAD_SIGNATURE = Add(nameof(temBAD_SIGNATURE), -399, "Malformed: Bad signature..");
        public static readonly EngineResult temBAD_SIGNER = Add(nameof(temBAD_SIGNER), -399, "Malformed: No signer may duplicate account or other signers..");
        public static readonly EngineResult temBAD_SRC_ACCOUNT = Add(nameof(temBAD_SRC_ACCOUNT), -399, "Malformed: Bad source account..");
        public static readonly EngineResult temBAD_TRANSFER_RATE = Add(nameof(temBAD_TRANSFER_RATE), -399, "Malformed: Transfer rate must be >= 1.0 and <= 2.0.");
        public static readonly EngineResult temBAD_WEIGHT = Add(nameof(temBAD_WEIGHT), -399, "Malformed: Weight must be a positive value..");
        public static readonly EngineResult temDST_IS_SRC = Add(nameof(temDST_IS_SRC), -399, "Destination may not be source..");
        public static readonly EngineResult temDST_NEEDED = Add(nameof(temDST_NEEDED), -399, "Destination not specified..");
        public static readonly EngineResult temINVALID = Add(nameof(temINVALID), -399, "The transaction is ill-formed..");
        public static readonly EngineResult temINVALID_FLAG = Add(nameof(temINVALID_FLAG), -399, "The transaction has an invalid flag..");
        public static readonly EngineResult temREDUNDANT = Add(nameof(temREDUNDANT), -399, "The transaction is redundant..");
        public static readonly EngineResult temRIPPLE_EMPTY = Add(nameof(temRIPPLE_EMPTY), -399, "PathSet with no paths..");
        public static readonly EngineResult temUNCERTAIN = Add(nameof(temUNCERTAIN), -399, "In process of determining result. Never returned..");
        public static readonly EngineResult temUNKNOWN = Add(nameof(temUNKNOWN), -399, "The transaction requires logic that is not implemented yet..");
        public static readonly EngineResult temDISABLED = Add(nameof(temDISABLED), -399, "The transaction requires logic that is currently disabled..");
        public static readonly EngineResult temBAD_TICK_SIZE = Add(nameof(temBAD_TICK_SIZE), -399, "Malformed: Tick size out of range..");
        public static readonly EngineResult temINVALID_ACCOUNT_ID = Add(nameof(temINVALID_ACCOUNT_ID), -399, "Malformed: A field contains an invalid account ID..");
        public static readonly EngineResult temCANNOT_PREAUTH_SELF = Add(nameof(temCANNOT_PREAUTH_SELF), -399, "Malformed: An account may not preauthorize itself..");
        public static readonly EngineResult temINVALID_COUNT = Add(nameof(temINVALID_COUNT), -399, "Malformed: Count field outside valid range..");
        public static readonly EngineResult temSEQ_AND_TICKET = Add(nameof(temSEQ_AND_TICKET), -399, "Transaction contains a TicketSequence and a non-zero Sequence..");
        public static readonly EngineResult temBAD_NFTOKEN_TRANSFER_FEE = Add(nameof(temBAD_NFTOKEN_TRANSFER_FEE), -399, "Malformed: The NFToken transfer fee must be between 1 and 5000.");
        public static readonly EngineResult temHOOK_DATA_TOO_LARGE = Add(nameof(temHOOK_DATA_TOO_LARGE), -399, "Malformed: The hook CreateCode field is to large to be applied to the ledger..");
        public static readonly EngineResult terRETRY = Add(nameof(terRETRY), -399, "Retry transaction..");
        public static readonly EngineResult terFUNDS_SPENT = Add(nameof(terFUNDS_SPENT), -399, "DEPRECATED..");
        public static readonly EngineResult terINSUF_FEE_B = Add(nameof(terINSUF_FEE_B), -399, "Account balance can't pay fee..");
        public static readonly EngineResult terLAST = Add(nameof(terLAST), -399, "DEPRECATED..");
        public static readonly EngineResult terNO_RIPPLE = Add(nameof(terNO_RIPPLE), -399, "Path does not permit rippling..");
        public static readonly EngineResult terNO_ACCOUNT = Add(nameof(terNO_ACCOUNT), -399, "The source account does not exist..");
        public static readonly EngineResult terNO_AUTH = Add(nameof(terNO_AUTH), -399, "Not authorized to hold IOUs..");
        public static readonly EngineResult terNO_LINE = Add(nameof(terNO_LINE), -399, "No such line..");
        public static readonly EngineResult terPRE_SEQ = Add(nameof(terPRE_SEQ), -399, "Missing/inapplicable prior transaction..");
        public static readonly EngineResult terOWNERS = Add(nameof(terOWNERS), -399, "Non-zero owner count..");
        public static readonly EngineResult terQUEUED = Add(nameof(terQUEUED), -399, "Held until escalated fee drops..");
        public static readonly EngineResult terPRE_TICKET = Add(nameof(terPRE_TICKET), -399, "Ticket is not yet in ledger..");
        public static readonly EngineResult terNO_HOOK = Add(nameof(terNO_HOOK), -399, "No hook with that hash exists on the ledger..");
        public static readonly EngineResult tesSUCCESS = Add(nameof(tesSUCCESS), -399, "The transaction was applied. Only final in a validated ledger..");
        public static readonly EngineResult tesPARTIAL = Add(nameof(tesPARTIAL), -399, "The transaction was applied but should be submitted again until returning tesSUCCESS..");



        // ReSharper restore InconsistentNaming
        public bool ShouldClaimFee()
        {
            // tesSUCCESS and tecCLAIMED are >= 0, rest are < 0
            return Ordinal >= 0;
        }
    }
}
