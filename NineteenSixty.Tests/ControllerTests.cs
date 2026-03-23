using NSubstitute;
using NineteenSixty;
using NineteenSixty.Interfaces;
using NSubstitute.ReturnsExtensions;


namespace NineteenSixty.Tests;

[TestClass]
public class ControllerTests
{
    #region GetGameState Tests
    
    [TestMethod]
    public void FFF()
    {
        var mockClass = Substitute.For<IEngine>();
        mockClass.GetGameState().ReturnsNull();
    }
    
    #endregion
}