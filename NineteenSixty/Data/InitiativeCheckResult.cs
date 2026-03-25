using NineteenSixty.Enums;

namespace NineteenSixty.Data;

public class InitiativeCheckResult
{
    public required Dictionary<Player, int> CubesDrawn { get; init; }
    public required Player PlayerWithInitiative { get; init; }
}