namespace PresidentialGameEngine.ClassLibrary.Interfaces;

public interface IPlayerStatusComponent<TPlayer, TStatus>
    where TPlayer : Enum
    where TStatus : Enum
{
    public IDictionary<TPlayer, TStatus> GetRawData();

    void UpdatePlayerStatus(TPlayer player, TStatus  status);
}
