using NineteenSixty.Data;
using NineteenSixty.Enums;
using NineteenSixty.Tests.Fixtures;
using PresidentialGameEngine.ClassLibrary.Data;

namespace NineteenSixty.Tests.Cards;

[TestClass]
public class CassiusClayWinsGold97Tests
{
    //"The leaders in Defense and Economy lose 1 issue support in those issues.  If the same player leads both, they also lose 1 momentum marker."
    private const int CardIndex = 97;
    
    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void CassiusClayWinsGold_97_LeaderInDefenseLosesOneIssueSupport(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        engine.GainSupport(player, Issue.Defense, 3);

        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(engine, player, EngineFixtures.EmptyChanges);

        Assert.AreEqual(2, engine.GetSupportAmount(Issue.Defense));
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void CassiusClayWinsGold_97_LeaderInEconomyLosesOneIssueSupport(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        engine.GainSupport(player, Issue.Economy, 2);

        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(engine, player, EngineFixtures.EmptyChanges);

        Assert.AreEqual(1, engine.GetSupportAmount(Issue.Economy));
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void CassiusClayWinsGold_97_LeaderInBothLosesOneMomentum(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        engine.GainSupport(player, Issue.Defense, 3);
        engine.GainSupport(player, Issue.Economy, 1);
        engine.GainMomentum(player, 3);

        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(engine, player, EngineFixtures.EmptyChanges);

        Assert.AreEqual(2, engine.GetPlayerMomentum(player));
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void CassiusClayWinsGold_97_LeaderSplitNoMomentumLoss(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        engine.GainSupport(Player.Nixon, Issue.Defense, 2);
        engine.GainSupport(Player.Kennedy, Issue.Economy, 1);
        engine.GainMomentum(Player.Nixon, 1);
        engine.GainMomentum(Player.Kennedy, 1);

        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(engine, player, EngineFixtures.EmptyChanges);

        Assert.AreEqual(1, engine.GetPlayerMomentum(Player.Nixon));
        Assert.AreEqual(1, engine.GetPlayerMomentum(Player.Kennedy));
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void CassiusClayWinsGold_97_LeaderInOnlyOneIssueNoMomentumLoss(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        engine.GainSupport(Player.Nixon, Issue.CivilRights, 3);
        engine.GainMomentum(Player.Nixon, 1);
        engine.GainMomentum(Player.Kennedy, 1);

        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(engine, player, EngineFixtures.EmptyChanges);

        Assert.AreEqual(1, engine.GetPlayerMomentum(Player.Nixon));
        Assert.AreEqual(1, engine.GetPlayerMomentum(Player.Kennedy));
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void CassiusClayWinsGold_97_NoLeaderInEitherNoMomentumLoss(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        engine.GainMomentum(Player.Nixon, 1);
        engine.GainMomentum(Player.Kennedy, 1);

        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(engine, player, EngineFixtures.EmptyChanges);

        Assert.AreEqual(1, engine.GetPlayerMomentum(Player.Nixon));
        Assert.AreEqual(1, engine.GetPlayerMomentum(Player.Kennedy));
    }

    [TestMethod]
    public void CassiusClayWinsGold_97_ValidationAlwaysTrue()
    {
        var sut = Manifest.GMTCards[CardIndex];

        var result = sut.AreChangesValid(EngineFixtures.InvalidChanges);

        Assert.IsTrue(result);
    }
}