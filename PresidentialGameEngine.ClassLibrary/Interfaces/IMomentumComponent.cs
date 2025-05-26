namespace PresidentialGameEngine.ClassLibrary.Interfaces
{
    public interface IMomentumComponent<PlayersEnum>
        where PlayersEnum : Enum
    {
        public int GetPlayerMomentum(PlayersEnum player);

        public void GainMomentum(PlayersEnum player, int amount);

        public void LoseMomentum(PlayersEnum player, int amount);
    }
}
