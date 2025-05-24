using Microsoft.VisualStudio.TestTools.UnitTesting;
using PresidentialGameEngine.ClassLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    class SeededRandomnessProviderForTesting : IRandomnessProvider
    {
        //Seeded for consistency
        private readonly Random Random = new(5);

        public int GetRandomNumber(int maxValue)
        {
            return Random.Next(maxValue);
        }
    }

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
    }
}
