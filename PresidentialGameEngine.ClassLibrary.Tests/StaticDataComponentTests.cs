using PresidentialGameEngine.ClassLibrary.Components;
using PresidentialGameEngine.ClassLibrary.Data;
using PresidentialGameEngine.ClassLibrary.Interfaces;
using static PresidentialGameEngine.ClassLibrary.Tests.TestStubsFakesAndMocks;

namespace PresidentialGameEngine.ClassLibrary.Tests
{
    [TestClass]
    public class StaticDataComponentTests
    {
        static IStaticDataComponent<FakeState, FakePlayer, FakeRegion> StaticDataComponent { get { return GetStaticDataComponent(); } }

        static IStaticDataComponent<FakeState, FakePlayer, FakeRegion> GetStaticDataComponent()
        {
            return new StaticDataComponent<FakeState, FakePlayer, FakeRegion>(ExampleDictionary);

        }

        static IDictionary<FakeState, ILocationData<FakeState, FakePlayer, FakeRegion>> ExampleDictionary { get { return GetExampleDictionary(); } }
        
        static IDictionary<FakeState, ILocationData<FakeState, FakePlayer, FakeRegion>> GetExampleDictionary() 
        {
            return new Dictionary<FakeState, ILocationData<FakeState, FakePlayer, FakeRegion>>()
            {
                {
                    FakeState.Mind, new LocationData<FakeState, FakePlayer, FakeRegion>
                    (FakeState.Mind, FakeRegion.SouthEast, 3, FakePlayer.PlayerOne, 3) 
                },
                {
                    FakeState.Denial, new LocationData<FakeState, FakePlayer, FakeRegion>
                    (FakeState.Denial, FakeRegion.North, 1, FakePlayer.PlayerTwo, 2)
                }
            };
        }

        #region Constructor Tests

        [TestMethod]
        public void Constructor_InputAccepted()
        {
            var sut = GetStaticDataComponent();

            Assert.AreEqual(2, sut.GetRawData().Count);
        }

        #endregion

        #region GetRawData

        [TestMethod]
        public void GetRawData_CorrectAmountReturned()
        {
            var sut = GetStaticDataComponent();

            var result = sut.GetRawData();

            Assert.AreEqual(2, result.Count);
        }

        #endregion

        #region GetStateData

        [TestMethod]
        [DataRow(FakeState.Mind, FakeRegion.SouthEast, 3, FakePlayer.PlayerOne, 3)]
        [DataRow(FakeState.Denial, FakeRegion.North, 1, FakePlayer.PlayerTwo, 2)]
        public void GetStateData_ExpectedValuesReturned
            (FakeState state, FakeRegion expectedRegion, int expectedElectoralVotes,
            FakePlayer expectedPlayerTilt, int expectedStartingSupport)
        {
            var sut = GetStaticDataComponent();

            var result = sut.GetStateData(state);

            Assert.AreEqual(expectedRegion, result.Region);
            Assert.AreEqual(expectedElectoralVotes, result.ElectoralVotes);
            Assert.AreEqual(expectedPlayerTilt, result.Tilt);
            Assert.AreEqual(expectedStartingSupport, result.StartingSupport);
        }

        #endregion

        #region GetStateElectoralCollegeVotes

        [TestMethod]
        [DataRow(FakeState.Mind, 3)]
        [DataRow(FakeState.Denial, 1)]
        public void GetStateElectoralCollegeVotes_FFF(FakeState state, int expectedElectoralVotes)
        {
            var sut = GetStaticDataComponent();

            var result = sut.GetStateElectoralCollegeVotes(state);

            Assert.AreEqual(expectedElectoralVotes, result);
        }

        #endregion

        #region GetStatesInRegion

        [TestMethod]
        [DataRow(FakeRegion.SouthEast, FakeState.Mind)]
        [DataRow(FakeRegion.North, FakeState.Denial)]
        public void GetStatesInRegion_FFF (FakeRegion Region, FakeState expectedState)
        {
            var sut = GetStaticDataComponent();

            var result = sut.GetStatesInRegion(Region);

            Assert.AreEqual(expectedState, result.First());
        }

        #endregion

        #region GetStateStartingSupportLevel

        [TestMethod]
        [DataRow(FakeState.Mind, 3)]
        [DataRow(FakeState.Denial, 2)]
        public void GetStateStartingSupportLevel_FFF(FakeState state, int expectedStartingSupport)
        {
            var sut = GetStaticDataComponent();

            var result = sut.GetStateStartingSupportLevel(state);

            Assert.AreEqual(expectedStartingSupport, result);
        }

        #endregion

        #region GetStateTilt

        [TestMethod]
        [DataRow(FakeState.Mind, FakePlayer.PlayerOne)]
        [DataRow(FakeState.Denial, FakePlayer.PlayerTwo)]
        public void GetStateTilt_FFF(FakeState state, FakePlayer expectedPlayerTilt)
        {
            var sut = GetStaticDataComponent();

            var result = sut.GetStateTilt(state);

            Assert.AreEqual(expectedPlayerTilt, result);

        }

        #endregion



    }
}
