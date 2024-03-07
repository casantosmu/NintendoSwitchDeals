namespace NintendoSwitchDeals.Scraper.Domain;

public class Game
{
    public long GameId { get; init; }

    public required string Name { get; init; }

    public decimal ThresholdPrice { get; init; }

    public required Uri Url { get; init; }
}