using NineteenSixty.Data;
using PresidentialGameEngine.ClassLibrary.Components;
using NineteenSixty.Enums;
using PresidentialGameEngine.ClassLibrary.Data;

namespace NineteenSixty.Tests.Fixtures;


public static class EngineFixtures
{
    public static Engine GetGameEngine() 
    {
        TestStubsFakesAndMocks.SeededRandomnessProviderForTesting seed = new();
        
        var momentumComponent = new AccumulatingComponent<Player>();
        var issueSupportComponent = new SupportComponent<Player, Leader, Issue>();
        var stateSupportComponent = new CarriableSupportComponent<Player, Leader, State>();
        var issuePositioningComponent = new PositioningComponent<Issue>();
        var politicalCapitalComponent = new PoliticalCapitalComponent<Player>(seed, 12);
        var playerLocationComponent = new PlayerLocationComponent<Player, State>(Manifest.PlayerStartingPositions);
        var restComponent = new AccumulatingComponent<Player>();
        var endorsementComponent = new SupportComponent<Player, Leader, Region>();
        var mediaSupportComponent = new SupportComponent<Player, Leader, Region>();
        var exhaustionComponent = new PlayerStatusComponent<Player, Status>();
        var cardZoneComponent = new CardZoneComponent<CardZone, Player, Data.Card>
            ([CardZone.Hand, CardZone.CampaignStrategy]);
        //var cardComponent = new CardComponent<Player, Card>(seed, Manifest.GMTCards);
        //var staticDataComponent = new StaticDataComponent<State, Player, Region>(Manifest.StateData);

        //        public StaticDataComponent(IDictionary<TState, ILocationData<TState, TPlayer, TRegion>> locationData)
        // {
        
        //Dictionary<State, StateData>
        
        //throw new NotImplementedException();

        return new Engine(momentumComponent, issueSupportComponent, stateSupportComponent,
            issuePositioningComponent, politicalCapitalComponent, playerLocationComponent,
            restComponent, endorsementComponent, mediaSupportComponent, exhaustionComponent,
            cardZoneComponent);

    }

    public static SetOfChanges EmptyChanges => GetEmptyChanges();

    private static SetOfChanges GetEmptyChanges() 
    {
        return new SetOfChanges();
    }

    public static SetOfChanges InvalidChanges => GetInvalidChanges();
    
    private static SetOfChanges GetInvalidChanges()
    {
        //We're sending these changes that are bound to be invalid
        //but only to the methods we expect to not use them.
        //This is to get the compiler to stop complaining about sending null

        SetOfChanges implausiblyLargeAndContradictoryChanges = new();

        var hugeKennedySupportInHawaii = new SupportChange<Player, State>(Player.Kennedy, State.HI, int.MaxValue);
        var nixonIsThereTooSupportInHawaii = new SupportChange<Player, State>(Player.Kennedy, State.HI, int.MaxValue);
        var changeInNoneIssue = new SupportChange<Player, Issue>(Player.Kennedy, Issue.None, int.MinValue);

        implausiblyLargeAndContradictoryChanges.StateChanges.Add(hugeKennedySupportInHawaii);
        implausiblyLargeAndContradictoryChanges.StateChanges.Add(nixonIsThereTooSupportInHawaii);
        implausiblyLargeAndContradictoryChanges.IssueChanges.Add(changeInNoneIssue);

        return implausiblyLargeAndContradictoryChanges;
    }
    
}