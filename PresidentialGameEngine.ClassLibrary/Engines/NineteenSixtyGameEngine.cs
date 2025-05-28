using PresidentialGameEngine.ClassLibrary.Enums;

namespace PresidentialGameEngine.ClassLibrary.Engines
{
    public class NineteenSixtyGameEngine : GenericPresidentialGameEngine<Player, Leader, Issue, State, Region>
    {
        public NineteenSixtyGameEngine(ComponentCollection<Player, Leader, Issue, State, Region> componentCollection)
            : base(componentCollection)
        {
        }
    }
}
