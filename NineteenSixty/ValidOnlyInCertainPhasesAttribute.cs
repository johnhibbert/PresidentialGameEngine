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
    Setup,
    Initiative,
    Activity,
}

public static class ActionValidator
{
    public static void ThrowIfActionNotAllowed(Phase currentPhase,
        [System.Runtime.CompilerServices.CallerMemberName] string memberName = "")
    {
        var type = typeof(Controller);
        
        var method = type.GetMethod(memberName);
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