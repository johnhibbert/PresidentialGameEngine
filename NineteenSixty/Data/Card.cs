using PresidentialGameEngine.ClassLibrary.Data;
using PresidentialGameEngine.ClassLibrary.Engines;
using PresidentialGameEngine.ClassLibrary.Enums;
using PresidentialGameEngine.ClassLibrary.Interfaces;

namespace NineteenSixty.Data;

public class Card : ITypicalCard<Player,Issue,State,Region,Affiliation,EventType>, ICard
{
    //<TPlayer, TIssue, TState, TRegion, TAffiliation, TEventType>     
    public int Index { get; init; }
    public required string Text { get; init; }
    public required string Title { get; init; }

    public int CampaignPoints { get; init; }
    public int RestCubes => 4 - CampaignPoints;
    public EventType EventType { get; init; }
    public bool RequiresPlayerInput { get; init; }
    public required Predicate<PlayerChosenChanges<Player, Issue, State, Region>> AreChangesValid { get; init; }
    public required Action<NineteenSixtyGameEngine, Player, PlayerChosenChanges<Player, Issue, State, Region>> Event
    {
        get;
        init;
    }

    public Affiliation Affiliation { get; init; }
    public State State { get; init; }
    public Issue Issue { get; init; }
    
    public override string ToString()
    {
        return $"{Title} [{Index}]";
    }

    public string ToLongString()
    {
        return $"{ToString()}: {Text}";
    }
};
    
