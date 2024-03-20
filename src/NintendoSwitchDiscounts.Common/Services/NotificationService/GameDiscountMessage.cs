using NintendoSwitchDiscounts.Common.Domain;

namespace NintendoSwitchDiscounts.Common.Services.NotificationService;

public class GameDiscountMessage
{
    public GameDiscountMessage(GameDiscount gameDiscount)
    {
        string discountPercentageText = $"{Math.Round(gameDiscount.DiscountPercentage)}%";

        Subject =
            $"{discountPercentageText} on {gameDiscount.Game.Name}!";

        Message =
            $"You can now get {discountPercentageText} on {gameDiscount.Game.Name}.\n" +
            $"Original Price: {gameDiscount.RegularPrice.Amount} €\n" +
            $"Discounted Price: {gameDiscount.DiscountPrice.Amount} €\n" +
            $"Offer starts at: {gameDiscount.DiscountPrice.StartDateTime.Date.ToShortDateString()}\n" +
            $"Offer ends at: {gameDiscount.DiscountPrice.EndDateTime.Date.ToShortDateString()}\n" +
            $"Link to the game: {gameDiscount.Game.Url}";
    }

    public string Subject { get; }
    public string Message { get; }

    public override string ToString()
    {
        return $"Subject: {Subject}\nMessage:{Message}";
    }
}