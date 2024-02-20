using System.Collections.ObjectModel;

namespace NintendoSwitchDeals.Scraper.NintendoApi;

public interface INintendoClient
{
    Task<NintendoPricesDto> GetPrices(IEnumerable<long> ids);
}