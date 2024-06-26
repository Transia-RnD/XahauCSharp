﻿
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Xahau.Sugar;

// https://github.com/XRPLF/xrpl.js/blob/main/packages/xrpl/test/client/getLedgerIndex.ts

namespace Xahau.Tests.ClientLib
{
    [TestClass]
    public class TestUGetLedgerIndex
    {

        public static SetupUnitClient runner;

        [TestInitialize]
        public async Task MyTestInitializeAsync()
        {
            runner = await new SetupUnitClient().SetupClient();
        }

        [TestCleanup]
        public async Task MyTestCleanupAsync()
        {
            await runner.client.Disconnect();
        }

        [TestMethod]
        public async Task TestGetLedgerIndex()
        {
            string jsonString = "{\"id\":0,\"status\":\"success\",\"type\":\"response\",\"result\":{\"ledger\":{\"account_hash\":\"EC028EC32896D537ECCA18D18BEBE6AE99709FEFF9EF72DBD3A7819E918D8B96\",\"close_time\":464908910,\"parent_close_time\":464908900,\"close_time_human\":\"2014-Sep-2421:21:50\",\"close_time_resolution\":10,\"closed\":true,\"close_flags\":0,\"ledger_hash\":\"0F7ED9F40742D8A513AE86029462B7A6768325583DF8EE21B7EC663019DD6A0F\",\"ledger_index\":\"9038214\",\"parent_hash\":\"4BB9CBE44C39DC67A1BE849C7467FE1A6D1F73949EA163C38A0121A15E04FFDE\",\"total_coins\":\"99999973964317514\",\"transaction_hash\":\"ECB730839EB55B1B114D5D1AD2CD9A932C35BA9AB6D3A8C2F08935EAC2BAC239\",\"transactions\":[\"1FC4D12C30CE206A6E23F46FAC62BD393BE9A79A1C452C6F3A04A13BC7A5E5A3\",\"E25C38FDB8DD4A2429649588638EE05D055EE6D839CABAF8ABFB4BD17CFE1F3E\"]},\"ledger_hash\":\"1723099E269C77C4BDE86C83FA6415D71CF20AA5CB4A94E5C388ED97123FB55B\",\"ledger_index\":9038214,\"validated\":true}}";
            Dictionary<string, dynamic> jsonData = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(jsonString);
            runner.mockedRippled.AddResponse("ledger", jsonData);
            uint ledgerIndex = await runner.client.GetLedgerIndex();
            Assert.IsTrue(ledgerIndex == 9038214);
        }
    }
}

