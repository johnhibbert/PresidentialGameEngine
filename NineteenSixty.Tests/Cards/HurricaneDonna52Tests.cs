using NineteenSixty.Data;
using NineteenSixty.Enums;
using NineteenSixty.Tests.Fixtures;
using PresidentialGameEngine.ClassLibrary.Data;

namespace NineteenSixty.Tests.Cards;

[TestClass]
public class HurricaneDonna52Tests
{
    //"Move player's candidate token to Florida.  Player gains 1 momentum marker and 1 state support in Florida."
    private const int CardIndex = 52;
    
    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void HurricaneDonna_52_PlayerMovedToFlorida(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(engine, player, EngineFixtures.EmptyChanges);

        Assert.AreEqual(State.FL, engine.GetPlayerState(player));
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void HurricaneDonna_52_PlayerGainsOneMomentum(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();
        
        engine.GainMomentum(player, 2);

        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(engine, player, EngineFixtures.EmptyChanges);

        Assert.AreEqual(3, engine.GetPlayerMomentum(player));
    }
    
    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void HurricaneDonna_52_PlayerGainsOneStateSupportInFlorida(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(engine, player, EngineFixtures.EmptyChanges);

        Assert.AreEqual(1, engine.GetSupportAmount(State.FL));
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void HurricaneDonna_52_WorksEvenIfPlayerWasAlreadyInFlorida(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();
        engine.MovePlayerToState(player, State.FL);

        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(engine, player, EngineFixtures.EmptyChanges);

        Assert.AreEqual(State.FL, engine.GetPlayerState(player));
        Assert.AreEqual(1, engine.GetSupportAmount(State.FL));
        Assert.AreEqual(1, engine.GetPlayerMomentum(player));
    }

    [TestMethod]
    public void HurricaneDonna_52_ValidationAlwaysTrue()
    {
        var sut = Manifest.GMTCards[CardIndex];

        var result = sut.AreChangesValid(EngineFixtures.InvalidChanges);

        Assert.IsTrue(result);
    }


}