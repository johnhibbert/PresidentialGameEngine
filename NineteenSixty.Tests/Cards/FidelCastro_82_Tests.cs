// ReSharper disable InconsistentNaming
using NineteenSixty.Data;
using NineteenSixty.Enums;
using NineteenSixty.Tests.Fixtures;
using PresidentialGameEngine.ClassLibrary.Data;

namespace NineteenSixty.Tests.Cards;

[TestClass]
public class FidelCastro_82_Tests
{
    //"The leader in Defense gains 1 momentum marker and 1 state support in Florida.",
    private const int CardIndex = 82;
    
    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void FidelCastro_82_LeaderGainsStateSupport(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        engine.GainSupport(player, Issue.Defense, 1);

        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(engine, player, EngineFixtures.EmptyChanges);

        Assert.AreEqual(1, engine.GetSupportAmount(State.FL));
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void FidelCastro_82_LeaderGainsMomentum(Player player)
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
    public void FidelCastro_82_NoLeaderInDefenseAwardsNothing(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        engine.LoseSupport(Player.Kennedy, Issue.Defense, int.MaxValue);
        engine.LoseSupport(Player.Nixon, Issue.Defense, int.MaxValue);

        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(engine, player, EngineFixtures.EmptyChanges);

        Assert.AreEqual(0, engine.GetSupportAmount(State.FL));
        Assert.AreEqual(0, engine.GetPlayerMomentum(player));
    }

    [TestMethod]
    public void FidelCastro_82_ValidationAlwaysTrue()
    {
        var sut = Manifest.GMTCards[CardIndex];

        var result = sut.AreChangesValid(EngineFixtures.InvalidChanges);

        Assert.IsTrue(result);
    }
}