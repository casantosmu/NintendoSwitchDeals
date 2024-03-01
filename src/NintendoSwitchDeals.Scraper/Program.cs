// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using NintendoSwitchDeals.Scraper.Domain;
using NintendoSwitchDeals.Scraper.Services.NintendoService;
using NintendoSwitchDeals.Scraper.Services.NotificationService;

ServiceCollection services = [];
services.AddLogging(builder => builder.AddConsole());
services.AddSingleton<NotificationService>();
services.AddSingleton<NintendoService>();
IServiceProvider serviceProvider = services.BuildServiceProvider();

NotificationService notificationService = serviceProvider.GetRequiredService<NotificationService>();
NintendoService nintendoService = serviceProvider.GetRequiredService<NintendoService>();

Game game1 = new()
{
    GameId = 70070000013727,
    Name = "Portal: colección complementaria",
    ThresholdPrice = 10,
    Url =
        "https://www.nintendo.es/Juegos/Programas-descargables-Nintendo-Switch/Portal-coleccion-complementaria-2168991.html"
};

Game game2 = new()
{
    GameId = 70010000063715,
    Name = "The Legend of Zelda: Tears of the Kingdom",
    ThresholdPrice = 40,
    Url =
        "https://www.nintendo.es/Juegos/Juegos-de-Nintendo-Switch/The-Legend-of-Zelda-Tears-of-the-Kingdom-1576884.html"
};

IEnumerable<GameDiscount> gamesWithDiscount =
    await nintendoService.GetGamesWithDiscount([game1, game2]);

foreach (GameDiscount gameDiscount in gamesWithDiscount)
{
    await notificationService.PublishGameDiscount(gameDiscount);
}