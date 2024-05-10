

using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xahau.Client;
using Xahau.Client.Exceptions;

// https://github.com/XRPLF/xrpl.js/blob/main/packages/xrpl/test/client/constructor.ts

namespace Xahau.Tests.ClientLib
{
    [TestClass]
    public class TestUConstructor
    {

        //public static SetupUnitClient runner;

        [TestMethod]
        public void TestImplicitPort()
        {
            new XahauClient("wss://s1.ripple.com");
        }

        //[TestMethod]
        //public void TestInvalidOptions()
        //{
        //    new XahauClient("wss://s1.ripple.com");
        //}

        [TestMethod]
        public void TestValidOptions()
        {
            XahauClient client = new XahauClient("wss://s:1");
            string privateConnectionUrl = client.Url();
            Assert.AreEqual("wss://s:1", privateConnectionUrl);
        }

        //[TestMethod]
        //[ExpectedException(typeof(XahauException))]
        //public void TestInvalidServer()
        //{
        //    new XahauClient("wss://s:1");
        //}
    }
}

