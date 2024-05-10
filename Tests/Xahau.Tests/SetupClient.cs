using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Xahau.Client;
using Xahau.Client.Exceptions;
using Newtonsoft.Json;
using System.Collections.Generic;
using Timer = System.Timers.Timer;

// https://github.com/XRPLF/xrpl.js/blob/main/packages/xrpl/test/setupClient.ts

namespace Xahau.Tests
{
    public class SetupUnitClient
    {
        public XahauClient client;
        public CreateMockRippled mockedRippled;
        public int _mockedServerPort;

        public async Task<SetupUnitClient> SetupClient()
        {
            int port = TestUtils.GetFreePort();
            var tcpListenerThread = new Thread(() =>
            {
                mockedRippled = new CreateMockRippled(port);
                mockedRippled.Start();
                string serverInfoString = "{\"id\":0,\"status\":\"success\",\"type\":\"response\",\"result\":{\"info\":{\"build_version\":\"0.24.0-rc1\",\"complete_ledgers\":\"32570-6595042\",\"hostid\":\"ARTS\",\"io_latency_ms\":1,\"last_close\":{\"converge_time_s\":2.007,\"proposers\":4},\"load_factor\":1,\"peers\":53,\"pubkey_node\":\"n94wWvFUmaKGYrKUGgpv1DyYgDeXRGdACkNQaSe7zJiy5Znio7UC\",\"server_state\":\"full\",\"validated_ledger\":{\"age\":5,\"base_fee_xrp\":0.00001,\"hash\":\"4482DEE5362332F54A4036ED57EE1767C9F33CF7CE5A6670355C16CECE381D46\",\"reserve_base_xrp\":20,\"reserve_inc_xrp\":5,\"seq\":6595042},\"validation_quorum\":3}}}";
                Dictionary<string, dynamic> serverInfoData = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(serverInfoString);
                mockedRippled.AddResponse("server_info", serverInfoData);
                _mockedServerPort = port;
            });
            tcpListenerThread.Start();
            Timer timer = new Timer(25000);
            timer.Elapsed += (sender, e) => tcpListenerThread.Abort();
            client = new XahauClient($"ws://127.0.0.1:{port}");
            client.connection.OnConnected += () =>
            {
                Debug.WriteLine("SETUP CLIENT: CONECTED");
                return Task.CompletedTask;
            };
            client.connection.OnDisconnect += (code) =>
            {
                Debug.WriteLine("SETUP CLIENT: DISCONECTED");
                return Task.CompletedTask;
            };
            client.connection.OnError += (e, em, m, d) =>
            {
                Debug.WriteLine($"SETUP CLIENT: ERROR: {e}");
                return Task.CompletedTask;
            };
            await client.Connect();
            return this;
        }
    }
}
