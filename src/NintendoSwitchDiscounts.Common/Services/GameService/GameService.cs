using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using NintendoSwitchDiscounts.Common.Data;
using NintendoSwitchDiscounts.Common.Domain;

namespace NintendoSwitchDiscounts.Common.Services.GameService;

public class GameService(NintendoSwitchDiscountsContext context, ILogger<GameService> logger) : IGameService
{
    public async Task<List<Game>> GetGames()
    {
        List<Game> games = (await context.Games.ToListAsync()).Select(MapGameModelToDomain).ToList();
        logger.LogDebug("Successfully retrieved {Count} games from the database.", games.Count);
        return games;
    }

    public async Task<bool> GameExists(Game game)
    {
        return await context.Games.AnyAsync(
            g => g.GameId == game.GameId);
    }

    public async Task AddGame(Game game)
    {
        await context.Games.AddAsync(new Models.Game { GameId = game.GameId, Name = game.Name });
        await context.SaveChangesAsync();
    }

    private static Game MapGameModelToDomain(Models.Game game)
    {
        return new Game { GameId = game.GameId, Name = game.Name };
    }
}