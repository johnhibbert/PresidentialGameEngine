using NineteenSixty.Data;
using NineteenSixty.Enums;
using NineteenSixty.Tests.Fixtures;
using PresidentialGameEngine.ClassLibrary.Data;

namespace NineteenSixty.Tests.Cards;

[TestClass]
public class MissileGap51Tests
{
    //"Kennedy gains 3 issue support in Defense."
    private const int CardIndex = 51;
    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void MissileGap_51_KennedyGainsThreeIssueSupportInDefense(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        var sut = Manifest.GMTCards[CardIndex];

        sut.Event(engine, player, EngineFixtures.EmptyChanges);

        Assert.AreEqual(Leader.Kennedy, engine.GetLeader(Issue.Defense));
        Assert.AreEqual(3, engine.GetSupportAmount(Issue.Defense));
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void MissileGap_51_IssueSupportGainedRemovesOpponentSupport(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();


        engine.GainSupport(Player.Nixon, Issue.Defense, 2);
        var sut = Manifest.GMTCards[CardIndex];

        sut.Event(engine, player, EngineFixtures.EmptyChanges);

        Assert.AreEqual(Leader.Kennedy, engine.GetLeader(Issue.Defense));
        Assert.AreEqual(1, engine.GetSupportAmount(Issue.Defense));
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void MissileGap_51_GainedSupportCanCauseNoLeaderInDefense(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        engine.GainSupport(Player.Nixon, Issue.Defense, 3);

        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(engine, player, EngineFixtures.EmptyChanges);

        Assert.AreEqual(Leader.None, engine.GetLeader(Issue.Defense));
        Assert.AreEqual(0, engine.GetSupportAmount(Issue.Defense));
    }

    [TestMethod]
    public void MissileGap_51_ValidationAlwaysTrue()
    {
        var sut = Manifest.GMTCards[CardIndex];

        var result = sut.AreChangesValid(EngineFixtures.InvalidChanges);

        Assert.IsTrue(result);
    }

}