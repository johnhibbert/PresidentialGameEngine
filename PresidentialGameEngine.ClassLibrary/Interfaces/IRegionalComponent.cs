namespace PresidentialGameEngine.ClassLibrary.Interfaces
{
    public interface IRegionalComponent<StatesEnum, RegionsEnum, PlayersEnum>
    {
        RegionsEnum GetRegionByState(StatesEnum state);
        IEnumerable<StatesEnum> GetStatesWithinRegion(RegionsEnum region);
        StatesEnum GetPlayerState(PlayersEnum player);
        void MovePlayerToState(PlayersEnum player, StatesEnum states);
    }
}