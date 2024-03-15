using NintendoSwitchDiscounts.Common.Domain;

namespace NintendoSwitchDiscounts.Common.Exceptions;

public class NotificationConflict(GameDiscount gameDiscount)
    : Exception($"'{gameDiscount}' has already been notified.");