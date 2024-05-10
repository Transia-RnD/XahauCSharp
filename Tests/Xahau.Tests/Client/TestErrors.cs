using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xahau.Client;
using Xahau.Client.Exceptions;

// https://github.com/XRPLF/xrpl.js/blob/main/packages/xrpl/test/client/errors.ts

namespace Xahau.Tests.ClientLib
{
    [TestClass]
    public class TestUErrors
    {
        //[TestMethod]
        //public void TestErrorWithData()
        //{
        //    XahauException error = new XahauException("_message_", "_data_");
        //    Assert.AreEqual("[XahauException(_message_, '_data_')]", error.ToString());
        //}

        [TestMethod]
        public void TestErrorNotFound()
        {
            XahauException error = new NotFoundException();
            Assert.AreEqual("Xahau.Client.Exceptions.NotFoundException: Not Found", error.ToString());
        }
    }
}

