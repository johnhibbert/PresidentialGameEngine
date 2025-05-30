using PresidentialGameEngine.ClassLibrary.Components;
using PresidentialGameEngine.ClassLibrary.Data;
using PresidentialGameEngine.ClassLibrary.Enums;

namespace PresidentialGameEngine.ClassLibrary.Engines
{
    public class NineteenSixtyGameEngine : GenericPresidentialGameEngine<Player, Leader, Issue, State, Region, Card>
    {
        public NineteenSixtyGameEngine(ComponentCollection<Player, Leader, Issue, State, Region, Card> componentCollection)
            : base(componentCollection)
        {
        }
    }
}
