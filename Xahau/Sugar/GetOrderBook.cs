
// https://github.com/XRPLF/xrpl.js/blob/main/packages/xrpl/src/sugar/utils.ts

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Xahau.Client;
using Xahau.Models.Common;
using Xahau.Models.Ledger;
using Xahau.Models.Methods;
using Xahau.Models.Transactions;

namespace Xahau.Sugar
{
    public static class GetOrderBookSugar
    {
        private const uint DEFAULT_LIMIT = 20;

        private static List<Xahau.Models.Transactions.Offer> SortOffers(List<Xahau.Models.Transactions.Offer> offers)
        {
            return offers.OrderBy(offer => offer.Quality ?? 0).ToList();
        }

        public static async Task<(List<Xahau.Models.Transactions.Offer> buy, List<Xahau.Models.Transactions.Offer> sell)> GetOrderbook(this IXahauClient Client,
            TakerAmount takerPays, TakerAmount takerGets,
            uint limit = DEFAULT_LIMIT, int ledgerIndex = -1, string ledgerHash = null, string taker = null)
        {
            var request = new BookOffersRequest
            {
                Command = "book_offers",
                TakerPays = takerPays,
                TakerGets = takerGets,
                LedgerIndex = new LedgerIndex(ledgerIndex==-1? LedgerIndexType.Validated: (LedgerIndexType)ledgerIndex),
                LedgerHash = ledgerHash,
                Limit = limit,
                Taker = taker
            };
            var directOfferResults = await Client.BookOffers(request);
            request.TakerGets = takerPays;
            request.TakerPays = takerGets;
            var reverseOfferResults = await Client.BookOffers(request);
            var directOffers = directOfferResults.Offers;
            var reverseOffers = reverseOfferResults.Offers;
            var orders = directOffers.Concat(reverseOffers).ToList();
            var buy = new List<Xahau.Models.Transactions.Offer>();
            var sell = new List<Xahau.Models.Transactions.Offer>();
            orders.ForEach(order =>
            {
                if ((order.Flags & OfferFlags.lsfSell) == 0)
                {
                    buy.Add(order);
                }
                else
                {
                    sell.Add(order);
                }
            });
            return (SortOffers(buy).Take((int)limit).ToList(), SortOffers(sell).Take((int)limit).ToList());
        }
    }

}

