namespace PresidentialGameEngine.ClassLibrary.Interfaces
{
    public interface IStaticDataComponent<StatesEnum, PlayersEnum, RegionsEnum>
        where StatesEnum : Enum
        where PlayersEnum : Enum
        where RegionsEnum : Enum
    {
        IDictionary<StatesEnum, ILocationData<StatesEnum, PlayersEnum, RegionsEnum>> GetRawData();
        ILocationData<StatesEnum, PlayersEnum, RegionsEnum> GetStateData(StatesEnum state);
        int GetStateElectoralCollegeVotes(StatesEnum state);
        IList<StatesEnum> GetStatesInRegion(RegionsEnum region);
        int GetStateStartingSupportLevel(StatesEnum state);
        PlayersEnum GetStateTilt(StatesEnum state);
    }
}