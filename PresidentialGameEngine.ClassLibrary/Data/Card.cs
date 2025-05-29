using PresidentialGameEngine.ClassLibrary.Engines;
using PresidentialGameEngine.ClassLibrary.Enums;

namespace PresidentialGameEngine.ClassLibrary.Data
{
    public class Card
    {
        public int Index { get; init; }

        public required string Title { get; init; }

        public required string Text { get; init; }

        public int CampaignPoints { get; init; }

        public int RestCubes
        {
            get { return 4 - CampaignPoints; }
        }

        public Issue Issue { get; init; }

        public Affiliation Affiliation { get; init; }

        public State State { get; init; }

        public EventType CardType { get; init; }

        public required Predicate<PlayerChosenChanges<Player, Issue, State>> AreChangesValid { get; init; }

        public required Action<NineteenSixtyGameEngine, Player, PlayerChosenChanges<Player, Issue, State>> Event { get; init; }

        public override string ToString()
        {
            return $"{Title} [{Index}]";
        }

        public string ToLongString() 
        {
            return $"{ToString()}: {Text}";
        }
    };
}
