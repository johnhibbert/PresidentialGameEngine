namespace PresidentialGameEngine.ClassLibrary.Interfaces
{
    public interface IRegionalComponent<StatesEnum, RegionsEnum>
    {
        RegionsEnum GetRegionByState(StatesEnum state);
        IEnumerable<StatesEnum> GetStatesWithinRegion(RegionsEnum region);
    }
}