using PresidentialGameEngine.ClassLibrary.Components;
using static PresidentialGameEngine.ClassLibrary.Tests.TestStubsFakesAndMocks;

namespace PresidentialGameEngine.ClassLibrary.Tests
{
    [TestClass]
    public class SupportComponentTests
    {

        #region Constructor Tests

        [TestMethod]
        public void Constructor_AllEntriesCreated()
        {
            SupportComponent<FakePlayer, FakeLeader, FakeSubject> sut = new();

            Assert.AreEqual(0, sut.GetSupportAmount(FakeSubject.SubjectOne));
            Assert.AreEqual(0, sut.GetSupportAmount(FakeSubject.SubjectTwo));
            Assert.AreEqual(0, sut.GetSupportAmount(FakeSubject.SubjectThree));
            Assert.AreEqual(0, sut.GetSupportAmount(FakeSubject.SubjectFour));
            Assert.AreEqual(0, sut.GetSupportAmount(FakeSubject.SubjectFive));
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void Constructor_EntryForNoneRemoved()
        {
            SupportComponent<FakePlayer, FakeLeader, FakeSubject> sut = new();

            Assert.AreEqual(0, sut.GetSupportAmount(FakeSubject.NoSubject));
        }

        #endregion

        #region GetLeader Tests

        [TestMethod]
        [DataRow(FakePlayer.PlayerThree, FakeSubject.SubjectFour, 2, FakeLeader.Leader3)]
        [DataRow(FakePlayer.PlayerOne, FakeSubject.SubjectOne, 10, FakeLeader.Leader)]
        [DataRow(FakePlayer.PlayerTwo, FakeSubject.SubjectFive, 1, FakeLeader.Leader2)]
        public void GetLeader_CorrectCorrespondingLeaderRetrieved(FakePlayer player, FakeSubject subject,
            int amount, FakeLeader expectedLeader)
        {

            SupportComponent<FakePlayer, FakeLeader, FakeSubject> sut = new();
            sut.GainSupport(player, subject, amount);

            var result = sut.GetLeader(subject);

            Assert.AreEqual(expectedLeader, result);
        }

        #endregion

        #region GetSupportAmount Tests

        [TestMethod]
        [DataRow(FakePlayer.PlayerOne, FakeSubject.SubjectThree, 3)]
        [DataRow(FakePlayer.PlayerTwo, FakeSubject.SubjectTwo, 12)]
        [DataRow(FakePlayer.PlayerThree, FakeSubject.SubjectFour, 6)]
        public void GetSupportAmount_CorrectAmountRetrieved(FakePlayer player, FakeSubject subject, int amount)
        {
            SupportComponent<FakePlayer, FakeLeader, FakeSubject> sut = new();
            sut.GainSupport(player, subject, amount);

            var result = sut.GetSupportAmount(subject);

            Assert.AreEqual(amount, result);
        }

        #endregion

        #region GainSupport Tests

        [TestMethod]
        [DataRow(FakePlayer.PlayerThree, FakeSubject.SubjectFour, 6)]
        [DataRow(FakePlayer.PlayerOne, FakeSubject.SubjectOne, 3)]
        [DataRow(FakePlayer.PlayerTwo, FakeSubject.SubjectTwo, 1)]
        public void GainSupport_CorrectAmountGained(FakePlayer player, FakeSubject subject, int amount)
        {
            SupportComponent<FakePlayer, FakeLeader, FakeSubject> sut = new();
            sut.GainSupport(player, subject, amount);

            var result = sut.GetSupportAmount(subject);

            Assert.AreEqual(amount, result);
        }
        #endregion

        #region LoseSupport Tests

        [TestMethod]
        [DataRow(FakePlayer.PlayerThree, FakeSubject.SubjectFour, 6, 2)]
        [DataRow(FakePlayer.PlayerOne, FakeSubject.SubjectOne, 3, 1)]
        [DataRow(FakePlayer.PlayerTwo, FakeSubject.SubjectTwo, 1, 1)]
        public void LoseSupport_CorrectAmountLost(FakePlayer player, FakeSubject subject, int startingAmount, int subtracted)
        {
            SupportComponent<FakePlayer, FakeLeader, FakeSubject> sut = new();
            sut.GainSupport(player, subject, startingAmount);
            sut.LoseSupport(player, subject, subtracted);

            var result = sut.GetSupportAmount(subject);

            Assert.AreEqual(startingAmount - subtracted, result);
        }

        [TestMethod]
        public void LoseSupport_AmountDoesNotGoNegative()
        {
            SupportComponent<FakePlayer, FakeLeader, FakeSubject> sut = new();
            sut.GainSupport(FakePlayer.PlayerOne, FakeSubject.SubjectOne, 5);
            sut.LoseSupport(FakePlayer.PlayerOne, FakeSubject.SubjectOne, 6);

            var result = sut.GetSupportAmount(FakeSubject.SubjectOne);

            Assert.AreEqual(0, result);
        }

        #endregion

    }
}
