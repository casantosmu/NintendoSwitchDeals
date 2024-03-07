// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using NintendoSwitchDeals.Scraper.Data;
using NintendoSwitchDeals.Scraper.Domain;
using NintendoSwitchDeals.Scraper.Services.GameService;
using NintendoSwitchDeals.Scraper.Services.NintendoService;
using NintendoSwitchDeals.Scraper.Services.NotificationService;

using Game = NintendoSwitchDeals.Scraper.Domain.Game;

ServiceCollection services = [];
services.AddLogging(builder => builder.AddConsole());
services.AddDbContext<ScraperContext>();
services.AddTransient<INotificationService, NotificationService>();
services.AddTransient<IGameService, GameService>();
services.AddTransient<INintendoService, NintendoService>();
IServiceProvider serviceProvider = services.BuildServiceProvider();

INotificationService notificationService = serviceProvider.GetRequiredService<INotificationService>();
IGameService gameService = serviceProvider.GetRequiredService<IGameService>();
INintendoService nintendoService = serviceProvider.GetRequiredService<INintendoService>();

List<Game> games = await gameService.GetGames();

IEnumerable<GameDiscount> gamesWithDiscount =
    await nintendoService.GetGamesWithDiscount(games);

foreach (GameDiscount gameDiscount in gamesWithDiscount)
{
    bool shouldNotifyGameDiscount = await notificationService.ShouldNotifyGameDiscount(gameDiscount);

    if (shouldNotifyGameDiscount)
    {
        await notificationService.PublishGameDiscount(gameDiscount);
    }
}