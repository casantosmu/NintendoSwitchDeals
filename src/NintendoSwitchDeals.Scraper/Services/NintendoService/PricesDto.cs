namespace NintendoSwitchDeals.Scraper.Services.NintendoService;

public record PricesDto(IEnumerable<PriceDto> Prices);

public record PriceDto(long TitleId, RegularPriceDto RegularPrice, DiscountPriceDto? DiscountPrice);

public record RegularPriceDto(string RawValue);

public record DiscountPriceDto(
    string RawValue,
    DateTime StartDatetime,
    DateTime EndDatetime
);