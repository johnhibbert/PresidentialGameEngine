// ReSharper disable InconsistentNaming
using NineteenSixty.Data;
using NineteenSixty.Enums;
using NineteenSixty.Tests.Fixtures;
using PresidentialGameEngine.ClassLibrary.Data;

namespace NineteenSixty.Tests.Cards;

[TestClass]

public class ExperienceCounts_93_Tests
{
    //"Kennedy loses 1 issue support in each issue.  The Nixon player gains one momentum marker."
    private const int CardIndex = 93;
    
    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void ExperienceCounts_93_NixonGainsOneMomentum(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        engine.GainMomentum(Player.Nixon, 2);

        engine.GainSupport(Player.Kennedy, Issue.CivilRights, 3);
        engine.GainSupport(Player.Kennedy, Issue.Defense, 2);
        engine.GainSupport(Player.Kennedy, Issue.Economy, 1);

        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(engine, player, EngineFixtures.EmptyChanges);

        Assert.AreEqual(engine.GetPlayerMomentum(Player.Nixon), 3);
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void ExperienceCounts_93_KennedyLosesOneSupportInEachIssue(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        engine.GainSupport(Player.Kennedy, Issue.CivilRights, 3);
        engine.GainSupport(Player.Kennedy, Issue.Defense, 2);
        engine.GainSupport(Player.Kennedy, Issue.Economy, 1);

        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(engine, player, EngineFixtures.EmptyChanges);

        Assert.AreEqual(2, engine.GetSupportAmount(Issue.CivilRights));
        Assert.AreEqual(1, engine.GetSupportAmount(Issue.Defense));
        Assert.AreEqual(0, engine.GetSupportAmount(Issue.Economy));
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void ExperienceCounts_93_KennedySupportDoesNotGoNegative(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        engine.LoseSupport(Player.Kennedy, Issue.Economy, int.MaxValue);

        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(engine, player, EngineFixtures.EmptyChanges);

        Assert.AreEqual(0, engine.GetSupportAmount(Issue.Economy));
        Assert.AreEqual(Leader.None, engine.GetLeader(Issue.Economy));
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void ExperienceCounts_93_NixonAndNeutralSupportNotLost(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        engine.GainSupport(Player.Kennedy, Issue.CivilRights, 3);
        engine.GainSupport(Player.Nixon, Issue.Defense, 2);

        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(engine, player, EngineFixtures.EmptyChanges);

        Assert.AreEqual(2, engine.GetSupportAmount(Issue.CivilRights));
        Assert.AreEqual(2, engine.GetSupportAmount(Issue.Defense));
        Assert.AreEqual(0, engine.GetSupportAmount(Issue.Economy));
    }

    [TestMethod]
    public void ExperienceCounts_93_ValidationAlwaysTrue()
    {
        var sut = Manifest.GMTCards[CardIndex];

        var result = sut.AreChangesValid(EngineFixtures.InvalidChanges);

        Assert.IsTrue(result);
    }

}