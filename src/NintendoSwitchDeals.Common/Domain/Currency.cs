namespace NintendoSwitchDeals.Common.Domain;

public class Currency()
{
    public CurrencyCode Code { get; init; }

    public string Symbol =>
        Code switch
        {
            CurrencyCode.Eur => "€",
            _ => throw new ArgumentOutOfRangeException()
        };
}