namespace PresidentialGameEngine.ClassLibrary.Abstracts
{
    public abstract class GameOfMomentum<PlayersEnum> where PlayersEnum : Enum
    {
        public Dictionary<PlayersEnum, int> PlayerMomentum { get; init; }

        protected GameOfMomentum()
        {
            PlayerMomentum = [];

            foreach (PlayersEnum player in (PlayersEnum[])Enum.GetValues(typeof(PlayersEnum)))
            {
                PlayerMomentum.Add(player, 0);
            }
        }

        public int GetPlayerMomentum(PlayersEnum player)
        {
            return PlayerMomentum[player];
        }

        public void GainMomentum(PlayersEnum player, int amount)
        {
            PlayerMomentum[player] += amount;
        }

        public void LoseMomentum(PlayersEnum player, int amount)
        {
            PlayerMomentum[player] -= amount;
            if (PlayerMomentum[player] < 0)
            {
                PlayerMomentum[player] = 0;
            }
        }
    }
}
