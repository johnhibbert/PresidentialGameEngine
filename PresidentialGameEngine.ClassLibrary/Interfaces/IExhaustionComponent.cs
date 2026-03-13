namespace PresidentialGameEngine.ClassLibrary.Interfaces
{
    public interface IExhaustionComponent<TPlayer> where TPlayer : Enum
    {
        public IDictionary<TPlayer, bool> GetRawData();

        void ExhaustPlayer(TPlayer player);
        bool IsPlayerReady(TPlayer player);
        void UnexhaustPlayer(TPlayer player);
    }
}