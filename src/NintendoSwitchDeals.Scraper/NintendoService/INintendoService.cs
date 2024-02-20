namespace NintendoSwitchDeals.Scraper.NintendoService;

public interface INintendoService
{
    Task<NintendoPricesDto> GetPrices(IEnumerable<long> ids);
}