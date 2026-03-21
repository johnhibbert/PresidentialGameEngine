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

    
    #endregion
    
    #region MoveCardFromOneZoneToAnother Tests
    
    [TestMethod]
    //[ExpectedException(typeof(ArgumentException))]
    public void MoveCardFromOneZoneToAnother_LLL()
    {
        var fakePlayer = new FakePlayer();
        var fakeCard = new FakeCardClass();
        
        var sut = new CardZoneComponent<FakeNonMatchingCardZone, FakePublicZone, FakePublicZone, FakePlayer, FakeCardClass>();
        
        //sut.MoveCardFromOneZoneToAnother(fakePlayer, fakeCard, FakeNonMatchingCardZone.Danger, FakeNonMatchingCardZone.Time);
    }
    
    #endregion
    
    
}