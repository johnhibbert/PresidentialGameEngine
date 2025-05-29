namespace PresidentialGameEngine.ClassLibrary.Interfaces
{
    public interface IExhaustionComponent<PlayersEnum> where PlayersEnum : Enum
    {
        void ExhaustPlayer(PlayersEnum player);
        bool IsPlayerReady(PlayersEnum player);
        void UnexhaustPlayer(PlayersEnum player);
    }
}