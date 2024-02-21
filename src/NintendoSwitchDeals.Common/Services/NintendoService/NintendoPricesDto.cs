using System.Text.Json.Serialization;

namespace NintendoSwitchDeals.Common.Services.NintendoService;

public record NintendoPricesDto(Price[] Prices);

public record Price(
    [property: JsonPropertyName("title_id")]
    long NintendoId,
    [property: JsonPropertyName("sales_status")]
    string SalesStatus,
    [property: JsonPropertyName("regular_price")]
    RegularPrice RegularPrice,
    [property: JsonPropertyName("discount_price")]
    DiscountPrice? DiscountPrice
);

public record DiscountPrice(
    [property: JsonPropertyName("raw_value")]
    decimal Amount,
    string Currency,
    [property: JsonPropertyName("start_datetime")]
    DateTime StartDatetime,
    [property: JsonPropertyName("end_datetime")]
    DateTime EndDatetime);

public record RegularPrice(
    [property: JsonPropertyName("raw_value")]
    decimal Amount,
    string Currency
);