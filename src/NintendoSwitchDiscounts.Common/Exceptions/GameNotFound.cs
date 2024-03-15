using NintendoSwitchDiscounts.Common.Domain;

namespace NintendoSwitchDiscounts.Common.Exceptions;

public class GameNotFound(Game game) : Exception($"Game {game} does not exists");