

// https://github.com/XRPLF/xrpl.js/blob/main/packages/xrpl/test/integration/requests/bookOffers.ts

using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xahau.Models.Methods;
using Xahau.Models.Transactions;

namespace XahauTests.Xahau.ClientLib.Integration
{
    [TestClass]
    public class TestIBookOffersRequests
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
            TakerAmount takerGets = new TakerAmount { Currency = "XAH" };
            TakerAmount takerPays = new TakerAmount { Currency = "USD", Issuer = runner.wallet.ClassicAddress };
            BookOffersRequest request = new BookOffersRequest() { TakerGets = takerGets, TakerPays = takerPays };
            BookOffers bookOffers = await runner.client.BookOffers(request);
            Assert.IsNotNull(bookOffers);
        }
    }
}