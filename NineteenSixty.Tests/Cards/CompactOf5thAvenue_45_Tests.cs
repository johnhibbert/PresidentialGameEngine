// ReSharper disable InconsistentNaming
using NineteenSixty.Data;
using NineteenSixty.Enums;
using NineteenSixty.Tests.Fixtures;
using PresidentialGameEngine.ClassLibrary.Data;

namespace NineteenSixty.Tests.Cards;

[TestClass]
public class CompactOf5thAvenue_45_Tests
{
    //"Immediately move the Nixon candidate token to New York without paying the normal travel costs.  Nixon gains 1 issue support in Civil Rights, 2 state support in New York, and 1 media support cube in the East."
    private const int CardIndex = 45;
    
    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void Compactof5thAvenue_45_NixonMovedToNewYork(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        engine.MovePlayerToState(Player.Nixon, State.AK);

        var sut = Manifest.GMTCards[CardIndex];

        sut.Event(engine, player, EngineFixtures.EmptyChanges);

        Assert.AreEqual(State.NY, engine.GetPlayerState(Player.Nixon));
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void Compactof5thAvenue_45_NixonGainsSupportInNewYork(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        engine.GainSupport(Player.Kennedy, State.NY, 1);

        var sut = Manifest.GMTCards[CardIndex];

        sut.Event(engine, player, EngineFixtures.EmptyChanges);

        Assert.AreEqual(Leader.Nixon, engine.GetLeader(State.NY));
        Assert.AreEqual(1, engine.GetSupportAmount(State.NY));
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void Compactof5thAvenue_45_NixonGainsSupportInCivilRights(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        var sut = Manifest.GMTCards[CardIndex];

        sut.Event(engine, player, EngineFixtures.EmptyChanges);

        Assert.AreEqual(Leader.Nixon, engine.GetLeader(Issue.CivilRights));
        Assert.AreEqual(1, engine.GetSupportAmount(Issue.CivilRights));
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void Compactof5thAvenue_45_NixonGainsMediaSupportInEast(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        var sut = Manifest.GMTCards[CardIndex];

        sut.Event(engine, player, EngineFixtures.EmptyChanges);

        var gameState = engine.GetGameState();
        
        Assert.AreEqual(Leader.Nixon, gameState.MediaSupportLevels[Region.East].Leader);
        Assert.AreEqual(1, gameState.MediaSupportLevels[Region.East].Amount);
    }

    [TestMethod]
    public void Compactof5thAvenue_45_ValidationAlwaysTrue()
    {
        var sut = Manifest.GMTCards[CardIndex];

        var result = sut.AreChangesValid(EngineFixtures.InvalidChanges);

        Assert.IsTrue(result);
    }

}