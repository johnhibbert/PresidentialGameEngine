namespace PresidentialGameEngine.ClassLibrary.Interfaces
{
    public interface ILocationData<TState, TPlayer, TRegion>
        where TState : Enum
        where TPlayer : Enum
        where TRegion : Enum
    {
        TState State { get; init; }

        int ElectoralVotes { get; init; }

        TPlayer Tilt { get; init; }

        TRegion Region { get; init; }

        int StartingSupport { get; init; }
    }

}
