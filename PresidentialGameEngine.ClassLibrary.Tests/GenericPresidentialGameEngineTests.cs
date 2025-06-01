using NSubstitute;
using PresidentialGameEngine.ClassLibrary.Components;
using PresidentialGameEngine.ClassLibrary.Data;
using PresidentialGameEngine.ClassLibrary.Engines;
using PresidentialGameEngine.ClassLibrary.Interfaces;
using static PresidentialGameEngine.ClassLibrary.Tests.TestStubsFakesAndMocks;

namespace PresidentialGameEngine.ClassLibrary.Tests
{
    [TestClass]
    public class GenericPresidentialGameEngineTests
    {
        //The vast majority of this will also be tested in greater detail
        //By the test defining the cards themselves

        //Hmm This might take a LOT of mocking.  Is it worth it?  Or should I just send concretes down?
        //Or let it be covered by everything else?

        private ComponentCollection<FakePlayer, FakeLeader, FakeIssue, FakeState, FakeRegion, FakeCardClass> GetMockComponentCollection() 
        {
            return new()
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
                CardComponent = Substitute.For<ICardComponent<FakePlayer, FakeCardClass>>(),
                StaticDataComponent = Substitute.For<IStaticDataComponent<FakeState, FakePlayer, FakeRegion>>(),
            };
        }

        private ComponentCollection<FakePlayer, FakeLeader, FakeIssue, FakeState, FakeRegion, FakeCardClass> GetMostlyRealComponentCollection()
        {
            return new()
            {
                MomentumComponent = new AccumulatingComponent<FakePlayer>(),
                IssueSupportComponent = new SupportComponent<FakePlayer, FakeLeader, FakeIssue>(),
                StateSupportComponent = new SupportComponent<FakePlayer, FakeLeader, FakeState>(),
                IssuePositioningComponent = new PositioningComponent<FakeIssue>(),
                PoliticalCapitalComponent = Substitute.For<IPoliticalCapitalComponent<FakePlayer>>(),
                RestComponent = new AccumulatingComponent<FakePlayer>(),
                PlayerLocationComponent = Substitute.For<IPlayerLocationComponent<FakePlayer, FakeState>>(),
                EndorsementComponent = new SupportComponent<FakePlayer, FakeLeader, FakeRegion>(),
                MediaSupportComponent = new SupportComponent<FakePlayer, FakeLeader, FakeRegion>(),
                ExhaustionComponent = new ExhaustionComponent<FakePlayer>(),
                CardComponent = Substitute.For<ICardComponent<FakePlayer, FakeCardClass>>(),
                StaticDataComponent = Substitute.For<IStaticDataComponent<FakeState, FakePlayer, FakeRegion>>(),
            };
        }

        #region Constructor Tests

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ThrowsIfCollectionNotReady()
        {
            var components = GetMockComponentCollection();
            components.MediaSupportComponent = null;

            var sut = new GenericPresidentialGameEngine
                <FakePlayer, FakeLeader, FakeIssue, FakeState, FakeRegion, FakeCardClass>(components);

        }

        [TestMethod]
        public void Constructor_OkayIfCollectionReady()
        {
            var components = GetMockComponentCollection();

            var sut = new GenericPresidentialGameEngine
                <FakePlayer, FakeLeader, FakeIssue, FakeState, FakeRegion, FakeCardClass>(components);

            Assert.IsNotNull(sut);

        }

        #endregion


        #region ImplementChanges Tests

        //One of the only methods with a CC above 1.
        [TestMethod]
        public void ImplementChanges_ChangesApplied()
        {
            NewMasterPlayerChosenChanges<FakePlayer, FakeIssue, FakeState, FakeRegion> playerChoices = new();

            var oneStateSupport =
                new SupportChange<FakePlayer, FakeState>(FakePlayer.PlayerTwo, FakeState.Being, 1);
            var oneIssueSupport =
                new SupportChange<FakePlayer, FakeIssue>(FakePlayer.PlayerTwo, FakeIssue.KetchupOnHotDogs, 1);

            playerChoices.StateChanges.Add(oneStateSupport);
            playerChoices.IssueChanges.Add(oneIssueSupport);

            var components = GetMostlyRealComponentCollection();

            var sut = new GenericPresidentialGameEngine
                <FakePlayer, FakeLeader, FakeIssue, FakeState, FakeRegion, FakeCardClass>(components);

            sut.NEWImplementChanges(playerChoices);

            Assert.AreEqual(1, sut.GetSupportAmount(FakeState.Being));
            Assert.AreEqual(1, sut.GetSupportAmount(FakeIssue.KetchupOnHotDogs));
        }

        [TestMethod]
        public void ImplementChanges_MoreChangesApplied()
        {
            NewMasterPlayerChosenChanges<FakePlayer, FakeIssue, FakeState, FakeRegion> playerChoices = new();

            var threeSupportInDenial =
                new SupportChange<FakePlayer, FakeState>(FakePlayer.PlayerTwo, FakeState.Denial, 3);
            var fiveSupportInBeing =
                new SupportChange<FakePlayer, FakeState>(FakePlayer.PlayerOne, FakeState.Being, 5);
            var oneSupportInOfTheArt =
                new SupportChange<FakePlayer, FakeState>(FakePlayer.PlayerThree, FakeState.OfTheArt, 1);

            playerChoices.StateChanges.Add(threeSupportInDenial);
            playerChoices.StateChanges.Add(fiveSupportInBeing);
            playerChoices.StateChanges.Add(oneSupportInOfTheArt);

            var components = GetMostlyRealComponentCollection();

            var sut = new GenericPresidentialGameEngine
                <FakePlayer, FakeLeader, FakeIssue, FakeState, FakeRegion, FakeCardClass>(components);

            sut.NEWImplementChanges(playerChoices);

            Assert.AreEqual(3, sut.GetSupportAmount(FakeState.Denial));
            Assert.AreEqual(5, sut.GetSupportAmount(FakeState.Being));
            Assert.AreEqual(1, sut.GetSupportAmount(FakeState.OfTheArt));
        }

        #endregion


    }
}
