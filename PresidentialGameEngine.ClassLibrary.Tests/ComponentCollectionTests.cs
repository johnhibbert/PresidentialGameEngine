using static PresidentialGameEngine.ClassLibrary.Tests.TestStubsFakesAndMocks;
using NSubstitute;
using PresidentialGameEngine.ClassLibrary.Interfaces;
using PresidentialGameEngine.ClassLibrary.Components;

namespace PresidentialGameEngine.ClassLibrary.Tests
{
    [TestClass]
    public class ComponentCollectionTests
    {
        #region IsReady Tests
        [TestMethod]
        public void IsReady_AllPropsAdded_ReturnsTrue()
        {
            ComponentCollection<FakePlayer, FakeLeader, FakeIssue, FakeState, FakeRegion, FakeCardClass> collection
                = new()
                {
                    MomentumComponent = Substitute.For<IAccumulatingComponent<FakePlayer>>(),
                    IssueSupportComponent = Substitute.For<ISupportComponent<FakePlayer, FakeLeader, FakeIssue>>(),
                    StateSupportComponent = Substitute.For<ISupportComponent<FakePlayer, FakeLeader, FakeState>>(),
                    IssuePositioningComponent = Substitute.For<IPositioningComponent<FakeIssue>>(),
                    PoliticalCapitalComponent = Substitute.For<IPoliticalCapitalComponent<FakePlayer>>(),
                    RestComponent = Substitute.For<IAccumulatingComponent<FakePlayer>>(),
                    PlayerLocationComponent = Substitute.For<IPlayerLocationComponent<FakePlayer, FakeState>>(),
                    EndorsementComponent = Substitute.For<ISupportComponent<FakePlayer, FakeLeader, FakeRegion>>(),
                    MediaSupportComponent = Substitute.For<ISupportComponent<FakePlayer, FakeLeader, FakeRegion>>(),
                    ExhaustionComponent = Substitute.For<IExhaustionComponent<FakePlayer>>(),
                    CardComponent = Substitute.For<ICardComponent<FakePlayer, FakeCardClass>>()
                };

            Assert.IsTrue(collection.IsReady());
        }

        [TestMethod]
        public void IsReady_MissingAnyProps_ReturnsFalse()
        {
            ComponentCollection<FakePlayer, FakeLeader, FakeIssue, FakeState, FakeRegion, FakeCardClass> collection
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
