using PresidentialGameEngine.ClassLibrary.Components;
using PresidentialGameEngine.ClassLibrary.Interfaces;
using static PresidentialGameEngine.ClassLibrary.Tests.TestStubsFakesAndMocks;

namespace PresidentialGameEngine.ClassLibrary.Tests
{
    [TestClass]
    public class MomentumComponentTests
    {

        #region Constructor Tests
        [TestMethod]
        public void Constructor_AllEntriesCreated_SizeTwoEnum()
        {
            MomentumComponent<FakeEnumWithTwo> sut = new();

            Assert.AreEqual(0, sut.GetPlayerMomentum(FakeEnumWithTwo.ElementOne));
            Assert.AreEqual(0, sut.GetPlayerMomentum(FakeEnumWithTwo.ElementTwo));
        }

        [TestMethod]
        public void Constructor_AllEntriesCreated_SizeThreeEnum()
        {
            MomentumComponent<FakeEnumWithThree> sut = new();

            Assert.AreEqual(0, sut.GetPlayerMomentum(FakeEnumWithThree.ElementOne));
            Assert.AreEqual(0, sut.GetPlayerMomentum(FakeEnumWithThree.ElementTwo));
            Assert.AreEqual(0, sut.GetPlayerMomentum(FakeEnumWithThree.ElementThree));
        }

        [TestMethod]
        public void Constructor_AllEntriesCreated_SizeFiveEnum()
        {
            MomentumComponent<FakeEnumWithFive> sut = new();

            Assert.AreEqual(0, sut.GetPlayerMomentum(FakeEnumWithFive.ElementOne));
            Assert.AreEqual(0, sut.GetPlayerMomentum(FakeEnumWithFive.ElementTwo));
            Assert.AreEqual(0, sut.GetPlayerMomentum(FakeEnumWithFive.ElementThree));
            Assert.AreEqual(0, sut.GetPlayerMomentum(FakeEnumWithFive.ElementFour));
            Assert.AreEqual(0, sut.GetPlayerMomentum(FakeEnumWithFive.ElementFive));
        }

        #endregion

        #region GetPlayerMomentum Tests

        [TestMethod]
        public void GetPlayerMomentum_CorrectMomentumReturned()
        {
            MomentumComponent<FakeEnumWithFive> sut = new();

            sut.GainMomentum(FakeEnumWithFive.ElementOne, 1);
            sut.GainMomentum(FakeEnumWithFive.ElementTwo, 2);
            sut.GainMomentum(FakeEnumWithFive.ElementThree, 3);
            sut.GainMomentum(FakeEnumWithFive.ElementFour, 4);
            sut.GainMomentum(FakeEnumWithFive.ElementFive, 5);

            Assert.AreEqual(1, sut.GetPlayerMomentum(FakeEnumWithFive.ElementOne));
            Assert.AreEqual(2, sut.GetPlayerMomentum(FakeEnumWithFive.ElementTwo));
            Assert.AreEqual(3, sut.GetPlayerMomentum(FakeEnumWithFive.ElementThree));
            Assert.AreEqual(4, sut.GetPlayerMomentum(FakeEnumWithFive.ElementFour));
            Assert.AreEqual(5, sut.GetPlayerMomentum(FakeEnumWithFive.ElementFive));
        }

        #endregion

        #region GainMomentum Tests

        [TestMethod]
        public void GainMomentum_CorrectAmountGained()
        {
            MomentumComponent<FakeEnumWithFive> sut = new();

            sut.GainMomentum(FakeEnumWithFive.ElementThree, 3);

            Assert.AreEqual(0, sut.GetPlayerMomentum(FakeEnumWithFive.ElementOne));
            Assert.AreEqual(0, sut.GetPlayerMomentum(FakeEnumWithFive.ElementTwo));
            Assert.AreEqual(3, sut.GetPlayerMomentum(FakeEnumWithFive.ElementThree));
            Assert.AreEqual(0, sut.GetPlayerMomentum(FakeEnumWithFive.ElementFour));
            Assert.AreEqual(0, sut.GetPlayerMomentum(FakeEnumWithFive.ElementFive));
        }

        #endregion

        #region LoseMomentum Tests

        [TestMethod]
        public void LoseMomentum_CorrectAmountLost()
        {
            MomentumComponent<FakeEnumWithFive> sut = new();

            sut.GainMomentum(FakeEnumWithFive.ElementFour, 3);
            sut.LoseMomentum(FakeEnumWithFive.ElementFour, 1);

            Assert.AreEqual(0, sut.GetPlayerMomentum(FakeEnumWithFive.ElementOne));
            Assert.AreEqual(0, sut.GetPlayerMomentum(FakeEnumWithFive.ElementTwo));
            Assert.AreEqual(0, sut.GetPlayerMomentum(FakeEnumWithFive.ElementThree));
            Assert.AreEqual(2, sut.GetPlayerMomentum(FakeEnumWithFive.ElementFour));
            Assert.AreEqual(0, sut.GetPlayerMomentum(FakeEnumWithFive.ElementFive));
        }

        [TestMethod]
        public void LoseMomentum_AmountDoesNotGoNegative()
        {
            MomentumComponent<FakeEnumWithFive> sut = new();

            sut.LoseMomentum(FakeEnumWithFive.ElementTwo, 7);

            Assert.AreEqual(0, sut.GetPlayerMomentum(FakeEnumWithFive.ElementTwo));
        }

        #endregion

    }
}
