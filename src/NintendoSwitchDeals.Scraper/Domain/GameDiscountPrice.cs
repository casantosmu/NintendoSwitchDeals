namespace NintendoSwitchDeals.Scraper.Domain;

public class GameDiscountPrice
{
    public decimal Amount { get; init; }

    public DateTime StartDateTime { get; init; }

    public DateTime EndDateTime { get; init; }
}