// ReSharper disable InconsistentNaming
using NineteenSixty.Data;
using NineteenSixty.Enums;
using NineteenSixty.Tests.Fixtures;
using PresidentialGameEngine.ClassLibrary.Data;

namespace NineteenSixty.Tests.Cards;

[TestClass]
public class TheNewNixon_89_Tests
{
    //"The Nixon player gains 1 momentum marker.",
    private const int CardIndex = 89;
    
    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void TheNewNixon_89_NixonGainsOneMomentum(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        var nixonStartingMomentum = engine.GetPlayerMomentum(Player.Nixon);

        var plan = new ActionPlan{Engine = engine,  Changes = EngineFixtures.EmptyChanges};
        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(plan, player);

        Assert.AreEqual(engine.GetPlayerMomentum(Player.Nixon), nixonStartingMomentum + 1);
    }
        
    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void TheNewNixon_89_KennedyGainsZeroMomentum(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();
            
        var kennedyStartingMomentum = engine.GetPlayerMomentum(Player.Kennedy);

        var plan = new ActionPlan{Engine = engine,  Changes = EngineFixtures.EmptyChanges};
        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(plan, player);

        Assert.AreEqual(engine.GetPlayerMomentum(Player.Kennedy), kennedyStartingMomentum);
    }

    [TestMethod]
    public void TheNewNixon_89_ValidationAlwaysTrue()
    {
        var sut = Manifest.GMTCards[CardIndex];

        var plan = new ActionPlan{Engine = null,  Changes = EngineFixtures.InvalidChanges};
        var result = sut.AreChangesValid(plan);

        Assert.IsTrue(result);
    }

}