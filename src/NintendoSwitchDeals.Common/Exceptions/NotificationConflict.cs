using NintendoSwitchDeals.Common.Domain;

namespace NintendoSwitchDeals.Common.Exceptions;

public class NotificationConflict(GameDiscount gameDiscount)
    : Exception($"'{gameDiscount}' has already been notified.");