using PresidentialGameEngine.ClassLibrary.Enums;
using PresidentialGameEngine.ClassLibrary.Engines;
using PresidentialGameEngine.ClassLibrary.Data;
using PresidentialGameEngine.ClassLibrary.Components;
namespace NineteenSixty;

public class Engine(ComponentCollection<Player, Leader, Issue, State, Region, Card> componentCollection)
    : GenericPresidentialGameEngine<Player, Leader, Issue, State, Region, Card>(componentCollection);
