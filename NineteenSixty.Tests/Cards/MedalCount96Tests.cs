using NineteenSixty.Data;
using NineteenSixty.Enums;
using NineteenSixty.Tests.Fixtures;
using PresidentialGameEngine.ClassLibrary.Data;

namespace NineteenSixty.Tests.Cards;

[TestClass]
public class MedalCount96Tests
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

        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(engine, player, EngineFixtures.EmptyChanges);

        Assert.AreEqual(2, engine.GetSupportAmount(Issue.CivilRights));
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void MedalCount_96_LeaderInEconomyLosesOneIssueSupport(Player player)
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
    public void MedalCount_96_LeaderInBothLosesOneMomentum(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        engine.GainSupport(player, Issue.CivilRights, 3);
        engine.GainSupport(player, Issue.Economy, 2);
        engine.GainMomentum(player, 2);

        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(engine, player, EngineFixtures.EmptyChanges);

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

        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(engine, player, EngineFixtures.EmptyChanges);

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

        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(engine, player, EngineFixtures.EmptyChanges);

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

        var sut = Manifest.GMTCards[CardIndex];

        sut.Event(engine, player, EngineFixtures.EmptyChanges);

        Assert.AreEqual(1, engine.GetPlayerMomentum(Player.Nixon));
        Assert.AreEqual(1, engine.GetPlayerMomentum(Player.Kennedy));
    }

    [TestMethod]
    public void MedalCount_96_ValidationAlwaysTrue()
    {
        var sut = Manifest.GMTCards[CardIndex];

        var result = sut.AreChangesValid(EngineFixtures.InvalidChanges);

        Assert.IsTrue(result);
    }

}