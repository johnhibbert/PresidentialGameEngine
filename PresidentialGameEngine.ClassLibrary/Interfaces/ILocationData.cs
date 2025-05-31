namespace PresidentialGameEngine.ClassLibrary.Interfaces
{
    public interface ILocationData<StatesEnum, PlayersEnum, RegionsEnum>
        where StatesEnum : Enum
        where PlayersEnum : Enum
        where RegionsEnum : Enum
    {
        StatesEnum State { get; init; }

        int ElectoralVotes { get; init; }

        PlayersEnum Tilt { get; init; }

        RegionsEnum Region { get; init; }

        int StartingSupport { get; init; }
    }

}
