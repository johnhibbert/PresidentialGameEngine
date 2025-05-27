using PresidentialGameEngine.ClassLibrary.Components;
using static PresidentialGameEngine.ClassLibrary.Tests.TestStubsFakesAndMocks;

namespace PresidentialGameEngine.ClassLibrary.Tests
{
    [TestClass]
    public class RegionalComponentTests
    {
        private RegionalComponent<FakeState, FakeRegion> GetRegionalComponent()
        {
            Dictionary<FakeState, FakeRegion> keyValuePairs = [];
            keyValuePairs.Add(FakeState.Mind, FakeRegion.North);
            keyValuePairs.Add(FakeState.Denial, FakeRegion.North);
            keyValuePairs.Add(FakeState.Being, FakeRegion.SouthEast);
            keyValuePairs.Add(FakeState.OfTheArt, FakeRegion.SouthEast);

            return new RegionalComponent<FakeState, FakeRegion>(keyValuePairs);
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

    }
}
