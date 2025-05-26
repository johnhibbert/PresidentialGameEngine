namespace PresidentialGameEngine.ClassLibrary.Data
{
    public class SupportChange<PlayersEnum, TargetEnum>(PlayersEnum player, TargetEnum target, int change)
        where PlayersEnum : Enum
        where TargetEnum : Enum
    {

        public PlayersEnum Player { get; init; } = player;

        public TargetEnum Target { get; init; } = target;

        //This is not init because we will want to deduct these after support checks.
        public int Change { get; set; } = change;
    }

}


