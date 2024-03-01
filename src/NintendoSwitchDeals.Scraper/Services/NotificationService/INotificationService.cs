using NintendoSwitchDeals.Scraper.Domain;

namespace NintendoSwitchDeals.Scraper.Services.NotificationService;

public interface INotificationService
{
    Task PublishGameDiscount(GameDiscount gameDiscount);
}