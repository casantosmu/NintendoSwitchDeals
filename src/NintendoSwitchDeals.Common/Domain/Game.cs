namespace NintendoSwitchDeals.Common.Domain;

public class Game()
{
    public int Id { get; set; }

    public required string Name { get; init; }

    public required string Link { get; init; }
}