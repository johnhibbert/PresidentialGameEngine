using PresidentialGameEngine.ClassLibrary.Interfaces;

namespace PresidentialGameEngine.ClassLibrary.Components
{
    public class PlayerLocationComponent<TPlayer, TState> : IPlayerLocationComponent<TPlayer, TState>
    {
        private IDictionary<TPlayer, TState> PlayerLocations { get; init; }

        public IDictionary<TPlayer, TState> GetRawData() { return PlayerLocations; }

        public PlayerLocationComponent(IDictionary<TPlayer, TState> playerStartingLocations)
        {
            PlayerLocations = playerStartingLocations;
        }
        public TState GetPlayerState(TPlayer player)
        {
            return PlayerLocations[player];
        }

        public void MovePlayerToState(TPlayer player, TState states)
        {
            PlayerLocations[player] = states;
        }
    }
}
