using Microsoft.VisualStudio.TestTools.UnitTesting;
using PresidentialGameEngine.ClassLibrary.Components;
using static PresidentialGameEngine.ClassLibrary.Tests.TestStubsFakesAndMocks;

namespace PresidentialGameEngine.ClassLibrary.Tests
{
    [TestClass]
    public class CarriableSupportComponentTests
    {
        #region Constructor Tests
        [TestMethod]
        [DataRow(2)]
        [DataRow(5)]
        [DataRow(7)]
        public void Constructor_ThresholdAccepted(int threshold)
        {
            var sut = new CarriableSupportComponent<FakePlayer, FakeLeader, FakeSubject>(threshold);

            Assert.AreEqual(threshold, sut.Threshold);

        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ZeroOrNegativeThresholdFails(int threshold)
        {
            new CarriableSupportComponent<FakePlayer, FakeLeader, FakeSubject>(threshold);
        }

        #endregion

        #region IsRegionCarried Tests

        [TestMethod]
        public void IsRegionCarried_NotCarriedIfThresholdUnmet()
        {
            var sut = new CarriableSupportComponent<FakePlayer, FakeLeader, FakeState>(4);

            sut.GainSupport(FakePlayer.PlayerOne, FakeState.Denial, 3);

            Assert.IsFalse(sut.IsCarried(FakeState.Denial));
        }

        [TestMethod]
        public void IsRegionCarried_CarriedIfThresholdMatched()
        {
            var sut = new CarriableSupportComponent<FakePlayer, FakeLeader, FakeState>(4);

            sut.GainSupport(FakePlayer.PlayerOne, FakeState.Denial, 4);

            Assert.IsTrue(sut.IsCarried(FakeState.Denial));
        }

        [TestMethod]
        public void IsRegionCarried_CarriedIfThresholdExceeded()
        {
            var sut = new CarriableSupportComponent<FakePlayer, FakeLeader, FakeState>(4);

            sut.GainSupport(FakePlayer.PlayerOne, FakeState.Denial, 6);

            Assert.IsTrue(sut.IsCarried(FakeState.Denial));
        }

        #endregion
    }
}
