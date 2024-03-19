using NintendoSwitchDiscounts.Common.Domain;

namespace NintendoSwitchDiscounts.Common.Exceptions;

public class NotificationConflictException(GameDiscount gameDiscount)
    : Exception($"'{gameDiscount}' has already been notified.");