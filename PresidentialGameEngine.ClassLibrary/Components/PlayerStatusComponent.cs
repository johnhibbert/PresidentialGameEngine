using PresidentialGameEngine.ClassLibrary.Interfaces;

namespace PresidentialGameEngine.ClassLibrary.Components;

public class PlayerStatusComponent<TPlayer, TStatus> : IPlayerStatusComponent<TPlayer, TStatus>
    where TPlayer : Enum
    where TStatus : Enum
{
    
    private IDictionary<TPlayer, TStatus> PlayerStatuses { get; init; }
    
    public PlayerStatusComponent()
    {
        PlayerStatuses = new Dictionary<TPlayer, TStatus>();

        var defaultStatus = (TStatus) Enum.ToObject(typeof(TStatus), 0);
        
        foreach (var player in (TPlayer[])Enum.GetValues(typeof(TPlayer)))
        {
            PlayerStatuses.Add(player, defaultStatus);
        }
    }
    
    public IDictionary<TPlayer, TStatus> GetRawData()
    {
        return PlayerStatuses;
    }

    public void UpdatePlayerStatus(TPlayer player, TStatus status)
    {
        PlayerStatuses[player] =  status;
    }
}