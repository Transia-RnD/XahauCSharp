using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Xahau.Models.Transactions;
using Xahau.Utils.Hashes;
using Xahau.Wallet;

// https://github.com/XRPLF/xrpl.js/blob/main/packages/xrpl/test/integration/transactions/paymentChannelClaim.ts

namespace XahauTests.Xahau.ClientLib.Integration
{
    [TestClass]
    public class TestIPaymentChannelClaim
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
            PaymentChannelCreate setupTx = new PaymentChannelCreate
            {
                Account = runner.wallet.ClassicAddress,
                Amount = "100",
                Destination = wallet2.ClassicAddress,
                SettleDelay = 86400,
                PublicKey = runner.wallet.PublicKey
            };
            Dictionary<string, dynamic> setupJson = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(setupTx.ToJson());

            Submit paymentChannelResponse = await runner.client.Submit(setupJson, runner.wallet);

            // USE SUBMIT ^^ TO GET THE RESPONSE
            //await Utils.TestTransaction(runner.client, setupJson, runner.wallet);

            // actually test PaymentChannelClaim
            PaymentChannelClaim tx = new PaymentChannelClaim
            {
               Account = runner.wallet.ClassicAddress,
               Channel = Hashes.HashPaymentChannel(
                    runner.wallet.ClassicAddress,
                    wallet2.ClassicAddress,
                    (int)paymentChannelResponse.TxJson.Sequence
                ),
                Amount = "100"
            };
            Dictionary<string, dynamic> txJson = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(tx.ToJson());
            await Utils.TestTransaction(runner.client, txJson, runner.wallet);
        }
    }
}