using PresidentialGameEngine.ClassLibrary.Components;
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
            FakeCardZone.Phantom
        };
        var publicZones = new HashSet<FakeCardZone>()
        {
            FakeCardZone.Time,
            FakeCardZone.Danger,
        };

        var sut = new CardZoneComponent<FakePlayer, FakeCardZone, FakeCardClass>(publicZones, privateZones);
        
        Assert.AreEqual(0, sut.GetCardsInPublicZone(FakeCardZone.Time).Count());
        Assert.AreEqual(0, sut.GetCardsInPublicZone(FakeCardZone.Danger).Count());
        
        Assert.AreEqual(0, sut.GetCardsInPrivateZone(FakeCardZone.Phantom, FakePlayer.PlayerOne).Count());
        Assert.AreEqual(0, sut.GetCardsInPrivateZone(FakeCardZone.Phantom, FakePlayer.PlayerTwo).Count());
        Assert.AreEqual(0, sut.GetCardsInPrivateZone(FakeCardZone.Phantom, FakePlayer.PlayerThree).Count());
                
    }
    
    
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Constructor_AllZonesMustBeIncluded()
    {
        var privateZones = new HashSet<FakeCardZone>()
        {
            FakeCardZone.Phantom
        };
        var publicZones = new HashSet<FakeCardZone>()
        {
            FakeCardZone.Time
        };

        var sut = new CardZoneComponent<FakePlayer, FakeCardZone, FakeCardClass>(publicZones, privateZones);
    }
    
    #endregion
    
    
    
}