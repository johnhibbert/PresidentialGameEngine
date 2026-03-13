using PresidentialGameEngine.ClassLibrary.Interfaces;

namespace PresidentialGameEngine.ClassLibrary.Components
{
    public class AccumulatingComponent<TPlayer> : IAccumulatingComponent<TPlayer>
        where TPlayer : Enum
    {
        private Dictionary<TPlayer, int> PlayerAmounts { get; init; }

        public IDictionary<TPlayer, int> GetRawData() { return PlayerAmounts; }


        public AccumulatingComponent()
        {
            PlayerAmounts = [];

            foreach (TPlayer player in (TPlayer[])Enum.GetValues(typeof(TPlayer)))
            {
                PlayerAmounts.Add(player, 0);
            }
        }

        public int GetPlayerAmount(TPlayer player)
        {
            return PlayerAmounts[player];
        }

        public void GainAmount(TPlayer player, int amount)
        {
            PlayerAmounts[player] += amount;
        }

        public void LoseAmount(TPlayer player, int amount)
        {
            PlayerAmounts[player] -= amount;
            if (PlayerAmounts[player] < 0)
            {
                PlayerAmounts[player] = 0;
            }
        }
    }
}
