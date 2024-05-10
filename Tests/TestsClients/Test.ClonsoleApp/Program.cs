﻿////See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using Newtonsoft.Json;
using Xahau.Client;
using Xahau.Models.Methods;
using Xahau.Models.Transactions;
using Xahau.Wallet;

namespace MyApp
{
    class Program
    {
        static async Task SampleClient()
        {
            //using System.Diagnostics;
            //using Xahau.Client;
            //var client = new XahauClient("wss://s.altnet.rippletest.net:51233");
            //client.OnConnected += async () =>
            //{
            //    Console.WriteLine("CONNECTED");
            //};
            //await client.Connect();
        }

        static void WalletFromSeed()
        {
            //using System.Diagnostics;
            //using Xahau.Wallet;
            string seed = "sEdSuqBPSQaood2DmNYVkwWTn1oQTj2";
            XahauWallet wallet = XahauWallet.FromSeed(seed);
            Console.WriteLine(wallet.ClassicAddress);
            Console.WriteLine(wallet.PrivateKey);
            Console.WriteLine(wallet.PublicKey);
            Console.WriteLine(wallet.Seed);
        }

        static void WalletGenerate()
        {
            //using System.Diagnostics;
            //using Xahau.Wallet;
            XahauWallet wallet = XahauWallet.Generate();
            Console.WriteLine(wallet.ClassicAddress);
            Console.WriteLine(wallet.PrivateKey);
            Console.WriteLine(wallet.PublicKey);
            Console.WriteLine(wallet.Seed);
        }

        static async Task SubmitTestTx()
        {
            //using Newtonsoft.Json;
            //using Xahau.Client;
            //using Xahau.Models.Methods;
            //using Xahau.Models.Transactions;
            //using Xahau.Wallet;

            var client = new XahauClient("wss://s.altnet.rippletest.net:51233");

            client.connection.OnConnected += async () =>
            {
                Console.WriteLine("CONNECTED");
            };

            await client.Connect();

            Console.WriteLine("NEXT");

            string seed = "sEdSuqBPSQaood2DmNYVkwWTn1oQTj2";
            XahauWallet wallet = XahauWallet.FromSeed(seed);

            AccountInfoRequest request = new AccountInfoRequest(wallet.ClassicAddress);
            AccountInfo accountInfo = await client.AccountInfo(request);

            // prepare the transaction
            // the amount is expressed in drops, not XRP
            // see https://xrpl.org/basic-data-types.html#specifying-currency-amounts
            IPayment tx = new Payment()
            {
                Account = wallet.ClassicAddress,
                Destination = "rEqtEHKbinqm18wQSQGstmqg9SFpUELasT",
                Amount = new Xahau.Models.Common.Currency { ValueAsXrp = 1 },
                Sequence = accountInfo.AccountData.Sequence
            };

            // sign and submit the transaction
            Dictionary<string, dynamic> txJson = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(tx.ToJson());
            Submit response = await client.Submit(txJson, wallet);
            Console.WriteLine(response.EngineResult);
        }

        static async Task WebsocketTest()
        {
            bool isFinished = false;

            var server = "wss://s.altnet.rippletest.net:51233";

            var client = new XahauClient(server);

            client.connection.OnConnected += async () =>
            {
                Console.WriteLine("CONNECTED");
                var subscribe = await client.Subscribe(
                new SubscribeRequest()
                {
                    Streams = new List<string>(new[]
                    {
                        "ledger",
                    })
                });
            };

            client.connection.OnDisconnect += (code) =>
            {
                Console.WriteLine($"DISCONECTED CODE: {code}");
                Console.WriteLine("DISCONECTED");
                return Task.CompletedTask;
            };

            client.connection.OnError += (errorCode, errorMessage, error, data) =>
            {
                Console.WriteLine(errorCode);
                Console.WriteLine(errorMessage);
                Console.WriteLine(data);
                return Task.CompletedTask;
            };

            client.connection.OnTransaction += Response =>
            {
                Console.WriteLine(Response.Transaction.TransactionType.ToString());
                return Task.CompletedTask;
            };

            client.connection.OnLedgerClosed += r =>
            {
                Console.WriteLine($"MESSAGE RECEIVED: {r}");
                isFinished = true;
                return Task.CompletedTask;
            };
            
            await client.Connect();

            while (!isFinished)
            {
                Debug.WriteLine($"WAITING: {DateTime.Now}");
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            }

            await client.Disconnect();
        }
        static async Task WebsocketChangeServerTest()
        {
            bool isFinished = false;
            var server1 = "wss://s1.ripple.com/";
            var server2 = "wss://s2.ripple.com/";
            var server3 = "wss://xrplcluster.com/";

            var client = new XahauClient(server1);

            client.connection.OnConnected += async () =>
            {
                Console.WriteLine("CONNECTED");
                var subscribe = await client.Subscribe(
                new SubscribeRequest()
                {
                    Streams = new List<string>(new[]
                    {
                        "ledger",
                    })
                });
            };

            client.connection.OnDisconnect += (code) =>
            {
                Console.WriteLine($"DISCONECTED CODE: {code}");
                return Task.CompletedTask;
            };

            client.connection.OnError += (errorCode, errorMessage, error, data) =>
            {
                Console.WriteLine(errorCode);
                Console.WriteLine(errorMessage);
                Console.WriteLine(data);
                return Task.CompletedTask;
            };

            client.connection.OnTransaction += Response =>
            {
                Console.WriteLine(Response.Transaction.TransactionType.ToString());
                return Task.CompletedTask;
            };

            client.connection.OnLedgerClosed += r =>
            {
                Console.WriteLine($"MESSAGE RECEIVED: {r}");
                isFinished = true;
                return Task.CompletedTask;
            };
            
            await client.Connect();

            while (!isFinished)
            {
                Debug.WriteLine($"WAITING: {DateTime.Now}");
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            }

            await Task.Delay(3000);
            isFinished = false;

            await client.connection.ChangeServer(server2);
            while (!isFinished)
            {
                Debug.WriteLine($"WAITING: {DateTime.Now}");
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            }
            await Task.Delay(3000);
            isFinished = false;

            await client.connection.ChangeServer(server3);
            while (!isFinished)
            {
                Debug.WriteLine($"WAITING: {DateTime.Now}");
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            }
            await Task.Delay(3000);
            isFinished = false;

            await client.connection.ChangeServer(server1);
            while (!isFinished)
            {
                Debug.WriteLine($"WAITING: {DateTime.Now}");
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            }
            await Task.Delay(3000);
            isFinished = false;

            await client.connection.ChangeServer(server2);
            while (!isFinished)
            {
                Debug.WriteLine($"WAITING: {DateTime.Now}");
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            }
            await Task.Delay(3000);
            isFinished = false;

            await client.connection.ChangeServer(server3);
            while (!isFinished)
            {
                Debug.WriteLine($"WAITING: {DateTime.Now}");
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            }
            await Task.Delay(3000);
            isFinished = false;

            await client.connection.ChangeServer(server1);
            while (!isFinished)
            {
                Debug.WriteLine($"WAITING: {DateTime.Now}");
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            }
            await Task.Delay(3000);

            await client.Disconnect();
        }

        static async Task Main(string[] args)
        {
            //await SampleClient();
            //WalletFromSeed();
            //WalletGenerate();
            //await SubmitTestTx();
            //await WebsocketTest();
            await WebsocketChangeServerTest();
        }
    }
}
