namespace NintendoSwitchDeals.Scraper.Domain;

public class GameDiscountPrice
{
    public decimal Amount { get; init; }

    public DateTimeOffset StartDateTime { get; init; }

    public DateTimeOffset EndDateTime { get; init; }
}