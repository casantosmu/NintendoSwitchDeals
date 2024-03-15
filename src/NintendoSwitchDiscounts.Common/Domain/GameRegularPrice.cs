using System.Globalization;

namespace NintendoSwitchDiscounts.Common.Domain;

public class GameRegularPrice
{
    public decimal Amount { get; init; }

    public override string ToString()
    {
        return $"{Amount.ToString(CultureInfo.CurrentCulture)}€";
    }
}