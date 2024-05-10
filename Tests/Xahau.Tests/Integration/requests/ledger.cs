

// https://github.com/XRPLF/xrpl.js/blob/main/packages/xrpl/test/integration/requests/ledger.ts

using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xahau.Models.Common;
using Xahau.Models.Ledger;
using Xahau.Models.Methods;

namespace XahauTests.Xahau.ClientLib.Integration
{
    [TestClass]
    public class TestILedger
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
            LedgerRequest request = new LedgerRequest()
            {
                LedgerIndex = index,
            };
            LOLedger response = await runner.client.Ledger(request);
            Assert.IsNotNull(response);
        }
    }
}