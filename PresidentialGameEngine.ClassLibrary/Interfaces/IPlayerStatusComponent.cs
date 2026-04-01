namespace PresidentialGameEngine.ClassLibrary.Interfaces;

/// <summary>
/// IPlayerStatusComponent describes the current state of players moving between a set of statuses
/// For 1960, this would cover the ready/exhausted state of Kennedy and Nixon
/// </summary>
/// <typeparam name="TPlayer">The player enumeration</typeparam>
/// <typeparam name="TStatus">The status enumeration</typeparam>
public interface IPlayerStatusComponent<TPlayer, TStatus>
    where TPlayer : Enum
    where TStatus : Enum
{
    public IDictionary<TPlayer, TStatus> GetRawData();

    void UpdatePlayerStatus(TPlayer player, TStatus  status);
}
