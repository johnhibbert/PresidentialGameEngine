using NineteenSixty.Data;
using NineteenSixty.Interfaces;

namespace NineteenSixty;

//I should find a better name for this.
public class ActionPlan
{
    public IEngine Engine { get; set; }
    public SetOfChanges Changes { get; set; }
}