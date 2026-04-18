using PresidentialGameEngine.ClassLibrary.Components;
using PresidentialGameEngine.ClassLibrary.Exceptions;
using static PresidentialGameEngine.ClassLibrary.Tests.TestStubsFakesAndMocks; 

namespace PresidentialGameEngine.ClassLibrary.Tests;

[TestClass]
public class BlindDrawComponentTests
{
    #region Constructor Tests

    [TestMethod]
    [DataRow(10, 20)]
    [DataRow(12, 24)]
    public void Constructor_AllEntriesCreated_EnumWithTwoEntries(int initialPopulation, int expectedTotal)
    {
        BlindDrawComponent<FakeEnumWithTwo> sut = new(initialPopulation,GetSeededRandomnessProvider());

        var result = sut.PeekAtPopulation();
        Assert.AreEqual(0, result[FakeEnumWithTwo.ElementOne]);
        Assert.AreEqual(0, result[FakeEnumWithTwo.ElementTwo]);
    }

    [TestMethod]
    [DataRow(10, 30)]
    [DataRow(12, 36)]
    public void Constructor_AllEntriesCreated_EnumWithThreeEntries(int initialPopulation, int expectedTotal)
    {
        BlindDrawComponent<FakeEnumWithThree> sut = new(initialPopulation,GetSeededRandomnessProvider());

        var result = sut.PeekAtPopulation();
        Assert.AreEqual(0, result[FakeEnumWithThree.ElementOne]);
        Assert.AreEqual(0, result[FakeEnumWithThree.ElementTwo]);
        Assert.AreEqual(0, result[FakeEnumWithThree.ElementThree]);
    }

    [TestMethod]
    [DataRow(10, 50)]
    [DataRow(12, 60)]
    public void Constructor_AllEntriesCreated_EnumWithFiveEntries(int initialPopulation, int expectedTotal)
    {
        BlindDrawComponent<FakeEnumWithFive> sut = new(initialPopulation,GetSeededRandomnessProvider());

        var result = sut.PeekAtPopulation();
        Assert.AreEqual(0, result[FakeEnumWithFive.ElementOne]);
        Assert.AreEqual(0, result[FakeEnumWithFive.ElementTwo]);
        Assert.AreEqual(0, result[FakeEnumWithFive.ElementThree]);
        Assert.AreEqual(0, result[FakeEnumWithFive.ElementFour]);
        Assert.AreEqual(0, result[FakeEnumWithFive.ElementFive]);
    }

    [TestMethod]
    public void Constructor_UnequalPopulationsStillCreatesAll()
    {
        IDictionary<FakeEnumWithTwo, int> initPop = new Dictionary<FakeEnumWithTwo, int>()
        {
            { FakeEnumWithTwo.ElementOne, 2 },
            { FakeEnumWithTwo.ElementTwo, 3 }
        };
        
        BlindDrawComponent<FakeEnumWithTwo> sut = new(initPop,GetSeededRandomnessProvider());

        var result = sut.PeekAtPopulation();
        Assert.AreEqual(0, result[FakeEnumWithTwo.ElementOne]);
        Assert.AreEqual(0, result[FakeEnumWithTwo.ElementTwo]);
    }
    
    [TestMethod]
    public void Constructor_UnequalPopulationsReflectedAfterFill()
    {
        IDictionary<FakeEnumWithTwo, int> initPop = new Dictionary<FakeEnumWithTwo, int>()
        {
            { FakeEnumWithTwo.ElementOne, 2 },
            { FakeEnumWithTwo.ElementTwo, 3 }
        };
        
        BlindDrawComponent<FakeEnumWithTwo> sut = new(initPop,GetSeededRandomnessProvider());
        sut.RefillInitialPopulation();
        
        var result = sut.PeekAtPopulation();
        Assert.AreEqual(2, result[FakeEnumWithTwo.ElementOne]);
        Assert.AreEqual(3, result[FakeEnumWithTwo.ElementTwo]);
    }

    
    #endregion
    
    #region RefillInitialPopulation Tests
    
    [TestMethod]
    [DataRow(10, 20)]
    [DataRow(12, 24)]
    public void RefillInitialPopulation_BagFilledWithCorrectNumber_EnumWithTwoEntries(int initialPopulation, int expectedTotal)
    {
        BlindDrawComponent<FakeEnumWithTwo> sut = new(initialPopulation,GetSeededRandomnessProvider());
        sut.RefillInitialPopulation();
        
        var result = sut.PeekAtPopulation();
        Assert.AreEqual(expectedTotal, result.Values.Sum());
    }
    
    [TestMethod]
    [DataRow(10, 30)]
    [DataRow(12, 36)]
    public void RefillInitialPopulation_BagFilledWithCorrectNumber_EnumWithThreeEntries(int initialPopulation, int expectedTotal)
    {
        BlindDrawComponent<FakeEnumWithThree> sut = new(initialPopulation,GetSeededRandomnessProvider());
        sut.RefillInitialPopulation();
        
        var result = sut.PeekAtPopulation();
        Assert.AreEqual(expectedTotal, result.Values.Sum());
    }
    
    [TestMethod]
    [DataRow(10, 50)]
    [DataRow(12, 60)]
    public void RefillInitialPopulation_BagFilledWithCorrectNumber_EnumWithFiveEntries(int initialPopulation, int expectedTotal)
    {
        BlindDrawComponent<FakeEnumWithFive> sut = new(initialPopulation,GetSeededRandomnessProvider());
        sut.RefillInitialPopulation();
        
        var result = sut.PeekAtPopulation();
        Assert.AreEqual(expectedTotal, result.Values.Sum());
    }
    

    #endregion
    
    #region Draw Tests
    
    [TestMethod]
    public void Draw_ConfirmDrawnCubeReducesPopulation()
    {
        const int initialPopulation = 12;
        BlindDrawComponent<FakePlayer> sut = new(initialPopulation, GetSeededRandomnessProvider());
        sut.RefillInitialPopulation();
        sut.Draw();

        var result = sut.PeekAtPopulation();

        Assert.AreEqual(initialPopulation, result[FakePlayer.PlayerOne]);
        Assert.AreEqual(initialPopulation, result[FakePlayer.PlayerTwo]);
        Assert.AreEqual(initialPopulation - 1, result[FakePlayer.PlayerThree]);
    }
    
    [TestMethod]
    public void Draw_ConfirmMultipleDrawingsReducesPopulation()
    {
        const int initialPopulation = 12;
        BlindDrawComponent<FakePlayer> sut = new(initialPopulation, GetSeededRandomnessProvider());
        sut.RefillInitialPopulation();
        sut.Draw();
        sut.Draw();
        sut.Draw();
        sut.Draw();
        sut.Draw();

        var result = sut.PeekAtPopulation();

        Assert.AreEqual(initialPopulation - 3, result[FakePlayer.PlayerOne]);
        Assert.AreEqual(initialPopulation, result[FakePlayer.PlayerTwo]);
        Assert.AreEqual(initialPopulation - 2, result[FakePlayer.PlayerThree]);
    }

    [TestMethod]
    public void Draw_DrawWhenEmptyRefills()
    {
        const int initialPopulation = 1;
        BlindDrawComponent<FakePlayer> sut = new(initialPopulation, GetSeededRandomnessProvider());
        sut.RefillInitialPopulation();
        sut.Draw();
        sut.Draw();
        sut.Draw();
        sut.Draw();

        var result = sut.PeekAtPopulation().Values.Sum();

        Assert.AreEqual(2, result);
    }
    
    [TestMethod]
    [ExpectedException(typeof(EmptyAndNonRefillableException))]
    public void Draw_DrawThrowsWhenEmptyAndNotRefillable()
    {
        const int initialPopulation = 1;
        BlindDrawComponent<FakePlayer> sut = new(initialPopulation, GetSeededRandomnessProvider());
        sut.RefillInitialPopulation();
        sut.StopAutomaticallyRefillingPopulation();
        
        sut.Draw();
        sut.Draw();
        sut.Draw();
        sut.Draw();

    }
    
    #endregion

    #region StopAutomaticallyRefillingPopulation Tests

    [TestMethod]
    public void StopAutomaticallyRefillingPopulation_BagRefillsAutomatically()
    {
        const int initialPopulation = 12;
        BlindDrawComponent<FakePlayer> sut = new(initialPopulation, GetSeededRandomnessProvider());
        sut.RefillInitialPopulation();
        var index = (initialPopulation * 3) + 1;

        while (index >= 1)
        {
            sut.Draw();
            index--;
        }

        var result = sut.PeekAtPopulation();

        Assert.AreEqual(initialPopulation, result[FakePlayer.PlayerOne]);
        Assert.AreEqual(initialPopulation, result[FakePlayer.PlayerTwo]);
        Assert.AreEqual(initialPopulation - 1, result[FakePlayer.PlayerThree]);
    }

    [TestMethod]
    [DataRow(1)]
    [DataRow(12)]
    public void StopAutomaticallyRefillingPopulation_BagDoesNotRefillAutomatically(int initialPopulation)
    {
        BlindDrawComponent<FakePlayer> sut = new(initialPopulation, GetSeededRandomnessProvider());
        sut.RefillInitialPopulation();
        sut.StopAutomaticallyRefillingPopulation();
        
        int index = initialPopulation * 3;

        while (index >= 1)
        {
            sut.Draw();
            index--;
        }

        var result = sut.PeekAtPopulation();

        Assert.AreEqual(0, result[FakePlayer.PlayerOne]);
        Assert.AreEqual(0, result[FakePlayer.PlayerTwo]);
        Assert.AreEqual(0, result[FakePlayer.PlayerThree]);
    }
    
    #endregion
    
    
}