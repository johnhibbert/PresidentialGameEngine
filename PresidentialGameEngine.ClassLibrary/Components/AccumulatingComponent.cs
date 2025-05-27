using PresidentialGameEngine.ClassLibrary.Interfaces;

namespace PresidentialGameEngine.ClassLibrary.Components
{
    public class AccumulatingComponent<PlayersEnum> : IAccumulatingComponent<PlayersEnum>
        where PlayersEnum : Enum
    {
        private Dictionary<PlayersEnum, int> PlayerAmounts { get; init; }

        public AccumulatingComponent()
        {
            PlayerAmounts = [];

            foreach (PlayersEnum player in (PlayersEnum[])Enum.GetValues(typeof(PlayersEnum)))
            {
                PlayerAmounts.Add(player, 0);
            }
        }

        public int GetPlayerAmount(PlayersEnum player)
        {
            return PlayerAmounts[player];
        }

        public void GainAmount(PlayersEnum player, int amount)
        {
            PlayerAmounts[player] += amount;
        }

        public void LoseAmount(PlayersEnum player, int amount)
        {
            PlayerAmounts[player] -= amount;
            if (PlayerAmounts[player] < 0)
            {
                PlayerAmounts[player] = 0;
            }
        }
    }
}
