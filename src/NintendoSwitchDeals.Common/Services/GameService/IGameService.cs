using NintendoSwitchDeals.Common.Domain;

namespace NintendoSwitchDeals.Common.Services.GameService;

public interface IGameService
{
    Task<List<Game>> GetGames();

    Task<bool> GameExists(Game game);
}