

//// https://github.com/XRPLF/xrpl.js/blob/main/packages/xrpl/test/integration/requests/accountNFTs.ts

//using System.Threading.Tasks;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Xahau.Models.Common;
//using Xahau.Models.Ledger;
//using Xahau.Models.Methods;

//namespace XahauTests.Xahau.ClientLib.Integration
//{
//    [TestClass]
//    public class TestINFTSellOffers
//    {
//        // private static int Timeout = 20;
//        public TestContext TestContext { get; set; }
//        public static SetupIntegration runner;

//        [ClassInitialize]
//        public static async Task MyClassInitializeAsync(TestContext testContext)
//        {
//            runner = await new SetupIntegration().SetupClient(ServerUrl.serverUrl);
//        }

//        [TestMethod]
//        public async Task TestRequestMethod()
//        {
//            LedgerIndex index = new LedgerIndex(LedgerIndexType.Validated);
//            string nft_id = "";
//            NFTBuyOffersRequest request = new NFTBuyOffersRequest(nft_id);
//            NFTBuyOffers response = await runner.client.NFTBuyOffers(request);
//            Assert.IsNotNull(response);
//        }
//    }
//}