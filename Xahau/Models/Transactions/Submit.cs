﻿using Newtonsoft.Json;

using Xahau.Models.Methods;

//https://github.com/XRPLF/xrpl.js/blob/b20c05c3680d80344006d20c44b4ae1c3b0ffcac/packages/xrpl/src/models/methods/submit.ts#L28
namespace Xahau.Models.Transactions;

/// <summary>
/// Response expected from a  <see cref="SubmitRequest"/>.
/// </summary>
public class Submit //todo rename to SubmitResponse extends BaseResponse
{
    [JsonProperty("Accepted")]
    public bool Accepted { get; set; }

    [JsonProperty("applied")]
    public bool Applied { get; set; }

    [JsonProperty("broadcast")]
    public bool Broadcast { get; set; }

    [JsonProperty("open_ledger_cost")]
    public string OpenLedgerCost { get; set; }

    /// <summary>
    /// Text result code indicating the preliminary result of the transaction,  for example `tesSUCCESS`.
    /// </summary>
    [JsonProperty("engine_result")]
    public string EngineResult { get; set; }

    /// <summary>
    /// Numeric version of the result code.
    /// </summary>
    [JsonProperty("engine_result_code")]
    public int EngineResultCode { get; set; }

    /// <summary>
    /// Human-readable explanation of the transaction's preliminary result.
    /// </summary>
    [JsonProperty("engine_result_message")]
    public string EngineResultMessage { get; set; }

    /// <summary>
    /// The complete transaction in hex string format.
    /// </summary>
    [JsonProperty("tx_blob")]
    public string TxBlob { get; set; }

    /// <summary>
    /// Next account sequence number.
    /// </summary>
    [JsonProperty("account_sequence_next")]
    public uint? AccountSequenceNext { get; set; }

    /// <summary>
    /// Available account sequence number.
    /// </summary>
    [JsonProperty("account_sequence_available")]
    public uint? AccountSequenceAvailable { get; set; }

    /// <summary>
    /// The complete transaction in JSON format.
    /// </summary>
    [JsonProperty("tx_json")]
    public dynamic TxJson { get; set; }

    //[JsonIgnore]
    /// <summary>
    /// The complete transaction.
    /// </summary>
    public ITransactionResponseCommon Transaction => JsonConvert.DeserializeObject<TransactionResponseCommon>(TxJson.ToString());


    //todo not found fields accepted: boolean,  account_sequence_available: number, account_sequence_next: number, applied: boolean,  broadcast: boolean
    //kept: boolean,  queued: boolean, open_ledger_cost: string,  validated_ledger_index: number
}