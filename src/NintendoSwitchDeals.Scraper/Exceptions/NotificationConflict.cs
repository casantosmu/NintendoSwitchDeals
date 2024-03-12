using NintendoSwitchDeals.Scraper.Domain;

namespace NintendoSwitchDeals.Scraper.Exceptions;

public class NotificationConflict(GameDiscount gameDiscount)
    : Exception($"'{gameDiscount}' has already been notified.");