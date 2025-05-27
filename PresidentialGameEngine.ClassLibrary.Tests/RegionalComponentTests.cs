using PresidentialGameEngine.ClassLibrary.Components;
using static PresidentialGameEngine.ClassLibrary.Tests.TestStubsFakesAndMocks;

namespace PresidentialGameEngine.ClassLibrary.Tests
{
    [TestClass]
    public class RegionalComponentTests
    {
        private RegionalComponent<FakeState, FakeRegion, FakePlayer> GetRegionalComponent()
        {
            Dictionary<FakeState, FakeRegion> statesAndRegions = [];
            statesAndRegions.Add(FakeState.Mind, FakeRegion.North);
            statesAndRegions.Add(FakeState.Denial, FakeRegion.North);
            statesAndRegions.Add(FakeState.Being, FakeRegion.SouthEast);
            statesAndRegions.Add(FakeState.OfTheArt, FakeRegion.SouthEast);

            Dictionary<FakePlayer, FakeState> playerStartingLocations = [];
            playerStartingLocations.Add(FakePlayer.PlayerOne, FakeState.Mind);
            playerStartingLocations.Add(FakePlayer.PlayerTwo, FakeState.Denial);

            return new RegionalComponent<FakeState, FakeRegion, FakePlayer>
                (statesAndRegions, playerStartingLocations);
        }

        #region Constructor Tests

        [TestMethod]
        public void Constructor_DictionaryAccepted()
        {
            var sut = GetRegionalComponent();
            var result = sut.GetRegionByState(FakeState.Mind);

            Assert.AreEqual(FakeRegion.North, result);
        }

        #endregion

        #region GetRegionByState Tests

        [TestMethod]
        [DataRow(FakeState.Mind, FakeRegion.North)]
        [DataRow(FakeState.Denial, FakeRegion.North)]
        [DataRow(FakeState.Being, FakeRegion.SouthEast)]
        [DataRow(FakeState.OfTheArt, FakeRegion.SouthEast)]
        public void GetRegionByState_CorrectRegionReturned(FakeState state, FakeRegion region)
        {
            var sut = GetRegionalComponent();
            var result = sut.GetRegionByState(state);

            Assert.AreEqual(region, result);
        }

        #endregion

        #region GetRegionByState Tests

        [TestMethod]
        public void GetStatesWithinRegion_CorrectStatesReturned()
        {
            var sut = GetRegionalComponent();
            var result = sut.GetStatesWithinRegion(FakeRegion.North);

            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.Contains(FakeState.Mind));
            Assert.IsTrue(result.Contains(FakeState.Denial));
        }

        #endregion

        #region GetPlayerState Tests

        [TestMethod]
        public void GetPlayerState_CorrectStatesReturned()
        {
            var sut = GetRegionalComponent();
            Assert.AreEqual(FakeState.Mind, sut.GetPlayerState(FakePlayer.PlayerOne));
            Assert.AreEqual(FakeState.Denial, sut.GetPlayerState(FakePlayer.PlayerTwo));
        }

        #endregion

        #region GetPlayerState Tests

        [TestMethod]
        public void MovePlayerToState_CorrectStatesReturned()
        {
            var sut = GetRegionalComponent();
            sut.MovePlayerToState(FakePlayer.PlayerOne, FakeState.OfTheArt);

            Assert.AreEqual(FakeState.OfTheArt, sut.GetPlayerState(FakePlayer.PlayerOne));
        }

        #endregion

    }
}
