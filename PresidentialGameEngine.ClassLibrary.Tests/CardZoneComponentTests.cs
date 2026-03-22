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
        var privateZones = new HashSet<FakeCardZone>()
        {
            FakeCardZone.Phantom,
        };

        var sut = new CardZoneComponent<FakeCardZone, FakePlayer, FakeCardClass>(privateZones);

        Assert.AreEqual(0, sut.GetCardsInZone(FakeCardZone.Time, FakePlayer.PlayerOne).Count());
        Assert.AreEqual(0, sut.GetCardsInZone(FakeCardZone.Danger, FakePlayer.PlayerOne).Count());

        Assert.AreEqual(0, sut.GetCardsInZone(FakeCardZone.Phantom, FakePlayer.PlayerOne).Count());
        Assert.AreEqual(0, sut.GetCardsInZone(FakeCardZone.Phantom, FakePlayer.PlayerTwo).Count());
        Assert.AreEqual(0, sut.GetCardsInZone(FakeCardZone.Phantom, FakePlayer.PlayerThree).Count());
    }

    #endregion
    
    
    #region AddCardsToZone Tests
    
    [TestMethod]
    public void AddCardsToZone_CardAddedToPublicZone()
    {
        var privateZones = new HashSet<FakeCardZone>()
        {
            FakeCardZone.Phantom,
        };
        
        var player = FakePlayer.PlayerOne;
        var zone = FakeCardZone.Danger;
        var card = new FakeCardClass();
        
        var sut = new CardZoneComponent<FakeCardZone, FakePlayer, FakeCardClass>(privateZones);
        
        sut.AddCardsToZone([card], zone, player);

        var result = sut.GetCardsInZone(zone, player);
        
        Assert.IsTrue(result.Count() == 1);
    }
    
    [TestMethod]
    public void AddCardsToZone_CardAddedToPrivateZone()
    {
        var privateZones = new HashSet<FakeCardZone>()
        {
            FakeCardZone.Phantom,
        };
        
        var player = FakePlayer.PlayerTwo;
        var zone = FakeCardZone.Phantom;
        var card = new FakeCardClass();
        
        var sut = new CardZoneComponent<FakeCardZone, FakePlayer, FakeCardClass>(privateZones);
        
        sut.AddCardsToZone([card], zone, player);

        var result = sut.GetCardsInZone(zone, player);
        Assert.IsTrue(result.Count() == 1);
    }
    
        
    [TestMethod]
    [DataRow(FakePlayer.PlayerOne, FakePlayer.PlayerTwo)]
    [DataRow(FakePlayer.PlayerOne, FakePlayer.PlayerThree)]
    [DataRow(FakePlayer.PlayerTwo, FakePlayer.PlayerThree)]
    [DataRow(FakePlayer.PlayerTwo, FakePlayer.PlayerOne)]
    [DataRow(FakePlayer.PlayerThree, FakePlayer.PlayerOne)]
    [DataRow(FakePlayer.PlayerThree, FakePlayer.PlayerTwo)]
    public void AddCardsToZone_CardAddedToPrivateZoneForCorrectPlayer(FakePlayer player, FakePlayer otherPlayer)
    {
        var privateZones = new HashSet<FakeCardZone>()
        {
            FakeCardZone.Phantom,
        };
        
        var zone = FakeCardZone.Phantom;
        var card = new FakeCardClass();

        var sut = new CardZoneComponent<FakeCardZone, FakePlayer, FakeCardClass>(privateZones);
        
        sut.AddCardsToZone([card], zone, player);

        var result = sut.GetCardsInZone(zone, otherPlayer);
        Assert.IsTrue(!result.Any());
    }
    
    #endregion
    
    
    #region MoveCardFromOneZoneToAnother Tests
    
    [TestMethod]
    [DataRow(FakeCardZone.Phantom, FakeCardZone.Time)]
    [DataRow(FakeCardZone.Phantom, FakeCardZone.Danger)]
    [DataRow(FakeCardZone.Time, FakeCardZone.Phantom)]
    [DataRow(FakeCardZone.Time, FakeCardZone.Danger)]
    [DataRow(FakeCardZone.Danger, FakeCardZone.Time)]
    [DataRow(FakeCardZone.Danger, FakeCardZone.Phantom)]
    public void MoveCardFromOneZoneToAnother_CardRemovedFromSource(FakeCardZone sourceZone, FakeCardZone destinationZone)
    {
        var privateZones = new HashSet<FakeCardZone>()
        {
            FakeCardZone.Phantom,
        };
        
        var player = FakePlayer.PlayerTwo;
        var card = new FakeCardClass();
        
        var sut = new CardZoneComponent<FakeCardZone, FakePlayer, FakeCardClass>(privateZones);
        
        sut.AddCardsToZone([card], sourceZone, player);

        sut.MoveCardFromOneZoneToAnother(player, card, sourceZone, destinationZone);
        
        var result = sut.GetCardsInZone(sourceZone, player);
        Assert.IsTrue(!result.Any());
        
    }
    
    [TestMethod]
    [DataRow(FakeCardZone.Phantom, FakeCardZone.Time)]
    [DataRow(FakeCardZone.Phantom, FakeCardZone.Danger)]
    [DataRow(FakeCardZone.Time, FakeCardZone.Phantom)]
    [DataRow(FakeCardZone.Time, FakeCardZone.Danger)]
    [DataRow(FakeCardZone.Danger, FakeCardZone.Time)]
    [DataRow(FakeCardZone.Danger, FakeCardZone.Phantom)]
    public void MoveCardFromOneZoneToAnother_CardAddedToDestination(FakeCardZone sourceZone, FakeCardZone destinationZone)
    {
        var privateZones = new HashSet<FakeCardZone>()
        {
            FakeCardZone.Phantom,
        };
        
        var player = FakePlayer.PlayerTwo;
        var card = new FakeCardClass();
        
        var sut = new CardZoneComponent<FakeCardZone, FakePlayer, FakeCardClass>(privateZones);
        
        sut.AddCardsToZone([card], sourceZone, player);

        sut.MoveCardFromOneZoneToAnother(player, card, sourceZone, destinationZone);
        
        var result = sut.GetCardsInZone(destinationZone, player);
        Assert.IsTrue(result.Any());
        
    }
    
        
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void MoveCardFromOneZoneToAnother_SourceAndDestinationCannotBeTheSame()
    {
        var privateZones = new HashSet<FakeCardZone>()
        {
            FakeCardZone.Phantom,
        };
        
        var player = FakePlayer.PlayerTwo;
        var sourceZone = FakeCardZone.Phantom;
        var destinationZone = FakeCardZone.Phantom;
        var card = new FakeCardClass();
        
        var sut = new CardZoneComponent<FakeCardZone, FakePlayer, FakeCardClass>(privateZones);
        
        sut.AddCardsToZone([card], sourceZone, player);

        sut.MoveCardFromOneZoneToAnother(player, card, sourceZone, destinationZone);
    }

    
    #endregion
    
    
}