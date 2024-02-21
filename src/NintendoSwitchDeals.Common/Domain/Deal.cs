namespace NintendoSwitchDeals.Common.Domain;

public class Deal()
{
    public int Id { get; set; }

    public required Price RegularPrice { get; init; }

    public required Price DiscountPrice { get; init; }

    public decimal DiscountPercentage
    {
        get
        {
            decimal discount = RegularPrice.Amount - DiscountPrice.Amount;
            return discount / RegularPrice.Amount * 100;
        }
    }

    public required Game Game { get; init; }
}