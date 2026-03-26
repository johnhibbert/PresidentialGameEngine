using System.Reflection;

namespace NineteenSixty;

[AttributeUsage(AttributeTargets.Method)]
public class ValidOnlyInCertainPhasesAttribute : Attribute
{
    public ValidOnlyInCertainPhasesAttribute(Phase[] phases)
    {
        Phases = phases;
    }
    
    public IEnumerable<Phase> Phases { get; init; }

}

public enum Phase
{
    Setup = -1,
    Initiative = 0,
    Activity,
    Momentum,
    CampaignStrategy,
}

public static class ActionValidator
{
    public static void ThrowIfActionNotAllowed(Phase currentPhase,
        [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
        [System.Runtime.CompilerServices.CallerFilePath] string filePath = "")
    {
        /*
        //This is slightly overkill but it lets us unit test it from a separate assembly.
        
        If we decide we want something simpler, here it is:
        
        var type = typeof(Controller);
           var method = type.GetMethod(memberName);
           var methodAttributes = method?.GetCustomAttributes(typeof(ValidOnlyInCertainPhasesAttribute), false).FirstOrDefault()
               as ValidOnlyInCertainPhasesAttribute;
               
        Just delete the tests if you do that.
        */
        
        var callingAssembly = Assembly.GetCallingAssembly();
        var callingClass = Path.GetFileNameWithoutExtension(filePath);
        var callingType = callingAssembly.DefinedTypes.Single(x => x.Name == callingClass).AsType();
        var callingMethod = memberName;
        
        var method = callingType.GetMethod(callingMethod);
        
        var methodAttributes = method?.GetCustomAttributes(typeof(ValidOnlyInCertainPhasesAttribute), false).FirstOrDefault()
            as ValidOnlyInCertainPhasesAttribute;

        if (methodAttributes != null && !methodAttributes.Phases.Contains(currentPhase))
        {
            string msg = $"Method {memberName} not allowed in {currentPhase}.";
            msg += $"{memberName} is only allowed in {string.Join(",",methodAttributes.Phases)}";
            
            throw new ActionNotAllowed(msg);
        }
    }
}

public class ActionNotAllowed :  ApplicationException
{
    public ActionNotAllowed() : base() { }
    public ActionNotAllowed(string? message) : base(message) { }
}