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
    public void LyndonJohnson_39_SupportAddedToStates(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        SetOfChanges playerChoices = new();
        var twoSupportInTexas = new SupportChange<Player, State>(Player.Kennedy, State.TX, 2);
        var oneSupportInFlorida = new SupportChange<Player, State>(Player.Kennedy, State.FL, 1);
        var oneSupportInAlabama = new SupportChange<Player, State>(Player.Kennedy, State.AL, 1);
        var oneSupportInTennessee = new SupportChange<Player, State>(Player.Kennedy, State.TN, 1);

        playerChoices.StateChanges.Add(twoSupportInTexas);
        playerChoices.StateChanges.Add(oneSupportInFlorida);
        playerChoices.StateChanges.Add(oneSupportInAlabama);
        playerChoices.StateChanges.Add(oneSupportInTennessee);

        var sut = Manifest.GMTCards[CardIndex];

        sut.Event(engine, player, playerChoices);

        Assert.AreEqual(2, engine.GetSupportAmount(State.TX));
        Assert.AreEqual(1, engine.GetSupportAmount(State.FL));
        Assert.AreEqual(1, engine.GetSupportAmount(State.AL));
        Assert.AreEqual(1, engine.GetSupportAmount(State.TN));
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void LyndonJohnson_39_ExhaustedKennedyBecomesReady(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        SetOfChanges playerChoices = new();
        var twoSupportInTexas = new SupportChange<Player, State>(Player.Kennedy, State.TX, 2);
        var oneSupportInFlorida = new SupportChange<Player, State>(Player.Kennedy, State.FL, 1);
        var oneSupportInAlabama = new SupportChange<Player, State>(Player.Kennedy, State.AL, 1);
        var oneSupportInTennessee = new SupportChange<Player, State>(Player.Kennedy, State.TN, 1);

        playerChoices.StateChanges.Add(twoSupportInTexas);
        playerChoices.StateChanges.Add(oneSupportInFlorida);
        playerChoices.StateChanges.Add(oneSupportInAlabama);
        playerChoices.StateChanges.Add(oneSupportInTennessee);

        engine.ExhaustPlayer(Player.Kennedy);

        var sut = Manifest.GMTCards[CardIndex];

        sut.Event(engine, player, playerChoices);

        Assert.AreEqual(Status.Ready, engine.GetGameState().PlayerStatuses[Player.Kennedy]);

    }
    
    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void LyndonJohnson_39_ReadyKennedyRemainsReady(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        SetOfChanges playerChoices = new();
        var twoSupportInTexas = new SupportChange<Player, State>(Player.Kennedy, State.TX, 2);
        var oneSupportInFlorida = new SupportChange<Player, State>(Player.Kennedy, State.FL, 1);
        var oneSupportInAlabama = new SupportChange<Player, State>(Player.Kennedy, State.AL, 1);
        var oneSupportInTennessee = new SupportChange<Player, State>(Player.Kennedy, State.TN, 1);

        playerChoices.StateChanges.Add(twoSupportInTexas);
        playerChoices.StateChanges.Add(oneSupportInFlorida);
        playerChoices.StateChanges.Add(oneSupportInAlabama);
        playerChoices.StateChanges.Add(oneSupportInTennessee);

        engine.UnexhaustPlayer(Player.Kennedy);

        var sut = Manifest.GMTCards[CardIndex];

        sut.Event(engine, player, playerChoices);

        Assert.AreEqual(Status.Ready, engine.GetGameState().PlayerStatuses[Player.Kennedy]);

    }

    [TestMethod]
    public void LyndonJohnson_39_FailsValidationIfNixonGains()
    {
        SetOfChanges playerChoices = new();
        var twoSupportInTexas = new SupportChange<Player, State>(Player.Kennedy, State.TX, 2);
        var oneSupportInFlorida = new SupportChange<Player, State>(Player.Nixon, State.FL, 1);
        var oneSupportInAlabama = new SupportChange<Player, State>(Player.Kennedy, State.AL, 1);
        var oneSupportInTennessee = new SupportChange<Player, State>(Player.Kennedy, State.TN, 1);

        playerChoices.StateChanges.Add(twoSupportInTexas);
        playerChoices.StateChanges.Add(oneSupportInFlorida);
        playerChoices.StateChanges.Add(oneSupportInAlabama);
        playerChoices.StateChanges.Add(oneSupportInTennessee);

        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(playerChoices);
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

        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(playerChoices);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void LyndonJohnson_39_FailsValidationIfGreaterThanOne()
    {
        SetOfChanges playerChoices = new();
        var twoSupportInTexas = new SupportChange<Player, State>(Player.Kennedy, State.TX, 2);
        var threeSupportInFlorida = new SupportChange<Player, State>(Player.Kennedy, State.FL, 3);

        playerChoices.StateChanges.Add(twoSupportInTexas);
        playerChoices.StateChanges.Add(threeSupportInFlorida);

        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(playerChoices);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void LyndonJohnson_39_FailsValidationIfStateNotInCorrectRegion()
    {
        SetOfChanges playerChoices = new();
        var twoSupportInTexas = new SupportChange<Player, State>(Player.Kennedy, State.TX, 2);
        var oneSupportInFlorida = new SupportChange<Player, State>(Player.Kennedy, State.FL, 1);
        var oneSupportInAlabama = new SupportChange<Player, State>(Player.Kennedy, State.AL, 1);
        var oneSupportInOhio = new SupportChange<Player, State>(Player.Kennedy, State.OH, 1);

        playerChoices.StateChanges.Add(twoSupportInTexas);
        playerChoices.StateChanges.Add(oneSupportInFlorida);
        playerChoices.StateChanges.Add(oneSupportInAlabama);
        playerChoices.StateChanges.Add(oneSupportInOhio);

        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(playerChoices);
        Assert.IsFalse(result);
    }

}