// ReSharper disable InconsistentNaming
using NineteenSixty.Enums;
using NineteenSixty.Tests.Fixtures;

namespace NineteenSixty.Tests.Cards;

[TestClass]
public class Volunteers_5_Tests
{
    //"Player gains 1 momentum marker."
    private const int CardIndex = 5;
    
    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void Volunteers_5_PlayerMomentumIncreasedByOne(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        var playerStartingMomentum = engine.GetPlayerMomentum(player);
        
        var plan = new ActionPlan{Engine = engine,  Changes = EngineFixtures.EmptyChanges};
        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(plan, player);

        Assert.AreEqual(playerStartingMomentum + 1, engine.GetPlayerMomentum(player));
    }
    
    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void Volunteers_5_OpponentMomentumIsUnchanged(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        var opponentStartingMomentum = engine.GetPlayerMomentum(player.ToOpponent());
        
        var plan = new ActionPlan{Engine = engine,  Changes = EngineFixtures.EmptyChanges};
        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(plan, player);
        Assert.AreEqual(opponentStartingMomentum, engine.GetPlayerMomentum(player.ToOpponent()));
    }

    [TestMethod]
    public void Volunteers_5_ValidationAlwaysTrue()
    {
        var sut = Manifest.GMTCards[CardIndex];

        var plan = new ActionPlan{Engine = null,  Changes = EngineFixtures.InvalidChanges};
        var result = sut.AreChangesValid(plan);

        Assert.IsTrue(result);
    }
    
}