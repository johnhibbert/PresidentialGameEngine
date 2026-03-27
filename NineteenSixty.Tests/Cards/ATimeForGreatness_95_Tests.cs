// ReSharper disable InconsistentNaming
using NineteenSixty.Data;
using NineteenSixty.Enums;
using NineteenSixty.Tests.Fixtures;
using PresidentialGameEngine.ClassLibrary.Data;

namespace NineteenSixty.Tests.Cards;

[TestClass]
public class ATimeForGreatness_95_Tests
{
    //"Nixon loses 1 issue support on each issue.  The Kennedy player may add 3 state support anywhere, no more than 1 per state."
    private const int CardIndex = 95;
    
    
    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void ATimeForGreatness_95_NixonLosesOneSupportOnEachIssue(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        engine.GainSupport(Player.Nixon, Issue.CivilRights, 1);
        engine.GainSupport(Player.Nixon, Issue.Defense, 3);
        engine.GainSupport(Player.Nixon, Issue.Economy, 4);

        SetOfChanges playerChoices = new();
        var oneSupportInNewYork = new SupportChange<Player, State>(Player.Kennedy, State.NY, 1);
        var oneSupportInColorado = new SupportChange<Player, State>(Player.Kennedy, State.CO, 1);
        var oneSupportInWestVirginia = new SupportChange<Player, State>(Player.Kennedy, State.WV, 1);

        playerChoices.StateChanges.Add(oneSupportInNewYork);
        playerChoices.StateChanges.Add(oneSupportInColorado);
        playerChoices.StateChanges.Add(oneSupportInWestVirginia);

        var plan = new ActionPlan{Engine = engine, Changes = playerChoices};
        
        var sut = Manifest.GMTCards[CardIndex];

        sut.Event(plan, player);
        Assert.AreEqual(Leader.None, engine.GetLeader(Issue.CivilRights));
        Assert.AreEqual(Leader.Nixon, engine.GetLeader(Issue.Defense));
        Assert.AreEqual(Leader.Nixon, engine.GetLeader(Issue.Economy));

        Assert.AreEqual(0, engine.GetSupportAmount(Issue.CivilRights));
        Assert.AreEqual(2, engine.GetSupportAmount(Issue.Defense));
        Assert.AreEqual(3, engine.GetSupportAmount(Issue.Economy));
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void ATimeForGreatness_95_KennedySupportAddedToStates(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        engine.GainSupport(Player.Nixon, Issue.CivilRights, 2);
        engine.GainSupport(Player.Nixon, Issue.Defense, 3);
        engine.GainSupport(Player.Nixon, Issue.Economy, 4);

        SetOfChanges playerChoices = new();
        var oneSupportInNewYork = new SupportChange<Player, State>(Player.Kennedy, State.NY, 1);
        var oneSupportInColorado = new SupportChange<Player, State>(Player.Kennedy, State.CO, 1);
        var oneSupportInWestVirginia = new SupportChange<Player, State>(Player.Kennedy, State.WV, 1);

        playerChoices.StateChanges.Add(oneSupportInNewYork);
        playerChoices.StateChanges.Add(oneSupportInColorado);
        playerChoices.StateChanges.Add(oneSupportInWestVirginia);

        var plan = new ActionPlan{Engine = engine, Changes = playerChoices};
        
        var sut = Manifest.GMTCards[CardIndex];

        sut.Event(plan, player);

        Assert.AreEqual(Leader.Kennedy, engine.GetLeader(State.NY));
        Assert.AreEqual(Leader.Kennedy, engine.GetLeader(State.CO));
        Assert.AreEqual(Leader.Kennedy, engine.GetLeader(State.WV));

        Assert.AreEqual(1, engine.GetSupportAmount(State.NY));
        Assert.AreEqual(1, engine.GetSupportAmount(State.CO));
        Assert.AreEqual(1, engine.GetSupportAmount(State.WV));
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void ATimeForGreatness_95_FailsValidationIfNixonGainsStateSupport(Player player)
    {
        SetOfChanges playerChoices = new();
        var oneSupportInNewYork = new SupportChange<Player, State>(Player.Nixon, State.NY, 1);
        var oneSupportInColorado = new SupportChange<Player, State>(Player.Kennedy, State.CO, 1);
        var oneSupportInWestVirginia = new SupportChange<Player, State>(Player.Kennedy, State.WV, 1);

        playerChoices.StateChanges.Add(oneSupportInNewYork);
        playerChoices.StateChanges.Add(oneSupportInColorado);
        playerChoices.StateChanges.Add(oneSupportInWestVirginia);

        var plan = new ActionPlan{Engine = null, Changes = playerChoices};
        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(plan);
        Assert.IsFalse(result);
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void ATimeForGreatness_95_FailsValidationIfAnyIssueGains(Player player)
    {
        SetOfChanges playerChoices = new();
        var oneSupportInNewYork = new SupportChange<Player, State>(Player.Kennedy, State.NY, 1);
        var oneSupportInColorado = new SupportChange<Player, State>(Player.Kennedy, State.CO, 1);
        var oneSupportInWestVirginia = new SupportChange<Player, State>(Player.Kennedy, State.WV, 1);

        playerChoices.StateChanges.Add(oneSupportInNewYork);
        playerChoices.StateChanges.Add(oneSupportInColorado);
        playerChoices.StateChanges.Add(oneSupportInWestVirginia);

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
    public void ATimeForGreatness_95_FailsValidationIfStateSupportGainsGreaterThanOne(Player player)
    {
        SetOfChanges playerChoices = new();
        var oneSupportInNewYork = new SupportChange<Player, State>(Player.Kennedy, State.NY, 1);
        var twoSupportInColorado = new SupportChange<Player, State>(Player.Kennedy, State.CO, 2);

        playerChoices.StateChanges.Add(oneSupportInNewYork);
        playerChoices.StateChanges.Add(twoSupportInColorado);

        var plan = new ActionPlan{Engine = null, Changes = playerChoices};
        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(plan);
        Assert.IsFalse(result);
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void ATimeForGreatness_95_FailsValidationIfTotalStateSupportGainedIsGreaterThanThree(Player player)
    {
        SetOfChanges playerChoices = new();
        var oneSupportInHawaii = new SupportChange<Player, State>(Player.Kennedy, State.HI, 1);
        var oneSupportInFlorida = new SupportChange<Player, State>(Player.Kennedy, State.FL, 1);
        var oneSupportInVermont = new SupportChange<Player, State>(Player.Kennedy, State.VT, 1);
        var oneSupportInMissouri = new SupportChange<Player, State>(Player.Kennedy, State.MO, 1);
        playerChoices.StateChanges.Add(oneSupportInHawaii);
        playerChoices.StateChanges.Add(oneSupportInFlorida);
        playerChoices.StateChanges.Add(oneSupportInVermont);
        playerChoices.StateChanges.Add(oneSupportInMissouri);

        var plan = new ActionPlan{Engine = null, Changes = playerChoices};
        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(plan);
        Assert.IsFalse(result);
    }

}