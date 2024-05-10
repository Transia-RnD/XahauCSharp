using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Xahau.Models.Common;
using Xahau.Models.Methods;
using Xahau.Models.Transactions;
using Xahau.Wallet;

// https://github.com/XRPLF/xrpl.js/blob/main/packages/xrpl/test/integration/transactions/offerCreate.ts

namespace XahauTests.Xahau.ClientLib.Integration
{
    [TestClass]
    public class TestIOfferCreate
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
            OfferCreate setupTx = new OfferCreate
            {
                Account = runner.wallet.ClassicAddress,
                TakerGets = new Currency() { ValueAsXrp = 13100000 },
                TakerPays = new Currency() { 
                    CurrencyCode = "USD",
                    Issuer = runner.wallet.ClassicAddress,
                    Value = "10",
                },
                NetworkID = runner.client.networkID,
            };
            Dictionary<string, dynamic> setupJson = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(setupTx.ToJson());
            await Utils.TestTransaction(runner.client, setupJson, runner.wallet);

            AccountOffersRequest request2 = new AccountOffersRequest(runner.wallet.ClassicAddress);
            AccountOffers response2 = await runner.client.AccountOffers(request2);
            Assert.AreEqual(1, response2.Offers.Count);
        }
    }
}