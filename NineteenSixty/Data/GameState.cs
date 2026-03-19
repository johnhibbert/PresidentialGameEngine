namespace NineteenSixty.Data;

using PresidentialGameEngine.ClassLibrary.Data;
using Enums;

public record GameState
{
    public required IDictionary<Player, int> Momentum { get; init; }

    public required IDictionary<Player, int> RestCubes { get; init; }

    public required IDictionary<Issue, SupportContest<Leader>> IssueContests { get; init; }

    public required IDictionary<State, SupportContest<Leader>> StateContests { get; init; }

    public required IList<Issue> IssueOrder { get; init; }

    public required IDictionary<Region, SupportContest<Leader>> Endorsements { get; init; }

    public required IDictionary<Region, SupportContest<Leader>> MediaSupportLevels { get; init; }

    public required IDictionary<Player, State> PlayerLocations { get; init; }

    public required IDictionary<Player, bool> Exhaustion { get; init; }
}