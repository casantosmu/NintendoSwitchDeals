using System.Text.Json;

using NintendoSwitchDeals.Scraper.Domain;

namespace NintendoSwitchDeals.Scraper.Services.NintendoService;

public static class NintendoService
{
    private static readonly HttpClient Client = new() { BaseAddress = new Uri("https://api.ec.nintendo.com") };

    private static GamePrice MapPriceDtoToDomainGamePrice(PriceDto priceDto, Game game)
    {
        GameRegularPrice regularPrice =
            new() { Amount = Decimal.Parse(priceDto.RegularPrice.RawValue.Replace(".", ",")) };

        GameDiscountPrice? discountPrice = null;
        if (priceDto.DiscountPrice is not null)
        {
            discountPrice = new GameDiscountPrice
            {
                Amount = Decimal.Parse(priceDto.DiscountPrice.RawValue.Replace(".", ",")),
                StartDateTime = priceDto.DiscountPrice.StartDateTime,
                EndDateTime = priceDto.DiscountPrice.EndDateTime
            };
        }

        return new GamePrice { Game = game, RegularPrice = regularPrice, DiscountPrice = discountPrice };
    }

    public static async Task<IEnumerable<GamePrice>> GetGamesWithCurrentPrice(List<Game> games)
    {
        IEnumerable<long> gameIds = games.Select(g => g.GameId);

        await using Stream stream =
            await Client.GetStreamAsync($"/v1/price?country=ES&lang=es&ids={string.Join(",", gameIds)}");

        PricesDto? response =
            await JsonSerializer.DeserializeAsync<PricesDto>(stream);

        if (response is null)
        {
            throw new Exception("Response deserialization failed.");
        }

        return games.Select(g => MapPriceDtoToDomainGamePrice(response.Prices.First(p => p.TitleId == g.GameId), g));
    }
}