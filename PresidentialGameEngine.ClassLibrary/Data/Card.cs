using PresidentialGameEngine.ClassLibrary.Engines;
using PresidentialGameEngine.ClassLibrary.Enums;

namespace PresidentialGameEngine.ClassLibrary.Data
{
    public class Card(int index, string title, string eventText, int campaignPoints, Issue issue, Affiliation candidate, State state) : ICard
    {
        public int Index { get; init; } = index;

        public string Title { get; init; } = title;

        public string Text { get; init; } = eventText;

        public int CampaignPoints { get; init; } = campaignPoints;

        public int RestCubes
        {
            get { return 4 - CampaignPoints; }
        }

        public Issue Issue { get; init; } = issue;

        public Affiliation Affiliation { get; init; } = candidate;

        public State State { get; init; } = state;

        public required Predicate<PlayerChosenChanges<Player, Issue, State>> AreChangesValid { get; init; }

        public required Action<NineteenSixtyGameEngine, Player, PlayerChosenChanges<Player, Issue, State>> Event { get; init; }

        public override string ToString()
        {
            return $"{Title} [{Index}]";
        }

    }

    public abstract class ICard 
    {
        public int Index { get; init; }

        public string Title { get; init; }

        public string Text { get; init; }

        public int CampaignPoints { get; init; }

        public int RestCubes
        {
            get { return 4 - CampaignPoints; }
        }

        public Issue Issue { get; init; }

        public Affiliation Affiliation { get; init; }

        public State State { get; init; }

        public Predicate<PlayerChosenChanges<Player, Issue, State>> AreChangesValid { get; init; }

        public Action<NineteenSixtyGameEngine, Player, PlayerChosenChanges<Player, Issue, State>> Event { get; init; }

        public override string ToString()
        {
            return $"{Title} [{Index}]";
        }
    };

    public class NewCard: ICard
    {

    }

}
