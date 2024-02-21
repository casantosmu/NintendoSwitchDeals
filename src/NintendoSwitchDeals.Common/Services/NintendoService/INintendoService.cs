namespace NintendoSwitchDeals.Common.Services.NintendoService;

public interface INintendoService
{
    Task<NintendoPricesDto> GetPrices(IEnumerable<long> ids);
}