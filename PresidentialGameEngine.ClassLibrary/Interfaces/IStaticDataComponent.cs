namespace PresidentialGameEngine.ClassLibrary.Interfaces
{
    public interface IStaticDataComponent<TState, TPlayer, TRegion>
        where TState : Enum
        where TPlayer : Enum
        where TRegion : Enum
    {
        IDictionary<TState, ILocationData<TState, TPlayer, TRegion>> GetRawData();
        ILocationData<TState, TPlayer, TRegion> GetStateData(TState state);
        int GetStateElectoralCollegeVotes(TState state);
        IList<TState> GetStatesInRegion(TRegion region);
        int GetStateStartingSupportLevel(TState state);
        TPlayer GetStateTilt(TState state);
    }
}