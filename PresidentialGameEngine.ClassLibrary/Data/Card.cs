using PresidentialGameEngine.ClassLibrary.Enums;

namespace PresidentialGameEngine.ClassLibrary.Data
{
    public class Card(int index, string title, string eventText, int campaignPoints, Issue issue, Affiliation candidate, State state)
    {
        public int Index { get; internal set; } = index;

        public string Title { get; internal set; } = title;

        public string Text { get; internal set; } = eventText;

        public int CampaignPoints { get; internal set; } = campaignPoints;

        public int RestCubes
        {
            get { return 4 - CampaignPoints; }
        }

        public Issue Issue { get; internal set; } = issue;

        public Affiliation Candidate { get; internal set; } = candidate;

        public State State { get; internal set; } = state;

        public required Predicate<PlayerChosenChanges<Player, Issue, State>> AreChangesValid { get; init; }

        public required Action<NineteenSixtyGameEngine, Player, PlayerChosenChanges<Player, Issue, State>> Event { get; init; }

        public override string ToString()
        {
            return $"{Title} [{Index}]";
        }

    }

}
