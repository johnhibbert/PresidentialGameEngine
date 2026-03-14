namespace PresidentialGameEngine.ClassLibrary.Data
{
    public class SupportChange<TPlayer, TTarget>(TPlayer player, TTarget target, int change)
        where TPlayer : Enum
        where TTarget : Enum
    {
        public TPlayer Player { get; init; } = player;

        public TTarget Target { get; init; } = target;

        //This is not init because we will want to deduct these after support checks.
        public int Change { get; set; } = change;
    }

}


