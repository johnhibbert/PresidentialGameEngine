// ReSharper disable InconsistentNaming
using NineteenSixty.Data;
using NineteenSixty.Enums;
using NineteenSixty.Tests.Fixtures;
using PresidentialGameEngine.ClassLibrary.Data;

namespace NineteenSixty.Tests.Cards;

[TestClass]
public class RisingFoodPrices_48_Tests
{
    //"Economy moves up one space on the Issue Track and Nixon gains 2 issue support in Economy."
    private const int CardIndex = 48;
    
    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void RisingFoodPrices_48_EconomyGoesUpOnePosition(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        engine.SetIssueOrder([Issue.CivilRights, Issue.Defense, Issue.Economy]);

        var sut = Manifest.GMTCards[CardIndex];

        sut.Event(engine, player, EngineFixtures.EmptyChanges);

        Assert.AreEqual(Issue.Economy, engine.GetGameState().IssueOrder[1]);
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void RisingFoodPrices_48_EconomyAtTopRemainsAtTop(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        engine.SetIssueOrder([Issue.Economy, Issue.Defense, Issue.CivilRights]);

        var sut = Manifest.GMTCards[CardIndex];

        sut.Event(engine, player, EngineFixtures.EmptyChanges);

        Assert.AreEqual(Issue.Economy, engine.GetGameState().IssueOrder[0]);
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void RisingFoodPrices_48_IssueSupportGained(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        engine.SetIssueOrder([Issue.Economy, Issue.Defense, Issue.CivilRights]);

        var sut = Manifest.GMTCards[CardIndex];

        sut.Event(engine, player, EngineFixtures.EmptyChanges);

        Assert.AreEqual(Leader.Nixon, engine.GetLeader(Issue.Economy));
        Assert.AreEqual(2, engine.GetSupportAmount(Issue.Economy));
    }

    [TestMethod]
    public void RisingFoodPrices_48_ValidationAlwaysTrue()
    {
        var sut = Manifest.GMTCards[CardIndex];

        var result = sut.AreChangesValid(EngineFixtures.InvalidChanges);

        Assert.IsTrue(result);
    }

}