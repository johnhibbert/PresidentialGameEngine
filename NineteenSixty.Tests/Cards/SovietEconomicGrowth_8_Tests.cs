// ReSharper disable InconsistentNaming
using NineteenSixty.Enums;
using NineteenSixty.Tests.Fixtures;

namespace NineteenSixty.Tests.Cards;

[TestClass]
public class SovietEconomicGrowth_8_Tests
{
    //"Economy moves up one space on the Issue Track.  The leader in Economy gains 1 state support in New York."
    private const int CardIndex = 8;

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void SovietEconomicGrowth_8_EconomyGoesUpOnePosition(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();
        engine.SetIssueOrder([Issue.CivilRights, Issue.Defense, Issue.Economy]);
        
        var plan = new ActionPlan{Engine = engine,  Changes = EngineFixtures.EmptyChanges};
        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(plan, player);

        Assert.AreEqual(Issue.Economy, engine.GetGameState().IssueOrder[1]);
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void SovietEconomicGrowth_8_EconomyAtTopRemainsAtTop(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();
        engine.SetIssueOrder([Issue.Economy, Issue.Defense, Issue.CivilRights]);

        var plan = new ActionPlan{Engine = engine,  Changes = EngineFixtures.EmptyChanges};
        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(plan, player);

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

        var plan = new ActionPlan{Engine = engine,  Changes = EngineFixtures.EmptyChanges};
        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(plan, player);

        Assert.AreEqual(1, engine.GetSupportAmount(State.NY));
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void SovietEconomicGrowth_8_NoStateSupportGainedIfNoEconLeader(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();
        engine.SetIssueOrder([Issue.Economy, Issue.Defense, Issue.CivilRights]);
        
        var plan = new ActionPlan{Engine = engine,  Changes = EngineFixtures.EmptyChanges};
        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(plan, player);

        Assert.AreEqual(0, engine.GetSupportAmount(State.NY));
    }

    [TestMethod]
    public void SovietEconomicGrowth_8_ValidationAlwaysTrue()
    {
        var sut = Manifest.GMTCards[CardIndex];

        var plan = new ActionPlan{Engine = null,  Changes = EngineFixtures.InvalidChanges};
        var result = sut.AreChangesValid(plan);

        Assert.IsTrue(result);
    }

}