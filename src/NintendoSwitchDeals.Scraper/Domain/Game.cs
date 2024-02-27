namespace NintendoSwitchDeals.Scraper.Domain;

public class Game
{
    public long GameId { get; set; }

    public required string Name { get; set; }

    public decimal ThresholdPrice { get; set; }

    public required string Url { get; set; }
}