// ReSharper disable InconsistentNaming
using NineteenSixty.Data;
using NineteenSixty.Enums;
using NineteenSixty.Tests.Fixtures;
using PresidentialGameEngine.ClassLibrary.Data;

namespace NineteenSixty.Tests.Cards;

[TestClass]
public class FatigueSetsIn_61_Tests
{
    //"If opponent's candidate card is currently available for play, flip it over to its Exhausted side."
    private const int CardIndex = 61;
    
    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void FatigueSetsIn_61_ReadyOpponentBecomesExhausted(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        var opponent = player.ToOpponent();
        
        var plan = new ActionPlan{Engine = engine,  Changes = EngineFixtures.EmptyChanges};
        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(plan, player);

        Assert.AreEqual(Status.Exhausted, engine.GetGameState().PlayerStatuses[opponent]);
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void FatigueSetsIn_61_ExhaustedOpponentRemainsExhausted(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();
        
        var opponent = player.ToOpponent();
        
        engine.ExhaustPlayer(opponent);

        var plan = new ActionPlan{Engine = engine,  Changes = EngineFixtures.EmptyChanges};
        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(plan, player);

        Assert.AreEqual(Status.Exhausted, engine.GetGameState().PlayerStatuses[opponent]);
    }
        
    [TestMethod]
    public void FatigueSetsIn_61_ValidationAlwaysTrue()
    {
        var sut = Manifest.GMTCards[CardIndex];

        var plan = new ActionPlan{Engine = null,  Changes = EngineFixtures.InvalidChanges};
        var result = sut.AreChangesValid(plan);

        Assert.IsTrue(result);
    }
}