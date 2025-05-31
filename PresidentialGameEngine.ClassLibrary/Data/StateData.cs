using PresidentialGameEngine.ClassLibrary.Enums;

namespace PresidentialGameEngine.ClassLibrary.Data
{
    public class StateData(State state, Region region, int electoralVotes, Player tilt, int startingSupport)
        : LocationData<State, Player, Region>(state, region, electoralVotes, tilt, startingSupport)
    {
    };

}
