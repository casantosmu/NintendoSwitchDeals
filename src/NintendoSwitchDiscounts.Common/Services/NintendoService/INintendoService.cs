using NintendoSwitchDiscounts.Common.Domain;

namespace NintendoSwitchDiscounts.Common.Services.NintendoService;

public interface INintendoService
{
    Task<IEnumerable<GameDiscount>> GetGamesWithDiscount(List<Game> games);
}