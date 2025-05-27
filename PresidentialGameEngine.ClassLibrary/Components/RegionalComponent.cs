using PresidentialGameEngine.ClassLibrary.Interfaces;

namespace PresidentialGameEngine.ClassLibrary.Components
{
    public class RegionalComponent<StatesEnum, RegionsEnum> : IRegionalComponent<StatesEnum, RegionsEnum>
    {
        private IDictionary<StatesEnum, RegionsEnum> StateRegions { get; init; }

        public RegionalComponent(IDictionary<StatesEnum, RegionsEnum> statesAndRegions)
        {
            StateRegions = statesAndRegions;
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
    }
}
