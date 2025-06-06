using PresidentialGameEngine.ClassLibrary.Data;
using PresidentialGameEngine.ClassLibrary.Engines;
using PresidentialGameEngine.ClassLibrary.Enums;

namespace PresidentialGameEngine.ClassLibrary.Interfaces
{
    public interface ICard
    {
        Affiliation Affiliation { get; init; }
        Predicate<PlayerChosenChanges<Player, Issue, State, Region>> AreChangesValid { get; init; }
        int CampaignPoints { get; init; }
        Action<NineteenSixtyGameEngine, Player, PlayerChosenChanges<Player, Issue, State, Region>> Event { get; init; }
        EventType EventType { get; init; }
        int Index { get; init; }
        Issue Issue { get; init; }
        bool RequiresPlayerInput { get; init; }
        int RestCubes { get; }
        State State { get; init; }
        string Text { get; init; }
        string Title { get; init; }

        string ToLongString();
        string ToString();
    }
}