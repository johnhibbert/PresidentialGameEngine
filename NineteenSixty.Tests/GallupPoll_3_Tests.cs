using NineteenSixty.Enums;
using NineteenSixty.Data;
using NineteenSixty.Tests.Fixtures;

namespace NineteenSixty.Tests;

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
        var engine = EngineFixtures.GetGameEngine(); // . .GetGameEngine();

        var initialPosition = new SetOfChanges()
        {
            NewIssuesOrder = [Issue.Economy, Issue.CivilRights, Issue.Defense]
        };
        engine.ImplementChanges(initialPosition);

        SetOfChanges playerChoices = new();
        playerChoices.NewIssuesOrder.AddRange([Issue.Defense, Issue.Economy, Issue.CivilRights]);

        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(engine, player, playerChoices);

        var result = engine.GetGameState().IssueOrder;

        Assert.AreEqual(Issue.Defense, result[0]);
        Assert.AreEqual(Issue.Economy, result[1]);
        Assert.AreEqual(Issue.CivilRights, result[2]);

    }

    /*
    [TestMethod]
    public void GallupPoll_3_ValidationFailsIfListLengthIsWrong()
    {
        int cardIndex = 3;
        var engine = GetGameEngine();

        engine.SetIssueOrder([Issue.CivilRights, Issue.Defense, Issue.Economy]);

        PlayerChosenChanges<Player, Issue, State, Region> playerChoices = new();
        playerChoices.NewIssuesOrder.AddRange([Issue.Defense, Issue.Economy]);

        var sut = NineteenSixty.GMTCards[cardIndex];
        var result = sut.AreChangesValid(playerChoices);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void GallupPoll_3_ValidationFailsIfListContainsDuplicates()
    {
        int cardIndex = 3;
        var engine = GetGameEngine();

        engine.SetIssueOrder([Issue.CivilRights, Issue.Defense, Issue.Economy]);

        PlayerChosenChanges<Player, Issue, State, Region> playerChoices = new();
        playerChoices.NewIssuesOrder.AddRange([Issue.Defense, Issue.Defense, Issue.Economy]);

        var sut = NineteenSixty.GMTCards[cardIndex];
        var result = sut.AreChangesValid(playerChoices);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void GallupPoll_3_ValidationFailsIfIssueSupportChanges()
    {
        int cardIndex = 3;

        PlayerChosenChanges<Player, Issue, State, Region> playerChoices = new();
        var invalidIssueSupportChange = new SupportChange<Player, Issue>(Player.Kennedy, Issue.Defense, 1);

        playerChoices.NewIssuesOrder.AddRange([Issue.Defense, Issue.Defense, Issue.Economy]);
        playerChoices.IssueChanges.Add(invalidIssueSupportChange);

        var sut = NineteenSixty.GMTCards[cardIndex];
        var result = sut.AreChangesValid(playerChoices);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void GallupPoll_3_ValidationFailsIfMediaSupportChanges()
    {
        int cardIndex = 3;

        PlayerChosenChanges<Player, Issue, State, Region> playerChoices = new();
        var invalidmediaSupportChange = new SupportChange<Player, Region>(Player.Kennedy, Region.Midwest, 1);

        playerChoices.NewIssuesOrder.AddRange([Issue.Defense, Issue.Defense, Issue.Economy]);
        playerChoices.MediaSupportChanges.Add(invalidmediaSupportChange);

        var sut = NineteenSixty.GMTCards[cardIndex];
        var result = sut.AreChangesValid(playerChoices);
        Assert.IsFalse(result);
    }*/

}