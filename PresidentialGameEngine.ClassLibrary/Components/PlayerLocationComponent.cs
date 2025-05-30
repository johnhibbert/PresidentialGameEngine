using PresidentialGameEngine.ClassLibrary.Interfaces;

namespace PresidentialGameEngine.ClassLibrary.Components
{
    public class PlayerLocationComponent<PlayersEnum, StatesEnum> : IPlayerLocationComponent<PlayersEnum, StatesEnum>
    {
        private IDictionary<PlayersEnum, StatesEnum> PlayerLocations { get; init; }

        public IDictionary<PlayersEnum, StatesEnum> GetRawData() { return PlayerLocations; }

        public PlayerLocationComponent(IDictionary<PlayersEnum, StatesEnum> playerStartingLocations)
        {
            PlayerLocations = playerStartingLocations;
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
