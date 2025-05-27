using PresidentialGameEngine.ClassLibrary.Components;
using static PresidentialGameEngine.ClassLibrary.Tests.TestStubsFakesAndMocks;

namespace PresidentialGameEngine.ClassLibrary.Tests
{
    [TestClass]
    public class PoliticalCapitalComponentTests
    {
        private static SeededRandomnessProviderForTesting GetSeededRandomnessProvider()
        {
            return new SeededRandomnessProviderForTesting();
        }

        #region Constructor Tests
        [TestMethod]
        [DataRow(10, 20)]
        [DataRow(12, 24)]
        public void Constructor_AllEntriesCreated_EnumWithTwoEntries(int initialPopulation, int expectedTotal)
        {
            PoliticalCapitalComponent<FakeEnumWithTwo> sut = new(GetSeededRandomnessProvider(), initialPopulation);

            var result = sut.Peek();

            Assert.AreEqual(expectedTotal, sut.TotalNumberOfCubesInBag);
            Assert.AreEqual(initialPopulation, result[FakeEnumWithTwo.ElementOne]);
            Assert.AreEqual(initialPopulation, result[FakeEnumWithTwo.ElementTwo]);
        }

        [TestMethod]
        [DataRow(10, 30)]
        [DataRow(12, 36)]
        public void Constructor_AllEntriesCreated_EnumWithThreeEntries(int initialPopulation, int expectedTotal)
        {
            PoliticalCapitalComponent<FakeEnumWithThree> sut = new(GetSeededRandomnessProvider(), initialPopulation);

            var result = sut.Peek();

            Assert.AreEqual(expectedTotal, sut.TotalNumberOfCubesInBag);
            Assert.AreEqual(initialPopulation, result[FakeEnumWithThree.ElementOne]);
            Assert.AreEqual(initialPopulation, result[FakeEnumWithThree.ElementTwo]);
            Assert.AreEqual(initialPopulation, result[FakeEnumWithThree.ElementThree]);
        }

        [TestMethod]
        [DataRow(10, 50)]
        [DataRow(12, 60)]
        public void Constructor_AllEntriesCreated_EnumWithFiveEntries(int initialPopulation, int expectedTotal)
        {
            PoliticalCapitalComponent<FakeEnumWithFive> sut = new(GetSeededRandomnessProvider(), initialPopulation);

            var result = sut.Peek();

            Assert.AreEqual(expectedTotal, sut.TotalNumberOfCubesInBag);
            Assert.AreEqual(initialPopulation, result[FakeEnumWithFive.ElementOne]);
            Assert.AreEqual(initialPopulation, result[FakeEnumWithFive.ElementTwo]);
            Assert.AreEqual(initialPopulation, result[FakeEnumWithFive.ElementThree]);
            Assert.AreEqual(initialPopulation, result[FakeEnumWithFive.ElementFour]);
            Assert.AreEqual(initialPopulation, result[FakeEnumWithFive.ElementFive]);
        }
        #endregion

        #region TotalNumberOfCubesInBag Tests
        [TestMethod]
        [DataRow(10, 50)]
        [DataRow(12, 60)]
        public void TotalNumberOfCubesInBag_ValuesCorrectlyAdded(int initialPopulation, int expectedTotal)
        {
            PoliticalCapitalComponent<FakeEnumWithFive> sut = new(GetSeededRandomnessProvider(), initialPopulation);
            Assert.AreEqual(expectedTotal, sut.TotalNumberOfCubesInBag);
        }
        #endregion

        #region InitiativeCheck Tests
        [TestMethod]
        [DataRow(10, 27)]
        [DataRow(12, 33)]
        public void InitiativeCheck_ConfirmResult(int initialPopulation, int expectedTotal)
        {
            PoliticalCapitalComponent<FakePlayer> sut = new(GetSeededRandomnessProvider(), initialPopulation);

            var result = sut.InitiativeCheck();
            
            Assert.AreEqual(FakePlayer.PlayerThree, result);
            Assert.AreEqual(expectedTotal, sut.TotalNumberOfCubesInBag);
        }
        #endregion

        #region SupportCheck Tests
        [TestMethod]
        [DataRow(10, 5, 25)]
        [DataRow(12, 5, 31)]
        public void SupportCheck_ConfirmResult(int initialPopulation, int cubesToTake, int expectedTotal)
        {
            PoliticalCapitalComponent<FakePlayer> sut = new(GetSeededRandomnessProvider(), initialPopulation);

            var result = sut.SupportCheck(FakePlayer.PlayerThree, cubesToTake);

            Assert.AreEqual(1, result);
            Assert.AreEqual(expectedTotal, sut.TotalNumberOfCubesInBag);
        }
        #endregion

        #region AddCubes Tests
        [TestMethod]
        [DataRow(10, 5, 35)]
        [DataRow(12, 5, 41)]
        public void AddCubes_ConfirmCubesAreAdded(int initialPopulation, int cubesToAdd, int expectedTotal)
        {
            PoliticalCapitalComponent<FakePlayer> sut = new(GetSeededRandomnessProvider(), initialPopulation);

            sut.AddCubes(FakePlayer.PlayerThree, cubesToAdd);

            Assert.AreEqual(expectedTotal, sut.TotalNumberOfCubesInBag);
        }
        #endregion
    }
}
