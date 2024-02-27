namespace NintendoSwitchDeals.Scraper.Models;

public class Notification
{
    public int NotificationId { get; set; }

    public long GameId { get; set; }

    public decimal DiscountPrice { get; set; }

    public DateTimeOffset EndDateTime { get; set; }

    public DateTimeOffset PublishedAt { get; set; }

    public required Game Game { get; set; }
}