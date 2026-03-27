// ReSharper disable InconsistentNaming
using NineteenSixty.Data;
using NineteenSixty.Enums;
using NineteenSixty.Tests.Fixtures;
using PresidentialGameEngine.ClassLibrary.Data;

namespace NineteenSixty.Tests.Cards;

[TestClass]
public class StumpSpeech_64_Tests
{
    //"If opponent has more momentum markers, player gains enough to have the same number."
    private const int CardIndex = 64;
    
    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void StumpSpeech_64_MomentumGainedIfOpponentHasMore(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        engine.GainMomentum(player.ToOpponent(), 5);

        var plan = new ActionPlan{Engine = engine,  Changes = EngineFixtures.EmptyChanges};
        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(plan, player);

        Assert.AreEqual(engine.GetPlayerMomentum(player), engine.GetPlayerMomentum(player.ToOpponent()));
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void StumpSpeech_64_NoMomentumGainedIfPlayedByPlayerWithMore(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        engine.GainMomentum(player, 5);

        var plan = new ActionPlan{Engine = engine,  Changes = EngineFixtures.EmptyChanges};
        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(plan, player);

        Assert.AreEqual(5, engine.GetPlayerMomentum(player));
        Assert.AreEqual(0, engine.GetPlayerMomentum(player.ToOpponent()));
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void StumpSpeech_64_NoMomentumGainedIfPlayersHaveSameAmount(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        engine.GainMomentum(Player.Kennedy, 2);
        engine.GainMomentum(Player.Nixon, 2);

        var plan = new ActionPlan{Engine = engine,  Changes = EngineFixtures.EmptyChanges};
        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(plan, player);

        Assert.AreEqual(2, engine.GetPlayerMomentum(Player.Nixon));
        Assert.AreEqual(2, engine.GetPlayerMomentum(Player.Kennedy));
    }

    [TestMethod]
    public void StumpSpeech_64_ValidationAlwaysTrue()
    {
        var sut = Manifest.GMTCards[CardIndex];
        var plan = new ActionPlan{Engine = null,  Changes = EngineFixtures.InvalidChanges};
        var result = sut.AreChangesValid(plan);

        Assert.IsTrue(result);
    }

}