using System.Text.Json;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using NintendoSwitchDiscounts.Common.Domain;
using NintendoSwitchDiscounts.Common.Options;

namespace NintendoSwitchDiscounts.Common.Services.NintendoService;

public class NintendoService(ILogger<NintendoService> logger, IOptions<NintendoOptions> options) : INintendoService
{
    private const string V1BaseAddress = "https://api.ec.nintendo.com/v1";
    private const string BaseAddress = "https://ec.nintendo.com/api";

    private static readonly HttpClient HttpClient = new();

    private readonly JsonSerializerOptions _jsonSerializerOptions =
        new() { PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower };

    public async Task<IEnumerable<GameDiscount>> GetGamesWithDiscount(List<Game> games)
    {
        const int maxBatchSize = 50;

        List<Game> remainingGames = games.ToList();
        List<GameDiscount> gamesWithDiscount = [];

        while (remainingGames.Count != 0)
        {
            List<Game> currentBatchGames = remainingGames.Take(maxBatchSize).ToList();
            IEnumerable<long> currentBatchIds = currentBatchGames.Select(g => g.GameId);

            using HttpResponseMessage response =
                await HttpClient.GetAsync(
                    $"{V1BaseAddress}/price?country=ES&lang=es&ids={string.Join(",", currentBatchIds)}");

            LogHttpRequest(response.RequestMessage);
            response.EnsureSuccessStatusCode();

            Stream stream = await response.Content.ReadAsStreamAsync();

            PricesDto? pricesDto =
                await JsonSerializer.DeserializeAsync<PricesDto>(stream, _jsonSerializerOptions);

            if (pricesDto is null)
            {
                throw new Exception("Response deserialization failed.");
            }

            foreach (Game game in currentBatchGames)
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

            remainingGames = remainingGames.Skip(maxBatchSize).ToList();
        }

        return gamesWithDiscount;
    }

    public async Task<IEnumerable<Game>> GetMyWishlist()
    {
        HttpRequestMessage requestMessage =
            new(HttpMethod.Get, $"{BaseAddress}/my/wishlist?page=1&per=400");

        requestMessage.Headers.Add("Cookie", options.Value.Cookie);

        using HttpResponseMessage response = await HttpClient.SendAsync(requestMessage);

        LogHttpRequest(response.RequestMessage);
        response.EnsureSuccessStatusCode();

        Stream stream = await response.Content.ReadAsStreamAsync();

        MyWishlistDto? myWishlistDto =
            await JsonSerializer.DeserializeAsync<MyWishlistDto>(stream, _jsonSerializerOptions);

        if (myWishlistDto is null)
        {
            throw new Exception("Response deserialization failed.");
        }

        if (myWishlistDto.HasNextPage)
        {
            throw new NotImplementedException("Wishlist with more than one page are not implemented.");
        }

        List<string> gamesIds = myWishlistDto.Items.Select(item => item.Product.Id).ToList();
        return await GetGames(gamesIds);
    }

    private async Task<IEnumerable<Game>> GetGames(IEnumerable<string> ids)
    {
        const int maxBatchSize = 50;

        List<string> remainingIds = ids.ToList();
        List<Game> games = [];

        while (remainingIds.Count != 0)
        {
            List<string> currentBatchIds = remainingIds.Take(maxBatchSize).ToList();

            HttpRequestMessage requestMessage =
                new(HttpMethod.Get, $"{BaseAddress}/contents?id={string.Join("&id=", currentBatchIds)}");

            requestMessage.Headers.Add("Cookie", options.Value.Cookie);

            using HttpResponseMessage response = await HttpClient.SendAsync(requestMessage);

            LogHttpRequest(response.RequestMessage);
            response.EnsureSuccessStatusCode();

            Stream stream = await response.Content.ReadAsStreamAsync();

            ContentsDto? contentsDto =
                await JsonSerializer.DeserializeAsync<ContentsDto>(stream, _jsonSerializerOptions);

            if (contentsDto == null)
            {
                throw new Exception("Response deserialization failed.");
            }

            games.AddRange(contentsDto.Contents.Select(content =>
                new Game { GameId = content.Id, Name = content.FormalName }));

            remainingIds = remainingIds.Skip(maxBatchSize).ToList();
        }

        return games;
    }

    private void LogHttpRequest(HttpRequestMessage? requestMessage)
    {
        if (requestMessage is null)
        {
            return;
        }

        logger.LogDebug("{method} {uri} {version}",
            requestMessage.Method, requestMessage.RequestUri, requestMessage.Version);
    }
}