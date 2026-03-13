namespace PresidentialGameEngine.ClassLibrary.Interfaces
{
    public interface IAccumulatingComponent<TPlayer>
        where TPlayer : Enum
    {
        public IDictionary<TPlayer, int> GetRawData();

        public int GetPlayerAmount(TPlayer player);

        public void GainAmount(TPlayer player, int amount);

        public void LoseAmount(TPlayer player, int amount);
    }
}
