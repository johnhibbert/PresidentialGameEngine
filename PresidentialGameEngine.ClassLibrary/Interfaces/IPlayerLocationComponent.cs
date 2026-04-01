namespace PresidentialGameEngine.ClassLibrary.Interfaces
{
    /// <summary>
    /// IPlayerLocationComponent describes the location of a player's candidate piece
    /// For 1960, this would cover the state the candidate is located in
    /// </summary>
    /// <typeparam name="TPlayer">The player enumeration</typeparam>
    /// <typeparam name="TLocation">The player enumeration</typeparam>
    public interface IPlayerLocationComponent<TPlayer, TLocation>
    {
        IDictionary<TPlayer, TLocation> GetRawData();
        TLocation GetPlayerLocation(TPlayer player);
        void MovePlayerToLocation(TPlayer player, TLocation location);
    }
}