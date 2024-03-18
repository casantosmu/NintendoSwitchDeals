using NintendoSwitchDiscounts.Common.Domain;

namespace NintendoSwitchDiscounts.Common.Services.GameService;

public interface IGameService
{
    Task<List<Game>> GetGames();

    Task<bool> GameExists(Game game);

    Task AddGame(Game game);
}