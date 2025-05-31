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

        readonly IDictionary<StatesEnum, PlayersEnum> stateTilts;
        readonly IDictionary<StatesEnum, int> stateStartingSupport;
        readonly IDictionary<StatesEnum, int> stateElectoralVotes;

        public StaticDataComponent(IDictionary<StatesEnum, ILocationData<StatesEnum, PlayersEnum, RegionsEnum>> locationData)
        {
            stateLocationData = locationData;

            stateByRegion = ReverseStateRegionDictionary();

            stateElectoralVotes = GetElectoralVoteDictionary();
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


        //Unclear if we want to actually do this.
        private IDictionary<StatesEnum, int> GetElectoralVoteDictionary() 
        {
            var oldDict = stateLocationData;

            Dictionary<StatesEnum, int> newDict = [];

            foreach (StatesEnum state in oldDict.Keys)
            {
                newDict.Add(state, oldDict[state].ElectoralVotes);
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
