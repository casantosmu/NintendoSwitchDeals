using NintendoSwitchDeals.Common.Domain;

namespace NintendoSwitchDeals.Common.Services.NintendoService;

public interface INintendoService
{
    Task<IEnumerable<GameDiscount>> GetGamesWithDiscount(List<Game> games);
}