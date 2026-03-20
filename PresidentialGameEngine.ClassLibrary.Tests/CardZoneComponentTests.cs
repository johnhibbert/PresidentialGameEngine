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
        var sut = new CardZoneComponent<FakeCardZone, FakePublicZone, FakePrivateZone, FakePlayer, FakeCardClass>();
        
        Assert.AreEqual(0, sut.GetCardsInPublicZone(FakePublicZone.Time).Count());
        Assert.AreEqual(0, sut.GetCardsInPublicZone(FakePublicZone.Danger).Count());
        
        Assert.AreEqual(0, sut.GetCardsInPrivateZone(FakePrivateZone.Phantom, FakePlayer.PlayerOne).Count());
        Assert.AreEqual(0, sut.GetCardsInPrivateZone(FakePrivateZone.Phantom, FakePlayer.PlayerTwo).Count());
        Assert.AreEqual(0, sut.GetCardsInPrivateZone(FakePrivateZone.Phantom, FakePlayer.PlayerThree).Count());
    }
    
    #endregion
    
    
    
}