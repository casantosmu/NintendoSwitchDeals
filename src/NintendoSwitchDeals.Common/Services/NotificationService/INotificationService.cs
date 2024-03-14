using NintendoSwitchDeals.Common.Domain;

namespace NintendoSwitchDeals.Common.Services.NotificationService;

public interface INotificationService
{
    Task PublishGameDiscount(GameDiscount gameDiscount);

    Task<bool> ShouldNotifyGameDiscount(GameDiscount gameDiscount);
}