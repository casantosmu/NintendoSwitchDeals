using NintendoSwitchDiscounts.Common.Domain;

namespace NintendoSwitchDiscounts.Common.Services.NintendoService;

public interface INintendoService
{
    Task<List<GameDiscount>> GetGamesWithDiscount(IEnumerable<Game> games);

    Task<List<Game>> GetMyWishlist();
}