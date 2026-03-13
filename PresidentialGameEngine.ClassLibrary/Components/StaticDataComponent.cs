using PresidentialGameEngine.ClassLibrary.Interfaces;

namespace PresidentialGameEngine.ClassLibrary.Components
{
    public class StaticDataComponent<TState, TPlayer, TRegion> : IStaticDataComponent<TState, TPlayer, TRegion> where TState : Enum
        where TPlayer : Enum
        where TRegion : Enum
    {
        readonly IDictionary<TState, ILocationData<TState, TPlayer, TRegion>> stateLocationData;
        readonly IDictionary<TRegion, IList<TState>> stateByRegion;

        readonly IDictionary<TState, TPlayer> stateTilts;
        readonly IDictionary<TState, int> stateStartingSupport;
        readonly IDictionary<TState, int> stateElectoralVotes;

        public StaticDataComponent(IDictionary<TState, ILocationData<TState, TPlayer, TRegion>> locationData)
        {
            stateLocationData = locationData;

            //We could simplify this
            //But I think encoding it now and making it available is better
            stateByRegion = ExtractStatesByRegion();
            stateElectoralVotes = ExtractElectoralVotes();
            stateStartingSupport = ExtractStartingSupportLevels();
            stateTilts = ExtractStateTilts();
        }

        private Dictionary<TRegion, IList<TState>> ExtractStatesByRegion()
        {
            var oldDict = stateLocationData;

            Dictionary<TRegion, IList<TState>> newDict = [];

            foreach (TRegion region in Enum.GetValues(typeof(TRegion)))
            {
                newDict.Add(region, []);
            }

            foreach (TState state in oldDict.Keys)
            {
                newDict[oldDict[state].Region].Add(state);
            }

            return newDict;
        }

        private IDictionary<TState, int> ExtractElectoralVotes()
        {
            var oldDict = stateLocationData;

            Dictionary<TState, int> newDict = [];

            foreach (TState state in oldDict.Keys)
            {
                newDict.Add(state, oldDict[state].ElectoralVotes);
            }

            return newDict;
        }

        private IDictionary<TState, TPlayer> ExtractStateTilts()
        {
            var oldDict = stateLocationData;

            Dictionary<TState, TPlayer> newDict = [];

            foreach (TState state in oldDict.Keys)
            {
                newDict.Add(state, oldDict[state].Tilt);
            }

            return newDict;
        }

        private IDictionary<TState, int> ExtractStartingSupportLevels()
        {
            var oldDict = stateLocationData;

            Dictionary<TState, int> newDict = [];

            foreach (TState state in oldDict.Keys)
            {
                newDict.Add(state, oldDict[state].StartingSupport);
            }

            return newDict;
        }

        public IDictionary<TState, ILocationData<TState, TPlayer, TRegion>> GetRawData()
        {
            return stateLocationData;
        }

        public ILocationData<TState, TPlayer, TRegion> GetStateData(TState state)
        {
            return stateLocationData[state];
        }

        public IList<TState> GetStatesInRegion(TRegion region)
        {
            return stateByRegion[region];
        }

        public int GetStateElectoralCollegeVotes(TState state)
        {
            return stateElectoralVotes[state];
        }

        public int GetStateStartingSupportLevel(TState state)
        {
            return stateStartingSupport[state];
        }

        public TPlayer GetStateTilt(TState state)
        {
            return stateTilts[state];
        }
    }
}
