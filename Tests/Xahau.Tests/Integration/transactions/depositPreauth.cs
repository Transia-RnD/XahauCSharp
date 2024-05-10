using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Xahau.Models.Transactions;
using Xahau.Wallet;

// https://github.com/XRPLF/xrpl.js/blob/main/packages/xrpl/test/integration/transactions/depositPreauth.ts

namespace XahauTests.Xahau.ClientLib.Integration
{
    [TestClass]
    public class TestIDepositPreauth
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
            DepositPreauth setupTx = new DepositPreauth
            {
                Account = runner.wallet.ClassicAddress,
                Authorize = wallet2.ClassicAddress,
                NetworkID = runner.client.networkID,
            };
            Dictionary<string, dynamic> setupJson = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(setupTx.ToJson());
            await Utils.TestTransaction(runner.client, setupJson, runner.wallet);
        }
    }
}