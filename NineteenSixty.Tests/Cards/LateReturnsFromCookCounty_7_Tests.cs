// ReSharper disable InconsistentNaming
using NineteenSixty.Data;
using NineteenSixty.Enums;
using NineteenSixty.Tests.Fixtures;
using PresidentialGameEngine.ClassLibrary.Data;

namespace NineteenSixty.Tests.Cards;

[TestClass]
public class LateReturnsFromCookCounty_7_Tests
{
    //"ELECTION DAY EVENT!  On Election Day, the Kennedy player gains 5 support checks in Illinois.",
    private const int CardIndex = 7;

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void LateReturnsFromCookCounty_7_SupportAddedToIllinois(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        SetOfChanges playerChoices = new();
        var fiveSupportInIllinois = new SupportChange<Player, State>(Player.Kennedy, State.IL, 5);

        playerChoices.StateChanges.Add(fiveSupportInIllinois);

        var plan = new ActionPlan{Engine = engine, Changes = playerChoices};
        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(plan, player);

        Assert.AreEqual(5, engine.GetSupportAmount(State.IL));
    }

    [TestMethod]
    public void LateReturnsFromCookCounty_7_FailsValidationIfNixonGains()
    {
        SetOfChanges playerChoices = new();
        var invalidSupportForNixon = new SupportChange<Player, State>(Player.Nixon, State.IL, 5);

        playerChoices.StateChanges.Add(invalidSupportForNixon);

        var plan = new ActionPlan{Engine = null, Changes = playerChoices};
        var sut = Manifest.GMTCards[CardIndex]; 
        var result = sut.AreChangesValid(plan);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void LateReturnsFromCookCounty_7_FailsValidationIfIssueGains()
    {
        SetOfChanges playerChoices = new();
        var fiveSupportInIllinois = new SupportChange<Player, State>(Player.Kennedy, State.IL, 5);
        var invalidIssueSupport = new SupportChange<Player, Issue>(Player.Kennedy, Issue.Defense, 1);

        playerChoices.StateChanges.Add(fiveSupportInIllinois);
        playerChoices.IssueChanges.Add(invalidIssueSupport);

        var plan = new ActionPlan{Engine = null, Changes = playerChoices};
        var sut = Manifest.GMTCards[CardIndex]; 
        var result = sut.AreChangesValid(plan);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void LateReturnsFromCookCounty_7_FailsValidationIfStateGainGreaterThanFive()
    {
        SetOfChanges playerChoices = new();
        var invalidSupportGreaterThanFive = new SupportChange<Player, State>(Player.Kennedy, State.IL, 6);
        playerChoices.StateChanges.Add(invalidSupportGreaterThanFive);

        var plan = new ActionPlan{Engine = null, Changes = playerChoices};
        var sut = Manifest.GMTCards[CardIndex]; 
        var result = sut.AreChangesValid(plan);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void LateReturnsFromCookCounty_7_FailsValidationIfAnyNegativeValues()
    {
        SetOfChanges playerChoices = new();
        var invalidNegativeValue = new SupportChange<Player, State>(Player.Kennedy, State.IL, -1);
        playerChoices.StateChanges.Add(invalidNegativeValue);

        var plan = new ActionPlan{Engine = null, Changes = playerChoices};
        var sut = Manifest.GMTCards[CardIndex]; 
        var result = sut.AreChangesValid(plan);
        Assert.IsFalse(result);
    }
    
}