using PresidentialGameEngine.ClassLibrary.Components;
using PresidentialGameEngine.ClassLibrary.Data;
using PresidentialGameEngine.ClassLibrary.Engines;
using PresidentialGameEngine.ClassLibrary.Interfaces;
using NineteenSixty.Enums;
using NineteenSixty.Interfaces;
using Card = NineteenSixty.Data.Card;

namespace NineteenSixty;

public class Engine(
    IAccumulatingComponent<Player> momentumComponent,
    ISupportComponent<Player, Leader, Issue> issueSupportComponent,
    ICarriableSupportComponent<Player, Leader, State> stateSupportComponent,
    IPositioningComponent<Issue> issuePositioningComponent,
    IPoliticalCapitalComponent<Player> politicalCapitalComponent,
    IPlayerLocationComponent<Player, State> playerLocationComponent,
    IAccumulatingComponent<Player> restComponent,
    ISupportComponent<Player, Leader, Region> endorsementComponent,
    ISupportComponent<Player, Leader, Region> mediaSupportComponent,
    IExhaustionComponent<Player> exhaustionComponent,
    ICardComponent<Player, Card> cardComponent,
    IStaticDataComponent<State, Player, Region> staticDataComponent)
    : IEngine
{
    private IAccumulatingComponent<Player> MomentumComponent { get; init; } = momentumComponent;
    private ISupportComponent<Player, Leader, Issue> IssueSupportComponent { get; init; } = issueSupportComponent;

    private ICarriableSupportComponent<Player, Leader, State> StateSupportComponent { get; init; } =
        stateSupportComponent;

    private IPositioningComponent<Issue> IssuePositioningComponent { get; init; } = issuePositioningComponent;
    private IPoliticalCapitalComponent<Player> PoliticalCapitalComponent { get; init; } = politicalCapitalComponent;
    private IPlayerLocationComponent<Player, State> PlayerLocationComponent { get; init; } = playerLocationComponent;
    private IAccumulatingComponent<Player> RestComponent { get; init; } = restComponent;
    private ISupportComponent<Player, Leader, Region> EndorsementComponent { get; init; } = endorsementComponent;
    private ISupportComponent<Player, Leader, Region> MediaSupportComponent { get; init; } = mediaSupportComponent;
    private IExhaustionComponent<Player> ExhaustionComponent { get; init; } = exhaustionComponent;
    private ICardComponent<Player, Card> CardComponent { get; init; } = cardComponent;
    private IStaticDataComponent<State, Player, Region> StaticDataComponent { get; init; } = staticDataComponent;

    public GameState GetGameState()
    {
        return new GameState()
        {
            Momentum = MomentumComponent.GetRawData(),
            RestCubes = RestComponent.GetRawData(),
            IssueContests = IssueSupportComponent.GetRawData(),
            IssueOrder = IssuePositioningComponent.GetSubjectOrder,
            Endorsements = EndorsementComponent.GetRawData(),
            Exhaustion = ExhaustionComponent.GetRawData(),
            MediaSupportLevels = MediaSupportComponent.GetRawData(),
            PlayerLocations = PlayerLocationComponent.GetRawData(),
            StateContests = StateSupportComponent.GetRawData(),
        };
    }

    public void ImplementChanges(PlayerChosenChanges<Player, Issue, State, Region> changes)
    {
        foreach (var issueChange in changes.IssueChanges)
        {
            if (issueChange.Change > 0)
            {
                IssueSupportComponent.GainSupport(issueChange.Player, issueChange.Target, issueChange.Change);
            }
            else
            {
                IssueSupportComponent.LoseSupport(issueChange.Player, issueChange.Target, Math.Abs(issueChange.Change));
            }
        }

        foreach (var stateChange in changes.StateChanges)
        {
            if (stateChange.Change > 0)
            {
                StateSupportComponent.GainSupport(stateChange.Player, stateChange.Target, stateChange.Change);
            }
            else
            {
                StateSupportComponent.LoseSupport(stateChange.Player, stateChange.Target, Math.Abs(stateChange.Change));
            }
        }


        foreach (var mediaChange in changes.MediaSupportChanges)
        {
            if (mediaChange.Change > 0)
            {
                //MediaSupportComponent.GainMediaSupportWithoutSupportCheck(mediaChange.Player, mediaChange.Target, mediaChange.Change);
            }
            else
            {
                //LoseMediaSupport(mediaChange.Player, mediaChange.Target, Math.Abs(mediaChange.Change));
            }
        }

        foreach (var endorsementChange in changes.EndorsementChanges)
        {
            //GainEndorsement(endorsementChange.Player, endorsementChange.Target, endorsementChange.Change);
        }

        if (changes.NewIssuesOrder.Count > 0)
        {
            IssuePositioningComponent.SetSubjectOrder(changes.NewIssuesOrder);
        }


    }



    public class GameState
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

}