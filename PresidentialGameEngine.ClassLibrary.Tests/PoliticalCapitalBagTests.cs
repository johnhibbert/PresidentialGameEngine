using Microsoft.VisualStudio.TestTools.UnitTesting;
using PresidentialGameEngine.ClassLibrary.Enums;
using PresidentialGameEngine.ClassLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using static PresidentialGameEngine.ClassLibrary.Tests.TestStubsFakesAndMocks;

namespace PresidentialGameEngine.ClassLibrary.Tests
{
    enum FakeEnumWithTwo 
    {
        ElementOne,
        ElementTwo
    }

    enum FakeEnumWithThree
    {
        ElementOne,
        ElementTwo,
        ElementThree
    }

    enum FakeEnumWithFive
    {
        ElementOne,
        ElementTwo,
        ElementThree,
        ElementFour,
        ElementFive,
    }

    //class SeededRandomnessProviderForTesting : IRandomnessProvider
    //{
    //    //Seeded for consistency
    //    //Was 5 but that was 3 straight kennedies.
    //    private readonly Random Random = new(1960);

    //    public int GetRandomNumber(int maxValue)
    //    {
    //        return Random.Next(maxValue);
    //    }
    //}

    [TestClass]
    public class PoliticalCapitalBagTests
    {

        #region Constructor Tests

        [TestMethod]
        [DataRow(10, 20)]
        [DataRow(12, 24)]
        public void Constructor_InitialPopulationCorrect_SizeTwoEnum(int initialPopulation, int expectedTotal)
        {
            var seededRNG = new SeededRandomnessProviderForTesting();

            var sut = new PoliticalCapitalBag<FakeEnumWithTwo>(seededRNG, initialPopulation);
            var result = sut.TotalCubesInBag;

            Assert.AreEqual(expectedTotal, result);
        }

        [TestMethod]
        [DataRow(10, 30)]
        [DataRow(12, 36)]
        public void Constructor_InitialPopulationCorrect_SizeThreeEnum(int initialPopulation, int expectedTotal)
        {
            var seededRNG = new SeededRandomnessProviderForTesting();

            var sut = new PoliticalCapitalBag<FakeEnumWithThree>(seededRNG, initialPopulation);
            var result = sut.TotalCubesInBag;

            Assert.AreEqual(expectedTotal, result);
        }

        [TestMethod]
        [DataRow(10, 50)]
        [DataRow(12, 60)]
        public void Constructor_InitialPopulationCorrect_SizeFiveEnum(int initialPopulation, int expectedTotal)
        {
            var seededRNG = new SeededRandomnessProviderForTesting();

            var sut = new PoliticalCapitalBag<FakeEnumWithFive>(seededRNG, initialPopulation);
            var result = sut.TotalCubesInBag;

            Assert.AreEqual(expectedTotal, result);
        }

        #endregion

        #region AddCubes Tests 
        [TestMethod]
        [DataRow(10, 2, 22)]
        [DataRow(12, 5, 29)]
        public void AddCubes_CubesAdded_SizeTwoEnum(int initialPopulation, int amountAdded, int expectedTotal)
        {
            var seededRNG = new SeededRandomnessProviderForTesting();

            var sut = new PoliticalCapitalBag<FakeEnumWithTwo>(seededRNG, initialPopulation);
            sut.AddCubes(FakeEnumWithTwo.ElementOne, amountAdded);

            Assert.AreEqual(expectedTotal, sut.TotalCubesInBag);
        }

        [TestMethod]
        [DataRow(10, 2, 32)]
        [DataRow(12, 5, 41)]
        public void AddCubes_CubesAdded_SizeThreeEnum(int initialPopulation, int amountAdded, int expectedTotal)
        {
            var seededRNG = new SeededRandomnessProviderForTesting();

            var sut = new PoliticalCapitalBag<FakeEnumWithThree>(seededRNG, initialPopulation);
            sut.AddCubes(FakeEnumWithThree.ElementOne, amountAdded);

            Assert.AreEqual(expectedTotal, sut.TotalCubesInBag);
        }

        [TestMethod]
        [DataRow(10, 2, 52)]
        [DataRow(12, 5, 65)]
        public void AddCubes_CubesAdded_SizeFiveEnum(int initialPopulation, int amountAdded, int expectedTotal)
        {
            var seededRNG = new SeededRandomnessProviderForTesting();

            var sut = new PoliticalCapitalBag<FakeEnumWithFive>(seededRNG, initialPopulation);
            sut.AddCubes(FakeEnumWithFive.ElementOne, amountAdded);

            Assert.AreEqual(expectedTotal, sut.TotalCubesInBag);
        }
        #endregion

        #region DrawCube Tests

        [TestMethod]
        public void DrawCube_CubeIsExpectedAndRemoved()
        {
            var seededRNG = new SeededRandomnessProviderForTesting();

            var sut = new PoliticalCapitalBag<Player>(seededRNG, 12);

            var result = sut.DrawCube();

            //Seeded value is expected to be Kennedy
            Assert.AreEqual(Player.Kennedy, result);
            Assert.AreEqual(23, sut.TotalCubesInBag);
        }

        [TestMethod]
        public void DrawCube_FinalCubeTakenSupplyRefreshed()
        {
            var seededRNG = new SeededRandomnessProviderForTesting();

            var sut = new PoliticalCapitalBag<Player>(seededRNG, 2);

            sut.DrawCube();
            sut.DrawCube();
            sut.DrawCube();
            sut.DrawCube();

            Assert.AreEqual(4, sut.TotalCubesInBag);
        }

        #endregion

        #region InititativeCheck Tests
        [TestMethod]
        public void InititativeCheck_ExpectedValueReturned()
        {
            var seededRNG = new SeededRandomnessProviderForTesting();

            var sut = new PoliticalCapitalBag<Player>(seededRNG, 12);

            var result = sut.InitiativeCheck();

            Assert.AreEqual(Player.Kennedy, result);
            Assert.AreEqual(22, sut.TotalCubesInBag);
        }

        #endregion

        #region InititativeCheck Tests
        [TestMethod]
        public void SupportCheck_ExpectedValueReturned()
        {
            var seededRNG = new SeededRandomnessProviderForTesting();

            var sut = new PoliticalCapitalBag<Player>(seededRNG, 12);

            var result = sut.SupportCheck(Player.Kennedy, 4);

            Assert.AreEqual(3, result);
            Assert.AreEqual(20, sut.TotalCubesInBag);
        }

        #endregion

    }
}
