using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Xahau.Models.Ledger;
using Xahau.Models.Transactions;
using Xahau.Wallet;

// https://github.com/XRPLF/xrpl.js/blob/main/packages/xrpl/test/integration/transactions/signerListSet.ts

namespace XahauTests.Xahau.ClientLib.Integration
{
    [TestClass]
    public class TestISignerListSet
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
            SignerEntry signer1 = new SignerEntry { Account = "r5nx8ZkwEbFztnc8Qyi22DE9JYjRzNmvs", SignerWeight = 1 };
            SignerEntryWrapper s1w = new SignerEntryWrapper { SignerEntry = signer1 };
            SignerEntry signer2 = new SignerEntry { Account = "r3RtUvGw9nMoJ5FuHxuoVJvcENhKtuF9ud", SignerWeight = 1 };
            SignerEntryWrapper s2w = new SignerEntryWrapper { SignerEntry = signer2 };
            SignerListSet setupTx = new SignerListSet
            {
                Account = runner.wallet.ClassicAddress,
                SignerQuorum = 2,
                SignerEntries = new List<SignerEntryWrapper>()
                {
                    s1w,
                    s2w,
                },
                NetworkID = runner.client.networkID,
            };
            Dictionary<string, dynamic> setupJson = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(setupTx.ToJson());
            await Utils.TestTransaction(runner.client, setupJson, runner.wallet);
        }
    }
}