using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using PresidentialGameEngine.ClassLibrary.Engines;
using PresidentialGameEngine.ClassLibrary.Interfaces;
using static PresidentialGameEngine.ClassLibrary.Tests.TestStubsFakesAndMocks;

namespace PresidentialGameEngine.ClassLibrary.Tests
{
    [TestClass]
    public class GenericPresidentialGameEngineTests
    {

        private ComponentCollection<FakePlayer, FakeLeader, FakeIssue, FakeState, FakeRegion> GetComponentCollection() 
        {
            return new()
            {
                MomentumComponent = Substitute.For<IAccumulatingComponent<FakePlayer>>(),
                IssueSupportComponent = Substitute.For<ISupportComponent<FakePlayer, FakeLeader, FakeIssue>>(),
                StateSupportComponent = Substitute.For<ISupportComponent<FakePlayer, FakeLeader, FakeState>>(),
                IssuePositioningComponent = Substitute.For<IPositioningComponent<FakeIssue>>(),
                PoliticalCapitalComponent = Substitute.For<IPoliticalCapitalComponent<FakePlayer>>(),
                RestComponent = Substitute.For<IAccumulatingComponent<FakePlayer>>(),
                RegionalComponent = Substitute.For<IRegionalComponent<FakeState, FakeRegion, FakePlayer>>(),
                EndorsementComponent = Substitute.For<ISupportComponent<FakePlayer, FakeLeader, FakeRegion>>(),
                MediaSupportComponent = Substitute.For<ISupportComponent<FakePlayer, FakeLeader, FakeRegion>>()
            };
        }

        #region Constructor Tests

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ThrowsIfCollectionNotReady()
        {
            var components = GetComponentCollection();
            components.MediaSupportComponent = null;

            var sut = new GenericPresidentialGameEngine
                <FakePlayer, FakeLeader, FakeIssue, FakeState, FakeRegion>(components);

        }

        [TestMethod]
        public void Constructor_OkayIfCollectionReady()
        {
            var components = GetComponentCollection();

            var sut = new GenericPresidentialGameEngine
                <FakePlayer, FakeLeader, FakeIssue, FakeState, FakeRegion>(components);

            Assert.IsNotNull(sut);

        }

        #endregion
    }
}
