// ReSharper disable InconsistentNaming
using NineteenSixty.Data;
using NineteenSixty.Enums;
using NineteenSixty.Tests.Fixtures;
using PresidentialGameEngine.ClassLibrary.Data;

namespace NineteenSixty.Tests.Cards;

[TestClass]
public class DwightEisenhower_18_Tests
{
    //"PREVENTION EVENT!  The Nixon player may add a total of 7 state support anywhere, no more than 1 per state.  This event prevents the Eisenhower's Silence event."
    private const int CardIndex = 18;

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void DwightEisenhower_18_SupportAddedToStates(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        SetOfChanges playerChoices = new();
        var oneSupportInWyoming = new SupportChange<Player, State>(Player.Nixon, State.WY, 1);
        var oneSupportInWashington = new SupportChange<Player, State>(Player.Nixon, State.WA, 1);
        var oneSupportInNorthDakota = new SupportChange<Player, State>(Player.Nixon, State.ND, 1);
        var oneSupportInNebraska = new SupportChange<Player, State>(Player.Nixon, State.NE, 1);
        var oneSupportInKentucky = new SupportChange<Player, State>(Player.Nixon, State.KY, 1);
        var oneSupportInRhodeIsland = new SupportChange<Player, State>(Player.Nixon, State.RI, 1);
        var oneSupportInAlaska = new SupportChange<Player, State>(Player.Nixon, State.AK, 1);

        playerChoices.StateChanges.Add(oneSupportInWyoming);
        playerChoices.StateChanges.Add(oneSupportInWashington);
        playerChoices.StateChanges.Add(oneSupportInNorthDakota);
        playerChoices.StateChanges.Add(oneSupportInNebraska);
        playerChoices.StateChanges.Add(oneSupportInKentucky);
        playerChoices.StateChanges.Add(oneSupportInRhodeIsland);
        playerChoices.StateChanges.Add(oneSupportInAlaska);

        var sut = Manifest.GMTCards[CardIndex];

        sut.Event(engine, player, playerChoices);

        Assert.AreEqual(1, engine.GetSupportAmount(State.WY));
        Assert.AreEqual(1, engine.GetSupportAmount(State.WA));
        Assert.AreEqual(1, engine.GetSupportAmount(State.ND));
        Assert.AreEqual(1, engine.GetSupportAmount(State.NE));
        Assert.AreEqual(1, engine.GetSupportAmount(State.KY));
        Assert.AreEqual(1, engine.GetSupportAmount(State.RI));
        Assert.AreEqual(1, engine.GetSupportAmount(State.AK));
    }

    [TestMethod]
    public void DwightEisenhower_18_FailsValidationIfKennedyGains()
    {
        SetOfChanges playerChoices = new();
        var oneSupportInWyoming = new SupportChange<Player, State>(Player.Nixon, State.WY, 1);
        var oneSupportInWashington = new SupportChange<Player, State>(Player.Nixon, State.WA, 1);
        var oneSupportInNorthDakota = new SupportChange<Player, State>(Player.Nixon, State.ND, 1);
        var oneSupportInNebraska = new SupportChange<Player, State>(Player.Nixon, State.NE, 1);
        var oneSupportInKentucky = new SupportChange<Player, State>(Player.Nixon, State.KY, 1);
        var oneSupportInRhodeIsland = new SupportChange<Player, State>(Player.Nixon, State.RI, 1);
        var invalidSupportGainForKennedy = new SupportChange<Player, State>(Player.Kennedy, State.MA, 1);

        playerChoices.StateChanges.Add(oneSupportInWyoming);
        playerChoices.StateChanges.Add(oneSupportInWashington);
        playerChoices.StateChanges.Add(oneSupportInNorthDakota);
        playerChoices.StateChanges.Add(oneSupportInNebraska);
        playerChoices.StateChanges.Add(oneSupportInKentucky);
        playerChoices.StateChanges.Add(oneSupportInRhodeIsland);
        playerChoices.StateChanges.Add(invalidSupportGainForKennedy);

        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(playerChoices);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void DwightEisenhower_18_FailsValidationIfIssueGains()
    {
        SetOfChanges playerChoices = new();
        var oneSupportInWyoming = new SupportChange<Player, State>(Player.Nixon, State.WY, 1);
        var oneSupportInWashington = new SupportChange<Player, State>(Player.Nixon, State.WA, 1);
        var oneSupportInNorthDakota = new SupportChange<Player, State>(Player.Nixon, State.ND, 1);
        var oneSupportInNebraska = new SupportChange<Player, State>(Player.Nixon, State.NE, 1);
        var oneSupportInKentucky = new SupportChange<Player, State>(Player.Nixon, State.KY, 1);
        var oneSupportInRhodeIsland = new SupportChange<Player, State>(Player.Nixon, State.RI, 1);
        var invalidIssueSupport = new SupportChange<Player, Issue>(Player.Kennedy, Issue.Defense, 1);

        playerChoices.StateChanges.Add(oneSupportInWyoming);
        playerChoices.StateChanges.Add(oneSupportInWashington);
        playerChoices.StateChanges.Add(oneSupportInNorthDakota);
        playerChoices.StateChanges.Add(oneSupportInNebraska);
        playerChoices.StateChanges.Add(oneSupportInKentucky);
        playerChoices.StateChanges.Add(oneSupportInRhodeIsland);
        playerChoices.IssueChanges.Add(invalidIssueSupport);

        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(playerChoices);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void DwightEisenhower_18_FailsValidationIfGreaterThanOne()
    {
        SetOfChanges playerChoices = new();
        var oneSupportInWyoming = new SupportChange<Player, State>(Player.Nixon, State.WY, 1);
        var oneSupportInWashington = new SupportChange<Player, State>(Player.Nixon, State.WA, 1);
        var oneSupportInNebraska = new SupportChange<Player, State>(Player.Nixon, State.NE, 1);
        var oneSupportInKentucky = new SupportChange<Player, State>(Player.Nixon, State.KY, 1);
        var oneSupportInRhodeIsland = new SupportChange<Player, State>(Player.Nixon, State.RI, 1);
        var invalidStateSupportGreaterThanOne = new SupportChange<Player, State>(Player.Nixon, State.OK, 2);

        playerChoices.StateChanges.Add(oneSupportInWyoming);
        playerChoices.StateChanges.Add(oneSupportInWashington);
        playerChoices.StateChanges.Add(oneSupportInNebraska);
        playerChoices.StateChanges.Add(oneSupportInKentucky);
        playerChoices.StateChanges.Add(oneSupportInRhodeIsland);
        playerChoices.StateChanges.Add(invalidStateSupportGreaterThanOne);

        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(playerChoices);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void DwightEisenhower_18_FailsValidationIfAnyNegativeValues()
    {
        SetOfChanges playerChoices = new();
        var oneSupportInWyoming = new SupportChange<Player, State>(Player.Nixon, State.WY, 1);
        var oneSupportInWashington = new SupportChange<Player, State>(Player.Nixon, State.WA, 1);
        var oneSupportInNorthDakota = new SupportChange<Player, State>(Player.Nixon, State.ND, 1);
        var oneSupportInNebraska = new SupportChange<Player, State>(Player.Nixon, State.NE, 1);
        var oneSupportInKentucky = new SupportChange<Player, State>(Player.Nixon, State.KY, 1);
        var oneSupportInRhodeIsland = new SupportChange<Player, State>(Player.Nixon, State.RI, 1);
        var oneSupportInHawaii = new  SupportChange<Player, State>(Player.Nixon, State.HI, 1);
        var invalidSupportLoss = new SupportChange<Player, State>(Player.Nixon, State.MA, -1);

        playerChoices.StateChanges.Add(oneSupportInWyoming);
        playerChoices.StateChanges.Add(oneSupportInWashington);
        playerChoices.StateChanges.Add(oneSupportInNorthDakota);
        playerChoices.StateChanges.Add(oneSupportInNebraska);
        playerChoices.StateChanges.Add(oneSupportInKentucky);
        playerChoices.StateChanges.Add(oneSupportInRhodeIsland);
        playerChoices.StateChanges.Add(oneSupportInHawaii);
        playerChoices.StateChanges.Add(invalidSupportLoss);

        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(playerChoices);
        Assert.IsFalse(result);
    }

}