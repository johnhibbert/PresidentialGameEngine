using NineteenSixty.Data;
using NineteenSixty.Enums;
using NineteenSixty.Tests.Fixtures;
using PresidentialGameEngine.ClassLibrary.Data;

namespace NineteenSixty.Tests.Cards;

[TestClass]
public class PeaceWithoutSurrender68Tests
{
    //"Defense moves up one space on the Issue Track and Nixon gains 1 issue support in Defense."
    private const int CardIndex = 68;
    
    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void PeaceWithoutSurrender_68_DefenseGoesUpOneSpace(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        engine.SetIssueOrder([Issue.CivilRights, Issue.Economy, Issue.Defense]);

        var sut = Manifest.GMTCards[CardIndex];

        sut.Event(engine, player, EngineFixtures.EmptyChanges);

        Assert.AreEqual(Issue.Defense, engine.GetGameState().IssueOrder[1]);
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void PeaceWithoutSurrender_68_EconomyAtTopRemainsAtTop(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        engine.SetIssueOrder([Issue.Defense, Issue.Economy, Issue.CivilRights]);

        var sut = Manifest.GMTCards[CardIndex];

        sut.Event(engine, player, EngineFixtures.EmptyChanges);

        Assert.AreEqual(Issue.Defense, engine.GetGameState().IssueOrder[0]);
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void PeaceWithoutSurrender_68_IssueSupportGained(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        engine.SetIssueOrder([Issue.Defense, Issue.Economy, Issue.CivilRights]);

        var sut = Manifest.GMTCards[CardIndex];

        sut.Event(engine, player, EngineFixtures.EmptyChanges);

        Assert.AreEqual(Leader.Nixon, engine.GetLeader(Issue.Defense));
        Assert.AreEqual(1, engine.GetSupportAmount(Issue.Defense));
    }

    [TestMethod]
    public void PeaceWithoutSurrender_68_ValidationAlwaysTrue()
    {
        var sut = Manifest.GMTCards[CardIndex];

        var result = sut.AreChangesValid(EngineFixtures.InvalidChanges);

        Assert.IsTrue(result);
    }

}