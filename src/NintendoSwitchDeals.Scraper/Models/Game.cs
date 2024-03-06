using System.ComponentModel.DataAnnotations;

namespace NintendoSwitchDeals.Scraper.Models;

public class Game
{
    public long GameId { get; set; }

    public required string Name { get; set; }

    public decimal ThresholdPrice { get; set; }

    public required Uri Url { get; set; }

    public IEnumerable<Notification> Notifications { get; } = new List<Notification>();
}