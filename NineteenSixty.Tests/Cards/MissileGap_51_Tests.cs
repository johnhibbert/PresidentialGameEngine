// ReSharper disable InconsistentNaming
using NineteenSixty.Data;
using NineteenSixty.Enums;
using NineteenSixty.Tests.Fixtures;
using PresidentialGameEngine.ClassLibrary.Data;

namespace NineteenSixty.Tests.Cards;

[TestClass]
public class MissileGap_51_Tests
{
    //"Kennedy gains 3 issue support in Defense."
    private const int CardIndex = 51;
    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void MissileGap_51_KennedyGainsThreeIssueSupportInDefense(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        var plan = new ActionPlan{Engine = engine,  Changes = EngineFixtures.EmptyChanges};
        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(plan, player);

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
        var plan = new ActionPlan{Engine = engine,  Changes = EngineFixtures.EmptyChanges};
        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(plan, player);

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

        var plan = new ActionPlan{Engine = engine,  Changes = EngineFixtures.EmptyChanges};
        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(plan, player);;

        Assert.AreEqual(Leader.None, engine.GetLeader(Issue.Defense));
        Assert.AreEqual(0, engine.GetSupportAmount(Issue.Defense));
    }

    [TestMethod]
    public void MissileGap_51_ValidationAlwaysTrue()
    {
        var sut = Manifest.GMTCards[CardIndex];

        var plan = new ActionPlan{Engine = null,  Changes = EngineFixtures.InvalidChanges};
        var result = sut.AreChangesValid(plan);

        Assert.IsTrue(result);
    }

}