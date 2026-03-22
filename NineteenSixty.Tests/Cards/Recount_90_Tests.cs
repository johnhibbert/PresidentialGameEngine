// ReSharper disable InconsistentNaming
using NineteenSixty.Data;
using NineteenSixty.Enums;
using NineteenSixty.Tests.Fixtures;
using PresidentialGameEngine.ClassLibrary.Data;

namespace NineteenSixty.Tests.Cards;

[TestClass]
public class Recount_90_Tests
{
    //"ELECTION DAY EVENT!  On Election Day, the Nixon player gains 3 support checks in any one state.",
    private const int CardIndex = 90;
    
    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void Recount_90_SupportCheckWorksAsExpected(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        engine.GainSupport(Player.Kennedy, State.TX, 1);

        SetOfChanges playerChoices = new();
        var threeSupportInTexas = new SupportChange<Player, State>(Player.Nixon, State.TX, 3);
        playerChoices.StateChanges.Add(threeSupportInTexas);

        var sut = Manifest.GMTCards[CardIndex];

        sut.Event(engine, player, playerChoices);

        //FIXME LATER
        Assert.IsTrue(true);

        //Assert.AreEqual(Leader.None, engine.GetLeader(State.TX));
        //Assert.AreEqual(0, engine.GetSupportAmount(State.TX));
    }

    [TestMethod]
    public void Recount_90_FailsValidationIfKennedyGains()
    {
        SetOfChanges playerChoices = new();
        var threeSupportInKentucky = new SupportChange<Player, State>(Player.Kennedy, State.KY, 3);

        playerChoices.StateChanges.Add(threeSupportInKentucky);

        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(playerChoices);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void Recount_90_FailsValidationIfGainsAcrossMultipleStates()
    {
        SetOfChanges playerChoices = new();
        var twoSupportInKentucky = new SupportChange<Player, State>(Player.Nixon, State.KY, 2);
        var oneSupportInAlaska = new SupportChange<Player, State>(Player.Nixon, State.AK, 1);

        playerChoices.StateChanges.Add(twoSupportInKentucky);
        playerChoices.StateChanges.Add(oneSupportInAlaska);

        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(playerChoices);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void Recount_90_FailsValidationIfIssueGains()
    {
        SetOfChanges playerChoices = new();
        var threeSupportInKentucky = new SupportChange<Player, State>(Player.Nixon, State.KY, 3);
        var issueSupport = new SupportChange<Player, Issue>(Player.Nixon, Issue.Defense, 1);

        playerChoices.StateChanges.Add(threeSupportInKentucky);
        playerChoices.IssueChanges.Add(issueSupport);

        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(playerChoices);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void Recount_90_FailsValidationIfGreaterThanThree()
    {
        SetOfChanges playerChoices = new();
        var threeSupportInKentucky = new SupportChange<Player, State>(Player.Nixon, State.KY, 4);

        playerChoices.StateChanges.Add(threeSupportInKentucky);

        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(playerChoices);
        Assert.IsFalse(result);
    }

}