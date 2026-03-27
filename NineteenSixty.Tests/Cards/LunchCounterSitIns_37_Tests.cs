// ReSharper disable InconsistentNaming
using NineteenSixty.Data;
using NineteenSixty.Enums;
using NineteenSixty.Tests.Fixtures;
using PresidentialGameEngine.ClassLibrary.Data;

namespace NineteenSixty.Tests.Cards;

[TestClass]
public class LunchCounterSitIns_37_Tests
{
    //"Civil Rights moves up one space on the Issue Track.  The leader in Civil Rights may add a total of 3 state support anywhere, no more than 1 per state."
    private const int CardIndex = 37;
    
    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void LunchCounterSitIns_37_CivilRightsMovesUp(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        engine.GainSupport(player, Issue.CivilRights, 1);
        engine.SetIssueOrder([Issue.Economy, Issue.Defense, Issue.CivilRights]);

        SetOfChanges playerChoices = new();
        var oneSupportInHawaii = new SupportChange<Player, State>(player, State.HI, 1);
        var oneSupportInFlorida = new SupportChange<Player, State>(player, State.FL, 1);
        var oneSupportInVermont = new SupportChange<Player, State>(player, State.VT, 1);

        playerChoices.StateChanges.Add(oneSupportInHawaii);
        playerChoices.StateChanges.Add(oneSupportInFlorida);
        playerChoices.StateChanges.Add(oneSupportInVermont);

        var plan = new ActionPlan{Engine = engine, Changes = playerChoices};
        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(plan, player);

        Assert.AreEqual(Issue.CivilRights, engine.GetGameState().IssueOrder[1]);

    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void LunchCounterSitIns_37_SupportAddedToStates(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        engine.GainSupport(player, Issue.CivilRights, 1);

        SetOfChanges playerChoices = new();
        var oneSupportInHawaii = new SupportChange<Player, State>(player, State.HI, 1);
        var oneSupportInFlorida = new SupportChange<Player, State>(player, State.FL, 1);
        var oneSupportInVermont = new SupportChange<Player, State>(player, State.VT, 1);

        playerChoices.StateChanges.Add(oneSupportInHawaii);
        playerChoices.StateChanges.Add(oneSupportInFlorida);
        playerChoices.StateChanges.Add(oneSupportInVermont);

        var plan = new ActionPlan{Engine = engine, Changes = playerChoices};
        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(plan, player);

        Assert.AreEqual(1, engine.GetSupportAmount(State.HI));
        Assert.AreEqual(1, engine.GetSupportAmount(State.FL));
        Assert.AreEqual(1, engine.GetSupportAmount(State.VT));
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void LunchCounterSitIns_37_FailsValidationIfNonLeaderGains(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        engine.GainSupport(player, Issue.CivilRights, 1);

        SetOfChanges playerChoices = new();
        var oneSupportInHawaii = new SupportChange<Player, State>(player, State.HI, 1);
        var oneSupportInFlorida = new SupportChange<Player, State>(player.ToOpponent(), State.FL, 1);
        var oneSupportInVermont = new SupportChange<Player, State>(player, State.VT, 1);

        playerChoices.StateChanges.Add(oneSupportInHawaii);
        playerChoices.StateChanges.Add(oneSupportInFlorida);
        playerChoices.StateChanges.Add(oneSupportInVermont);

        var plan = new ActionPlan{Engine = null, Changes = playerChoices};
        var sut = Manifest.GMTCards[CardIndex]; 
        var result = sut.AreChangesValid(plan);
        Assert.IsFalse(result);
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void LunchCounterSitIns_37_FailsValidationIfIssueGains(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        engine.GainSupport(player, Issue.CivilRights, 1);

        SetOfChanges playerChoices = new();
        var oneSupportInHawaii = new SupportChange<Player, State>(player, State.HI, 1);
        var oneSupportInFlorida = new SupportChange<Player, State>(player, State.FL, 1);
        var oneSupportInVermont = new SupportChange<Player, State>(player, State.VT, 1);
        playerChoices.StateChanges.Add(oneSupportInHawaii);
        playerChoices.StateChanges.Add(oneSupportInFlorida);
        playerChoices.StateChanges.Add(oneSupportInVermont);

        var issueSupport = new SupportChange<Player, Issue>(player, Issue.Defense, 1);
        playerChoices.IssueChanges.Add(issueSupport);

        var plan = new ActionPlan{Engine = null, Changes = playerChoices};
        var sut = Manifest.GMTCards[CardIndex]; 
        var result = sut.AreChangesValid(plan);
        Assert.IsFalse(result);
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void LunchCounterSitIns_37_FailsValidationIfGreaterThanOne(Player player)
    {
        SetOfChanges playerChoices = new();
        var oneSupportInHawaii = new SupportChange<Player, State>(player, State.HI, 1);
        var twoSupportInFlorida = new SupportChange<Player, State>(player, State.FL, 2);
        playerChoices.StateChanges.Add(oneSupportInHawaii);
        playerChoices.StateChanges.Add(twoSupportInFlorida);

        var plan = new ActionPlan{Engine = null, Changes = playerChoices};
        var sut = Manifest.GMTCards[CardIndex]; 
        var result = sut.AreChangesValid(plan);
        Assert.IsFalse(result);
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void LunchCounterSitIns_37_FailsValidationIfSumGreaterThanThree(Player player)
    {
        SetOfChanges playerChoices = new();
        var oneSupportInHawaii = new SupportChange<Player, State>(player, State.HI, 1);
        var oneSupportInFlorida = new SupportChange<Player, State>(player, State.FL, 1);
        var oneSupportInVermont = new SupportChange<Player, State>(player, State.VT, 1);
        var oneSupportInMissouri = new SupportChange<Player, State>(player, State.MO, 1);
        playerChoices.StateChanges.Add(oneSupportInHawaii);
        playerChoices.StateChanges.Add(oneSupportInFlorida);
        playerChoices.StateChanges.Add(oneSupportInVermont);
        playerChoices.StateChanges.Add(oneSupportInMissouri);

        var plan = new ActionPlan{Engine = null, Changes = playerChoices};
        var sut = Manifest.GMTCards[CardIndex]; 
        var result = sut.AreChangesValid(plan);
        Assert.IsFalse(result);
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void LunchCounterSitIns_37_FailsValidationIfAnyNegativeValues(Player player)
    {
        SetOfChanges playerChoices = new();
        var oneSupportInOregon = new SupportChange<Player, State>(player, State.OR, 1);
        var oneSupportInFlorida = new SupportChange<Player, State>(player, State.FL, 1);
        var invalidSupportLoss = new SupportChange<Player, State>(player, State.AL, -1);

        playerChoices.StateChanges.Add(oneSupportInOregon);
        playerChoices.StateChanges.Add(oneSupportInFlorida);
        playerChoices.StateChanges.Add(invalidSupportLoss);

        var plan = new ActionPlan{Engine = null, Changes = playerChoices};
        var sut = Manifest.GMTCards[CardIndex]; 
        var result = sut.AreChangesValid(plan);
        Assert.IsFalse(result);
    }
}