using NineteenSixty.Data;
using NineteenSixty.Enums;
using NineteenSixty.Tests.Fixtures;
using PresidentialGameEngine.ClassLibrary.Data;

namespace NineteenSixty.Tests.Cards;

[TestClass]
public class GallupPoll3Tests
{
    //"Player may alter the order of the issues on the Issue Track as desired."
    private const int CardIndex = 3;

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void GallupPoll_3_IssueOrderIsSetAsExpected(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();
        engine.SetIssueOrder([Issue.Economy, Issue.CivilRights, Issue.Defense]);

        SetOfChanges issueOrderChosenByPlayer = new();
        issueOrderChosenByPlayer.NewIssuesOrder.AddRange([Issue.Defense, Issue.Economy, Issue.CivilRights]);

        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(engine, player, issueOrderChosenByPlayer);

        var result = engine.GetGameState().IssueOrder;

        Assert.AreEqual(Issue.Defense, result[0]);
        Assert.AreEqual(Issue.Economy, result[1]);
        Assert.AreEqual(Issue.CivilRights, result[2]);

    }
    
    [TestMethod]
    public void GallupPoll_3_ValidationFailsIfListLengthIsWrong()
    {
        SetOfChanges invalidIssueOrderWithWrongLength = new();
        invalidIssueOrderWithWrongLength.NewIssuesOrder.AddRange([Issue.Defense, Issue.Economy]);

        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(invalidIssueOrderWithWrongLength);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void GallupPoll_3_ValidationFailsIfListContainsDuplicates()
    {
        SetOfChanges invalidIssueOrderWithDuplicates = new();
        invalidIssueOrderWithDuplicates.NewIssuesOrder.AddRange([Issue.Defense, Issue.Defense, Issue.Economy]);

        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(invalidIssueOrderWithDuplicates);
        Assert.IsFalse(result);
    }
  
    [TestMethod]
    public void GallupPoll_3_ValidationFailsIfIssueSupportChanges()
    {
        SetOfChanges playerChoices = new();
        playerChoices.NewIssuesOrder.AddRange([Issue.CivilRights, Issue.Defense, Issue.Economy]);
        var invalidIssueSupportChange = new SupportChange<Player, Issue>(Player.Kennedy, Issue.Defense, 1);
        playerChoices.IssueChanges.Add(invalidIssueSupportChange);

        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(playerChoices);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void GallupPoll_3_ValidationFailsIfStateSupportChanges()
    {
        SetOfChanges playerChoices = new();
        playerChoices.NewIssuesOrder.AddRange([Issue.CivilRights, Issue.Defense, Issue.Economy]);
        var invalidStateSupportChange = new SupportChange<Player, State>(Player.Kennedy, State.CO, 1);
        playerChoices.StateChanges.Add(invalidStateSupportChange);

        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(playerChoices);
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

        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(playerChoices);
        Assert.IsFalse(result);
    }

}