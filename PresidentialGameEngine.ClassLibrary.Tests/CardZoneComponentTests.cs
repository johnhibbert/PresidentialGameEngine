using PresidentialGameEngine.ClassLibrary.Components;
using PresidentialGameEngine.ClassLibrary.Enums;
using PresidentialGameEngine.ClassLibrary.Exceptions;
using static PresidentialGameEngine.ClassLibrary.Tests.TestStubsFakesAndMocks;

namespace PresidentialGameEngine.ClassLibrary.Tests;

[TestClass]
public class CardZoneComponentTests
{
    #region Constructor Tests
    
    [TestMethod]
    public void Constructor_PrivateCollectionsCreatedSuccessfully()
    {
        var sut = new CardZoneComponent<FakeCardZone, FakePublicZone, FakePrivateZone, FakePlayer, FakeCardClass>();
        
        Assert.AreEqual(0, sut.GetCardsInPublicZone(FakePublicZone.Time).Count());
        Assert.AreEqual(0, sut.GetCardsInPublicZone(FakePublicZone.Danger).Count());
        
        Assert.AreEqual(0, sut.GetCardsInPrivateZone(FakePrivateZone.Phantom, FakePlayer.PlayerOne).Count());
        Assert.AreEqual(0, sut.GetCardsInPrivateZone(FakePrivateZone.Phantom, FakePlayer.PlayerTwo).Count());
        Assert.AreEqual(0, sut.GetCardsInPrivateZone(FakePrivateZone.Phantom, FakePlayer.PlayerThree).Count());
    }
    
    [TestMethod]
    [ExpectedException(typeof(EnumMismatchException))]
    public void Constructor_PrivateAndPublicZonesCannotHaveNameCrossover()
    {
        var sut = new CardZoneComponent<FakeCardZone, FakePublicZone, FakePrivateZoneWithDuplicatedName, FakePlayer, FakeCardClass>();
    }
    
    [TestMethod]
    [ExpectedException(typeof(EnumMismatchException))]
    public void Constructor_PrivateAndPublicZonesCannotHaveValueCrossover()
    {
        var sut = new CardZoneComponent<FakeCardZone, FakePublicZone, FakePrivateZoneWithDuplicatedValue, FakePlayer, FakeCardClass>();
    }

    [TestMethod]
    [ExpectedException(typeof(EnumMismatchException))]
    public void Constructor_ZoneCannotHaveExtraValuesToPublicAndPrivate()
    {
        var sut = new CardZoneComponent<FakeCardZoneWithExtraValue, FakePublicZone, FakePrivateZone, FakePlayer, FakeCardClass>();
    }
    
    [TestMethod]
    [ExpectedException(typeof(EnumMismatchException))]
    public void Constructor_ZoneCannotBeMissingValuesToPublicAndPrivate()
    {
        var sut = new CardZoneComponent<FakeCardZoneWithMissingValue, FakePublicZone, FakePrivateZone, FakePlayer, FakeCardClass>();
    }
    
    #endregion
    
    
    #region AddCardsToPublicZone Tests
    
    [TestMethod]
    public void AddCardsToPublicZone_LLL()
    {
        var zone = FakePublicZone.Danger;
        var card = new FakeCardClass();
        
        var sut = new CardZoneComponent<FakeCardZone, FakePublicZone, FakePrivateZone, FakePlayer, FakeCardClass>();
        
        sut.AddCardsToPublicZone([card], zone);

        var result = sut.GetCardsInPublicZone(zone);
        
        Assert.IsTrue(result.Count() == 1);
    }
    
    #endregion
    
    #region AddCardsToPrivateZone Tests
    
    [TestMethod]
    public void AddCardsToPrivateZone_CardAddedSuccessfully()
    {
        var player = FakePlayer.PlayerOne;
        var zone = FakePrivateZone.Phantom;
        var card = new FakeCardClass();
        
        var sut = new CardZoneComponent<FakeCardZone, FakePublicZone, FakePrivateZone, FakePlayer, FakeCardClass>();
        
        sut.AddCardsToPrivateZone([card], zone, player);

        var result = sut.GetCardsInPrivateZone(zone, player);
        
        Assert.IsTrue(result.Count() == 1);
    }
    
    #endregion
    
    
    #region MoveCardFromOneZoneToAnother Tests
    
    [TestMethod]
    public void MoveCardFromOneZoneToAnother_CardMovedSuccessfully()
    {
        /*var player = FakePlayer.PlayerOne;
        var publicZone = FakePublicZone.Danger;
        var sourceZone = FakeCardZone.Danger;
        var destinationZone = FakeCardZone.Time;
        var card = new FakeCardClass()
        {
            Index = 1,
            Title = "Test Card",
            AreChangesValid = changes => true,
            Event = (engine, player, choices) => { }
        };
        
        var sut = new CardZoneComponent<FakeCardZone, FakePublicZone, FakePrivateZone, FakePlayer, FakeCardClass>();
        
        sut.AddCardsToPublicZone([card], publicZone);

        sut.MoveCardFromOneZoneToAnother(player, card, sourceZone, destinationZone);*/
        
    }
    
    
        
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void MoveCardFromOneZoneToAnother_SourceAndDestinationCannotBeTheSame()
    {
        var player = FakePlayer.PlayerOne;
        var zone = FakeCardZone.Danger;
        var publicZone = FakePublicZone.Danger;
        var card = new FakeCardClass()
        {
            Index = 1,
            Title = "Test Card",
            AreChangesValid = changes => true,
            Event = (engine, player, choices) => { }
        };
        
        var sut = new CardZoneComponent<FakeCardZone, FakePublicZone, FakePrivateZone, FakePlayer, FakeCardClass>();
        
        sut.AddCardsToPublicZone([card], publicZone);

        sut.MoveCardFromOneZoneToAnother(player, card, zone, zone);
    }

    
    #endregion
    
    
}