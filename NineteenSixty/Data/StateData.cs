// using NineteenSixty.Enums;
// using PresidentialGameEngine.ClassLibrary.Data;
// using PresidentialGameEngine.ClassLibrary.Interfaces;
//
// namespace NineteenSixty.Data;
//
// public class StateData(State state, Region region, int electoralVotes, Player tilt, int startingSupport)
//     : ILocationData<State, Player, Region>
// {
//     public State State { get; init; } = state;
//
//     public int ElectoralVotes { get; init; } = electoralVotes;
//
//     public Player Tilt { get; init; } = tilt;
//
//     public Region Region { get; init; } = region;
//
//     public int StartingSupport { get; init; } = startingSupport;
//
//     public override string ToString()
//     {
//         return $"{State} [{ElectoralVotes}]";
//     }
// };