// ReSharper disable InconsistentNaming
using NineteenSixty.Data;
using NineteenSixty.Enums;
using NineteenSixty.Tests.Fixtures;
using PresidentialGameEngine.ClassLibrary.Data;

namespace NineteenSixty.Tests.Cards;

[TestClass]
public class MedalCount_96_Tests
{
    //"The leaders in Civil Rights and Economy lose 1 issue support in those issues.  If the same player leads both, they also lose 1 momentum marker."
    private const int CardIndex = 96;
    
    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void MedalCount_96_LeaderInCivilRightsLosesOneIssueSupport(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        engine.GainSupport(player, Issue.CivilRights, 3);

        var plan = new ActionPlan{Engine = engine,  Changes = EngineFixtures.EmptyChanges};
        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(plan, player);

        Assert.AreEqual(2, engine.GetSupportAmount(Issue.CivilRights));
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void MedalCount_96_LeaderInEconomyLosesOneIssueSupport(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        engine.GainSupport(player, Issue.Economy, 2);

        var plan = new ActionPlan{Engine = engine,  Changes = EngineFixtures.EmptyChanges};
        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(plan, player);

        Assert.AreEqual(1, engine.GetSupportAmount(Issue.Economy));
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void MedalCount_96_LeaderInBothLosesOneMomentum(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        engine.GainSupport(player, Issue.CivilRights, 3);
        engine.GainSupport(player, Issue.Economy, 2);
        engine.GainMomentum(player, 2);

        var plan = new ActionPlan{Engine = engine,  Changes = EngineFixtures.EmptyChanges};
        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(plan, player);

        Assert.AreEqual(1, engine.GetPlayerMomentum(player));
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void MedalCount_96_LeaderSplitNoMomentumLoss(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        engine.GainSupport(Player.Nixon, Issue.CivilRights, 3);
        engine.GainSupport(Player.Kennedy, Issue.Economy, 2);
        engine.GainMomentum(Player.Nixon, 1);
        engine.GainMomentum(Player.Kennedy, 1);

        var plan = new ActionPlan{Engine = engine,  Changes = EngineFixtures.EmptyChanges};
        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(plan, player);

        Assert.AreEqual(1, engine.GetPlayerMomentum(Player.Nixon));
        Assert.AreEqual(1, engine.GetPlayerMomentum(Player.Kennedy));
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void MedalCount_96_NoLeaderInEitherNoMomentumLoss(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        engine.GainMomentum(Player.Nixon, 1);
        engine.GainMomentum(Player.Kennedy, 1);

        var plan = new ActionPlan{Engine = engine,  Changes = EngineFixtures.EmptyChanges};
        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(plan, player);

        Assert.AreEqual(1, engine.GetPlayerMomentum(Player.Nixon));
        Assert.AreEqual(1, engine.GetPlayerMomentum(Player.Kennedy));
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void MedalCount_96_LeaderInOnlyOneIssueNoMomentumLoss(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        engine.GainSupport(player, Issue.Defense, 2);
        engine.GainMomentum(Player.Nixon, 1);
        engine.GainMomentum(Player.Kennedy, 1);

        var plan = new ActionPlan{Engine = engine,  Changes = EngineFixtures.EmptyChanges};
        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(plan, player);

        Assert.AreEqual(1, engine.GetPlayerMomentum(Player.Nixon));
        Assert.AreEqual(1, engine.GetPlayerMomentum(Player.Kennedy));
    }

    [TestMethod]
    public void MedalCount_96_ValidationAlwaysTrue()
    {
        var sut = Manifest.GMTCards[CardIndex];

        var plan = new ActionPlan{Engine = null,  Changes = EngineFixtures.InvalidChanges};
        var result = sut.AreChangesValid(plan);

        Assert.IsTrue(result);
    }

}