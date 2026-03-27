// ReSharper disable InconsistentNaming

using NineteenSixty.Data;
using NineteenSixty.Enums;
using NineteenSixty.Tests.Fixtures;
using PresidentialGameEngine.ClassLibrary.Data;

namespace NineteenSixty.Tests.Cards;

[TestClass]
public class TheGreatSealBug_29_Tests
{
    //"Nixon gains 1 issue support in Defense and may retrieve the Henry Cabot Lodge card from the discard pile if it is there."
    private const int CardIndex = 29;
    private static readonly Data.Card HenryCabotLodgeCard = Manifest.GMTCards[42];

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void TheGreatSealBug_29_NixonGainsDefenseSupport(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        var plan = new ActionPlan{Engine = engine,  Changes = EngineFixtures.EmptyChanges};
        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(plan, player);

        Assert.AreEqual(1, engine.GetSupportAmount(Issue.Defense));
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void TheGreatSealBug_29_HenryCabotLodgeRecoveredFromDiscardPile(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();
        engine.AddCardsToZone([HenryCabotLodgeCard], CardZone.DiscardPile, player);

        var plan = new ActionPlan{Engine = engine,  Changes = EngineFixtures.EmptyChanges};
        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(plan, player);

        Assert.AreEqual(HenryCabotLodgeCard.Index, engine.GetCardsInZone(CardZone.Hand, Player.Nixon).First().Index);
    }
    
    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void TheGreatSealBug_29_HenryCabotLodgeNotRecoveredFromRemovedPile(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();
        engine.AddCardsToZone([HenryCabotLodgeCard], CardZone.Removed, player);

        var plan = new ActionPlan{Engine = engine,  Changes = EngineFixtures.EmptyChanges};
        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(plan, player);

        Assert.AreEqual(0, engine.GetCardsInZone(CardZone.Hand, Player.Nixon).Count());
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void TheGreatSealBug_29_HenryCabotLodgeNotRecoveredFromStrategyPile(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();
        engine.AddCardsToZone([HenryCabotLodgeCard], CardZone.CampaignStrategy, player);

        var plan = new ActionPlan{Engine = engine,  Changes = EngineFixtures.EmptyChanges};
        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(plan, player);

        Assert.AreEqual(0, engine.GetCardsInZone(CardZone.Hand, Player.Nixon).Count());
    }

    [TestMethod]
    public void TheGreatSealBug_29_HenryCabotLodgeNotRecoveredFromHandOfKennedyPlayer()
    {
        var engine = EngineFixtures.GetGameEngine();
        engine.AddCardsToZone([HenryCabotLodgeCard], CardZone.Hand, Player.Kennedy);

        var plan = new ActionPlan{Engine = engine,  Changes = EngineFixtures.EmptyChanges};
        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(plan, Player.Nixon);

        Assert.AreEqual(0, engine.GetCardsInZone(CardZone.Hand, Player.Nixon).Count());
    }
    
    [TestMethod]
    public void TheGreatSealBug_29_HenryCabotLodgeRemainsInHandOfNixonPlayer()
    {
        var engine = EngineFixtures.GetGameEngine();
        engine.AddCardsToZone([HenryCabotLodgeCard], CardZone.Hand, Player.Nixon);

        var plan = new ActionPlan{Engine = engine,  Changes = EngineFixtures.EmptyChanges};
        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(plan, Player.Nixon);

        Assert.AreEqual(1, engine.GetCardsInZone(CardZone.Hand, Player.Nixon).Count());
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void TheGreatSealBug_29_HenryCabotLodgeNotRecoveredFromDeck(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();
        engine.AddCardsToZone([HenryCabotLodgeCard], CardZone.Deck, player);
        
        var plan = new ActionPlan{Engine = engine,  Changes = EngineFixtures.EmptyChanges};
        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(plan, player);

        Assert.AreEqual(0, engine.GetCardsInZone(CardZone.Hand, Player.Nixon).Count());
    }
    
    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void TheGreatSealBug_29_HenryCabotLodgeNotRecoveredFromInPlay(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();
        engine.AddCardsToZone([HenryCabotLodgeCard], CardZone.InPlay, player);
        
        var plan = new ActionPlan{Engine = engine,  Changes = EngineFixtures.EmptyChanges};
        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(plan, player);

        Assert.AreEqual(0, engine.GetCardsInZone(CardZone.Hand, Player.Nixon).Count());
    }

    [TestMethod]
    public void TheGreatSealBug_29_ValidationAlwaysTrue()
    {
        var sut = Manifest.GMTCards[CardIndex];

        var plan = new ActionPlan{Engine = null,  Changes = EngineFixtures.InvalidChanges};
        var result = sut.AreChangesValid(plan);

        Assert.IsTrue(result);
    }
    
}