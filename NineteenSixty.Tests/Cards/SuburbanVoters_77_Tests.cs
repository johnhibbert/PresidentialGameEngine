// ReSharper disable InconsistentNaming
using NineteenSixty.Data;
using NineteenSixty.Enums;
using NineteenSixty.Tests.Fixtures;
using PresidentialGameEngine.ClassLibrary.Data;

namespace NineteenSixty.Tests.Cards;

[TestClass]
public class SuburbanVoters_77_Tests
{
    //"The Kennedy player may add a total of 5 state support in states having 20 or more electoral votes, no more than 2 per state."
    private const int CardIndex = 77;
    
    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void SuburbanVoters_77_KennedyGainsFiveStateSupport(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        SetOfChanges playerChoices = new();
        var oneSupportInMichigan = new SupportChange<Player, State>(Player.Kennedy, State.MI, 1);
        var oneSupportInPenn = new SupportChange<Player, State>(Player.Kennedy, State.PA, 1);
        var twoSupportInCali = new SupportChange<Player, State>(Player.Kennedy, State.CA, 2);
        var oneSupportInNewYork = new SupportChange<Player, State>(Player.Kennedy, State.NY, 1);

        playerChoices.StateChanges.Add(oneSupportInMichigan);
        playerChoices.StateChanges.Add(oneSupportInPenn);
        playerChoices.StateChanges.Add(twoSupportInCali);
        playerChoices.StateChanges.Add(oneSupportInNewYork);

        var sut = Manifest.GMTCards[CardIndex];

        sut.Event(engine, player, playerChoices);

        Assert.AreEqual(1, engine.GetSupportAmount(State.MI));
        Assert.AreEqual(1, engine.GetSupportAmount(State.PA));
        Assert.AreEqual(2, engine.GetSupportAmount(State.CA));
        Assert.AreEqual(1, engine.GetSupportAmount(State.NY));
    }

    [TestMethod]
    public void SuburbanVoters_77_FailsValidationIfNixonGains()
    {
        SetOfChanges playerChoices = new();
        var oneSupportInMichigan = new SupportChange<Player, State>(Player.Kennedy, State.MI, 1);
        var oneSupportInPenn = new SupportChange<Player, State>(Player.Nixon, State.PA, 1);
        var twoSupportInCali = new SupportChange<Player, State>(Player.Kennedy, State.CA, 2);
        var oneSupportInNewYork = new SupportChange<Player, State>(Player.Kennedy, State.NY, 1);

        playerChoices.StateChanges.Add(oneSupportInMichigan);
        playerChoices.StateChanges.Add(oneSupportInPenn);
        playerChoices.StateChanges.Add(twoSupportInCali);
        playerChoices.StateChanges.Add(oneSupportInNewYork);

        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(playerChoices);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void SuburbanVoters_77_FailsValidationIfIssueGains()
    {
        SetOfChanges playerChoices = new();
        var oneSupportInMichigan = new SupportChange<Player, State>(Player.Kennedy, State.MI, 1);
        var oneSupportInPenn = new SupportChange<Player, State>(Player.Kennedy, State.PA, 1);
        var twoSupportInCali = new SupportChange<Player, State>(Player.Kennedy, State.CA, 2);
        var oneSupportInNewYork = new SupportChange<Player, State>(Player.Kennedy, State.NY, 1);
        var issueSupport = new SupportChange<Player, Issue>(Player.Kennedy, Issue.Defense, 1);

        playerChoices.StateChanges.Add(oneSupportInMichigan);
        playerChoices.StateChanges.Add(oneSupportInPenn);
        playerChoices.StateChanges.Add(twoSupportInCali);
        playerChoices.StateChanges.Add(oneSupportInNewYork);
        playerChoices.IssueChanges.Add(issueSupport);

        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(playerChoices);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void SuburbanVoters_77_FailsValidationIfGreaterThanTwo()
    {
        SetOfChanges playerChoices = new();
        var oneSupportInMichigan = new SupportChange<Player, State>(Player.Kennedy, State.MI, 1);
        var oneSupportInPenn = new SupportChange<Player, State>(Player.Nixon, State.PA, 1);
        var threeSupportInCali = new SupportChange<Player, State>(Player.Kennedy, State.CA, 2);

        playerChoices.StateChanges.Add(oneSupportInMichigan);
        playerChoices.StateChanges.Add(oneSupportInPenn);
        playerChoices.StateChanges.Add(threeSupportInCali);

        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(playerChoices);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void SuburbanVoters_77_FailsValidationIfExcludedState()
    {
        SetOfChanges playerChoices = new();
        var oneSupportInMichigan = new SupportChange<Player, State>(Player.Kennedy, State.MI, 1);
        var oneSupportInPenn = new SupportChange<Player, State>(Player.Kennedy, State.PA, 1);
        var twoSupportInCali = new SupportChange<Player, State>(Player.Kennedy, State.CA, 2);
        var invalidSupportInStateWithTooFewElectoralVotes = new SupportChange<Player, State>(Player.Kennedy, State.KS, 1);

        playerChoices.StateChanges.Add(oneSupportInMichigan);
        playerChoices.StateChanges.Add(oneSupportInPenn);
        playerChoices.StateChanges.Add(twoSupportInCali);
        playerChoices.StateChanges.Add(invalidSupportInStateWithTooFewElectoralVotes);

        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(playerChoices);
        Assert.IsFalse(result);
    }

}