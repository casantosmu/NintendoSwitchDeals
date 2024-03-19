using NintendoSwitchDiscounts.Common.Domain;

namespace NintendoSwitchDiscounts.Common.Exceptions;

public class GameNotFoundException(Game game) : Exception($"Game {game} does not exists");