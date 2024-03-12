using System.Text.Json;

using Microsoft.Extensions.Logging;

using NintendoSwitchDeals.Scraper.Domain;

namespace NintendoSwitchDeals.Scraper.Services.NintendoService;

public class NintendoService(ILogger<NintendoService> logger) : INintendoService
{
    private static readonly HttpClient HttpClient = new() { BaseAddress = new Uri("https://api.ec.nintendo.com") };

    private readonly JsonSerializerOptions _jsonSerializerOptions =
        new() { PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower };

    public async Task<IEnumerable<GameDiscount>> GetGamesWithDiscount(List<Game> games)
    {
        IEnumerable<long> gameIds = games.Select(g => g.GameId);

        using HttpResponseMessage response =
            await HttpClient.GetAsync($"/v1/price?country=ES&lang=es&ids={string.Join(",", gameIds)}");

        response.EnsureSuccessStatusCode();

        Stream stream = await response.Content.ReadAsStreamAsync();

        PricesDto? pricesDto =
            await JsonSerializer.DeserializeAsync<PricesDto>(stream, _jsonSerializerOptions);

        if (pricesDto is null)
        {
            throw new Exception("Response deserialization failed.");
        }

        List<GameDiscount> gamesWithDiscount = [];

        foreach (Game game in games)
        {
            PriceDto price = pricesDto.Prices.First(p => p.TitleId == game.GameId);

            if (price.DiscountPrice is null)
            {
                logger.LogDebug("Game fetched without a discount price: {game}", game);
                continue;
            }

            GameRegularPrice regularPrice =
                new() { Amount = Decimal.Parse(price.RegularPrice.RawValue.Replace(".", ",")) };

            GameDiscountPrice discountPrice = new()
            {
                Amount = Decimal.Parse(price.DiscountPrice.RawValue.Replace(".", ",")),
                StartDateTime = price.DiscountPrice.StartDatetime,
                EndDateTime = price.DiscountPrice.EndDatetime
            };

            GameDiscount gameDiscount =
                new() { Game = game, RegularPrice = regularPrice, DiscountPrice = discountPrice };

            logger.LogDebug("Game fetched with a discount price: {gameDiscount}", gameDiscount);

            gamesWithDiscount.Add(gameDiscount);
        }

        return gamesWithDiscount;
    }
}