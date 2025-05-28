using PresidentialGameEngine.ClassLibrary.Components;
using static PresidentialGameEngine.ClassLibrary.Tests.TestStubsFakesAndMocks;

namespace PresidentialGameEngine.ClassLibrary.Tests
{
    [TestClass]
    public class AccumulatingComponentTests
    {

        #region Constructor Tests
        [TestMethod]
        public void Constructor_AllEntriesCreated_SizeTwoEnum()
        {
            AccumulatingComponent<FakeEnumWithTwo> sut = new();

            Assert.AreEqual(0, sut.GetPlayerAmount(FakeEnumWithTwo.ElementOne));
            Assert.AreEqual(0, sut.GetPlayerAmount(FakeEnumWithTwo.ElementTwo));
        }

        [TestMethod]
        public void Constructor_AllEntriesCreated_SizeThreeEnum()
        {
            AccumulatingComponent<FakeEnumWithThree> sut = new();

            Assert.AreEqual(0, sut.GetPlayerAmount(FakeEnumWithThree.ElementOne));
            Assert.AreEqual(0, sut.GetPlayerAmount(FakeEnumWithThree.ElementTwo));
            Assert.AreEqual(0, sut.GetPlayerAmount(FakeEnumWithThree.ElementThree));
        }

        [TestMethod]
        public void Constructor_AllEntriesCreated_SizeFiveEnum()
        {
            AccumulatingComponent<FakeEnumWithFive> sut = new();

            Assert.AreEqual(0, sut.GetPlayerAmount(FakeEnumWithFive.ElementOne));
            Assert.AreEqual(0, sut.GetPlayerAmount(FakeEnumWithFive.ElementTwo));
            Assert.AreEqual(0, sut.GetPlayerAmount(FakeEnumWithFive.ElementThree));
            Assert.AreEqual(0, sut.GetPlayerAmount(FakeEnumWithFive.ElementFour));
            Assert.AreEqual(0, sut.GetPlayerAmount(FakeEnumWithFive.ElementFive));
        }

        #endregion

        #region GetPlayerAmount Tests

        [TestMethod]
        public void GetPlayerAmount_CorrectMomentumReturned()
        {
            AccumulatingComponent<FakeEnumWithFive> sut = new();

            sut.GainAmount(FakeEnumWithFive.ElementOne, 1);
            sut.GainAmount(FakeEnumWithFive.ElementTwo, 2);
            sut.GainAmount(FakeEnumWithFive.ElementThree, 3);
            sut.GainAmount(FakeEnumWithFive.ElementFour, 4);
            sut.GainAmount(FakeEnumWithFive.ElementFive, 5);

            Assert.AreEqual(1, sut.GetPlayerAmount(FakeEnumWithFive.ElementOne));
            Assert.AreEqual(2, sut.GetPlayerAmount(FakeEnumWithFive.ElementTwo));
            Assert.AreEqual(3, sut.GetPlayerAmount(FakeEnumWithFive.ElementThree));
            Assert.AreEqual(4, sut.GetPlayerAmount(FakeEnumWithFive.ElementFour));
            Assert.AreEqual(5, sut.GetPlayerAmount(FakeEnumWithFive.ElementFive));
        }

        #endregion

        #region GainAmount Tests

        [TestMethod]
        public void GainAmount_CorrectAmountGained()
        {
            AccumulatingComponent<FakeEnumWithFive> sut = new();

            sut.GainAmount(FakeEnumWithFive.ElementThree, 3);

            Assert.AreEqual(0, sut.GetPlayerAmount(FakeEnumWithFive.ElementOne));
            Assert.AreEqual(0, sut.GetPlayerAmount(FakeEnumWithFive.ElementTwo));
            Assert.AreEqual(3, sut.GetPlayerAmount(FakeEnumWithFive.ElementThree));
            Assert.AreEqual(0, sut.GetPlayerAmount(FakeEnumWithFive.ElementFour));
            Assert.AreEqual(0, sut.GetPlayerAmount(FakeEnumWithFive.ElementFive));
        }

        #endregion

        #region LoseAmount Tests

        [TestMethod]
        public void LoseAmount_CorrectAmountLost()
        {
            AccumulatingComponent<FakeEnumWithFive> sut = new();

            sut.GainAmount(FakeEnumWithFive.ElementFour, 3);
            sut.LoseAmount(FakeEnumWithFive.ElementFour, 1);

            Assert.AreEqual(0, sut.GetPlayerAmount(FakeEnumWithFive.ElementOne));
            Assert.AreEqual(0, sut.GetPlayerAmount(FakeEnumWithFive.ElementTwo));
            Assert.AreEqual(0, sut.GetPlayerAmount(FakeEnumWithFive.ElementThree));
            Assert.AreEqual(2, sut.GetPlayerAmount(FakeEnumWithFive.ElementFour));
            Assert.AreEqual(0, sut.GetPlayerAmount(FakeEnumWithFive.ElementFive));
        }

        [TestMethod]
        public void LoseAmount_AmountDoesNotGoNegative()
        {
            AccumulatingComponent<FakeEnumWithFive> sut = new();

            sut.LoseAmount(FakeEnumWithFive.ElementTwo, 7);

            Assert.AreEqual(0, sut.GetPlayerAmount(FakeEnumWithFive.ElementTwo));
        }

        #endregion

    }
}
