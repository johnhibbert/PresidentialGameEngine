using PresidentialGameEngine.ClassLibrary.Interfaces;

namespace PresidentialGameEngine.ClassLibrary.Components
{
    public class ExhaustionComponent<TPlayer> : IExhaustionComponent<TPlayer>
        where TPlayer : Enum
    {
        private IDictionary<TPlayer, bool> PlayerStatuses { get; init; }

        public IDictionary<TPlayer, bool> GetRawData() { return PlayerStatuses; }

        public ExhaustionComponent()
        {
            PlayerStatuses = new Dictionary<TPlayer, bool>();

            foreach (TPlayer player in (TPlayer[])Enum.GetValues(typeof(TPlayer)))
            {
                PlayerStatuses.Add(player, true);
            }
        }

        public bool IsPlayerReady(TPlayer player)
        {
            return PlayerStatuses[player];
        }

        public void ExhaustPlayer(TPlayer player)
        {
            PlayerStatuses[player] = false;
        }

        public void UnexhaustPlayer(TPlayer player)
        {
            PlayerStatuses[player] = true;
        }
    }
}
