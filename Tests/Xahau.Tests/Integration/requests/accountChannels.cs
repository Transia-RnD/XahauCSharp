using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xahau.Models.Common;
using Xahau.Models.Ledger;
using Xahau.Models.Methods;

// https://github.com/XRPLF/xrpl.js/blob/main/packages/xrpl/test/integration/requests/accountChannels.ts

namespace XahauTests.Xahau.ClientLib.Integration
{
    [TestClass]
    public class TestIAccountChannels
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
            AccountChannelsRequest request = new AccountChannelsRequest(runner.wallet.ClassicAddress) { LedgerIndex = index };
            AccountChannels response = await runner.client.AccountChannels(request);
            Assert.IsNotNull(response);
        }
    }
}