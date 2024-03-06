namespace NintendoSwitchDeals.Scraper.Models;

public class Notification
{
    public int NotificationId { get; set; }

    public long GameId { get; set; }

    public decimal DiscountPrice { get; set; }

    public DateTime EndDateTime { get; set; }

    public DateTime PublishedAt { get; set; }

    public Game Game { get; set; } = null!;
}