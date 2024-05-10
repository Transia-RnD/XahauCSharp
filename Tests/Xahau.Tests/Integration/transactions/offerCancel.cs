using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Xahau.Models.Common;
using Xahau.Models.Methods;
using Xahau.Models.Transactions;
using Xahau.Wallet;

// https://github.com/XRPLF/xrpl.js/blob/main/packages/xrpl/test/integration/transactions/offerCancel.ts

namespace XahauTests.Xahau.ClientLib.Integration
{
    [TestClass]
    public class TestIOfferCancel
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

            // sequence
            AccountOffersRequest request1 = new AccountOffersRequest(runner.wallet.ClassicAddress);
            AccountOffers response1 = await runner.client.AccountOffers(request1);
            uint sequence = (uint)response1.Offers[0].Sequence;
            
            // actually test OfferCancel
            OfferCancel tx = new OfferCancel
            {
               Account = runner.wallet.ClassicAddress,
               OfferSequence = sequence,
               NetworkID = runner.client.networkID,
            };
            Dictionary<string, dynamic> txJson = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(tx.ToJson());
            await Utils.TestTransaction(runner.client, txJson, runner.wallet);

            AccountOffersRequest request2 = new AccountOffersRequest(runner.wallet.ClassicAddress);
            AccountOffers response2 = await runner.client.AccountOffers(request1);
            Assert.AreEqual(response2.Offers.Count, 0);
        }
    }
}