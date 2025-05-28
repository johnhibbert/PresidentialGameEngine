using static PresidentialGameEngine.ClassLibrary.Tests.TestStubsFakesAndMocks;
using NSubstitute;
using PresidentialGameEngine.ClassLibrary.Interfaces;

namespace PresidentialGameEngine.ClassLibrary.Tests
{
    [TestClass]
    public class ComponentCollectionTests
    {
        #region IsReady Tests
        [TestMethod]
        public void IsReady_AllPropsAdded_ReturnsTrue()
        {
            ComponentCollection<FakePlayer, FakeLeader, FakeIssue, FakeState, FakeRegion> collection
                = new()
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

            Assert.IsTrue(collection.IsReady());
        }

        [TestMethod]
        public void IsReady_MissingAnyProps_ReturnsFalse()
        {
            ComponentCollection<FakePlayer, FakeLeader, FakeIssue, FakeState, FakeRegion> collection
                = new()
                {
                    MomentumComponent = Substitute.For<IAccumulatingComponent<FakePlayer>>(),
                    IssueSupportComponent = Substitute.For<ISupportComponent<FakePlayer, FakeLeader, FakeIssue>>(),
                    StateSupportComponent = Substitute.For<ISupportComponent<FakePlayer, FakeLeader, FakeState>>(),
                    IssuePositioningComponent = Substitute.For<IPositioningComponent<FakeIssue>>(),
                    PoliticalCapitalComponent = Substitute.For<IPoliticalCapitalComponent<FakePlayer>>(),
                    RestComponent = Substitute.For<IAccumulatingComponent<FakePlayer>>(),
                };

            Assert.IsFalse(collection.IsReady());
        }

        #endregion
    }
}
