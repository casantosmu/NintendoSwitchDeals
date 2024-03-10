using NintendoSwitchDeals.Scraper.Domain;

namespace NintendoSwitchDeals.Scraper.Services.GameService;

public interface IGameService
{
    Task<List<Game>> GetGames();

    Task<bool> GameExists(Game game);
}