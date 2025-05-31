using PresidentialGameEngine.ClassLibrary.Interfaces;

namespace PresidentialGameEngine.ClassLibrary.Data
{
    public class LocationData<StatesEnum, PlayersEnum, RegionsEnum>
        (StatesEnum state, RegionsEnum region, int electoralVotes, PlayersEnum tilt, int startingSupport)
        : ILocationData<StatesEnum, PlayersEnum, RegionsEnum>
        where StatesEnum : Enum
        where PlayersEnum : Enum
        where RegionsEnum : Enum
    {
        public StatesEnum State { get; init; } = state;

        public int ElectoralVotes { get; init; } = electoralVotes;

        public PlayersEnum Tilt { get; init; } = tilt;

        public RegionsEnum Region { get; init; } = region;

        public int StartingSupport { get; init; } = startingSupport;

        public override string ToString()
        {
            return $"{State} [{ElectoralVotes}]";
        }
    }

}
