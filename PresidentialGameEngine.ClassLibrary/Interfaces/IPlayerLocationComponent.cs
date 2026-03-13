namespace PresidentialGameEngine.ClassLibrary.Interfaces
{
    public interface IPlayerLocationComponent<TPlayer, TState>
    {
        IDictionary<TPlayer, TState> GetRawData();
        TState GetPlayerState(TPlayer player);
        void MovePlayerToState(TPlayer player, TState states);
    }
}