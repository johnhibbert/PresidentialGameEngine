namespace PresidentialGameEngine.ClassLibrary.Interfaces
{
    public interface IRegionalComponent<StatesEnum, RegionsEnum, PlayersEnum>
    {
        IDictionary<PlayersEnum, StatesEnum> GetRawData();
        RegionsEnum GetRegionByState(StatesEnum state);
        IEnumerable<StatesEnum> GetStatesWithinRegion(RegionsEnum region);
        StatesEnum GetPlayerState(PlayersEnum player);
        void MovePlayerToState(PlayersEnum player, StatesEnum states);
    }
}