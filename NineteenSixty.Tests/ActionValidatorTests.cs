namespace NineteenSixty.Tests;

[TestClass]
public class ActionValidatorTests
{

    [ValidOnlyInCertainPhases([Phase.Setup])]
    [TestMethod]
    [DataRow(Phase.Setup)]
    public void ThrowIfActionNotAllowed_TestWithAllowedAction(Phase currentPhase)
    {
        ActionValidator.ThrowIfActionNotAllowed(currentPhase);
    }
    
    [ValidOnlyInCertainPhases([Phase.Setup])]
    [TestMethod]
    [DataRow(Phase.Initiative)]
    [DataRow(Phase.Activity)]
    [ExpectedException(typeof(ActionNotAllowed))]
    public void ThrowIfActionNotAllowed_TestWithDisallowedAction(Phase currentPhase)
    {
        ActionValidator.ThrowIfActionNotAllowed(currentPhase);
    }
    
}