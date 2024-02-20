using System.Net.Http.Json;

namespace NintendoSwitchDeals.Scraper.NintendoService;

public class NintendoService : INintendoService
{
    private static readonly HttpClient HttpClient = new() { BaseAddress = new Uri("https://api.ec.nintendo.com") };

    public async Task<NintendoPricesDto> GetPrices(IEnumerable<long> ids)
    {
        using HttpResponseMessage response =
            await HttpClient.GetAsync($"/v1/price?country=ES&lang=es&ids={string.Join(",", ids)}");
        response.EnsureSuccessStatusCode();
        string str = await response.Content.ReadAsStringAsync();
        NintendoPricesDto? jsonResponse = await response.Content.ReadFromJsonAsync<NintendoPricesDto>();
        return jsonResponse ?? throw new Exception("Response deserialization failed.");
    }
}