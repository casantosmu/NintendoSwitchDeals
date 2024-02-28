using System.Text.Json.Serialization;

namespace NintendoSwitchDeals.Scraper.Services.NintendoService;

public class PricesDto
{
    [JsonPropertyName("prices")] public required IEnumerable<PriceDto> Prices { get; set; }
}

public class PriceDto
{
    [JsonPropertyName("title_id")] public long TitleId { get; set; }

    [JsonPropertyName("sales_status")] public required string SalesStatus { get; set; }

    [JsonPropertyName("regular_price")] public required RegularPriceDto RegularPrice { get; set; }

    [JsonPropertyName("discount_price")] public DiscountPriceDto? DiscountPrice { get; set; }
}

public class RegularPriceDto
{
    [JsonPropertyName("amount")]
    public required string Amount { get; set; }

    [JsonPropertyName("currency")]
    public required string Currency { get; set; }

    [JsonPropertyName("raw_value")] public required string RawValue { get; set; }
}

public class DiscountPriceDto
{
    [JsonPropertyName("amount")]
    public required string Amount { get; set; }

    [JsonPropertyName("currency")]
    public required string Currency { get; set; }

    [JsonPropertyName("raw_value")] public required string RawValue { get; set; }

    [JsonPropertyName("start_datetime")] public DateTimeOffset StartDateTime { get; set; }

    [JsonPropertyName("end_datetime")] public DateTimeOffset EndDateTime { get; set; }
}