using NineteenSixty.Data;

namespace NineteenSixty;

//I should find a better name for this.
public class ActionPlan
{
    public Engine Engine { get; set; }
    public SetOfChanges Changes { get; set; }
}