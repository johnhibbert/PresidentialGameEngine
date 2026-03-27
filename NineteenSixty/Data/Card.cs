//using PresidentialGameEngine.ClassLibrary.Data;
//using PresidentialGameEngine.ClassLibrary.Engines;
//using PresidentialGameEngine.ClassLibrary.Enums;
using PresidentialGameEngine.ClassLibrary.Interfaces;

using NineteenSixty.Enums;
using NineteenSixty.Interfaces;


namespace NineteenSixty.Data;

public class Card : 
    ICardWithCoupledCampaignPointsAndRestValues,
    ICardWithEventType<EventType>,
    //Card with action removed temporarily
    //ICardWithAction<TPlayer, TIssue, TState, TRegion>,
    ICardWithAffiliation<Affiliation>,
    ICardWithState<State>,
    ICardWithIssue<Issue>,
    ICardWithEffect

//ITypicalCard<Player,Issue,State,Region,Affiliation,EventType>, ICard
{
    public int Index { get; init; }
    public required string Text { get; init; }
    public required string Title { get; init; }

    public int CampaignPoints { get; init; }
    public int RestCubes => 4 - CampaignPoints;
    public EventType EventType { get; init; }
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

    public Predicate<ActionPlan> AreChangesValid { get; init; }
    public Action<ActionPlan, Player> Event { get; init; }
    public bool RequiresPlayerInput { get; init; }
    
};

public interface ICardWithEffect
{
    public Predicate<ActionPlan> AreChangesValid { get; init; }
    public Action<ActionPlan, Player> Event{ get; init; }
    public bool RequiresPlayerInput { get; init; }
}

