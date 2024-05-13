using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Xahau.Models.Common;
using Xahau.Models.Transactions;
using Xahau.Wallet;

// https://github.com/XRPLF/xrpl.js/blob/main/packages/xrpl/test/integration/transactions/ticketCreate.ts

namespace XahauTests.Xahau.ClientLib.Integration
{
    [TestClass]
    public class TestITicketCreate

    {
        // private static int Timeout = 20;
        public TestContext TestContext;

        public static SetupIntegration runner;

        [ClassInitialize]
        public static async Task MyClassInitializeAsync(TestContext testContext)
        {
            runner = await new SetupIntegration().SetupClient(ServerUrl.serverUrl);
        }

        //[ClassCleanup]
        //public static async Task MyClassCleanupAsync()
        //{
        //    await runner.client.Disconnect();
        //}

        [TestMethod]
        public async Task TestRequestMethod()
        {
            TicketCreate tx = new TicketCreate
            {
                Account = runner.wallet.ClassicAddress,
                TicketCount = 2,
                NetworkID = runner.client.networkID,
            };
            Dictionary<string, dynamic> txJson = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(tx.ToJson());
            await Utils.TestTransaction(runner.client, txJson, runner.wallet);
        }
    }
}
