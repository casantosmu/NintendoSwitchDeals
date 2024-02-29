namespace NintendoSwitchDeals.Scraper.Domain;

public class GameDiscount
{
    public required Game Game { get; set; }

    public required GameRegularPrice RegularPrice { get; set; }

    public required GameDiscountPrice DiscountPrice { get; set; }

    public decimal DiscountPercentage
    {
        get
        {
            decimal discount = RegularPrice.Amount - DiscountPrice.Amount;
            return discount / RegularPrice.Amount * 100;
        }
    }
}