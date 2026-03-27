// ReSharper disable InconsistentNaming
using NineteenSixty.Data;
using NineteenSixty.Enums;
using NineteenSixty.Tests.Fixtures;
using PresidentialGameEngine.ClassLibrary.Data;

namespace NineteenSixty.Tests.Cards;

[TestClass]
public class GallupPoll_3_Tests
{
    //"Player may alter the order of the issues on the Issue Track as desired."
    private const int CardIndex = 3;

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void GallupPoll_3_IssueOrderIsChanged(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();
        engine.SetIssueOrder([Issue.Economy, Issue.CivilRights, Issue.Defense]);

        SetOfChanges issueOrderChosenByPlayer = new();
        issueOrderChosenByPlayer.NewIssuesOrder.AddRange([Issue.Defense, Issue.Economy, Issue.CivilRights]);

        var plan = new ActionPlan{Engine = engine, Changes = issueOrderChosenByPlayer};
        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(plan, player);

        var result = engine.GetGameState().IssueOrder;

        Assert.AreEqual(Issue.Defense, result[0]);
        Assert.AreEqual(Issue.Economy, result[1]);
        Assert.AreEqual(Issue.CivilRights, result[2]);

    }
    
    [TestMethod]
    public void GallupPoll_3_ValidationFailsIfListWrongLength()
    {
        SetOfChanges invalidIssueOrderWithWrongLength = new();
        invalidIssueOrderWithWrongLength.NewIssuesOrder.AddRange([Issue.Defense, Issue.Economy]);

        var plan = new ActionPlan{Engine = null,  Changes = invalidIssueOrderWithWrongLength};
        
        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(plan);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void GallupPoll_3_ValidationFailsIfListContainsDuplicates()
    {
        SetOfChanges invalidIssueOrderWithDuplicates = new();
        invalidIssueOrderWithDuplicates.NewIssuesOrder.AddRange([Issue.Defense, Issue.Defense, Issue.Economy]);

        var plan = new ActionPlan{Engine = null,  Changes = invalidIssueOrderWithDuplicates};
        
        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(plan);
        Assert.IsFalse(result);
    }
  
    [TestMethod]
    public void GallupPoll_3_ValidationFailsIfIssueSupportChanges()
    {
        SetOfChanges playerChoices = new();
        playerChoices.NewIssuesOrder.AddRange([Issue.CivilRights, Issue.Defense, Issue.Economy]);
        var invalidIssueSupportChange = new SupportChange<Player, Issue>(Player.Kennedy, Issue.Defense, 1);
        playerChoices.IssueChanges.Add(invalidIssueSupportChange);

        var plan = new ActionPlan{Engine = null,  Changes = playerChoices};
        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(plan);
        
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void GallupPoll_3_ValidationFailsIfStateSupportChanges()
    {
        SetOfChanges playerChoices = new();
        playerChoices.NewIssuesOrder.AddRange([Issue.CivilRights, Issue.Defense, Issue.Economy]);
        var invalidStateSupportChange = new SupportChange<Player, State>(Player.Kennedy, State.CO, 1);
        playerChoices.StateChanges.Add(invalidStateSupportChange);

        var plan = new ActionPlan{Engine = null,  Changes = playerChoices};
        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(plan);
        Assert.IsFalse(result);
    }
    
    [TestMethod]
    public void GallupPoll_3_ValidationFailsIfMediaSupportChanges()
    {
        SetOfChanges playerChoices = new();
        playerChoices.NewIssuesOrder.AddRange([Issue.CivilRights, Issue.Defense, Issue.Economy]);
        var invalidMediaSupportChange = new SupportChange<Player, Region>(Player.Kennedy, Region.Midwest, 1);

        playerChoices.NewIssuesOrder.AddRange([Issue.Defense, Issue.Defense, Issue.Economy]);
        playerChoices.MediaSupportChanges.Add(invalidMediaSupportChange);

        var plan = new ActionPlan{Engine = null,  Changes = playerChoices};
        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(plan);
        Assert.IsFalse(result);
    }

}