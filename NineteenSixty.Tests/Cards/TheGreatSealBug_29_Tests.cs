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

        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(engine, player, EngineFixtures.EmptyChanges);

        Assert.AreEqual(1, engine.GetSupportAmount(Issue.Defense));
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void TheGreatSealBug_29_HenryCabotLodgeRecoveredFromDiscardPile(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();
        engine.AddCardsToZone([HenryCabotLodgeCard], CardZone.DiscardPile, player);

        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(engine, player, EngineFixtures.EmptyChanges);

        Assert.AreEqual(HenryCabotLodgeCard.Index, engine.GetCardsInZone(CardZone.Hand, Player.Nixon).First().Index);
    }
    
    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void TheGreatSealBug_29_HenryCabotLodgeNotRecoveredFromRemovedPile(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();
        engine.AddCardsToZone([HenryCabotLodgeCard], CardZone.Removed, player);

        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(engine, player, EngineFixtures.EmptyChanges);

        Assert.AreEqual(0, engine.GetCardsInZone(CardZone.Hand, Player.Nixon).Count());
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void TheGreatSealBug_29_HenryCabotLodgeNotRecoveredFromStrategyPile(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();
        engine.AddCardsToZone([HenryCabotLodgeCard], CardZone.CampaignStrategy, player);

        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(engine, player, EngineFixtures.EmptyChanges);

        Assert.AreEqual(0, engine.GetCardsInZone(CardZone.Hand, Player.Nixon).Count());
    }

    [TestMethod]
    public void TheGreatSealBug_29_HenryCabotLodgeNotRecoveredFromHandOfKennedyPlayer()
    {
        var engine = EngineFixtures.GetGameEngine();
        engine.AddCardsToZone([HenryCabotLodgeCard], CardZone.Hand, Player.Kennedy);

        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(engine, Player.Nixon, EngineFixtures.EmptyChanges);

        Assert.AreEqual(0, engine.GetCardsInZone(CardZone.Hand, Player.Nixon).Count());
    }
    
    [TestMethod]
    public void TheGreatSealBug_29_HenryCabotLodgeRemainsInHandOfNixonPlayer()
    {
        var engine = EngineFixtures.GetGameEngine();
        engine.AddCardsToZone([HenryCabotLodgeCard], CardZone.Hand, Player.Nixon);

        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(engine, Player.Nixon, EngineFixtures.EmptyChanges);

        Assert.AreEqual(1, engine.GetCardsInZone(CardZone.Hand, Player.Nixon).Count());
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void TheGreatSealBug_29_HenryCabotLodgeNotRecoveredFromDeck(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();
        engine.AddCardsToZone([HenryCabotLodgeCard], CardZone.Deck, player);
        
        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(engine, player, EngineFixtures.EmptyChanges);

        Assert.AreEqual(0, engine.GetCardsInZone(CardZone.Hand, Player.Nixon).Count());
    }
    
    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void TheGreatSealBug_29_HenryCabotLodgeNotRecoveredFromInPlay(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();
        engine.AddCardsToZone([HenryCabotLodgeCard], CardZone.InPlay, player);
        
        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(engine, player, EngineFixtures.EmptyChanges);

        Assert.AreEqual(0, engine.GetCardsInZone(CardZone.Hand, Player.Nixon).Count());
    }

    [TestMethod]
    public void TheGreatSealBug_29_ValidationAlwaysTrue()
    {
        var sut = Manifest.GMTCards[CardIndex];

        var result = sut.AreChangesValid(EngineFixtures.InvalidChanges);

        Assert.IsTrue(result);
    }
    
}