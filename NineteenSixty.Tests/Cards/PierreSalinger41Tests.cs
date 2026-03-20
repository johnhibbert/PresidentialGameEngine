using NineteenSixty.Data;
using NineteenSixty.Enums;
using NineteenSixty.Tests.Fixtures;
using PresidentialGameEngine.ClassLibrary.Data;

namespace NineteenSixty.Tests.Cards;

[TestClass]
public class PierreSalinger41Tests
{
    //"The Kennedy player may add 3 issue support in any one issue."
    private const int CardIndex = 41;
    
    [TestMethod]
    [DataRow(Issue.CivilRights)]
    [DataRow(Issue.Defense)]
    [DataRow(Issue.Economy)]
    public void PierreSalinger_41_KennedyGainsThreeSupportInOneIssue(Issue issue)
    {
        var engine = EngineFixtures.GetGameEngine();

        engine.GainSupport(Player.Kennedy, issue, 1);

        SetOfChanges playerChoices = new();
        var threeSupportInOneIssue = new SupportChange<Player, Issue>(Player.Kennedy, issue, 3);
        playerChoices.IssueChanges.Add(threeSupportInOneIssue);

        var sut = Manifest.GMTCards[CardIndex];

        sut.Event(engine, Player.Kennedy, playerChoices);

        Assert.AreEqual(4, engine.GetSupportAmount(issue));
    }

    [TestMethod]
    [DataRow(Issue.CivilRights)]
    [DataRow(Issue.Defense)]
    [DataRow(Issue.Economy)]
    public void PierreSalinger_41_IssueSupportGainedRemovesOpponentSupport(Issue issue)
    {
        var engine = EngineFixtures.GetGameEngine();

        engine.GainSupport(Player.Nixon, issue, 2);

        SetOfChanges playerChoices = new();
        var threeSupportInOneIssue = new SupportChange<Player, Issue>(Player.Kennedy, issue, 3);
        playerChoices.IssueChanges.Add(threeSupportInOneIssue);

        var sut = Manifest.GMTCards[CardIndex];

        sut.Event(engine, Player.Kennedy, playerChoices);

        Assert.AreEqual(1, engine.GetSupportAmount(issue));
    }

    [TestMethod]
    [DataRow(Issue.CivilRights)]
    [DataRow(Issue.Defense)]
    [DataRow(Issue.Economy)]
    public void PierreSalinger_41_FailsValidationIfNixonGainsSupport(Issue issue)
    {
        SetOfChanges playerChoices = new();
        var threeSupportInOneIssue = new SupportChange<Player, Issue>(Player.Nixon, issue, 3);
        playerChoices.IssueChanges.Add(threeSupportInOneIssue);

        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(playerChoices);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void PierreSalinger_41_FailsValidationIfSupportGainedInMoreThanOneIssue()
    {
        SetOfChanges playerChoices = new();
        var oneSupportInCivilRights = new SupportChange<Player, Issue>(Player.Kennedy, Issue.CivilRights, 1);
        var oneSupportInDefense = new SupportChange<Player, Issue>(Player.Kennedy, Issue.Defense, 1);
        var oneSupportInEconomy = new SupportChange<Player, Issue>(Player.Kennedy, Issue.Economy, 1);
        playerChoices.IssueChanges.Add(oneSupportInCivilRights);
        playerChoices.IssueChanges.Add(oneSupportInDefense);
        playerChoices.IssueChanges.Add(oneSupportInEconomy);

        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(playerChoices);
        Assert.IsFalse(result);
    }

    [TestMethod]
    [DataRow(Issue.CivilRights)]
    [DataRow(Issue.Defense)]
    [DataRow(Issue.Economy)]
    public void PierreSalinger_41_FailsValidationIfStateSupportGained(Issue issue)
    {
        SetOfChanges playerChoices = new();
        var threeSupportInOneIssue = new SupportChange<Player, Issue>(Player.Kennedy, issue, 3);
        var invalidSupportInNewYork = new SupportChange<Player, State>(Player.Kennedy, State.NY, 1);
        playerChoices.IssueChanges.Add(threeSupportInOneIssue);
        playerChoices.StateChanges.Add(invalidSupportInNewYork);

        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(playerChoices);
        Assert.IsFalse(result);
    }


}