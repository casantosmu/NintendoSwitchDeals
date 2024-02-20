// See https://aka.ms/new-console-template for more information

using Microsoft.EntityFrameworkCore;

using NintendoSwitchDeals.Common;
using NintendoSwitchDeals.Common.Models;
using NintendoSwitchDeals.Scraper.NintendoApi;

DealsContext dealsContext = new();

List<Deal> deals = await dealsContext.Deals.ToListAsync();

NintendoClient nintendoClient = new();
NintendoPricesDto response = await nintendoClient.GetPrices(deals.Select(deal => deal.NintendoId));

foreach (Price price in response.Prices)
{
    Deal? deal = deals.Find(deal => deal.NintendoId == price.NintendoId);

    if (deal is null)
    {
        throw new Exception($"Deal not found for {price}");
    }

    string message = price.DiscountPrice?.Amount <= deal.ThresholdPrice
        ? $"Discount OK {price}"
        : $"Discount NOT {price}";

    Console.WriteLine(message);
}