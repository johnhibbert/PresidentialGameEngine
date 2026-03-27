// ReSharper disable InconsistentNaming
using NineteenSixty.Data;
using NineteenSixty.Enums;
using NineteenSixty.Tests.Fixtures;
using PresidentialGameEngine.ClassLibrary.Data;

namespace NineteenSixty.Tests.Cards;

[TestClass]
public class HurricaneDonna_52_Tests
{
    //"Move player's candidate token to Florida.  Player gains 1 momentum marker and 1 state support in Florida."
    private const int CardIndex = 52;
    
    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void HurricaneDonna_52_PlayerMovedToFlorida(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        var plan = new ActionPlan{Engine = engine,  Changes = EngineFixtures.EmptyChanges};
        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(plan, player);

        Assert.AreEqual(State.FL, engine.GetPlayerState(player));
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void HurricaneDonna_52_PlayerGainsOneMomentum(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();
        
        engine.GainMomentum(player, 2);

        var plan = new ActionPlan{Engine = engine,  Changes = EngineFixtures.EmptyChanges};
        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(plan, player);

        Assert.AreEqual(3, engine.GetPlayerMomentum(player));
    }
    
    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void HurricaneDonna_52_PlayerGainsOneStateSupportInFlorida(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        var plan = new ActionPlan{Engine = engine,  Changes = EngineFixtures.EmptyChanges};
        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(plan, player);

        Assert.AreEqual(1, engine.GetSupportAmount(State.FL));
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void HurricaneDonna_52_WorksEvenIfPlayerWasAlreadyInFlorida(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();
        engine.MovePlayerToState(player, State.FL);

        var plan = new ActionPlan{Engine = engine,  Changes = EngineFixtures.EmptyChanges};
        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(plan, player);

        Assert.AreEqual(State.FL, engine.GetPlayerState(player));
        Assert.AreEqual(1, engine.GetSupportAmount(State.FL));
        Assert.AreEqual(1, engine.GetPlayerMomentum(player));
    }

    [TestMethod]
    public void HurricaneDonna_52_ValidationAlwaysTrue()
    {
        var sut = Manifest.GMTCards[CardIndex];

        var plan = new ActionPlan{Engine = null,  Changes = EngineFixtures.InvalidChanges};
        var result = sut.AreChangesValid(plan);

        Assert.IsTrue(result);
    }


}