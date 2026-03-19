using NineteenSixty.Enums;
using NineteenSixty.Data;

namespace NineteenSixty.Tests;

[TestClass]
public class GallupPoll_3_Tests
{
    //"Player may alter the order of the issues on the Issue Track as desired."
    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void GallupPoll_3_IssueOrderIsSetAsExpected(Player player)
    {
        /*
        int cardIndex = 3;
        var engine = new Engine();

        //engine.SetIssueOrder([Issue.CivilRights, Issue.Defense, Issue.Economy]);

        SetOfChanges playerChoices = new();
        playerChoices.NewIssuesOrder.AddRange([Issue.Defense, Issue.Economy, Issue.CivilRights]);

        var sut = NineteenSixty.GMTCards[cardIndex];
        sut.Event(engine, player, playerChoices);

        var result = engine.GetIssueOrder;

        Assert.AreEqual(Issue.Defense, result[0]);
        Assert.AreEqual(Issue.Economy, result[1]);
        Assert.AreEqual(Issue.CivilRights, result[2]);
        */
    }

}