﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Ocsp;
using Xahau.Client.Exceptions;
using Xahau.Models.Ledger;
using Xahau.Models.Methods;
using Xahau.Models.Subscriptions;
using Xahau.Models.Transactions;
using Xahau.Sugar;
using Xahau.Wallet;
using static Xahau.Client.Connection;
using static Xahau.Client.XahauClient;

using BookOffers = Xahau.Models.Transactions.BookOffers;
using Submit = Xahau.Models.Transactions.Submit;

// https://github.com/XRPLF/xrpl.js/blob/main/packages/xrpl/src/client/index.ts

// https://xrpl.org/public-api-methods.html
namespace Xahau.Client
{

    public delegate Task OnError(string error, string errorMessage, string message, dynamic data);
    public delegate Task OnConnected();
    public delegate Task OnDisconnect(int? code);
    public delegate Task OnLedgerClosed(LedgerStream response);
    public delegate Task OnTransaction(TransactionStream response);
    public delegate Task OnManifestReceived(ValidationStream response);
    public delegate Task OnPeerStatusChange(PeerStatusStream response);
    public delegate Task OnConsensusPhase(ConsensusStream response);
    public delegate Task OnPathFind(PathFindStream response);


    public interface IXahauClient : IDisposable
    {
        Connection connection { get; set; }
        double feeCushion { get; set; }
        string maxFeeXRP { get; set; }
        uint? networkID { get; set; }
        string? buildVersion { get; set; }

        //event OnError OnError;
        //event OnConnected OnConnected;
        //event OnDisconnect OnDisconnect;
        //event OnLedgerClosed OnLedgerClosed;
        //event OnTransaction OnTransaction;
        //event OnManifestReceived OnManifestReceived;
        //event OnPeerStatusChange OnPeerStatusChange;
        //event OnConsensusPhase OnConsensusPhase;
        //event OnPathFind OnPathFind;

        #region Server
        /// <summary> the url </summary>
        string Url();
        /// <summary> connect to the server </summary>
        Task Connect();
        /// <summary> Disconnect from server </summary>
        Task Disconnect();
        /// <summary> if the websocket is connected </summary>
        bool IsConnected();
        /// <summary> The subscribe method requests periodic notifications from the server when certain events happen. </summary>
        /// <param name="request">An <see cref="SubscribeRequest"/> request.</param>
        /// <returns></returns>
        Task<object> Subscribe(SubscribeRequest request);
        /// <summary> The unsubscribe command tells the server to stop sending messages for a particular subscription or set of subscriptions.</summary>
        /// <param name="request">An <see cref="UnsubscribeRequest"/> request.</param>
        /// <returns></returns>
        Task<object> Unsubscribe(UnsubscribeRequest request);
        /// <summary>
        /// The ping command returns an acknowledgement,
        /// so that clients can test the connection status and latency
        /// </summary>
        /// <param name="request">An <see cref="PingRequest"/> request.</param>
        /// <returns></returns>
        Task<object> Ping(PingRequest request);
        /// <summary> The server_info command asks the server for a human-readable version of various information about the rippled server being queried. </summary>
        /// <param name="request">An <see cref="ServerInfoRequest"/> request.</param>
        /// <returns>A <see cref="ServerInfo"/> response.</returns>
        Task<ServerInfo> ServerInfo(ServerInfoRequest request);
        /// <summary> The server_state command asks the server for a human-readable version of various information about the rippled server being queried. </summary>
        /// <param name="request">An <see cref="ServerStateRequest"/> request.</param>
        /// <returns>A <see cref="ServerState"/> response.</returns>
        Task<ServerState> ServerState(ServerStateRequest request);
        /// <summary> The fee command reports the current state of the open-ledger requirements for the transaction cost. </summary>
        /// <param name="request">An <see cref="FeeRequest"/> request.</param>
        /// <returns>An <see cref="Models.Methods.Fee"/> response.</returns>
        Task<Fee> Fee(FeeRequest request);

        #endregion

        #region Account
        //https://xrpl.org/account-methods.html
        /// <summary> The account_info command retrieves information about an account, its activity, and its XRP balance. </summary>
        /// <param name="request">An <see cref="AccountInfoRequest"/> request.</param>
        /// <returns>An <see cref="Models.Methods.AccountInfo"/> response.</returns>
        Task<AccountInfo> AccountInfo(AccountInfoRequest request);


        /// <summary> The account_offers method retrieves a list of offers made by a given account that are outstanding as of a particular ledger version </summary>
        /// <param name="request">An <see cref="AccountOffersRequest"/> request.</param>
        /// <returns>An <see cref="Models.Methods.AccountOffers"/> response.</returns>
        Task<AccountOffers> AccountOffers(AccountOffersRequest request);

        /// <summary> The account_currencies command retrieves a list of currencies that an account can send or receive, based on its trust lines. </summary>
        /// <param name="request">An <see cref="AccountCurrenciesRequest"/> request.</param>
        /// <returns>An <see cref="Models.Methods.AccountCurrencies"/> response.</returns>
        Task<AccountCurrencies> AccountCurrencies(AccountCurrenciesRequest request);


        /// <summary>
        /// The account_lines method returns information about an account's trust lines, including balances in all non-XRP currencies and assets.
        /// </summary>
        /// <param name="request">An <see cref="AccountLinesRequest"/> request.</param>
        /// <returns>An <see cref="Models.Methods.AccountLines"/> response.</returns>
        Task<AccountLines> AccountLines(AccountLinesRequest request);


        /// <summary>
        /// The AccountObjects command returns the raw ledger format for all objects owned by an account. For a higher-level view of an account's trust lines and balances, see <see cref="Models.Methods.AccountLines"/> instead.
        /// </summary>
        /// <param name="request">An <see cref="AccountObjectsRequest"/> request.</param>
        /// <returns>An <see cref="Models.Methods.AccountObjects"/> response.</returns>
        Task<AccountObjects> AccountObjects(AccountObjectsRequest request);


        /// <summary>
        /// The noripple_check command provides a quick way to check the status of the Default Ripple field
        /// for an account and the No Ripple flag of its trust lines, compared with the recommended settings
        /// </summary>
        /// <returns>An <see cref="NoRippleCheckRequest"/> response.</returns>
        /// <returns>An <see cref="Models.Methods.NoRippleCheck"/> response.</returns>
        Task<NoRippleCheck> NoRippleCheck(NoRippleCheckRequest request);


        /// <summary> The gateway_balances command calculates the total balances issued by a given account,
        /// optionally excluding amounts held by operational addresses. </summary>
        /// <param name="request">An <see cref="GatewayBalancesRequest"/> request.</param>
        /// <returns>An <see cref="Models.Methods.GatewayBalances"/> response.</returns>
        Task<GatewayBalances> GatewayBalances(GatewayBalancesRequest request);


        /// <summary> The account_tx method retrieves a list of transactions that involved the specified account </summary>
        /// <param name="request">An <see cref="AccountTransactionsRequest"/> request.</param>
        /// <returns>An <see cref="Models.Methods.AccountTransactions"/> response.</returns>
        Task<AccountTransactions> AccountTransactions(AccountTransactionsRequest request);
        /// <summary> The account_channels method returns information about an account's Payment Channels.
        /// This includes only channels where the specified account is the channel's source, not the destination. </summary>
        /// <param name="request">An <see cref="AccountChannelsRequest"/> request.</param>
        /// <returns>An <see cref="Models.Methods.AccountChannels"/> response.</returns>
        Task<AccountChannels> AccountChannels(AccountChannelsRequest request);

        #endregion

        #region NFT


        /// <summary> The nft_buy_offers method returns a list of buy offers for a given NFToken object. </summary>
        /// <param name="request">An <see cref="NFTBuyOffersRequest"/> request.</param>
        /// <returns>An <see cref="Models.Methods.NFTBuyOffers"/> response.</returns>
        Task<NFTBuyOffers> NFTBuyOffers(NFTBuyOffersRequest request);

        /// <summary> The nft_sell_offers method returns a list of sell offers for a given NFToken object</summary>
        /// <param name="request">An <see cref="NFTSellOffersRequest"/> request.</param>
        /// <returns>An <see cref="Models.Methods.NFTSellOffers"/> response.</returns>
        Task<NFTSellOffers> NFTSellOffers(NFTSellOffersRequest request);


        /// <summary> The account_nfts method returns a list of NFToken objects for the specified account.</summary>
        /// <param name="request">An <see cref="AccountNFTsRequest"/> request.</param>
        /// <returns>An <see cref="Models.Methods.AccountNFTs"/> response.</returns>
        Task<AccountNFTs> AccountNFTs(AccountNFTsRequest request);


        #endregion

        #region Transactions
        ////https://xrpl.org/transaction-methods.html
        ///// <summary>
        ///// The submit method applies a transaction and sends it to the network to be confirmed and included in future ledgers.
        ///// </summary>
        ///// <param name="request">An <see cref="SubmitRequest"/> request.</param>
        ///// <returns>An <see cref="Models.Transaction.Submit"/> response.</returns>
        //Task<Submit> Submit(SubmitRequest request);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tx">
        /// Transaction in JSON format with an array of Signers.<br/>
        /// To be successful, the weights of the signatures must be equal or higher than the quorum of the SignerList.
        /// </param>
        /// <param name="wallet"></param>//todo add description
        /// <returns>An <see cref="Models.Transactions.Submit"/> response.</returns>
        Task<Submit> Submit(Dictionary<string, dynamic> tx, XahauWallet wallet);
        Task<Submit> Submit(ITransactionCommon tx, XahauWallet wallet);
        /// <summary>
        /// The tx method retrieves information on a single transaction, by its identifying hash
        /// </summary>
        /// <param name="request">An <see cref="TxRequest"/> request.</param>
        /// <returns>An <see cref="TransactionResponseCommon"/> response.</returns>
        Task<TransactionResponseCommon> Tx(TxRequest request);

        #endregion

        #region Channels


        #endregion

        #region Ledger
        //https://xrpl.org/ledger-methods.html

        /// <summary>
        /// The ledger_request command tells server to fetch a specific ledger version from its connected peers.
        /// This only works if one of the server's immediately-connected peers has that ledger.
        /// You may need to run the command several times to completely fetch a ledger
        /// </summary>
        /// <param name="request">An <see cref="LedgerRequest"/> request.</param>
        /// <returns>An <see cref="LOLedger"/> response.</returns>
        Task<LOLedger> Ledger(LedgerRequest request);

        /// <summary>
        /// The ledger_data method retrieves contents of the specified ledger.
        /// You can iterate through several calls to retrieve the entire contents of a single ledger version.
        /// </summary>
        /// <param name="request">An <see cref="LedgerDataRequest"/> request.</param>
        /// <returns>An <see cref="LOLedgerData"/> response.</returns>
        Task<LOLedgerData> LedgerData(LedgerDataRequest request);
        /// <summary> The ledger_closed method returns the unique identifiers of the most recently closed ledger. </summary>
        /// <param name="request">An <see cref="LedgerClosedRequest"/> response.</param>
        /// <returns>An <see cref="LOBaseLedger"/> response.</returns>
        Task<LOBaseLedger> LedgerClosed(LedgerClosedRequest request);
        /// <summary>
        /// The ledger_current method returns the unique identifiers of the current in-progress ledger.<br/>
        /// This command is mostly useful for testing, because the ledger returned is still in flux.
        /// </summary>
        /// <param name="request">An <see cref="LedgerCurrentRequest"/> response.</param>
        /// <returns>An <see cref="LOLedgerCurrentIndex"/> response.</returns>
        Task<LOLedgerCurrentIndex> LedgerCurrent(LedgerCurrentRequest request);
        /// <summary>
        /// The ledger_entry method returns a single ledger object from the XRP Ledger in its raw format.<br/>
        /// See ledger format for information on the different types of objects you can retrieve.
        /// </summary>
        /// <param name="request">An <see cref="LedgerEntryRequest"/> response.</param>
        /// <returns>An <see cref="LOLedgerEntry"/> response.</returns>
        Task<LOLedgerEntry> LedgerEntry(LedgerEntryRequest request);


        #endregion

        /// <summary>
        /// The book_offers method retrieves a list of offers, also known as the order book , between two currencies
        /// </summary>
        /// <param name="request">An <see cref="BookOffersRequest"/> request.</param>
        /// <returns>An <see cref="Models.Transactions.BookOffers"/> response.</returns>
        Task<BookOffers> BookOffers(BookOffersRequest request);
        /// <summary>
        /// The random command provides a random number to be used as a source of entropy for random number generation by clients.<br/>
        /// https://xrpl.org/random.html#random
        /// </summary>
        /// <param name="request">An <see cref="RandomRequest"/> request.</param>
        /// <returns></returns>
        Task<object> Random(RandomRequest request);

        //Task<DepositAuthorized> DepositAuthorized(DepositAuthorizedRequest request);
        //Task<PathFind> PathFind(PathFindRequest request);
        //Task<RipplePathFind> RipplePathFind(RipplePathFindRequest request);
        // Task<ServerState> ServerState(ServerStateRequest request);
        //Task<SubmitMultisign> SubmitMultisign(SubmitMultisignRequest request);
        //Task<TransactionEntry> TransactionEntry(TransactionEntryRequest request);
        Task<object> AnyRequest(BaseRequest request);

        Task<Dictionary<string, dynamic>> Request(Dictionary<string, dynamic> request);
        Task<T> GRequest<T, R>(R request);

        // Sugars
        Task<Dictionary<string, dynamic>> Autofill(Dictionary<string, dynamic> tx);
        Task<uint> GetLedgerIndex();
        Task<string> GetXrpBalance(string address);
        Task ChangeServer(string server, ClientOptions? options = null);

    }

    public class XahauClient : IXahauClient
    {

        public class ClientOptions : ConnectionOptions
        {
            public uint? NetworkID { get; set; }
            public string? BuildVersion { get; set; }
            public double? feeCushion { get; set; }
            public string? maxFeeXRP { get; set; }
        }

        public Connection connection { get; set; }
        public double feeCushion { get; set; }
        public string maxFeeXRP { get; set; }
        public uint? networkID { get; set; }
        public string? buildVersion { get; set; }

        //public event OnError OnError;
        //public event OnConnected OnConnected;
        //public event OnDisconnect OnDisconnect;
        //public event OnLedgerClosed OnLedgerClosed;
        //public event OnTransaction OnTransaction;
        //public event OnManifestReceived OnManifestReceived;
        //public event OnPeerStatusChange OnPeerStatusChange;
        //public event OnConsensusPhase OnConsensusPhase;
        //public event OnPathFind OnPathFind;

        ///// <summary> Current web socket client state </summary>
        //public WebSocketState SocketState => client.State;

        private readonly ConcurrentDictionary<int, TaskInfo> tasks;

        public XahauClient(string server, ClientOptions? options = null)
        {

            if (!IsValidWss(server))
            {
                throw new Exception("Invalid WSS Server Url");
            }
            feeCushion = options?.feeCushion ?? 1.2;
            maxFeeXRP = options?.maxFeeXRP ?? "2";
            networkID = options?.NetworkID;
            buildVersion = options?.BuildVersion;

            connection = new Connection(server, options);
            //connection.OnError += (e, em, m, d) => OnError?.Invoke(e, em, m, d);
            //connection.OnConnected += () => OnConnected?.Invoke();
            //connection.OnDisconnect += (c) => OnDisconnect?.Invoke(c);
            //connection.OnLedgerClosed += (s) => OnLedgerClosed?.Invoke(s);
            //connection.OnTransaction += (s) => OnTransaction?.Invoke(s);
            //connection.OnManifestReceived += (s) => OnManifestReceived?.Invoke(s);
            //connection.OnPeerStatusChange += (s) => OnPeerStatusChange?.Invoke(s);
            //connection.OnConsensusPhase += (s) => OnConsensusPhase?.Invoke(s);
            //connection.OnPathFind += (s) => OnPathFind?.Invoke(s);
        }

        public async Task GetServerInfo()
        {
            ServerInfoRequest request = new ServerInfoRequest();
            ServerInfo serverInfo = await this.ServerInfo(request);
            this.networkID = serverInfo.Info.NetworkID ?? 0;
            this.buildVersion = serverInfo.Info.BuildVersion;
        }

        public async Task ChangeServer(string server, ClientOptions? options = null)
        {
            if (!IsValidWss(server))
            {
                throw new Exception("Invalid WSS Server Url");
            }
            feeCushion = options?.feeCushion ?? 1.2;
            maxFeeXRP = options?.maxFeeXRP ?? "2";

            await connection.ChangeServer(server, options);
        }

        /// <inheritdoc />
        public string Url()
        {
            return this.connection.GetUrl();
        }

        public bool IsValidWss(string server)
        {
            return true;
        }

        /// <inheritdoc />
        public async Task Connect()
        {
            await connection.Connect();
            await this.GetServerInfo();
        }

        /// <inheritdoc />
        public async Task Disconnect()
        {
            await connection.Disconnect();
        }

        /// <inheritdoc />
        public bool IsConnected()
        {
            return this.connection.IsConnected();
        }

        // SUGARS
        public Task<Dictionary<string, dynamic>> Autofill(Dictionary<string, dynamic> tx)
        {
            return this.Autofill(tx, null);
        }

        /// <inheritdoc />
        public Task<Submit> Submit(Dictionary<string, dynamic> tx, XahauWallet wallet)
        {
            return this.Submit(tx, true, false, wallet);
        }
        /// <inheritdoc />
        public Task<Submit> Submit(ITransactionCommon tx, XahauWallet wallet)
        {
            if (networkID is { } network)
            {
                tx.NetworkID = network;
            }
            var json = tx.ToJson();
            //var json = JsonConvert.SerializeObject(tx);
            Dictionary<string, dynamic> txJson = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(json);
            return this.Submit(txJson, true, false, wallet);
        }

        /// <inheritdoc />
        public Task<uint> GetLedgerIndex()
        {
            return GetLedgerSugar.GetLedgerIndex(this);
        }
        /// <inheritdoc />
        public Task<string> GetXrpBalance(string address)
        {
            return BalancesSugar.GetXrpBalance(this, address);
        }

        // REQUESTS
        /// <inheritdoc />
        public Task<AccountChannels> AccountChannels(AccountChannelsRequest request)
        {
            return this.GRequest<AccountChannels, AccountChannelsRequest>(request);
        }

        /// <inheritdoc />
        public Task<AccountCurrencies> AccountCurrencies(AccountCurrenciesRequest request)
        {
            return this.GRequest<AccountCurrencies, AccountCurrenciesRequest>(request);
        }

        /// <inheritdoc />
        public Task<AccountInfo> AccountInfo(AccountInfoRequest request)
        {
            return this.GRequest<AccountInfo, AccountInfoRequest>(request);
        }

        /// <inheritdoc />
        public Task<AccountLines> AccountLines(AccountLinesRequest request)
        {
            return this.GRequest<AccountLines, AccountLinesRequest>(request);
        }

        /// <inheritdoc />
        public Task<AccountNFTs> AccountNFTs(AccountNFTsRequest request)
        {
            return this.GRequest<AccountNFTs, AccountNFTsRequest>(request);
        }

        /// <inheritdoc />
        public Task<AccountObjects> AccountObjects(AccountObjectsRequest request)
        {
            return this.GRequest<AccountObjects, AccountObjectsRequest>(request);
        }

        /// <inheritdoc />
        public Task<AccountOffers> AccountOffers(AccountOffersRequest request)
        {
            return this.GRequest<AccountOffers, AccountOffersRequest>(request);
        }

        /// <inheritdoc />
        public Task<AccountTransactions> AccountTransactions(AccountTransactionsRequest request)
        {
            return this.GRequest<AccountTransactions, AccountTransactionsRequest>(request);
        }

        /// <inheritdoc />
        public Task<BookOffers> BookOffers(BookOffersRequest request)
        {
            return this.GRequest<BookOffers, BookOffersRequest>(request);
        }

        //public Task<DepositAuthorized> DepositAuthorized(DepositAuthorizedRequest request)
        //{
        //    return this.GRequest<DepositAuthorized, DepositAuthorizedRequest>(request);
        //}

        /// <inheritdoc />
        public Task<LOLedger> Ledger(LedgerRequest request)
        {
            return this.GRequest<LOLedger, LedgerRequest>(request);
        }

        /// <inheritdoc />
        public Task<LOBaseLedger> LedgerClosed(LedgerClosedRequest request)
        {
            return this.GRequest<LOBaseLedger, LedgerClosedRequest>(request);
        }

        /// <inheritdoc />
        public Task<LOLedgerCurrentIndex> LedgerCurrent(LedgerCurrentRequest request)
        {
            return this.GRequest<LOLedgerCurrentIndex, LedgerCurrentRequest>(request);
        }
        /// <inheritdoc />
        public Task<LOLedgerData> LedgerData(LedgerDataRequest request)
        {
            return this.GRequest<LOLedgerData, LedgerDataRequest>(request);
        }

        /// <inheritdoc />
        public Task<LOLedgerEntry> LedgerEntry(LedgerEntryRequest request)
        {
            return this.GRequest<LOLedgerEntry, LedgerEntryRequest>(request);
        }

        /// <inheritdoc />
        public Task<Fee> Fee(FeeRequest request)
        {
            return this.GRequest<Fee, FeeRequest>(request);
        }

        /// <inheritdoc />
        public Task<GatewayBalances> GatewayBalances(GatewayBalancesRequest request)
        {
            return this.GRequest<GatewayBalances, GatewayBalancesRequest>(request);
        }

        /// <inheritdoc />
        public Task<NFTBuyOffers> NFTBuyOffers(NFTBuyOffersRequest request)
        {
            return this.GRequest<NFTBuyOffers, NFTBuyOffersRequest>(request);
        }

        /// <inheritdoc />
        public Task<NFTSellOffers> NFTSellOffers(NFTSellOffersRequest request)
        {
            return this.GRequest<NFTSellOffers, NFTSellOffersRequest>(request);
        }

        /// <inheritdoc />
        public Task<NoRippleCheck> NoRippleCheck(NoRippleCheckRequest request)
        {
            return this.GRequest<NoRippleCheck, NoRippleCheckRequest>(request);
        }

        //public Task<PathFind> PathFind(PathFindRequest request)
        //{
        //    return this.GRequest<object, PingRequest>(request);
        //}

        /// <inheritdoc />
        public Task<object> Ping(PingRequest request)
        {
            return this.GRequest<object, PingRequest>(request);
        }

        /// <inheritdoc />
        public Task<object> Random(RandomRequest request)
        {
            return this.GRequest<object, RandomRequest>(request);
        }

        //public Task<RipplePathFind> RipplePathFind(RipplePathFindRequest request)
        //{
        //    return this.GRequest<object, PingRequest>(request);
        //}

        public Task<ServerInfo> ServerInfo(ServerInfoRequest request)
        {
            return this.GRequest<ServerInfo, ServerInfoRequest>(request);
        }

        public Task<ServerState> ServerState(ServerStateRequest request)
        {
            return this.GRequest<ServerState, ServerStateRequest>(request);
        }

        /// <inheritdoc />
        //public Task<Submit> Submit(SubmitRequest request)
        //{
        //    return this.GRequest<Submit, SubmitRequest>(request);
        //}

        //public Task<SubmitMultisign> SubmitMultisign(SubmitMultisignRequest request, Wallet wallet)
        //{
        //    return this.GRequest<SubmitMultisign, SubmitMultisignRequest>(request);
        //}

        /// <inheritdoc />
        public Task<object> Subscribe(SubscribeRequest request)
        {

            return this.GRequest<object, SubscribeRequest>(request);
        }

        /// <inheritdoc />
        public Task<object> Unsubscribe(UnsubscribeRequest request)
        {

            return this.GRequest<object, UnsubscribeRequest>(request);
        }

        //public Task<TransactionEntry> TransactionEntry(TransactionEntryRequest request)
        //{
        //    return this.GRequest<TransactionEntry, TransactionEntryRequest>(request);
        //}

        /// <inheritdoc />
        public Task<TransactionResponseCommon> Tx(TxRequest request)
        {
            return this.GRequest<TransactionResponseCommon, TxRequest>(request);
        }

        /// <inheritdoc />
        public Task<object> AnyRequest(BaseRequest request)
        {
            return this.GRequest<object, BaseRequest>(request);
        }

        /// <inheritdoc />
        public async Task<Dictionary<string, dynamic>> Request(Dictionary<string, dynamic> request)
        {
            //string account = request["Account"] ? EnsureClassicAddress((string)request["account"]) : null;
            //request["Account"] = account;
            var response = await this.connection.Request(request);

            // mutates `response` to add warnings
            //handlePartialPayment(req.command, response)
            return response;

        }

        /// <inheritdoc />
        public async Task<T> GRequest<T, R>(R request)
        {
            //string account = request["Account"] ? EnsureClassicAddress((string)request["account"]) : null;
            //request["Account"] = account;
            var response = await this.connection.GRequest<T, R>(request);

            // mutates `response` to add warnings
            //handlePartialPayment(req.command, response)

            return response;
        }

        public string EnsureClassicAddress(string address)
        {
            return Xahau.Sugar.Utils.EnsureClassicAddress(address);
        }

        #region IDisposable

        public void Dispose()
        {
            // todo: should check for ws...
            connection?.Disconnect();
        }

        #endregion
    }
}
