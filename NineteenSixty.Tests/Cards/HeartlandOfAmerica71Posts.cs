using NineteenSixty.Data;
using NineteenSixty.Enums;
using NineteenSixty.Tests.Fixtures;
using PresidentialGameEngine.ClassLibrary.Data;

namespace NineteenSixty.Tests.Cards;

[TestClass]
public class HeartlandOfAmerica71Posts
{
    //"The Nixon player may add a total of 7 state support in states in the West or Midwest having 10 or fewer electoral votes, no more than 1 per state."
    private const int CardIndex = 71;
    
    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void HeartlandOfAmerica_71_SupportAddedToStates(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        SetOfChanges playerChoices = new();
        var oneSupportInWyoming = new SupportChange<Player, State>(Player.Nixon, State.WY, 1);
        var oneSupportInIdaho = new SupportChange<Player, State>(Player.Nixon, State.ID, 1);
        var oneSupportInNorthDakota = new SupportChange<Player, State>(Player.Nixon, State.ND, 1);
        var oneSupportInIowa = new SupportChange<Player, State>(Player.Nixon, State.IA, 1);
        var oneSupportInKentucky = new SupportChange<Player, State>(Player.Nixon, State.KY, 1);
        var oneSupportInOklahoma = new SupportChange<Player, State>(Player.Nixon, State.OK, 1);
        var oneSupportInNebraska = new SupportChange<Player, State>(Player.Nixon, State.NE, 1);

        playerChoices.StateChanges.Add(oneSupportInWyoming);
        playerChoices.StateChanges.Add(oneSupportInIdaho);
        playerChoices.StateChanges.Add(oneSupportInNorthDakota);
        playerChoices.StateChanges.Add(oneSupportInIowa);
        playerChoices.StateChanges.Add(oneSupportInKentucky);
        playerChoices.StateChanges.Add(oneSupportInOklahoma);
        playerChoices.StateChanges.Add(oneSupportInNebraska);

        var sut = Manifest.GMTCards[CardIndex];

        sut.Event(engine, player, playerChoices);

        Assert.AreEqual(1, engine.GetSupportAmount(State.WY));
        Assert.AreEqual(1, engine.GetSupportAmount(State.ID));
        Assert.AreEqual(1, engine.GetSupportAmount(State.ND));
        Assert.AreEqual(1, engine.GetSupportAmount(State.IA));
        Assert.AreEqual(1, engine.GetSupportAmount(State.KY));
        Assert.AreEqual(1, engine.GetSupportAmount(State.OK));
        Assert.AreEqual(1, engine.GetSupportAmount(State.NE));
    }

    [TestMethod]
    public void HeartlandOfAmerica_71_FailsValidationIfKennedyGains()
    {
        SetOfChanges playerChoices = new();
        var oneSupportInIdaho = new SupportChange<Player, State>(Player.Nixon, State.ID, 1);
        var oneSupportInNorthDakota = new SupportChange<Player, State>(Player.Nixon, State.ND, 1);
        var oneSupportInIowa = new SupportChange<Player, State>(Player.Nixon, State.IA, 1);
        var oneSupportInKentucky = new SupportChange<Player, State>(Player.Nixon, State.KY, 1);
        var oneSupportInOklahoma = new SupportChange<Player, State>(Player.Nixon, State.OK, 1);
        var oneSupportInNebraska = new SupportChange<Player, State>(Player.Nixon, State.NE, 1);
        var invalidSupportForKennedy = new SupportChange<Player, State>(Player.Kennedy, State.WY, 1);

        playerChoices.StateChanges.Add(oneSupportInIdaho);
        playerChoices.StateChanges.Add(oneSupportInNorthDakota);
        playerChoices.StateChanges.Add(oneSupportInIowa);
        playerChoices.StateChanges.Add(oneSupportInKentucky);
        playerChoices.StateChanges.Add(oneSupportInOklahoma);
        playerChoices.StateChanges.Add(oneSupportInNebraska);
        playerChoices.StateChanges.Add(invalidSupportForKennedy);

        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(playerChoices);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void HeartlandOfAmerica_71_FailsValidationIfIssueGains()
    {
        SetOfChanges playerChoices = new();
        var oneSupportInWyoming = new SupportChange<Player, State>(Player.Nixon, State.WY, 1);
        var oneSupportInIdaho = new SupportChange<Player, State>(Player.Nixon, State.ID, 1);
        var oneSupportInNorthDakota = new SupportChange<Player, State>(Player.Nixon, State.ND, 1);
        var oneSupportInIowa = new SupportChange<Player, State>(Player.Nixon, State.IA, 1);
        var oneSupportInKentucky = new SupportChange<Player, State>(Player.Nixon, State.KY, 1);
        var oneSupportInOklahoma = new SupportChange<Player, State>(Player.Nixon, State.OK, 1);
        var oneSupportInNebraska = new SupportChange<Player, State>(Player.Nixon, State.NE, 1);
        ;
        var invalidIssueSupport = new SupportChange<Player, Issue>(Player.Kennedy, Issue.Defense, 1);

        playerChoices.StateChanges.Add(oneSupportInWyoming);
        playerChoices.StateChanges.Add(oneSupportInIdaho);
        playerChoices.StateChanges.Add(oneSupportInNorthDakota);
        playerChoices.StateChanges.Add(oneSupportInIowa);
        playerChoices.StateChanges.Add(oneSupportInKentucky);
        playerChoices.StateChanges.Add(oneSupportInOklahoma);
        playerChoices.StateChanges.Add(oneSupportInNebraska);
        playerChoices.IssueChanges.Add(invalidIssueSupport);

        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(playerChoices);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void HeartlandOfAmerica_71_FailsValidationIfGreaterThanOne()
    {
        SetOfChanges playerChoices = new();
        var oneSupportInWyoming = new SupportChange<Player, State>(Player.Nixon, State.WY, 1);
        var oneSupportInIdaho = new SupportChange<Player, State>(Player.Nixon, State.ID, 1);
        var oneSupportInNorthDakota = new SupportChange<Player, State>(Player.Nixon, State.ND, 1);
        var oneSupportInIowa = new SupportChange<Player, State>(Player.Nixon, State.IA, 1);
        var oneSupportInKentucky = new SupportChange<Player, State>(Player.Nixon, State.KY, 1);
        var invalidStateSupportGreaterThanOne = new SupportChange<Player, State>(Player.Nixon, State.OK, 2);

        playerChoices.StateChanges.Add(oneSupportInWyoming);
        playerChoices.StateChanges.Add(oneSupportInIdaho);
        playerChoices.StateChanges.Add(oneSupportInNorthDakota);
        playerChoices.StateChanges.Add(oneSupportInIowa);
        playerChoices.StateChanges.Add(oneSupportInKentucky);
        playerChoices.StateChanges.Add(invalidStateSupportGreaterThanOne);

        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(playerChoices);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void HeartlandOfAmerica_71_FailsValidationIfStateOutsideWestOrMidwest()
    {
        SetOfChanges playerChoices = new();
        var oneSupportInIdaho = new SupportChange<Player, State>(Player.Nixon, State.ID, 1);
        var oneSupportInNorthDakota = new SupportChange<Player, State>(Player.Nixon, State.ND, 1);
        var oneSupportInIowa = new SupportChange<Player, State>(Player.Nixon, State.IA, 1);
        var oneSupportInKentucky = new SupportChange<Player, State>(Player.Nixon, State.KY, 1);
        var oneSupportInOklahoma = new SupportChange<Player, State>(Player.Nixon, State.OK, 1);
        var oneSupportInWyoming = new SupportChange<Player, State>(Player.Nixon, State.WY, 1);
        var invalidSupportOutsideWestOrMidwest = new SupportChange<Player, State>(Player.Nixon, State.FL, 1);

        playerChoices.StateChanges.Add(oneSupportInIdaho);
        playerChoices.StateChanges.Add(oneSupportInNorthDakota);
        playerChoices.StateChanges.Add(oneSupportInIowa);
        playerChoices.StateChanges.Add(oneSupportInKentucky);
        playerChoices.StateChanges.Add(oneSupportInOklahoma);
        playerChoices.StateChanges.Add(oneSupportInWyoming);
        playerChoices.StateChanges.Add(invalidSupportOutsideWestOrMidwest);

        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(playerChoices);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void HeartlandOfAmerica_71_FailsValidationIfStateHasTooManyVotes()
    {
        SetOfChanges playerChoices = new();
        var oneSupportInWyoming = new SupportChange<Player, State>(Player.Nixon, State.WY, 1);
        var oneSupportInIdaho = new SupportChange<Player, State>(Player.Nixon, State.ID, 1);
        var oneSupportInNorthDakota = new SupportChange<Player, State>(Player.Nixon, State.ND, 1);
        var oneSupportInIowa = new SupportChange<Player, State>(Player.Nixon, State.IA, 1);
        var oneSupportInKentucky = new SupportChange<Player, State>(Player.Nixon, State.KY, 1);
        var oneSupportInOklahoma = new SupportChange<Player, State>(Player.Nixon, State.OK, 1);
        var invalidSupportForStateWithTooManyVotes = new SupportChange<Player, State>(Player.Nixon, State.CA, 1);

        playerChoices.StateChanges.Add(oneSupportInWyoming);
        playerChoices.StateChanges.Add(oneSupportInIdaho);
        playerChoices.StateChanges.Add(oneSupportInNorthDakota);
        playerChoices.StateChanges.Add(oneSupportInIowa);
        playerChoices.StateChanges.Add(oneSupportInKentucky);
        playerChoices.StateChanges.Add(oneSupportInOklahoma);
        playerChoices.StateChanges.Add(invalidSupportForStateWithTooManyVotes);

        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(playerChoices);
        Assert.IsFalse(result);
    }

}