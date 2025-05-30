using PresidentialGameEngine.ClassLibrary.Components;
using static PresidentialGameEngine.ClassLibrary.Tests.TestStubsFakesAndMocks;

namespace PresidentialGameEngine.ClassLibrary.Tests
{
    [TestClass]
    public class PlayerLocationTests
    {
        public static PlayerLocationComponent<FakePlayer, FakeState> PlayerLocationComponent { get { return GetLocationComponent(); } }

        private static PlayerLocationComponent<FakePlayer, FakeState> GetLocationComponent()
        {
            Dictionary<FakePlayer, FakeState> playerStartingLocations = [];
            playerStartingLocations.Add(FakePlayer.PlayerOne, FakeState.Mind);
            playerStartingLocations.Add(FakePlayer.PlayerTwo, FakeState.Denial);

            return new PlayerLocationComponent<FakePlayer, FakeState>(playerStartingLocations);
        }

        #region Constructor Tests

        [TestMethod]
        public void Constructor_DictionaryAccepted()
        {
            var sut = PlayerLocationComponent;
            var result = sut.GetPlayerState(FakePlayer.PlayerOne);
            Assert.AreEqual(FakeState.Mind, result);
        }

        #endregion

        #region GetPlayerState Tests

        [TestMethod]
        public void GetPlayerState_CorrectStatesReturned()
        {
            var sut = PlayerLocationComponent;
            Assert.AreEqual(FakeState.Mind, sut.GetPlayerState(FakePlayer.PlayerOne));
            Assert.AreEqual(FakeState.Denial, sut.GetPlayerState(FakePlayer.PlayerTwo));
        }

        #endregion

        #region GetPlayerState Tests

        [TestMethod]
        public void MovePlayerToState_CorrectStatesReturned()
        {
            var sut = PlayerLocationComponent;
            sut.MovePlayerToState(FakePlayer.PlayerOne, FakeState.OfTheArt);
            Assert.AreEqual(FakeState.OfTheArt, sut.GetPlayerState(FakePlayer.PlayerOne));
        }

        #endregion

    }
}
