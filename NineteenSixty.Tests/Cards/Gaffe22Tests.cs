using NineteenSixty.Enums;
using NineteenSixty.Tests.Fixtures;

namespace NineteenSixty.Tests.Cards;

[TestClass]
public class Gaffe22Tests
{
    //"Opponent loses 1 momentum marker and 3 state support in the state currently occupied by their candidate token."
    private const int CardIndex = 22;
    
    [TestMethod]
    [DataRow(Player.Nixon, State.MI)]
    [DataRow(Player.Kennedy, State.AZ)]
    public void Gaffe_22_SupportLost(Player player, State state)
    {
        var engine = EngineFixtures.GetGameEngine();

        var opponent = player.ToOpponent();

        engine.MovePlayerToState(opponent, state);
        engine.GainSupport(opponent, state, 3);

        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(engine, player, EngineFixtures.EmptyChanges);

        Assert.AreEqual(0, engine.GetSupportAmount(state));
        Assert.AreEqual(Leader.None, engine.GetLeader(state));
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void Gaffe_22_MomentumLost(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        var opponent = player.ToOpponent();
        engine.GainMomentum(opponent, 2);

        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(engine, player, EngineFixtures.EmptyChanges);

        Assert.AreEqual(1, engine.GetPlayerMomentum(opponent));
    }

    [TestMethod]
    [DataRow(Player.Nixon, State.MO)]
    [DataRow(Player.Kennedy, State.HI)]
    public void Gaffe_22_SupportAndMomentumDoNotGoNegative(Player player, State state)
    {
        var engine = EngineFixtures.GetGameEngine();

        var opponent = player.ToOpponent();

        engine.MovePlayerToState(opponent, state);
        engine.GainSupport(opponent, state, 1);

        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(engine, player, EngineFixtures.EmptyChanges);

        Assert.AreEqual(0, engine.GetSupportAmount(state));
        Assert.AreEqual(Leader.None, engine.GetLeader(state));
    }

    [TestMethod]
    public void Gaffe_22_ValidationAlwaysTrue()
    {
        var sut = Manifest.GMTCards[CardIndex];

        var result = sut.AreChangesValid(EngineFixtures.InvalidChanges);

        Assert.IsTrue(result);
    }
}