namespace NintendoSwitchDiscounts.Common.Models;

public class Notification
{
    public int NotificationId { get; set; }

    public long GameId { get; set; }

    public decimal DiscountPrice { get; set; }

    public DateTime EndDateTime { get; set; }

    public DateTime PublishedAt { get; set; } = DateTime.Now;

    public Game Game { get; set; } = null!;
}