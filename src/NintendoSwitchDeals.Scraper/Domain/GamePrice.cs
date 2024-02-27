namespace NintendoSwitchDeals.Scraper.Domain;

public class GamePrice
{
    public required Game Game { get; set; }

    public required GameRegularPrice RegularPrice { get; set; }

    public GameDiscountPrice? DiscountPrice { get; set; }
}