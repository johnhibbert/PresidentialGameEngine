// ReSharper disable InconsistentNaming
using NineteenSixty.Data;
using NineteenSixty.Enums;
using NineteenSixty.Tests.Fixtures;
using PresidentialGameEngine.ClassLibrary.Data;

namespace NineteenSixty.Tests.Cards;

[TestClass]
public class NewEngland_6_Tests
{
    //"The Kennedy player may add a total of 5 state support in Connecticut, Massachusetts, Maine, New York, Rhode Island, and Vermont, no more than 2 per state."
    private const int CardIndex = 6;

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void NewEngland_6_SupportAddedToStates(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        SetOfChanges playerChoices = new();
        var oneSupportInRhodeIsland = new SupportChange<Player, State>(Player.Kennedy, State.RI, 1);
        var oneSupportInMaine = new SupportChange<Player, State>(Player.Kennedy, State.ME, 1);
        var twoSupportInNewHampshire = new SupportChange<Player, State>(Player.Kennedy, State.NH, 2);
        var oneSupportInVermont = new SupportChange<Player, State>(Player.Kennedy, State.VT, 1);

        playerChoices.StateChanges.Add(oneSupportInRhodeIsland);
        playerChoices.StateChanges.Add(oneSupportInMaine);
        playerChoices.StateChanges.Add(twoSupportInNewHampshire);
        playerChoices.StateChanges.Add(oneSupportInVermont);

        var sut = Manifest.GMTCards[CardIndex];

        sut.Event(engine, player, playerChoices);

        Assert.AreEqual(1, engine.GetSupportAmount(State.RI));
        Assert.AreEqual(1, engine.GetSupportAmount(State.ME));
        Assert.AreEqual(2, engine.GetSupportAmount(State.NH));
        Assert.AreEqual(1, engine.GetSupportAmount(State.VT));
    }

    [TestMethod]
    public void NewEngland_6_FailsValidationIfNixonGains()
    {
        SetOfChanges playerChoices = new();
        var oneSupportInRhodeIsland = new SupportChange<Player, State>(Player.Kennedy, State.RI, 1);
        var twoSupportInNewHampshire = new SupportChange<Player, State>(Player.Kennedy, State.NH, 2);
        var oneSupportInVermont = new SupportChange<Player, State>(Player.Kennedy, State.VT, 1);
        var invalidSupportForNixon = new SupportChange<Player, State>(Player.Nixon, State.ME, 1);

        playerChoices.StateChanges.Add(oneSupportInRhodeIsland);
        playerChoices.StateChanges.Add(twoSupportInNewHampshire);
        playerChoices.StateChanges.Add(oneSupportInVermont);
        playerChoices.StateChanges.Add(invalidSupportForNixon);

        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(playerChoices);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void NewEngland_6_FailsValidationIfIssueGains()
    {
        SetOfChanges playerChoices = new();
        var oneSupportInRhodeIsland = new SupportChange<Player, State>(Player.Kennedy, State.RI, 1);
        var oneSupportInMaine = new SupportChange<Player, State>(Player.Kennedy, State.ME, 1);
        var twoSupportInNewHampshire = new SupportChange<Player, State>(Player.Kennedy, State.NH, 2);
        var oneSupportInVermont = new SupportChange<Player, State>(Player.Kennedy, State.VT, 1);
        var invalidIssueSupport = new SupportChange<Player, Issue>(Player.Kennedy, Issue.Defense, 1);

        playerChoices.StateChanges.Add(oneSupportInRhodeIsland);
        playerChoices.StateChanges.Add(oneSupportInMaine);
        playerChoices.StateChanges.Add(twoSupportInNewHampshire);
        playerChoices.StateChanges.Add(oneSupportInVermont);
        playerChoices.IssueChanges.Add(invalidIssueSupport);

        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(playerChoices);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void NewEngland_6_FailsValidationIfSingleGainGreaterThanTwo()
    {
        SetOfChanges playerChoices = new();
        var oneSupportInRhodeIsland = new SupportChange<Player, State>(Player.Kennedy, State.RI, 1);
        var oneSupportInMaine = new SupportChange<Player, State>(Player.Kennedy, State.ME, 1);
        var invalidSupportOverThree = new SupportChange<Player, State>(Player.Kennedy, State.NH, 3);

        playerChoices.StateChanges.Add(oneSupportInRhodeIsland);
        playerChoices.StateChanges.Add(oneSupportInMaine);
        playerChoices.StateChanges.Add(invalidSupportOverThree);

        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(playerChoices);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void NewEngland_6_FailsValidationIfExcludedState()
    {
        SetOfChanges playerChoices = new();
        var oneSupportInRhodeIsland = new SupportChange<Player, State>(Player.Kennedy, State.RI, 1);
        var twoSupportInMaine = new SupportChange<Player, State>(Player.Kennedy, State.ME, 2);
        var invalidSupportForExcludedState = new SupportChange<Player, State>(Player.Kennedy, State.AK, 2);

        playerChoices.StateChanges.Add(oneSupportInRhodeIsland);
        playerChoices.StateChanges.Add(twoSupportInMaine);
        playerChoices.StateChanges.Add(invalidSupportForExcludedState);

        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(playerChoices);
        Assert.IsFalse(result);
    }

}