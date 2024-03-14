using Microsoft.EntityFrameworkCore;

using NintendoSwitchDeals.Common.Data;
using NintendoSwitchDeals.Common.Domain;

namespace NintendoSwitchDeals.Common.Services.GameService;

public class GameService(ScraperContext scraperContext) : IGameService
{
    public async Task<List<Game>> GetGames()
    {
        List<Models.Game> listAsync = await scraperContext.Games.ToListAsync();
        return listAsync.Select(MapGameModelToDomain).ToList();
    }

    public async Task<bool> GameExists(Game game)
    {
        return await scraperContext.Games.AnyAsync(
            g => g.GameId == game.GameId);
    }

    private static Game MapGameModelToDomain(Models.Game game)
    {
        return new Game { GameId = game.GameId, Name = game.Name, ThresholdPrice = game.ThresholdPrice };
    }
}