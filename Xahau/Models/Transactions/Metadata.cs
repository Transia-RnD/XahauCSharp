using Newtonsoft.Json;

using System.Collections.Generic;

using Xahau.Client.Json.Converters;

using Xahau.Models.Common;

//https://github.com/XRPLF/xrpl.js/blob/45963b70356f4609781a6396407e2211fd15bcf1/packages/xrpl/src/models/transactions/metadata.ts#L32
namespace Xahau.Models.Transactions
{
    //todo replace Meta in transactionCommon to this interfaces;

    public interface ICreatedNode
    {
        string LedgerEntryType { get; set; }
        string LedgerIndex { get; set; }
        Dictionary<string, object> NewFields { get; set; }
    }

    public interface IModifiedNode
    {
        string LedgerEntryType { get; set; }
        string LedgerIndex { get; set; }
        Dictionary<string, object> FinalFields { get; set; }
        Dictionary<string, object> PreviousFields { get; set; }
        string PreviousTxnID { get; set; }
        int PreviousTxnLgrSeq { get; set; }
    }

    public interface IDeletedNode
    {
        string LedgerEntryType { get; set; }
        string LedgerIndex { get; set; }
        Dictionary<string, object> FinalFields { get; set; }
    }

    public interface INode : ICreatedNode, IModifiedNode, IDeletedNode
    {
    }

    public interface IHookExecution
    {
        string HookAccount { get; set; }
        uint HookEmitCount { get; set; }
        uint HookExecutionIndex { get; set; }
        string HookHash { get; set; }
        string HookInstructionCount { get; set; }
        uint HookResult { get; set; }
        string HookReturnCode { get; set; }
        string HookReturnString { get; set; }
        uint HookStateChangeCount { get; set; }
        uint Flags { get; set; }
    }

    public interface IHookEmission
    {
        string EmittedTxnID { get; set; }
        string HookAccount { get; set; }
        string HookHash { get; set; }
        string EmitNonce { get; set; }
    }

    public interface TransactionMetadata
    {
        List<IHookExecution>? HookExecutions { get; set; }
        List<IHookEmission>? HookEmissions { get; set; }
        List<INode> AffectedNodes { get; set; }
        [JsonConverter(typeof(CurrencyConverter))]
        [JsonProperty("DeliveredAmount")]
        Currency DeliveredAmount { get; set; }
        [JsonConverter(typeof(CurrencyConverter))]
        [JsonProperty("delivered_amount")]
        Currency Delivered_amount { get; set; }
        int TransactionIndex { get; set; }
        string TransactionResult { get; set; }
    }
}
