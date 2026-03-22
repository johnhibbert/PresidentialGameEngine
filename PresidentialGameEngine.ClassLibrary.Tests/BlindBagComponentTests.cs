using PresidentialGameEngine.ClassLibrary.Components;
using static PresidentialGameEngine.ClassLibrary.Tests.TestStubsFakesAndMocks; 

namespace PresidentialGameEngine.ClassLibrary.Tests;

[TestClass]
public class BlindBagComponentTests
{
    #region Constructor Tests

    [TestMethod]
    [DataRow(10, 20)]
    [DataRow(12, 24)]
    public void Constructor_AllEntriesCreated_EnumWithTwoEntries(int initialPopulation, int expectedTotal)
    {
        BlindBagComponent<FakeEnumWithTwo> sut = new(initialPopulation,GetSeededRandomnessProvider());

        var result = sut.PeekIntoBag();
        Assert.AreEqual(0, result[FakeEnumWithTwo.ElementOne]);
        Assert.AreEqual(0, result[FakeEnumWithTwo.ElementTwo]);
    }

    [TestMethod]
    [DataRow(10, 30)]
    [DataRow(12, 36)]
    public void Constructor_AllEntriesCreated_EnumWithThreeEntries(int initialPopulation, int expectedTotal)
    {
        BlindBagComponent<FakeEnumWithThree> sut = new(initialPopulation,GetSeededRandomnessProvider());

        var result = sut.PeekIntoBag();
        Assert.AreEqual(0, result[FakeEnumWithThree.ElementOne]);
        Assert.AreEqual(0, result[FakeEnumWithThree.ElementTwo]);
        Assert.AreEqual(0, result[FakeEnumWithThree.ElementThree]);
    }

    [TestMethod]
    [DataRow(10, 50)]
    [DataRow(12, 60)]
    public void Constructor_AllEntriesCreated_EnumWithFiveEntries(int initialPopulation, int expectedTotal)
    {
        BlindBagComponent<FakeEnumWithFive> sut = new(initialPopulation,GetSeededRandomnessProvider());

        var result = sut.PeekIntoBag();
        Assert.AreEqual(0, result[FakeEnumWithFive.ElementOne]);
        Assert.AreEqual(0, result[FakeEnumWithFive.ElementTwo]);
        Assert.AreEqual(0, result[FakeEnumWithFive.ElementThree]);
        Assert.AreEqual(0, result[FakeEnumWithFive.ElementFour]);
        Assert.AreEqual(0, result[FakeEnumWithFive.ElementFive]);
    }

    #endregion
    
    #region FillBag Tests
    
    [TestMethod]
    [DataRow(10, 20)]
    [DataRow(12, 24)]
    public void FillBag_BagFilledWithCorrectNumber_EnumWithTwoEntries(int initialPopulation, int expectedTotal)
    {
        BlindBagComponent<FakeEnumWithTwo> sut = new(initialPopulation,GetSeededRandomnessProvider());
        sut.FillBag();
        
        var result = sut.PeekIntoBag();
        Assert.AreEqual(expectedTotal, result.Values.Sum());
    }
    
    [TestMethod]
    [DataRow(10, 30)]
    [DataRow(12, 36)]
    public void FillBag_BagFilledWithCorrectNumber_EnumWithThreeEntries(int initialPopulation, int expectedTotal)
    {
        BlindBagComponent<FakeEnumWithThree> sut = new(initialPopulation,GetSeededRandomnessProvider());
        sut.FillBag();
        
        var result = sut.PeekIntoBag();
        Assert.AreEqual(expectedTotal, result.Values.Sum());
    }
    
    [TestMethod]
    [DataRow(10, 50)]
    [DataRow(12, 60)]
    public void FillBag_BagFilledWithCorrectNumber_EnumWithFiveEntries(int initialPopulation, int expectedTotal)
    {
        BlindBagComponent<FakeEnumWithFive> sut = new(initialPopulation,GetSeededRandomnessProvider());
        sut.FillBag();
        
        var result = sut.PeekIntoBag();
        Assert.AreEqual(expectedTotal, result.Values.Sum());
    }
    
    #endregion
    
    #region DrawCube Tests
    
    [TestMethod]
    public void DrawCube_ConfirmDrawnCubeReducesPopulation()
    {
        const int initialPopulation = 12;
        BlindBagComponent<FakePlayer> sut = new(initialPopulation, GetSeededRandomnessProvider());
        sut.FillBag();
        sut.DrawCube();

        var result = sut.PeekIntoBag();

        Assert.AreEqual(initialPopulation, result[FakePlayer.PlayerOne]);
        Assert.AreEqual(initialPopulation, result[FakePlayer.PlayerTwo]);
        Assert.AreEqual(initialPopulation - 1, result[FakePlayer.PlayerThree]);
    }
    
    [TestMethod]
    public void DrawCube_ConfirmMultipleDrawingsReducesPopulation()
    {
        const int initialPopulation = 12;
        BlindBagComponent<FakePlayer> sut = new(initialPopulation, GetSeededRandomnessProvider());
        sut.FillBag();
        sut.DrawCube();
        sut.DrawCube();
        sut.DrawCube();
        sut.DrawCube();
        sut.DrawCube();

        var result = sut.PeekIntoBag();

        Assert.AreEqual(initialPopulation - 3, result[FakePlayer.PlayerOne]);
        Assert.AreEqual(initialPopulation, result[FakePlayer.PlayerTwo]);
        Assert.AreEqual(initialPopulation - 2, result[FakePlayer.PlayerThree]);
    }

    #endregion

    #region StopAutomaticallyRefillingBag Tests

    [TestMethod]
    public void StopAutomaticallyRefillingBag_BagRefillsAutomatically()
    {
        const int initialPopulation = 12;
        BlindBagComponent<FakePlayer> sut = new(initialPopulation, GetSeededRandomnessProvider());
        sut.FillBag();
        int index = initialPopulation * 3;

        while (index >= 1)
        {
            sut.DrawCube();
            index--;
        }

        var result = sut.PeekIntoBag();

        Assert.AreEqual(initialPopulation, result[FakePlayer.PlayerOne]);
        Assert.AreEqual(initialPopulation, result[FakePlayer.PlayerTwo]);
        Assert.AreEqual(initialPopulation, result[FakePlayer.PlayerThree]);
    }

    [TestMethod]
    [DataRow(1)]
    [DataRow(12)]
    public void StopAutomaticallyRefillingBag_BagDoesNotRefillAutomatically(int initialPopulation)
    {
        BlindBagComponent<FakePlayer> sut = new(initialPopulation, GetSeededRandomnessProvider());
        sut.FillBag();
        sut.StopAutomaticallyRefillingBag();
        
        int index = initialPopulation * 3;

        while (index >= 1)
        {
            sut.DrawCube();
            index--;
        }

        var result = sut.PeekIntoBag();

        Assert.AreEqual(0, result[FakePlayer.PlayerOne]);
        Assert.AreEqual(0, result[FakePlayer.PlayerTwo]);
        Assert.AreEqual(0, result[FakePlayer.PlayerThree]);
    }
    
    #endregion
    
    
}