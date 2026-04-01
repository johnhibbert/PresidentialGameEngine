namespace PresidentialGameEngine.ClassLibrary.Interfaces
{
    /// <summary>
    /// IAccumulatingComponent represents when something is gained or lost by a player
    /// For 1960, this would cover the momentum and rest cubes gained by playing cards
    /// </summary>
    /// <typeparam name="TPlayer"></typeparam>
    public interface IAccumulatingComponent<TPlayer>
        where TPlayer : Enum
    {
        public IDictionary<TPlayer, int> GetRawData();

        public int GetPlayerAmount(TPlayer player);

        public void GainAmount(TPlayer player, int amount);

        public void LoseAmount(TPlayer player, int amount);
    }
}
