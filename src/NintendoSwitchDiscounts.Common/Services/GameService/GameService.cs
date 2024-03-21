using Microsoft.EntityFrameworkCore;

using NintendoSwitchDiscounts.Common.Data;
using NintendoSwitchDiscounts.Common.Domain;

namespace NintendoSwitchDiscounts.Common.Services.GameService;

public class GameService(NintendoSwitchDiscountsContext context) : IGameService
{
    public async Task<List<Game>> GetGames()
    {
        List<Models.Game> listAsync = await context.Games.ToListAsync();
        return listAsync.Select(MapGameModelToDomain).ToList();
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