using PresidentialGameEngine.ClassLibrary.Components;
using NineteenSixty.Enums;

namespace NineteenSixty.Tests.Fixtures;


public static class EngineFixtures
{
    public static Engine GetGameEngine() 
    {
        TestStubsFakesAndMocks.SeededRandomnessProviderForTesting seed = new();
        
        /*
        ComponentCollection<Player, Leader, Issue, State, Region, Card> compColl = new()
        {
            MomentumComponent = new AccumulatingComponent<Player>(),
            IssueSupportComponent = new SupportComponent<Player, Leader, Issue>(),
            StateSupportComponent = new CarriableSupportComponent<Player, Leader, State>(),
            IssuePositioningComponent = new PositioningComponent<Issue>(),
            PoliticalCapitalComponent = new PoliticalCapitalComponent<Player>(seed, 12),
            PlayerLocationComponent = new PlayerLocationComponent<Player, State>(NineteenSixty.PlayerStartingPositions),
            RestComponent = new AccumulatingComponent<Player>(),
            EndorsementComponent = new SupportComponent<Player, Leader, Region>(),
            MediaSupportComponent = new SupportComponent<Player, Leader, Region>(),
            ExhaustionComponent = new ExhaustionComponent<Player>(),
            CardComponent = new CardComponent<Player, Card>(seed, NineteenSixty.GMTCards),
            StaticDataComponent = new StaticDataComponent<State, Player, Region>(NineteenSixty.StateData)
        };

        return new NineteenSixtyGameEngine(compColl);
        */

        var momentumComponent = new AccumulatingComponent<Player>();
        var issueSupportComponent = new SupportComponent<Player, Leader, Issue>();
        var stateSupportComponent = new CarriableSupportComponent<Player, Leader, State>();
        var issuePositioningComponent = new PositioningComponent<Issue>();
        var politicalCapitalComponent = new PoliticalCapitalComponent<Player>(seed, 12);
        var playerLocationComponent = new PlayerLocationComponent<Player, State>(Manifest.PlayerStartingPositions);
        var restComponent = new AccumulatingComponent<Player>();
        var endorsementComponent = new SupportComponent<Player, Leader, Region>();
        var mediaSupportComponent = new SupportComponent<Player, Leader, Region>();
        var exhaustionComponent = new ExhaustionComponent<Player>();
        //var cardComponent = new CardComponent<Player, Card>(seed, Manifest.GMTCards);
        //var staticDataComponent = new StaticDataComponent<State, Player, Region>(Manifest.StateData);

        //        public StaticDataComponent(IDictionary<TState, ILocationData<TState, TPlayer, TRegion>> locationData)
        // {
        
        //Dictionary<State, StateData>
        
        //throw new NotImplementedException();

        return new Engine(momentumComponent, issueSupportComponent, stateSupportComponent,
            issuePositioningComponent, politicalCapitalComponent, playerLocationComponent,
            restComponent, endorsementComponent, mediaSupportComponent, exhaustionComponent);

    }
    
}