using System.Globalization;

namespace NintendoSwitchDeals.Scraper.Domain;

public class GameDiscountPrice
{
    public decimal Amount { get; init; }

    public DateTime StartDateTime { get; init; }

    public DateTime EndDateTime { get; init; }

    public override string ToString()
    {
        return $"{Amount.ToString(CultureInfo.CurrentCulture)}€";
    }
}