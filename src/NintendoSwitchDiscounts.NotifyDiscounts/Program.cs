﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using NintendoSwitchDiscounts.Common.Data;
using NintendoSwitchDiscounts.Common.Domain;
using NintendoSwitchDiscounts.Common.Options;
using NintendoSwitchDiscounts.Common.Services.GameService;
using NintendoSwitchDiscounts.Common.Services.NintendoService;
using NintendoSwitchDiscounts.Common.Services.NotificationService;

HostApplicationBuilder builder = Host.CreateApplicationBuilder();

builder.Services.Configure<NotificationOptions>(builder.Configuration.GetRequiredSection(nameof(NotificationOptions)));

builder.Services.AddSingleton<NintendoSwitchDiscountsContext>();
builder.Services.AddTransient<INotificationService, NotificationService>();
builder.Services.AddTransient<IGameService, GameService>();
builder.Services.AddTransient<INintendoService, NintendoService>();

using IHost host = builder.Build();

INotificationService notificationService = host.Services.GetRequiredService<INotificationService>();
IGameService gameService = host.Services.GetRequiredService<IGameService>();
INintendoService nintendoService = host.Services.GetRequiredService<INintendoService>();

List<Game> games = await gameService.GetGames();
IEnumerable<GameDiscount> gamesWithDiscount = await nintendoService.GetGamesWithDiscount(games);

foreach (GameDiscount gameDiscount in gamesWithDiscount)
{
    bool shouldNotifyGameDiscount = await notificationService.ShouldNotifyGameDiscount(gameDiscount);

    if (shouldNotifyGameDiscount)
    {
        await notificationService.NotifyGameDiscount(gameDiscount);
    }
}