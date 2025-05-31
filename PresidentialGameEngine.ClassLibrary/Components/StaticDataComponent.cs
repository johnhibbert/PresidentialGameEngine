using PresidentialGameEngine.ClassLibrary.Interfaces;

namespace PresidentialGameEngine.ClassLibrary.Components
{
    public class StaticDataComponent<StatesEnum, PlayersEnum, RegionsEnum>
        where StatesEnum : Enum
        where PlayersEnum : Enum
        where RegionsEnum : Enum
    {
        readonly IDictionary<StatesEnum, ILocationData<StatesEnum, PlayersEnum, RegionsEnum>> stateLocationData;
        readonly IDictionary<RegionsEnum, IList<StatesEnum>> stateByRegion;

        public StaticDataComponent(IDictionary<StatesEnum, ILocationData<StatesEnum, PlayersEnum, RegionsEnum>> locationData)
        {
            stateLocationData = locationData;

            stateByRegion = ReverseStateRegionDictionary();
        }

        private Dictionary<RegionsEnum, IList<StatesEnum>> ReverseStateRegionDictionary()
        {
            var oldDict = stateLocationData;

            Dictionary<RegionsEnum, IList<StatesEnum>> newDict = [];

            foreach (RegionsEnum region in Enum.GetValues(typeof(RegionsEnum)))
            {
                newDict.Add(region, []);
            }

            foreach (StatesEnum state in oldDict.Keys)
            {
                newDict[oldDict[state].Region].Add(state);
            }

            return newDict;
        }


        public ILocationData<StatesEnum, PlayersEnum, RegionsEnum> GetStateData(StatesEnum state) 
        {
            return stateLocationData[state];
        }

        public IList<StatesEnum> GetStatesInRegion(RegionsEnum region) 
        {
            return stateByRegion[region];
        }
    }
}
