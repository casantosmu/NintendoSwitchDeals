using NintendoSwitchDeals.Scraper.Domain;

namespace NintendoSwitchDeals.Scraper.Exceptions;

public class NotificationConflict(GameDiscount gameDiscount) : Exception(
    $"A notification for the game '{gameDiscount.Game.Name}' (ID: {gameDiscount.Game.GameId}) already exists.\n" +
    $"Discount amount: {gameDiscount.DiscountPrice.Amount}\n" +
    $"Discount end date: {gameDiscount.DiscountPrice.EndDateTime}");