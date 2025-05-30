namespace PresidentialGameEngine.ClassLibrary.Interfaces
{
    public interface IExhaustionComponent<PlayersEnum> where PlayersEnum : Enum
    {
        public IDictionary<PlayersEnum, bool> GetRawData();

        void ExhaustPlayer(PlayersEnum player);
        bool IsPlayerReady(PlayersEnum player);
        void UnexhaustPlayer(PlayersEnum player);
    }
}