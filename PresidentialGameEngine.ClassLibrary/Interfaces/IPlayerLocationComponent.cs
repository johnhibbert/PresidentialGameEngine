namespace PresidentialGameEngine.ClassLibrary.Interfaces
{
    public interface IPlayerLocationComponent<PlayersEnum, StatesEnum>
    {
        IDictionary<PlayersEnum, StatesEnum> GetRawData();
        StatesEnum GetPlayerState(PlayersEnum player);
        void MovePlayerToState(PlayersEnum player, StatesEnum states);
    }
}