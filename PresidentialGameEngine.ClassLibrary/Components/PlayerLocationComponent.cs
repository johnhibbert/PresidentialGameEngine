using PresidentialGameEngine.ClassLibrary.Interfaces;

namespace PresidentialGameEngine.ClassLibrary.Components
{
    public class PlayerLocationComponent<TPlayer, TLocation> : IPlayerLocationComponent<TPlayer, TLocation>
    {
        private IDictionary<TPlayer, TLocation> PlayerLocations { get; init; }

        public IDictionary<TPlayer, TLocation> GetRawData() { return PlayerLocations; }

        public PlayerLocationComponent(IDictionary<TPlayer, TLocation> playerStartingLocations)
        {
            PlayerLocations = playerStartingLocations;
        }
        public TLocation GetPlayerLocation(TPlayer player)
        {
            return PlayerLocations[player];
        }

        public void MovePlayerToLocation(TPlayer player, TLocation location)
        {
            PlayerLocations[player] = location;
        }
    }
}
