using PresidentialGameEngine.ClassLibrary.Components;
using static PresidentialGameEngine.ClassLibrary.Tests.TestStubsFakesAndMocks;

namespace PresidentialGameEngine.ClassLibrary.Tests
{
    [TestClass]
    public class ExhaustionComponentTests
    {
        #region Constructor Tests

        [TestMethod]
        public void Constructor_AllPlayersAddedAndAreReady()
        {
            var sut = new ExhaustionComponent<FakePlayer>();

            Assert.IsTrue(sut.IsPlayerReady(FakePlayer.PlayerOne));
            Assert.IsTrue(sut.IsPlayerReady(FakePlayer.PlayerTwo));
            Assert.IsTrue(sut.IsPlayerReady(FakePlayer.PlayerThree));
        }

        #endregion

        #region IsPlayerReady Tests

        [TestMethod]
        public void IsPlayerReady_PlayerReadyAsExpected()
        {
            var sut = new ExhaustionComponent<FakePlayer>();

            sut.ExhaustPlayer(FakePlayer.PlayerTwo);

            Assert.IsTrue(sut.IsPlayerReady(FakePlayer.PlayerOne));
            Assert.IsFalse(sut.IsPlayerReady(FakePlayer.PlayerTwo));
            Assert.IsTrue(sut.IsPlayerReady(FakePlayer.PlayerThree));
        }

        #endregion

        #region ExhaustPlayer Tests

        [TestMethod]
        [DataRow(FakePlayer.PlayerOne)]
        [DataRow(FakePlayer.PlayerTwo)]
        [DataRow(FakePlayer.PlayerThree)]
        public void ExhaustPlayer_PlayerExhaustedAsExpected(FakePlayer player)
        {
            var sut = new ExhaustionComponent<FakePlayer>();

            sut.ExhaustPlayer(player);

            Assert.IsFalse(sut.IsPlayerReady(player));

        }

        #endregion

        #region UnexhaustPlayer Tests

        [DataRow(FakePlayer.PlayerOne)]
        [DataRow(FakePlayer.PlayerTwo)]
        [DataRow(FakePlayer.PlayerThree)]
        public void UnexhaustPlayer_PlayerRestoredAsExpected(FakePlayer player)
        {
            var sut = new ExhaustionComponent<FakePlayer>();

            sut.ExhaustPlayer(player);
            sut.UnexhaustPlayer(player);

            Assert.IsTrue(sut.IsPlayerReady(player));
        }

        #endregion

    }
}
