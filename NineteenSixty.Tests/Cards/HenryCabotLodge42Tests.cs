using NineteenSixty.Data;
using NineteenSixty.Enums;
using NineteenSixty.Tests.Fixtures;
using PresidentialGameEngine.ClassLibrary.Data;

namespace NineteenSixty.Tests.Cards;

[TestClass]
public class HenryCabotLodge42Tests
{
    //"Nixon gains 2 state support in Massachusetts and 2 issue support in Defense.  If the Nixon candidate card is currently flipped to its Exhausted side, the Nixon player may reclaim it face-up.
    private const int CardIndex = 42;
    
    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void HenryCabotLodge_42_NixonGainsStateSupportInMass(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(engine, player, EngineFixtures.EmptyChanges);

        Assert.AreEqual(2, engine.GetSupportAmount(State.MA));
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void HenryCabotLodge_42_NixonGainsTwoIssueSupportInDefense(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(engine, player, EngineFixtures.EmptyChanges);

        Assert.AreEqual(2, engine.GetSupportAmount(Issue.Defense));
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void HenryCabotLodge_42_NixonBecomesUnexhausted(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();
        engine.ExhaustPlayer(Player.Nixon);

        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(engine, player, EngineFixtures.EmptyChanges);

        Assert.IsTrue(engine.GetGameState().Exhaustion[Player.Nixon]);
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void HenryCabotLodge_42_NixonRemainsUnexhaustedWhenPlayed(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();
        engine.UnexhaustPlayer(Player.Nixon);

        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(engine, player, EngineFixtures.EmptyChanges);

        Assert.IsTrue(engine.GetGameState().Exhaustion[Player.Nixon]);
    }

    [TestMethod]
    public void HenryCabotLodge_42_ValidationAlwaysTrue()
    {
        var sut = Manifest.GMTCards[CardIndex];

        var result = sut.AreChangesValid(EngineFixtures.InvalidChanges);

        Assert.IsTrue(result);
    }

}