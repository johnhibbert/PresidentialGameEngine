using NineteenSixty.Enums;

namespace NineteenSixty.Data;

public class IssueRewards
{
    public Dictionary<Player, int> MomentumGains { get; set; } = new()
    {
        { Player.Kennedy , 0},
        { Player.Nixon , 0},
    };
    public Dictionary<Player, List<Region>> EndorsementGains { get; set; } = new()
    {
        { Player.Kennedy , [] },
        { Player.Nixon , [] },
    };
}