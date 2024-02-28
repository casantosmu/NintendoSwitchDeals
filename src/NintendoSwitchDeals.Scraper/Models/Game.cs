using System.ComponentModel.DataAnnotations;

namespace NintendoSwitchDeals.Scraper.Models;

public class Game
{
    public long GameId { get; set; }

    [StringLength(200)] public required string Name { get; set; }

    public decimal ThresholdPrice { get; set; }

    [StringLength(200)] public required Uri Url { get; set; }

    public required ICollection<Notification> Notifications { get; set; }
}