using System.Globalization;

namespace NintendoSwitchDeals.Common.Domain;

public class GameRegularPrice
{
    public decimal Amount { get; init; }

    public override string ToString()
    {
        return $"{Amount.ToString(CultureInfo.CurrentCulture)}€";
    }
}