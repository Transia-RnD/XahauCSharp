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

// https://github.com/XRPLF/xrpl.js/blob/main/packages/xrpl/test/integration/transactions/accountDelete.ts

namespace XahauTests.Xahau.ClientLib.Integration
{
    [TestClass]
    public class TestIAccountDelete
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
            XahauWallet wallet2 = await Utils.GenerateFundedWallet(runner.client);

            var promises = new List<Task>();
            for (var iter = 0; iter < 256; iter++)
            {
                promises.Add(Utils.LedgerAccept(runner.client));
            }
            await Task.WhenAll(promises);

            LedgerIndex index = new LedgerIndex(LedgerIndexType.Validated);
            AccountChannelsRequest request = new AccountChannelsRequest(runner.wallet.ClassicAddress) { LedgerIndex = index };
            AccountChannels response = await runner.client.AccountChannels(request);
            Assert.IsNotNull(response);
            AccountDelete tx = new AccountDelete
            {
                Account = runner.wallet.ClassicAddress,
                Destination = wallet2.ClassicAddress,
                NetworkID = runner.client.networkID,
            };
            Dictionary<string, dynamic> txJson = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(tx.ToJson());
            // AccountDelete is not active on Xahau
            // await Utils.TestTransaction(runner.client, txJson, runner.wallet);
        }
    }
}