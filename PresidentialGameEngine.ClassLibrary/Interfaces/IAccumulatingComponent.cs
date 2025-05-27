namespace PresidentialGameEngine.ClassLibrary.Interfaces
{
    public interface IAccumulatingComponent<PlayersEnum>
        where PlayersEnum : Enum
    {
        public int GetPlayerAmount(PlayersEnum player);

        public void GainAmount(PlayersEnum player, int amount);

        public void LoseAmount(PlayersEnum player, int amount);
    }
}
