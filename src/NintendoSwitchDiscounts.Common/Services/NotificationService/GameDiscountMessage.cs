using NintendoSwitchDiscounts.Common.Domain;

namespace NintendoSwitchDiscounts.Common.Services.NotificationService;

public class GameDiscountMessage
{
    public GameDiscountMessage(GameDiscount gameDiscount)
    {
        string discountPercentageText = $"{Math.Round(gameDiscount.DiscountPercentage)}%";

        Subject =
            $"{discountPercentageText} OFF {gameDiscount.Game.Name}!";

        Message =
            $"🎉 {discountPercentageText} OFF on {gameDiscount.Game.Name}! 🎉\n" +
            $"Check out this deal: {gameDiscount.Game.Url}\n\n" +
            $"Original Price: {gameDiscount.RegularPrice.Amount} €\n" +
            $"Discounted Price: {gameDiscount.DiscountPrice.Amount} €\n\n" +
            $"Offer starts: {gameDiscount.DiscountPrice.StartDateTime.Date.ToShortDateString()}\n" +
            $"Offer ends: {gameDiscount.DiscountPrice.EndDateTime.Date.ToShortDateString()}";
    }

    public string Subject { get; }
    public string Message { get; }

    public override string ToString()
    {
        return $"{nameof(Subject)}: {Subject}, {nameof(Message)}: {Message}";
    }
}