namespace NintendoSwitchDeals.Scraper.Models;

public class Game
{
    public long GameId { get; set; }

    public required string Name { get; set; }

    public decimal ThresholdPrice { get; set; }

    public IEnumerable<Notification> Notifications { get; } = new List<Notification>();
}