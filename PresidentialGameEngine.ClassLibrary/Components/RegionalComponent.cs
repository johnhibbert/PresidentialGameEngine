using PresidentialGameEngine.ClassLibrary.Interfaces;

namespace PresidentialGameEngine.ClassLibrary.Components
{
    public class RegionalComponent<StatesEnum, RegionsEnum, PlayersEnum> : IRegionalComponent<StatesEnum, RegionsEnum, PlayersEnum>
    {
        private IDictionary<StatesEnum, RegionsEnum> StateRegions { get; init; }

        private IDictionary<PlayersEnum, StatesEnum> PlayerLocations { get; init; }

        public IDictionary<PlayersEnum, StatesEnum> GetRawData() { return PlayerLocations; }

        public RegionalComponent(IDictionary<StatesEnum, RegionsEnum> statesAndRegions,
            IDictionary<PlayersEnum, StatesEnum> playerStartingLocations)
        {
            StateRegions = statesAndRegions;
            PlayerLocations = playerStartingLocations;
        }

        public RegionsEnum GetRegionByState(StatesEnum state)
        {
            return StateRegions[state];
        }

        public IEnumerable<StatesEnum> GetStatesWithinRegion(RegionsEnum region)
        {
            //https://stackoverflow.com/questions/21704383/linq-query-dictionary-where-value-in-list
            return StateRegions.Where(x => EqualityComparer<RegionsEnum>.Default.Equals(region, x.Value)).Select(y => y.Key);
        }

        public StatesEnum GetPlayerState(PlayersEnum player)
        {
            return PlayerLocations[player];
        }

        public void MovePlayerToState(PlayersEnum player, StatesEnum states)
        {
            PlayerLocations[player] = states;
        }
    }
}
