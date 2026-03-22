// ReSharper disable InconsistentNaming
using NineteenSixty.Data;
using NineteenSixty.Enums;
using NineteenSixty.Tests.Fixtures;
using PresidentialGameEngine.ClassLibrary.Data;

namespace NineteenSixty.Tests.Cards;

[TestClass]
public class TheOldNixon_70_Tests
{
    //"The Nixon player loses 1 momentum marker.  The Kennedy player loses 3 momentum markers."
    private const int CardIndex = 70;

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void TheOldNixon_70_NixonLosesOneMomentum(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        engine.GainMomentum(Player.Nixon, 5);

        var nixonStartingMomentum = engine.GetPlayerMomentum(Player.Nixon);

        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(engine, player, EngineFixtures.EmptyChanges);

        Assert.AreEqual(engine.GetPlayerMomentum(Player.Nixon), nixonStartingMomentum - 1);
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void TheOldNixon_70_KennedyLosesThreeMomentum(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        engine.GainMomentum(Player.Kennedy, 5);

        var kennedyStartingMomentum = engine.GetPlayerMomentum(Player.Kennedy);

        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(engine, player, EngineFixtures.EmptyChanges);

        Assert.AreEqual(engine.GetPlayerMomentum(Player.Kennedy), kennedyStartingMomentum - 3);
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void TheOldNixon_70_MomentumDoesNotGoNegative(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        engine.GainMomentum(Player.Nixon, 1);
        engine.GainMomentum(Player.Kennedy, 2);

        var sut = Manifest.GMTCards[CardIndex];

        sut.Event(engine, player, EngineFixtures.EmptyChanges);

        Assert.AreEqual(engine.GetPlayerMomentum(Player.Nixon), 0);
        Assert.AreEqual(engine.GetPlayerMomentum(Player.Kennedy), 0);
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void TheOldNixon_70_EventIsPlayableEvenWithZeroMomentum(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        engine.GainMomentum(player, 0);

        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(engine, player, EngineFixtures.EmptyChanges);

        Assert.AreEqual(engine.GetPlayerMomentum(player), 0);
    }

    [TestMethod]
    public void TheOldNixon_70_ValidationAlwaysTrue()
    {
        var sut = Manifest.GMTCards[CardIndex];

        var result = sut.AreChangesValid(EngineFixtures.InvalidChanges);

        Assert.IsTrue(result);
    }
}