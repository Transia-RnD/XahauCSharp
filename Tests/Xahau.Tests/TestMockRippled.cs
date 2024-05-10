using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xahau.AddressCodec;
using Xahau.Client.Exceptions;
using Xahau.Models.Methods;
using Timer = System.Timers.Timer;

// https://github.com/XRPLF/xrpl.js/blob/main/packages/xrpl/test/mockRippledTest.ts

namespace Xahau.Tests
{
    [TestClass]
    public class TestUMockRippled
    {

        [TestMethod]
        public void TestErrorMockNotProvided()
        {
            var tcpListenerThread = new Thread(() =>
            {
                CreateMockRippled mockedRippled = new CreateMockRippled(9999);
                mockedRippled.Start();
            });
            tcpListenerThread.Start();
            Timer timer = new Timer(5000);
            timer.Elapsed += (sender, e) => tcpListenerThread.Abort();
            timer.Start();
        }

        //[TestMethod]
        //[ExpectedException(typeof(XahauException), "")]
        //public async Task TestErrorMockNotProvided()
        //{
        //    ServerInfoRequest request = new ServerInfoRequest();
        //    await runner.client.Request(request);
        //}
    }
}

