using NineteenSixty.Data;
using NineteenSixty.Enums;
using NineteenSixty.Tests.Fixtures;
using PresidentialGameEngine.ClassLibrary.Data;

namespace NineteenSixty.Tests.Cards;

[TestClass]
public class SouthernRevolt72Tests
{
    //"If Kennedy is leading in Civil Rights, the Nixon player may add a total of 5 state support in the South, no more than 2 per state."
    private const int CardIndex = 72;
    
    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void SouthernRevolt_72_NixonGainsStateSupportIfKennedyLeadsCivilRights(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();
        engine.GainSupport(Player.Kennedy, Issue.CivilRights, 2);

        SetOfChanges playerChoices = new();
        var oneSupportInFlorida = new SupportChange<Player, State>(Player.Nixon, State.FL, 1);
        var oneSupportInVirginia = new SupportChange<Player, State>(Player.Nixon, State.VA, 1);
        var twoSupportInLouisiana = new SupportChange<Player, State>(Player.Nixon, State.LA, 2);
        var oneSupportInTexas = new SupportChange<Player, State>(Player.Nixon, State.TX, 1);

        playerChoices.StateChanges.Add(oneSupportInFlorida);
        playerChoices.StateChanges.Add(oneSupportInVirginia);
        playerChoices.StateChanges.Add(twoSupportInLouisiana);
        playerChoices.StateChanges.Add(oneSupportInTexas);

        var sut = Manifest.GMTCards[CardIndex];

        sut.Event(engine, player, playerChoices);

        Assert.AreEqual(1, engine.GetSupportAmount(State.FL));
        Assert.AreEqual(1, engine.GetSupportAmount(State.VA));
        Assert.AreEqual(2, engine.GetSupportAmount(State.LA));
        Assert.AreEqual(1, engine.GetSupportAmount(State.TX));
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void SouthernRevolt_72_NoChangeIfNixonLeadsInCivilRights(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();
        engine.GainSupport(Player.Nixon, Issue.CivilRights, 2);

        SetOfChanges playerChoices = new();
        var oneSupportInFlorida = new SupportChange<Player, State>(Player.Nixon, State.FL, 1);
        var oneSupportInVirginia = new SupportChange<Player, State>(Player.Nixon, State.VA, 1);
        var twoSupportInLouisiana = new SupportChange<Player, State>(Player.Nixon, State.LA, 2);
        var oneSupportInTexas = new SupportChange<Player, State>(Player.Nixon, State.TX, 1);

        playerChoices.StateChanges.Add(oneSupportInFlorida);
        playerChoices.StateChanges.Add(oneSupportInVirginia);
        playerChoices.StateChanges.Add(twoSupportInLouisiana);
        playerChoices.StateChanges.Add(oneSupportInTexas);

        var sut = Manifest.GMTCards[CardIndex];

        sut.Event(engine, player, playerChoices);

        Assert.AreEqual(0, engine.GetSupportAmount(State.FL));
        Assert.AreEqual(0, engine.GetSupportAmount(State.VA));
        Assert.AreEqual(0, engine.GetSupportAmount(State.LA));
        Assert.AreEqual(0, engine.GetSupportAmount(State.TX));
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void SouthernRevolt_72_NoChangeIfNoLeaderInCivilRights(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        SetOfChanges playerChoices = new();
        var oneSupportInFlorida = new SupportChange<Player, State>(Player.Nixon, State.FL, 1);
        var oneSupportInVirginia = new SupportChange<Player, State>(Player.Nixon, State.VA, 1);
        var twoSupportInLouisiana = new SupportChange<Player, State>(Player.Nixon, State.LA, 2);
        var oneSupportInTexas = new SupportChange<Player, State>(Player.Nixon, State.TX, 1);

        playerChoices.StateChanges.Add(oneSupportInFlorida);
        playerChoices.StateChanges.Add(oneSupportInVirginia);
        playerChoices.StateChanges.Add(twoSupportInLouisiana);
        playerChoices.StateChanges.Add(oneSupportInTexas);

        var sut = Manifest.GMTCards[CardIndex];

        sut.Event(engine, player, playerChoices);

        Assert.AreEqual(0, engine.GetSupportAmount(State.FL));
        Assert.AreEqual(0, engine.GetSupportAmount(State.VA));
        Assert.AreEqual(0, engine.GetSupportAmount(State.LA));
        Assert.AreEqual(0, engine.GetSupportAmount(State.TX));
    }

    [TestMethod]
    public void SouthernRevolt_72_PassedValidationEvenIfNixonLeadsCivilRights()
    {
        SetOfChanges playerChoices = new();
        var oneSupportInFlorida = new SupportChange<Player, State>(Player.Nixon, State.FL, 1);
        var oneSupportInVirginia = new SupportChange<Player, State>(Player.Nixon, State.VA, 1);
        var twoSupportInLouisiana = new SupportChange<Player, State>(Player.Nixon, State.LA, 2);
        var oneSupportInTexas = new SupportChange<Player, State>(Player.Nixon, State.TX, 1);

        playerChoices.StateChanges.Add(oneSupportInFlorida);
        playerChoices.StateChanges.Add(oneSupportInVirginia);
        playerChoices.StateChanges.Add(twoSupportInLouisiana);
        playerChoices.StateChanges.Add(oneSupportInTexas);

        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(playerChoices);
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void SouthernRevolt_72_PassedValidationEvenIfNoLeaderInCivilRights()
    {
        SetOfChanges playerChoices = new();
        var oneSupportInFlorida = new SupportChange<Player, State>(Player.Nixon, State.FL, 1);
        var oneSupportInVirginia = new SupportChange<Player, State>(Player.Nixon, State.VA, 1);
        var twoSupportInLouisiana = new SupportChange<Player, State>(Player.Nixon, State.LA, 2);
        var oneSupportInTexas = new SupportChange<Player, State>(Player.Nixon, State.TX, 1);

        playerChoices.StateChanges.Add(oneSupportInFlorida);
        playerChoices.StateChanges.Add(oneSupportInVirginia);
        playerChoices.StateChanges.Add(twoSupportInLouisiana);
        playerChoices.StateChanges.Add(oneSupportInTexas);

        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(playerChoices);
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void SouthernRevolt_72_FailsValidationIfKennedyGains()
    {
        SetOfChanges playerChoices = new();
        var oneSupportInFlorida = new SupportChange<Player, State>(Player.Nixon, State.FL, 1);
        var oneSupportInVirginia = new SupportChange<Player, State>(Player.Kennedy, State.VA, 1);
        var twoSupportInLouisiana = new SupportChange<Player, State>(Player.Nixon, State.LA, 2);
        var oneSupportInTexas = new SupportChange<Player, State>(Player.Nixon, State.TX, 1);

        playerChoices.StateChanges.Add(oneSupportInFlorida);
        playerChoices.StateChanges.Add(oneSupportInVirginia);
        playerChoices.StateChanges.Add(twoSupportInLouisiana);
        playerChoices.StateChanges.Add(oneSupportInTexas);

        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(playerChoices);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void SouthernRevolt_72_FailsValidationIfIssueGains()
    {
        SetOfChanges playerChoices = new();
        var oneSupportInFlorida = new SupportChange<Player, State>(Player.Nixon, State.FL, 1);
        var oneSupportInVirginia = new SupportChange<Player, State>(Player.Nixon, State.VA, 1);
        var twoSupportInLouisiana = new SupportChange<Player, State>(Player.Nixon, State.LA, 2);
        var oneSupportInTexas = new SupportChange<Player, State>(Player.Nixon, State.TX, 1);
        var issueSupport = new SupportChange<Player, Issue>(Player.Nixon, Issue.Defense, 1);

        playerChoices.StateChanges.Add(oneSupportInFlorida);
        playerChoices.StateChanges.Add(oneSupportInVirginia);
        playerChoices.StateChanges.Add(twoSupportInLouisiana);
        playerChoices.StateChanges.Add(oneSupportInTexas);
        playerChoices.IssueChanges.Add(issueSupport);

        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(playerChoices);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void SouthernRevolt_72_FailsValidationIfGreaterThanTwo()
    {
        SetOfChanges playerChoices = new();
        var oneSupportInFlorida = new SupportChange<Player, State>(Player.Nixon, State.FL, 1);
        var oneSupportInVirginia = new SupportChange<Player, State>(Player.Nixon, State.VA, 1);
        var threeSupportInLouisiana = new SupportChange<Player, State>(Player.Nixon, State.LA, 3);

        playerChoices.StateChanges.Add(oneSupportInFlorida);
        playerChoices.StateChanges.Add(oneSupportInVirginia);
        playerChoices.StateChanges.Add(threeSupportInLouisiana);

        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(playerChoices);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void SouthernRevolt_72_FailsValidationIfExcludedState()
    {
        SetOfChanges playerChoices = new();
        var oneSupportInFlorida = new SupportChange<Player, State>(Player.Nixon, State.FL, 1);
        var oneSupportInVirginia = new SupportChange<Player, State>(Player.Nixon, State.VA, 1);
        var twoSupportInLouisiana = new SupportChange<Player, State>(Player.Nixon, State.LA, 2);
        var oneSupportInRhodeIsland = new SupportChange<Player, State>(Player.Kennedy, State.RI, 1);

        playerChoices.StateChanges.Add(oneSupportInFlorida);
        playerChoices.StateChanges.Add(oneSupportInVirginia);
        playerChoices.StateChanges.Add(twoSupportInLouisiana);
        playerChoices.StateChanges.Add(oneSupportInRhodeIsland);

        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(playerChoices);
        Assert.IsFalse(result);
    }
}