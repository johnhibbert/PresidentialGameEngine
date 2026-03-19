using NineteenSixty.Enums;
using NineteenSixty.Tests.Fixtures;

namespace NineteenSixty.Tests.Cards;

[TestClass]
public class SovietEconomicGrowth8Tests
{
    //"Economy moves up one space on the Issue Track.  The leader in Economy gains 1 state support in New York."
    private const int CardIndex = 8;

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void SovietEconomicGrowth_8_EconomyGoesUpOneSpace(Player player)
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
    public void SovietEconomicGrowth_8_EconomyAtTopRemainsAtTop(Player player)
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
    public void SovietEconomicGrowth_8_EconLeaderGainsSupportInNewYork(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();
        engine.SetIssueOrder([Issue.Economy, Issue.Defense, Issue.CivilRights]);
        engine.GainSupport(player, Issue.Economy, 1);

        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(engine, player, EngineFixtures.EmptyChanges);

        Assert.AreEqual(1, engine.GetSupportAmount(State.NY));
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void SovietEconomicGrowth_8_NoStateSupportGainedIfNoEconLeader(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();
        engine.SetIssueOrder([Issue.Economy, Issue.Defense, Issue.CivilRights]);
        
        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(engine, player, EngineFixtures.EmptyChanges);

        Assert.AreEqual(0, engine.GetSupportAmount(State.NY));
    }

    [TestMethod]
    public void SovietEconomicGrowth_8_ValidationAlwaysTrue()
    {
        var sut = Manifest.GMTCards[CardIndex];

        var result = sut.AreChangesValid(EngineFixtures.InvalidChanges);

        Assert.IsTrue(result);
    }

}