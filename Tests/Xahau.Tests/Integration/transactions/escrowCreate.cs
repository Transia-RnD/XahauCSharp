using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Xahau.Models.Common;
using Xahau.Models.Ledger;
using Xahau.Models.Methods;
using Xahau.Models.Transactions;
using Xahau.Wallet;

// https://github.com/XRPLF/xrpl.js/blob/main/packages/xrpl/test/integration/transactions/escrowCreate.ts

namespace XahauTests.Xahau.ClientLib.Integration
{
    [TestClass]
    public class TestIEscrowCreate
    {
        // private static int Timeout = 20;
        public TestContext TestContext { get; set; }
        public static SetupIntegration runner;

        [ClassInitialize]
        public static async Task MyClassInitializeAsync(TestContext testContext)
        {
            runner = await new SetupIntegration().SetupClient(ServerUrl.serverUrl);
        }

        [TestMethod]
        public async Task TestRequestMethod()
        {

            LedgerIndex index = new LedgerIndex(LedgerIndexType.Validated);
            LedgerRequest request = new LedgerRequest() { LedgerIndex = index };
            LOLedger ledgerResponse = await runner.client.Ledger(request);
            LedgerEntity ledgerEntity = (LedgerEntity)ledgerResponse.LedgerEntity;
            uint closeTime = ledgerEntity.CloseTime;

            XahauWallet wallet2 = await Utils.GenerateFundedWallet(runner.client);
            EscrowCreate setupTx = new EscrowCreate
            {
                Account = runner.wallet.ClassicAddress,
                Amount = new Currency { ValueAsXrp = 100 },
                Destination = wallet2.ClassicAddress,
                FinishAfter = closeTime + 2,
            };
            Dictionary<string, dynamic> setupJson = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(setupTx.ToJson());
            await Utils.TestTransaction(runner.client, setupJson, runner.wallet);

            AccountObjectsRequest request2 = new AccountObjectsRequest(runner.wallet.ClassicAddress) { Type = "escrow" };
            AccountObjects response2 = await runner.client.AccountObjects(request2);
            Assert.AreEqual(response2.AccountObjectList.Count, 1);
        }
    }
}