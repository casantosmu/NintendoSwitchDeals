using NintendoSwitchDeals.Scraper.Domain;

namespace NintendoSwitchDeals.Scraper.Services.NintendoService;

public interface INintendoService
{
    Task<IEnumerable<GameDiscount>> GetGamesWithDiscount(List<Game> games);
}