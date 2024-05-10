using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// https://github.com/XRPLF/xrpl.js/blob/main/packages/xrpl/test/integration/requests/subscribe.ts

namespace XahauTests.Xahau.ClientLib.Integration
{
    [TestClass]
    public class TestISubscribe
    {
        // private static int Timeout = 20;
        public TestContext TestContext { get; set; }
        public static SetupIntegration runner;

        [ClassInitialize]
        public static async Task MyClassInitializeAsync(TestContext testContext)
        {
            runner = await new SetupIntegration().SetupClient(ServerUrl.serverUrl);
        }
    }
}