namespace NintendoSwitchDeals.Scraper.Domain;

public class Game
{
    public long GameId { get; init; }

    public required string Name { get; init; }

    public decimal ThresholdPrice { get; init; }

    public Uri Url => new($"https://ec.nintendo.com/ES/es/titles/{GameId}");
}