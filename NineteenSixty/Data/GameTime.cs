using NineteenSixty.Enums;

namespace NineteenSixty.Data;

public class GameTime
{
    public int TurnNumber { get; init; }
    
    public Phase CurrentPhase { get; init; }
    
    public int ActivityPhaseNumber { get; init; }
    
    public Player FirstPlayer { get; init; }
    
    public Player ActivePlayer { get; init; }
}