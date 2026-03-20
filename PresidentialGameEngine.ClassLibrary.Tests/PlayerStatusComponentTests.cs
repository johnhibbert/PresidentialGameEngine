using PresidentialGameEngine.ClassLibrary.Components;
using static PresidentialGameEngine.ClassLibrary.Tests.TestStubsFakesAndMocks;

namespace PresidentialGameEngine.ClassLibrary.Tests;

[TestClass]
public class PlayerStatusComponentTests
{
    #region Constructor Tests

    [TestMethod]
    public void Constructor_AllPlayersAdded()
    {
        var sut = new PlayerStatusComponent<FakePlayer, FakeStatus>();

        var result = sut.GetRawData();
        
        Assert.IsTrue(result.ContainsKey(FakePlayer.PlayerOne));
        Assert.IsTrue(result.ContainsKey(FakePlayer.PlayerTwo));
        Assert.IsTrue(result.ContainsKey(FakePlayer.PlayerThree));
    }

    #endregion

    #region UpdatePlayerStatus Tests

    [TestMethod]
    public void UpdatePlayerStatus_PlayerStatusUpdated()
    {
        var sut = new PlayerStatusComponent<FakePlayer, FakeStatus>();
        
        sut.UpdatePlayerStatus(FakePlayer.PlayerOne, FakeStatus.Sleepy);
        sut.UpdatePlayerStatus(FakePlayer.PlayerTwo, FakeStatus.Bashful);
        sut.UpdatePlayerStatus(FakePlayer.PlayerThree, FakeStatus.Hungry);
        
        var result = sut.GetRawData();
        
        Assert.IsTrue(result[FakePlayer.PlayerOne] == FakeStatus.Sleepy);
        Assert.IsTrue(result[FakePlayer.PlayerTwo] == FakeStatus.Bashful);
        Assert.IsTrue(result[FakePlayer.PlayerThree] == FakeStatus.Hungry);
    }

    #endregion
}