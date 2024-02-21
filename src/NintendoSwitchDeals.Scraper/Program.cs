using Microsoft.EntityFrameworkCore;

using NintendoSwitchDeals.Common;
using NintendoSwitchDeals.Common.Domain;
using NintendoSwitchDeals.Common.Services.DealEmailService;
using NintendoSwitchDeals.Common.Services.NintendoService;

using Price = NintendoSwitchDeals.Common.Domain.Price;

DealsContext dealsContext = new();
NintendoService nintendoService = new();
DealEmailService dealEmailService = new(new AmazonSns());

List<NintendoSwitchDeals.Common.Models.Deal> deals = await dealsContext.Deals.ToListAsync();

NintendoPricesDto response = await nintendoService.GetPrices(deals.Select(deal => deal.NintendoId));

foreach (NintendoSwitchDeals.Common.Services.NintendoService.Price price in response.Prices)
{
    NintendoSwitchDeals.Common.Models.Deal? deal = deals.Find(deal => deal.NintendoId == price.NintendoId);

    if (deal is null)
    {
        throw new Exception($"Deal not found for {price}");
    }

    if (price.DiscountPrice?.Amount <= deal.ThresholdPrice)
    {
        Price regularPrice = new()
        {
            Amount = price.RegularPrice.Amount, Currency = new Currency { Code = CurrencyCode.Eur }
        };
        Price discountPrice = new()
        {
            Amount = price.DiscountPrice.Amount, Currency = new Currency { Code = CurrencyCode.Eur }
        };
        Deal domainDeal = new()
        {
            Id = 0,
            RegularPrice = regularPrice,
            DiscountPrice = discountPrice,
            Game = new Game { Id = 0, Name = deal.GameName, Link = deal.Link }
        };
        
        await dealEmailService.PublishDeal(domainDeal);
    }
}