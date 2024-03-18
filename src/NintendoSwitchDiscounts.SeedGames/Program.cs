using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using NintendoSwitchDiscounts.Common.Data;
using NintendoSwitchDiscounts.Common.Domain;
using NintendoSwitchDiscounts.Common.Options;
using NintendoSwitchDiscounts.Common.Services.GameService;
using NintendoSwitchDiscounts.Common.Services.NintendoService;

HostApplicationBuilder builder = Host.CreateApplicationBuilder();

builder.Services.Configure<NintendoOptions>(builder.Configuration.GetRequiredSection(nameof(NintendoOptions)));

builder.Services.AddSingleton<NintendoSwitchDiscountsContext>();
builder.Services.AddTransient<IGameService, GameService>();
builder.Services.AddTransient<INintendoService, NintendoService>();

IHost host = builder.Build();

NintendoSwitchDiscountsContext context = host.Services.GetRequiredService<NintendoSwitchDiscountsContext>();
IGameService gameService = host.Services.GetRequiredService<IGameService>();
INintendoService nintendoService = host.Services.GetRequiredService<INintendoService>();

IEnumerable<Game> games = await nintendoService.GetMyWishlist();

foreach (Game game in games)
{
    bool gameExists = await gameService.GameExists(game);
    if (!gameExists)
    {
        await gameService.AddGame(game);
    }
}