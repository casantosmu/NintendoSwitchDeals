namespace NintendoSwitchDeals.Scraper.Domain;

public class GameDiscountPrice
{
    public decimal Amount { get; set; }

    public DateTimeOffset StartDateTime { get; set; }

    public DateTimeOffset EndDateTime { get; set; }
}