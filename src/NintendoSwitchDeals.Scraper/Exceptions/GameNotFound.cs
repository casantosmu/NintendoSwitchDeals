using NintendoSwitchDeals.Scraper.Domain;

namespace NintendoSwitchDeals.Scraper.Exceptions;

public class GameNotFound(Game game) : Exception($"Game '{game.Name}' (ID: {game.GameId}) does not exists");