namespace NintendoSwitchDeals.Common.Domain;

public class Price()
{
    public decimal Amount { get; init; }

    public required Currency Currency { get; init; }
}