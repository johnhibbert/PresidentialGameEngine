// ReSharper disable InconsistentNaming
using NineteenSixty.Data;
using NineteenSixty.Enums;
using NineteenSixty.Tests.Fixtures;
using PresidentialGameEngine.ClassLibrary.Data;

namespace NineteenSixty.Tests.Cards;

[TestClass]
public class TrialOfGaryPowers_62_Tests
{
    //"Defense moves up two spaces on the Issue Track.  The leader in Defense gains 1 momentum marker."
    private const int CardIndex = 62;
    
    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void TheTrialOfGaryPowers_62_DefenseMovesUpTwoPositions(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        engine.SetIssueOrder([Issue.CivilRights, Issue.Economy, Issue.Defense]);

        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(engine, player, EngineFixtures.EmptyChanges);

        Assert.AreEqual(Issue.Defense, engine.GetGameState().IssueOrder[0]);
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void TheTrialOfGaryPowers_62_DefenseAtTopUnchanged(Player player)
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
    public void TheTrialOfGaryPowers_62_LeaderAwardedMomentum(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        engine.GainSupport(player, Issue.Defense, 1);

        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(engine, player, EngineFixtures.EmptyChanges);

        Assert.AreEqual(1, engine.GetPlayerMomentum(player));
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void TheTrialOfGaryPowers_62_NoLeaderNoMomentumChange(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(engine, player, EngineFixtures.EmptyChanges);

        Assert.AreEqual(0, engine.GetPlayerMomentum(Player.Kennedy));
        Assert.AreEqual(0, engine.GetPlayerMomentum(Player.Nixon));
    }

    [TestMethod]
    public void TheTrialOfGaryPowers_62_ValidationAlwaysTrue()
    {
        var sut = Manifest.GMTCards[CardIndex];

        var result = sut.AreChangesValid(EngineFixtures.InvalidChanges);

        Assert.IsTrue(result);
    }

}