using NintendoSwitchDiscounts.Common.Domain;

namespace NintendoSwitchDiscounts.Common.Services.NotificationService;

public interface INotificationService
{
    Task NotifyGameDiscount(GameDiscount gameDiscount);

    Task<bool> ShouldNotifyGameDiscount(GameDiscount gameDiscount);
}