using NintendoSwitchDeals.Common.Domain;

namespace NintendoSwitchDeals.Common.Exceptions;

public class GameNotFound(Game game) : Exception($"Game {game} does not exists");