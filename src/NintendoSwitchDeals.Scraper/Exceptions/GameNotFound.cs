using NintendoSwitchDeals.Scraper.Domain;

namespace NintendoSwitchDeals.Scraper.Exceptions;

public class GameNotFound(Game game) : Exception($"Game {game} does not exists");