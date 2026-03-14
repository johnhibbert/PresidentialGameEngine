using PresidentialGameEngine.ClassLibrary.Interfaces;

namespace PresidentialGameEngine.ClassLibrary.Data
{
    public class LocationData<TState, TPlayer, TRegion>
        (TState state, TRegion region, int electoralVotes, TPlayer tilt, int startingSupport)
        : ILocationData<TState, TPlayer, TRegion>
        where TState : Enum
        where TPlayer : Enum
        where TRegion : Enum
    {
        public TState State { get; init; } = state;

        public int ElectoralVotes { get; init; } = electoralVotes;

        public TPlayer Tilt { get; init; } = tilt;

        public TRegion Region { get; init; } = region;

        public int StartingSupport { get; init; } = startingSupport;

        public override string ToString()
        {
            return $"{State} [{ElectoralVotes}]";
        }
    }

}
