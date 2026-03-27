// ReSharper disable InconsistentNaming
using NineteenSixty.Data;
using NineteenSixty.Enums;
using NineteenSixty.Tests.Fixtures;
using PresidentialGameEngine.ClassLibrary.Data;

namespace NineteenSixty.Tests.Cards;

[TestClass]
public class LyndonJohnson_39_Tests
{
    //"The Kennedy player may add 2 state support in Texas and a total of 3 additional state support anywhere in the South, no more than 2 per state.  If the Kennedy candidate card is currently flipped to its Exhausted side, the Kennedy player may reclaim it face up."
    private const int CardIndex = 39;
    
    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void LyndonJohnson_39_TwoSupportAutomaticallyAddedToTexas(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        SetOfChanges playerChoices = new();
        var oneSupportInFlorida = new SupportChange<Player, State>(Player.Kennedy, State.FL, 1);
        var oneSupportInAlabama = new SupportChange<Player, State>(Player.Kennedy, State.AL, 1);
        var oneSupportInTennessee = new SupportChange<Player, State>(Player.Kennedy, State.TN, 1);
        
        playerChoices.StateChanges.Add(oneSupportInFlorida);
        playerChoices.StateChanges.Add(oneSupportInAlabama);
        playerChoices.StateChanges.Add(oneSupportInTennessee);

        var plan = new ActionPlan{Engine = engine, Changes = playerChoices};
        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(plan, player);

        Assert.AreEqual(2, engine.GetSupportAmount(State.TX));
    }
    
    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void LyndonJohnson_39_SupportAddedToOtherStates(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        SetOfChanges playerChoices = new();
        var oneSupportInFlorida = new SupportChange<Player, State>(Player.Kennedy, State.FL, 1);
        var oneSupportInAlabama = new SupportChange<Player, State>(Player.Kennedy, State.AL, 1);
        var oneSupportInTennessee = new SupportChange<Player, State>(Player.Kennedy, State.TN, 1);
        
        playerChoices.StateChanges.Add(oneSupportInFlorida);
        playerChoices.StateChanges.Add(oneSupportInAlabama);
        playerChoices.StateChanges.Add(oneSupportInTennessee);

        var plan = new ActionPlan{Engine = engine, Changes = playerChoices};
        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(plan, player);
        
        Assert.AreEqual(1, engine.GetSupportAmount(State.FL));
        Assert.AreEqual(1, engine.GetSupportAmount(State.AL));
        Assert.AreEqual(1, engine.GetSupportAmount(State.TN));
    }
    
    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void LyndonJohnson_39_AdditionalSupportCanBeAddedToTexas(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        SetOfChanges playerChoices = new();
        var twoMoreSupportInTexas = new SupportChange<Player, State>(Player.Kennedy, State.TX, 2);
        var oneSupportInAlabama = new SupportChange<Player, State>(Player.Kennedy, State.AL, 1);
        
        playerChoices.StateChanges.Add(twoMoreSupportInTexas);
        playerChoices.StateChanges.Add(oneSupportInAlabama);

        var plan = new ActionPlan{Engine = engine, Changes = playerChoices};
        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(plan, player);
        
        Assert.AreEqual(4, engine.GetSupportAmount(State.TX));
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void LyndonJohnson_39_ExhaustedKennedyBecomesReady(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        SetOfChanges playerChoices = new();
        var oneSupportInFlorida = new SupportChange<Player, State>(Player.Kennedy, State.FL, 1);
        var oneSupportInAlabama = new SupportChange<Player, State>(Player.Kennedy, State.AL, 1);
        var oneSupportInTennessee = new SupportChange<Player, State>(Player.Kennedy, State.TN, 1);

        playerChoices.StateChanges.Add(oneSupportInFlorida);
        playerChoices.StateChanges.Add(oneSupportInAlabama);
        playerChoices.StateChanges.Add(oneSupportInTennessee);

        var plan = new ActionPlan{Engine = engine, Changes = playerChoices};
        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(plan, player);

        Assert.AreEqual(Status.Ready, engine.GetGameState().PlayerStatuses[Player.Kennedy]);

    }
    
    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void LyndonJohnson_39_ReadyKennedyRemainsReady(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        SetOfChanges playerChoices = new();
        var oneSupportInFlorida = new SupportChange<Player, State>(Player.Kennedy, State.FL, 1);
        var oneSupportInAlabama = new SupportChange<Player, State>(Player.Kennedy, State.AL, 1);
        var oneSupportInTennessee = new SupportChange<Player, State>(Player.Kennedy, State.TN, 1);

        playerChoices.StateChanges.Add(oneSupportInFlorida);
        playerChoices.StateChanges.Add(oneSupportInAlabama);
        playerChoices.StateChanges.Add(oneSupportInTennessee);

        engine.UnexhaustPlayer(Player.Kennedy);

        var plan = new ActionPlan{Engine = engine, Changes = playerChoices};
        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(plan, player);

        Assert.AreEqual(Status.Ready, engine.GetGameState().PlayerStatuses[Player.Kennedy]);

    }

    [TestMethod]
    public void LyndonJohnson_39_FailsValidationIfNixonGains()
    {
        SetOfChanges playerChoices = new();
        var twoSupportInTexas = new SupportChange<Player, State>(Player.Kennedy, State.TX, 2);
        var oneSupportInAlabama = new SupportChange<Player, State>(Player.Kennedy, State.AL, 1);
        var oneSupportInTennessee = new SupportChange<Player, State>(Player.Kennedy, State.TN, 1);
        var invalidNixonSupport = new SupportChange<Player, State>(Player.Nixon, State.FL, 1);

        playerChoices.StateChanges.Add(twoSupportInTexas);
        playerChoices.StateChanges.Add(oneSupportInAlabama);
        playerChoices.StateChanges.Add(oneSupportInTennessee);
        playerChoices.StateChanges.Add(invalidNixonSupport);
        
        var plan = new ActionPlan{Engine = null, Changes = playerChoices};
        var sut = Manifest.GMTCards[CardIndex]; 
        var result = sut.AreChangesValid(plan);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void LyndonJohnson_39_FailsValidationIfIssueGains()
    {
        SetOfChanges playerChoices = new();

        var twoSupportInTexas = new SupportChange<Player, State>(Player.Kennedy, State.TX, 2);
        var oneSupportInFlorida = new SupportChange<Player, State>(Player.Nixon, State.FL, 1);
        var oneSupportInAlabama = new SupportChange<Player, State>(Player.Kennedy, State.AL, 1);
        var oneSupportInTennessee = new SupportChange<Player, State>(Player.Kennedy, State.TN, 1);
        var issueSupport = new SupportChange<Player, Issue>(Player.Kennedy, Issue.Defense, 1);

        playerChoices.StateChanges.Add(twoSupportInTexas);
        playerChoices.StateChanges.Add(oneSupportInFlorida);
        playerChoices.StateChanges.Add(oneSupportInAlabama);
        playerChoices.StateChanges.Add(oneSupportInTennessee);
        playerChoices.IssueChanges.Add(issueSupport);

        var plan = new ActionPlan{Engine = null, Changes = playerChoices};
        var sut = Manifest.GMTCards[CardIndex]; 
        var result = sut.AreChangesValid(plan);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void LyndonJohnson_39_FailsValidationIfGreaterThanThree()
    {
        SetOfChanges playerChoices = new();
        
        var twoSupportInFlorida = new SupportChange<Player, State>(Player.Kennedy, State.FL, 2);
        var twoSupportInAlabama = new SupportChange<Player, State>(Player.Kennedy, State.AL, 2);

        playerChoices.StateChanges.Add(twoSupportInFlorida);
        playerChoices.StateChanges.Add(twoSupportInAlabama);

        var plan = new ActionPlan{Engine = null, Changes = playerChoices};
        var sut = Manifest.GMTCards[CardIndex]; 
        var result = sut.AreChangesValid(plan);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void LyndonJohnson_39_FailsValidationIfStateNotInCorrectRegion()
    {
        SetOfChanges playerChoices = new();
        var oneSupportInFlorida = new SupportChange<Player, State>(Player.Kennedy, State.FL, 1);
        var oneSupportInAlabama = new SupportChange<Player, State>(Player.Kennedy, State.AL, 1);
        var invalidSupportInOhio = new SupportChange<Player, State>(Player.Kennedy, State.OH, 1);

        playerChoices.StateChanges.Add(oneSupportInFlorida);
        playerChoices.StateChanges.Add(oneSupportInAlabama);
        playerChoices.StateChanges.Add(invalidSupportInOhio);

        var plan = new ActionPlan{Engine = null, Changes = playerChoices};
        var sut = Manifest.GMTCards[CardIndex]; 
        var result = sut.AreChangesValid(plan);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void LyndonJohnson_39_FailsValidationIfAnyNegativeValues()
    {
        SetOfChanges playerChoices = new();
        
        var oneSupportInFlorida = new SupportChange<Player, State>(Player.Kennedy, State.FL, 2);
        var oneSupportInAlabama = new SupportChange<Player, State>(Player.Kennedy, State.AL, 1);
        var invalidNegativeSupport = new SupportChange<Player, State>(Player.Kennedy, State.TN, -1);

        playerChoices.StateChanges.Add(oneSupportInFlorida);
        playerChoices.StateChanges.Add(oneSupportInAlabama);
        playerChoices.StateChanges.Add(invalidNegativeSupport);

        var plan = new ActionPlan{Engine = null, Changes = playerChoices};
        var sut = Manifest.GMTCards[CardIndex]; 
        var result = sut.AreChangesValid(plan);
        Assert.IsFalse(result);
    }
    
}