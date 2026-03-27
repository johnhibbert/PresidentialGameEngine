// ReSharper disable InconsistentNaming
using NineteenSixty.Data;
using NineteenSixty.Enums;
using NineteenSixty.Tests.Fixtures;
using PresidentialGameEngine.ClassLibrary.Data;

namespace NineteenSixty.Tests.Cards;

[TestClass]
public class CivilRightsAct_25_Tests
{
    //"Civil Rights moves up one space on the Issue Track and Nixon gains 1 issue support in Civil Rights."
    private const int CardIndex = 25;
    
    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void CivilRightsAct_25_CivilRightsGoesUpOnePosition(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        engine.SetIssueOrder([Issue.Economy, Issue.Defense, Issue.CivilRights]);

        var plan = new ActionPlan{Engine = engine,  Changes = EngineFixtures.EmptyChanges};
        
        var sut = Manifest.GMTCards[CardIndex];

        sut.Event(plan, player);

        Assert.AreEqual(Issue.CivilRights, engine.GetGameState().IssueOrder[1]);
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void CivilRightsAct_25_EconomyAtTopRemainsAtTop(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        engine.SetIssueOrder([Issue.CivilRights, Issue.Defense, Issue.Economy]);

        var plan = new ActionPlan{Engine = engine,  Changes = EngineFixtures.EmptyChanges};
        
        var sut = Manifest.GMTCards[CardIndex];

        sut.Event(plan, player);

        Assert.AreEqual(Issue.CivilRights, engine.GetGameState().IssueOrder[0]);
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void CivilRightsAct_25_IssueSupportGained(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        engine.SetIssueOrder([Issue.Economy, Issue.Defense, Issue.CivilRights]);
        engine.GainSupport(Player.Nixon, Issue.CivilRights, 1);

        var plan = new ActionPlan{Engine = engine,  Changes = EngineFixtures.EmptyChanges};
        
        var sut = Manifest.GMTCards[CardIndex];

        sut.Event(plan, player);

        Assert.AreEqual(2, engine.GetSupportAmount(Issue.CivilRights));
        Assert.AreEqual(Leader.Nixon, engine.GetLeader(Issue.CivilRights));
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void CivilRightsAct_25_StateSupportDecaysOpponent(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        engine.SetIssueOrder([Issue.Economy, Issue.Defense, Issue.CivilRights]);
        engine.GainSupport(Player.Kennedy, Issue.CivilRights, 2);

        var plan = new ActionPlan{Engine = engine,  Changes = EngineFixtures.EmptyChanges};
        
        var sut = Manifest.GMTCards[CardIndex];

        sut.Event(plan, player);

        Assert.AreEqual(1, engine.GetSupportAmount(Issue.CivilRights));
        Assert.AreEqual(Leader.Kennedy, engine.GetLeader(Issue.CivilRights));
    }

    [TestMethod]
    public void CivilRightsAct_25_ValidationAlwaysTrue()
    {
        var sut = Manifest.GMTCards[CardIndex];

        var plan = new ActionPlan{Engine = null,  Changes = EngineFixtures.InvalidChanges};
        var result = sut.AreChangesValid(plan);

        Assert.IsTrue(result);
    }

}