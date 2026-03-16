using PresidentialGameEngine.ClassLibrary.Data;
using PresidentialGameEngine.ClassLibrary.Engines;
using PresidentialGameEngine.ClassLibrary.Enums;

namespace PresidentialGameEngine.ClassLibrary.Interfaces
{
    public interface ICard
    {
        Affiliation Affiliation { get; init; }
        Predicate<PlayerChosenChanges<Player, Issue, State, Region>> AreChangesValid { get; init; }
        int CampaignPoints { get; init; }
        Action<NineteenSixtyGameEngine, Player, PlayerChosenChanges<Player, Issue, State, Region>> Event { get; init; }
        EventType EventType { get; init; }
        int Index { get; init; }
        Issue Issue { get; init; }
        bool RequiresPlayerInput { get; init; }
        int RestCubes { get; }
        State State { get; init; }
        string Text { get; init; }
        string Title { get; init; }

        string ToLongString();
        string ToString();
    }
    
    /// <summary>
    /// ITypicalCard represents the properties contained on a card from in 1960
    /// </summary>
    /// <typeparam name="TPlayer">The player enumeration</typeparam>
    /// <typeparam name="TIssue">The issue enumeration</typeparam>
    /// <typeparam name="TState">The state enumeration</typeparam>
    /// <typeparam name="TRegion">The region enumeration</typeparam>
    /// <typeparam name="TAffiliation">The affiliation enumeration</typeparam>
    /// <typeparam name="TEventType">The event type enumeration</typeparam>
    public interface ITypicalCard<TPlayer, TIssue, TState, TRegion, TAffiliation, TEventType> :
        ICardWithCoupledCampaignPointsAndRestValues,
        ICardWithEventType<TEventType>,
        ICardWithAction<TPlayer, TIssue, TState, TRegion>,
        ICardWithAffiliation<TAffiliation>,
        ICardWithState<TState>,
        ICardWithIssue<TIssue>
        where TPlayer : Enum
        where TIssue : Enum
        where TState : Enum
        where TRegion : Enum
        where TAffiliation : Enum
        where TEventType : Enum
    {
        
    }
    
    /// <summary>
    /// IBasicGameCard represents the simplest and most basic parts of a card.
    /// </summary>
    public interface IBasicGameCard
    {
        int Index { get; init; }

        string Text { get; init; }
        string Title { get; init; }

        string ToLongString();
        string ToString();
    }

    /// <summary>
    /// ICardWithCoupledCampaignPointsAndRestValues represents cards where the campaign points
    /// and rest values are related.
    /// In 1960, all cards sum to 4 between rest and CP.
    /// </summary>
    public interface ICardWithCoupledCampaignPointsAndRestValues : IBasicGameCard
    {
        int CampaignPoints { get; init; }
        int RestCubes { get; }
    }
    
    
    /// <summary>
    /// This represents cards that have an effect when played.
    /// This is likely to be all cards, but we might change the method signatures some day.
    /// </summary>
    /// <typeparam name="TPlayer">The player enumeration</typeparam>
    /// <typeparam name="TIssue">The issue enumeration</typeparam>
    /// <typeparam name="TState">The state enumeration</typeparam>
    /// <typeparam name="TRegion">The region enumeration</typeparam>
    public interface ICardWithAction<TPlayer, TIssue, TState, TRegion> : IBasicGameCard
        where TPlayer : Enum
        where TIssue : Enum
        where TState : Enum
        where TRegion : Enum
    {
        bool RequiresPlayerInput { get; init; }
        Predicate<PlayerChosenChanges<TPlayer, TIssue, TState, TRegion>> AreChangesValid { get; init; }
        Action<NineteenSixtyGameEngine, TPlayer, PlayerChosenChanges<TPlayer, TIssue, TState, TRegion>> Event { get; init; }
    }
 
    /// <summary>
    /// ICardWithEventType represents when cards have a special type/time to be played/triggered.
    /// These are directly reflected in the card text.
    /// Example: In 1960, Harvard Brain trust [35] is a Debate Event.
    /// </summary>
    /// <typeparam name="TEventType"></typeparam>
    public interface ICardWithEventType<TEventType>  : IBasicGameCard
        where TEventType : Enum
    {
        TEventType EventType { get; init; }
    }
    
    /// <summary>
    /// ICardWithAffiliation represents when cards can have an affiliation symbol, even if blank or both.
    /// These do not directly relate to the card text, but often reflect which player benefits from a card.
    /// Example: In 1960, New England [6] has the Democrat Donkey symbol.
    /// </summary>
    /// <typeparam name="TAffiliation"></typeparam>
    public interface ICardWithAffiliation<TAffiliation>  : IBasicGameCard 
        where TAffiliation : Enum
    {
        TAffiliation Affiliation { get; init; }
    }
    
    /// <summary>
    /// ICardWithState represents when cards can have a state symbol, even if blank.
    /// These symbols do not relate to the card text.
    /// Example: In 1960, New England [6] has 'Louisiana'
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    public interface ICardWithState<TState>  : IBasicGameCard 
        where TState : Enum
    {
        TState State { get; init; }
    }

    /// <summary>
    /// ICardWithIssue represents when cards can have an issue symbol, even if blank.
    /// These symbols do not relate to the card text.
    /// Example: In 1960, New England [6] has 'Defense'
    /// </summary>
    /// <typeparam name="TIssue"></typeparam>
    public interface ICardWithIssue<TIssue>  : IBasicGameCard 
        where TIssue : Enum
    {
        TIssue Issue { get; init; }
    }
    
}


