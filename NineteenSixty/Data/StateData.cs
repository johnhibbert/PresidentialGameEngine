using NineteenSixty.Enums;
using PresidentialGameEngine.ClassLibrary.Data;
using PresidentialGameEngine.ClassLibrary.Interfaces;

namespace NineteenSixty.Data;

public class StateData(State state, Region region, int electoralVotes, Player tilt, int startingSupport)
    : LocationData<State, Player, Region>(state, region, electoralVotes, tilt, startingSupport)
{
};