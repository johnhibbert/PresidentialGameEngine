using NineteenSixty.Data;
using NineteenSixty.Enums;
using NineteenSixty.Tests.Fixtures;
using PresidentialGameEngine.ClassLibrary.Data;

namespace NineteenSixty.Tests.Cards;

[TestClass]
public class GiveMeAWeek63Tests
{
    //"The Nixon player loses 2 momentum markers and must subtract 1 issue support in each issue."
    private const int CardIndex = 63;
    
    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void GiveMeAWeek_63_NixonLosesTwoMomentum(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        engine.GainMomentum(Player.Nixon, 5);

        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(engine, player, EngineFixtures.EmptyChanges);

        Assert.AreEqual(3, engine.GetPlayerMomentum(Player.Nixon));
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void GiveMeAWeek_63_NixonLosesOneSupportInEachIssue(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        engine.GainSupport(Player.Nixon, Issue.Defense, 4);
        engine.GainSupport(Player.Nixon, Issue.Economy, 3);
        engine.GainSupport(Player.Nixon, Issue.CivilRights, 2);

        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(engine, player, EngineFixtures.EmptyChanges);

        Assert.AreEqual(3, engine.GetSupportAmount(Issue.Defense));
        Assert.AreEqual(2, engine.GetSupportAmount(Issue.Economy));
        Assert.AreEqual(1, engine.GetSupportAmount(Issue.CivilRights));
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void GiveMeAWeek_63_SupportAndMomentumDoNotGoNegative(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        engine.GainMomentum(Player.Nixon, 1);
        engine.GainSupport(Player.Nixon, Issue.Defense, 0);
        engine.GainSupport(Player.Nixon, Issue.Economy, 0);
        engine.GainSupport(Player.Nixon, Issue.CivilRights, 0);

        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(engine, player, EngineFixtures.EmptyChanges);

        Assert.AreEqual(0, engine.GetPlayerMomentum(Player.Nixon));
        Assert.AreEqual(0, engine.GetSupportAmount(Issue.Defense));
        Assert.AreEqual(0, engine.GetSupportAmount(Issue.Economy));
        Assert.AreEqual(0, engine.GetSupportAmount(Issue.CivilRights));
    }

    [TestMethod]
    public void GiveMeAWeek_63_ValidationAlwaysTrue()
    {
        var sut = Manifest.GMTCards[CardIndex];

        var result = sut.AreChangesValid(EngineFixtures.InvalidChanges);

        Assert.IsTrue(result);
    }
}