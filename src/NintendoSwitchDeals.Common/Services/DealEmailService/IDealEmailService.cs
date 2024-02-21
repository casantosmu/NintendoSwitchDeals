using NintendoSwitchDeals.Common.Domain;

namespace NintendoSwitchDeals.Common.Services.DealEmailService;

public interface IDealEmailService
{
    Task PublishDeal(Deal deal);
}