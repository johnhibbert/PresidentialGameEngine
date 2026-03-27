// ReSharper disable InconsistentNaming
using NineteenSixty.Data;
using NineteenSixty.Enums;
using NineteenSixty.Tests.Fixtures;
using PresidentialGameEngine.ClassLibrary.Data;

namespace NineteenSixty.Tests.Cards;

[TestClass]
public class HerbKlein_86_Tests
{
    //"The Nixon player may add a total of 3 issue support in any issues."
    private const int CardIndex = 86;
    
    [TestMethod]
    [DataRow(Issue.CivilRights)]
    [DataRow(Issue.Defense)]
    [DataRow(Issue.Economy)]
    public void HerbKlein_86_SupportChangesReflectedInSingleIssue(Issue issue)
    {
        var engine = EngineFixtures.GetGameEngine();

        engine.GainSupport(Player.Nixon, issue, 1);

        SetOfChanges playerChoices = new();
        var threeSupportInOneIssue = new SupportChange<Player, Issue>(Player.Nixon, issue, 3);
        playerChoices.IssueChanges.Add(threeSupportInOneIssue);

        var plan = new ActionPlan{Engine = engine, Changes = playerChoices};
        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(plan, Player.Nixon);
        
        Assert.AreEqual(4, engine.GetSupportAmount(issue));
    }

    [TestMethod]
    public void HerbKlein_86_SupportChangesReflectedAcrossMultipleIssues()
    {
        var engine = EngineFixtures.GetGameEngine();

        SetOfChanges playerChoices = new();
        var oneSupportInCivilRights = new SupportChange<Player, Issue>(Player.Nixon, Issue.CivilRights, 1);
        var oneSupportInDefense = new SupportChange<Player, Issue>(Player.Nixon, Issue.Defense, 1);
        var oneSupportInEconomy = new SupportChange<Player, Issue>(Player.Nixon, Issue.Economy, 1);
        playerChoices.IssueChanges.Add(oneSupportInCivilRights);
        playerChoices.IssueChanges.Add(oneSupportInDefense);
        playerChoices.IssueChanges.Add(oneSupportInEconomy);

        var plan = new ActionPlan{Engine = engine, Changes = playerChoices};
        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(plan, Player.Nixon);

        Assert.AreEqual(1, engine.GetSupportAmount(Issue.CivilRights));
        Assert.AreEqual(1, engine.GetSupportAmount(Issue.Defense));
        Assert.AreEqual(1, engine.GetSupportAmount(Issue.Economy));
    }

    [TestMethod]
    public void HerbKlein_86_FailsValidationIfKennedyGainsSupport()
    {
        SetOfChanges playerChoices = new();
        var oneSupportInCivilRights = new SupportChange<Player, Issue>(Player.Nixon, Issue.CivilRights, 1);
        var oneSupportInDefense = new SupportChange<Player, Issue>(Player.Kennedy, Issue.Defense, 1);
        var oneSupportInEconomy = new SupportChange<Player, Issue>(Player.Nixon, Issue.Economy, 1);
        playerChoices.IssueChanges.Add(oneSupportInCivilRights);
        playerChoices.IssueChanges.Add(oneSupportInDefense);
        playerChoices.IssueChanges.Add(oneSupportInEconomy);

        var plan = new ActionPlan{Engine = null, Changes = playerChoices};
        var sut = Manifest.GMTCards[CardIndex]; 
        var result = sut.AreChangesValid(plan);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void HerbKlein_86_FailsValidationIfStateSupportGained()
    {
        SetOfChanges playerChoices = new();
        var oneSupportInCivilRights = new SupportChange<Player, Issue>(Player.Nixon, Issue.CivilRights, 1);
        var oneSupportInDefense = new SupportChange<Player, Issue>(Player.Nixon, Issue.Defense, 1);
        var oneSupportInEconomy = new SupportChange<Player, Issue>(Player.Nixon, Issue.Economy, 1);
        var oneSupportInNewYork = new SupportChange<Player, State>(Player.Nixon, State.NY, 1);
        playerChoices.IssueChanges.Add(oneSupportInCivilRights);
        playerChoices.IssueChanges.Add(oneSupportInDefense);
        playerChoices.IssueChanges.Add(oneSupportInEconomy);
        playerChoices.StateChanges.Add(oneSupportInNewYork);

        var plan = new ActionPlan{Engine = null, Changes = playerChoices};
        var sut = Manifest.GMTCards[CardIndex]; 
        var result = sut.AreChangesValid(plan);
        Assert.IsFalse(result);
    }

}