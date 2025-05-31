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


            //Unsure if this is something we want to bother with.
            //But it might be faster than looking it up every time?
            //Might not matter overall.
            stateByRegion = ExtractStatesByRegion();
            stateElectoralVotes = ExtractElectoralVotes();
            stateStartingSupport = ExtractStartingSupportLevels();
            stateTilts = ExtractStateTilts();


        }

        private Dictionary<RegionsEnum, IList<StatesEnum>> ExtractStatesByRegion()
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

        private IDictionary<StatesEnum, int> ExtractElectoralVotes() 
        {
            var oldDict = stateLocationData;

            Dictionary<StatesEnum, int> newDict = [];

            foreach (StatesEnum state in oldDict.Keys)
            {
                newDict.Add(state, oldDict[state].ElectoralVotes);
            }

            return newDict;
        }

        private IDictionary<StatesEnum, PlayersEnum> ExtractStateTilts()
        {
            var oldDict = stateLocationData;

            Dictionary<StatesEnum, PlayersEnum> newDict = [];

            foreach (StatesEnum state in oldDict.Keys)
            {
                newDict.Add(state, oldDict[state].Tilt);
            }

            return newDict;
        }

        private IDictionary<StatesEnum, int> ExtractStartingSupportLevels()
        {
            var oldDict = stateLocationData;

            Dictionary<StatesEnum, int> newDict = [];

            foreach (StatesEnum state in oldDict.Keys)
            {
                newDict.Add(state, oldDict[state].StartingSupport);
            }

            return newDict;
        }

        public IDictionary<StatesEnum, ILocationData<StatesEnum, PlayersEnum, RegionsEnum>> GetRawData() 
        {
            return stateLocationData;
        }

        public ILocationData<StatesEnum, PlayersEnum, RegionsEnum> GetStateData(StatesEnum state) 
        {
            return stateLocationData[state];
        }

        public IList<StatesEnum> GetStatesInRegion(RegionsEnum region) 
        {
            return stateByRegion[region];
        }

        public int GetStateElectoralCollegeVotes(StatesEnum state) 
        {
            return stateElectoralVotes[state];
        }

        public int GetStateStartingSupportLevel(StatesEnum state)
        {
            return stateStartingSupport[state];
        }

        public PlayersEnum GetStateTilt(StatesEnum state)
        {
            return stateTilts[state];
        }
    }
}
