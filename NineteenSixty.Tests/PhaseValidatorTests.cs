using NineteenSixty.Exceptions;

namespace NineteenSixty.Tests;

[TestClass]
public class PhaseValidatorTests
{

    [ValidOnlyInCertainPhases([Phase.Setup])]
    [TestMethod]
    [DataRow(Phase.Setup)]
    public void ThrowIfActionNotAllowed_TestWithAllowedAction(Phase currentPhase)
    {
        var sut = new PhaseValidator();
        sut.ThrowIfActionNotAllowed(currentPhase);
    }
    
    [ValidOnlyInCertainPhases([Phase.Setup])]
    [TestMethod]
    [DataRow(Phase.Initiative)]
    [DataRow(Phase.Activity)]
    [ExpectedException(typeof(ActionNotAllowed))]
    public void ThrowIfActionNotAllowed_TestWithDisallowedAction(Phase currentPhase)
    {
        var sut = new PhaseValidator();
        sut.ThrowIfActionNotAllowed(currentPhase);
    }
    
}