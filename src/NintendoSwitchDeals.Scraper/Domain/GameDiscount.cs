namespace NintendoSwitchDeals.Scraper.Domain;

public class GameDiscount
{
    public required Game Game { get; init; }

    public required GameRegularPrice RegularPrice { get; init; }

    public required GameDiscountPrice DiscountPrice { get; init; }

    public decimal DiscountPercentage
    {
        get
        {
            decimal discount = RegularPrice.Amount - DiscountPrice.Amount;
            return discount / RegularPrice.Amount * 100;
        }
    }
}