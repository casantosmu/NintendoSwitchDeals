using System.Text.Json;

using NintendoSwitchDeals.Scraper.Domain;

namespace NintendoSwitchDeals.Scraper.Services.NintendoService;

public class NintendoService: INintendoService
{
    private readonly HttpClient _client = new() { BaseAddress = new Uri("https://api.ec.nintendo.com") };

    public async Task<IEnumerable<GameDiscount>> GetGamesWithDiscount(List<Game> games)
    {
        IEnumerable<long> gameIds = games.Select(g => g.GameId);

        await using Stream stream =
            await _client.GetStreamAsync($"/v1/price?country=ES&lang=es&ids={string.Join(",", gameIds)}");

        PricesDto? response =
            await JsonSerializer.DeserializeAsync<PricesDto>(stream);

        if (response is null)
        {
            throw new Exception("Response deserialization failed.");
        }

        List<GameDiscount> gamesWithDiscount = [];

        foreach (Game game in games)
        {
            PriceDto price = response.Prices.First(p => p.TitleId == game.GameId);

            if (price.DiscountPrice is null)
            {
                continue;
            }

            GameRegularPrice regularPrice =
                new() { Amount = Decimal.Parse(price.RegularPrice.RawValue.Replace(".", ",")) };

            GameDiscountPrice discountPrice = new()
            {
                Amount = Decimal.Parse(price.DiscountPrice.RawValue.Replace(".", ",")),
                StartDateTime = price.DiscountPrice.StartDateTime,
                EndDateTime = price.DiscountPrice.EndDateTime
            };

            GameDiscount gameDiscount =
                new() { Game = game, RegularPrice = regularPrice, DiscountPrice = discountPrice };

            gamesWithDiscount.Add(gameDiscount);
        }

        return gamesWithDiscount;
    }
}