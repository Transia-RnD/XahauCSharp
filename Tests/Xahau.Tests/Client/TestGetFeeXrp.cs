using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Xahau.Client;
using Xahau.Models.Methods;
using Xahau.Sugar;

// https://github.com/XRPLF/xrpl.js/blob/main/packages/xrpl/test/client/getFeeXrp.ts

namespace Xahau.Tests.ClientLib
{
    [TestClass]
    public class TestUGetFeeXrp
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
        public async Task TestGetFeeXrp()
        {
            string jsonString = "{\"id\":0,\"status\":\"success\",\"type\":\"response\",\"result\":{\"info\":{\"build_version\":\"0.24.0-rc1\",\"complete_ledgers\":\"32570-6595042\",\"hostid\":\"ARTS\",\"io_latency_ms\":1,\"last_close\":{\"converge_time_s\":2.007,\"proposers\":4},\"load_factor\":1,\"peers\":53,\"pubkey_node\":\"n94wWvFUmaKGYrKUGgpv1DyYgDeXRGdACkNQaSe7zJiy5Znio7UC\",\"server_state\":\"full\",\"validated_ledger\":{\"age\":5,\"base_fee_xrp\":0.00001,\"hash\":\"4482DEE5362332F54A4036ED57EE1767C9F33CF7CE5A6670355C16CECE381D46\",\"reserve_base_xrp\":20,\"reserve_inc_xrp\":5,\"seq\":6595042},\"validation_quorum\":3}}}";
            Dictionary<string, dynamic> jsonData = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(jsonString);
            runner.mockedRippled.AddResponse("server_info", jsonData);
            string fee = await GetFeeXrpSugar.GetFeeXrp(runner.client);
            Assert.AreEqual(fee, "0.000012");
        }
        [TestMethod]
        public async Task TestGetFeeEstimateXrp()
        {
            string txBlob = "12000C22000000002400000014201B0000002B201D000053592023000000026840000000001E84B073008114AE123A8556F3CF91154711376AFB0F894F832B3DF4EB1300018114AA266540F7DACC27E264B75ED0A5ED7330BFB614E1EB1300018114D91B8EE5C7ABF632469D4C0907C5E40C8B8F79B3E1F1";
            string jsonString = "{\n\"id\": 0,\n\"status\":\"success\",\n\"type\":\"response\",\n\"result\":{\ncurrent_ledger_size:\n\"2\",\ncurrent_queue_size:\n\"0\",\ndrops:\n{\nbase_fee:\n\"227\",\nbase_fee_no_hooks:\n\"10\",\nmedian_fee:\n\"113500\",\nminimum_fee:\n\"227\",\nopen_ledger_fee:\n\"227\"\n},\nexpected_ledger_size:\n\"32\",\nfee_hooks_feeunits:\n\"227\",\nledger_current_index:\n13409586,\nlevels:\n{\nmedian_level:\n\"128000\",\nminimum_level:\n\"256\",\nopen_ledger_level:\n\"256\",\nreference_level:\n\"256\"\n},\nmax_queue_size:\n\"2000\"\n  }\n}";
            Dictionary<string, dynamic> jsonData = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(jsonString);
            runner.mockedRippled.AddResponse("fee", jsonData);
            string fee = await GetFeeXrpSugar.GetFeeEstimateXrp(runner.client, txBlob, 0);
            Assert.AreEqual(fee, "227");
        }
    }
}

