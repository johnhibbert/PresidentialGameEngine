using PresidentialGameEngine.ClassLibrary.Components;
using PresidentialGameEngine.ClassLibrary.Data;
using PresidentialGameEngine.ClassLibrary.Interfaces;
using NineteenSixty.Enums;
using NineteenSixty.Interfaces;
using NineteenSixty.Data;
using Card = NineteenSixty.Data.Card;

namespace NineteenSixty;

public class Engine(
    IAccumulatingComponent<Player> momentumComponent,
    ISupportComponent<Player, Leader, Issue> issueSupportComponent,
    ICarriableSupportComponent<Player, Leader, State> stateSupportComponent,
    IPositioningComponent<Issue> issuePositioningComponent,
    IBlindBagComponent<Player> politicalCapitalComponent,
    IPlayerLocationComponent<Player, State> playerLocationComponent,
    IAccumulatingComponent<Player> restComponent,
    ISupportComponent<Player, Leader, Region> endorsementComponent,
    ISupportComponent<Player, Leader, Region> mediaSupportComponent,
    IPlayerStatusComponent<Player, Status> exhaustionComponent,
    ICardZoneComponent<CardZone, Player, Card> cardZoneComponent)
    //IStaticDataComponent<State, Player, Region> staticDataComponent)
    : IEngine
{
    private IAccumulatingComponent<Player> MomentumComponent { get; init; } = momentumComponent;
    private ISupportComponent<Player, Leader, Issue> IssueSupportComponent { get; init; } = issueSupportComponent;

    private ICarriableSupportComponent<Player, Leader, State> StateSupportComponent { get; init; } =
        stateSupportComponent;

    private IPositioningComponent<Issue> IssuePositioningComponent { get; init; } = issuePositioningComponent;
    private IBlindBagComponent<Player> PoliticalCapitalComponent { get; init; } = politicalCapitalComponent;
    private IPlayerLocationComponent<Player, State> PlayerLocationComponent { get; init; } = playerLocationComponent;
    private IAccumulatingComponent<Player> RestComponent { get; init; } = restComponent;
    private ISupportComponent<Player, Leader, Region> EndorsementComponent { get; init; } = endorsementComponent;
    private ISupportComponent<Player, Leader, Region> MediaSupportComponent { get; init; } = mediaSupportComponent;
    private IPlayerStatusComponent<Player, Status> ExhaustionComponent { get; init; } = exhaustionComponent;
    private ICardZoneComponent<CardZone, Player, Card> CardZoneComponent { get; init; } = cardZoneComponent;
    //private IStaticDataComponent<State, Player, Region> StaticDataComponent { get; init; } = staticDataComponent;

    public GameState GetGameState()
    {
        return new GameState()
        {
            Momentum = MomentumComponent.GetRawData(),
            RestCubes = RestComponent.GetRawData(),
            IssueContests = IssueSupportComponent.GetRawData(),
            IssueOrder = IssuePositioningComponent.GetSubjectOrder,
            Endorsements = EndorsementComponent.GetRawData(),
            PlayerStatuses = ExhaustionComponent.GetRawData(),
            MediaSupportLevels = MediaSupportComponent.GetRawData(),
            PlayerLocations = PlayerLocationComponent.GetRawData(),
            StateContests = StateSupportComponent.GetRawData(),
        };
    }

    public void ImplementChanges(SetOfChanges changes)
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
                MediaSupportComponent.GainSupport(mediaChange.Player, mediaChange.Target, mediaChange.Change);
            }
            else
            {
                MediaSupportComponent.LoseSupport(mediaChange.Player, mediaChange.Target, Math.Abs(mediaChange.Change));
            }
        }

        foreach (var endorsementChange in changes.EndorsementChanges)
        {
            GainEndorsement(endorsementChange.Player, endorsementChange.Target, endorsementChange.Change);
        }

        if (changes.NewIssuesOrder.Count > 0)
        {
            IssuePositioningComponent.SetSubjectOrder(changes.NewIssuesOrder);
        }


    }

    public void GainMomentum(Player player, int amount)
    {
        MomentumComponent.GainAmount(player, amount);
    }

    public int GetPlayerMomentum(Player player)
    {
        return MomentumComponent.GetPlayerAmount(player);
    }

    public void LoseMomentum(Player player, int amount)
    {
        MomentumComponent.LoseAmount(player, amount);
    }

    public Leader GetLeader(State state)
    {
        return StateSupportComponent.GetLeader(state);
    }

    public int GetSupportAmount(State state)
    {
        return StateSupportComponent.GetSupportStatus(state).Support;
    }

    public void GainSupport(Player player, State state, int amount)
    {
        StateSupportComponent.GainSupport(player, state, amount);
    }

    public void LoseSupport(Player player, State state, int amount)
    {
        StateSupportComponent.LoseSupport(player, state, amount);
    }

    public Leader GetLeader(Issue issue)
    {
        return IssueSupportComponent.GetLeader(issue);
    }

    public int GetSupportAmount(Issue issue)
    {
        return IssueSupportComponent.GetSupportStatus(issue).Support;
    }

    public void GainSupport(Player player, Issue issue, int amount)
    {
        IssueSupportComponent.GainSupport(player, issue, amount);
    }

    public void LoseSupport(Player player, Issue issue, int amount)
    {
        IssueSupportComponent.LoseSupport(player, issue, amount);
    }

    public void SetIssueOrder(IEnumerable<Issue> orderedIssues)
    {
        IssuePositioningComponent.SetSubjectOrder(orderedIssues);
    }

    public void MoveIssueUp(Issue issue)
    {
        IssuePositioningComponent.MoveSubjectUp(issue);
    }

    public State GetPlayerState(Player player)
    {
        return PlayerLocationComponent.GetPlayerState(player);
    }

    public void MovePlayerToState(Player player, State state)
    {
        PlayerLocationComponent.MovePlayerToState(player, state);
    }

    public void GainEndorsement(Player player, Region region, int amount)
    {
        EndorsementComponent.GainSupport(player, region, amount);
    }

    public void LoseEndorsement(Player player, Region region, int amount)
    {
        EndorsementComponent.LoseSupport(player, region, amount);
    }

    public Leader GetEndorsementLeader(Region region)
    {
        return EndorsementComponent.GetLeader(region);
    }

    public int GetNumberOfEndorsements(Region region)
    {
        return EndorsementComponent.GetSupportStatus(region).Support;
    }

    public void ExhaustPlayer(Player player)
    {
        ExhaustionComponent.UpdatePlayerStatus(player, Status.Exhausted);
    }

    public void UnexhaustPlayer(Player player)
    {
        ExhaustionComponent.UpdatePlayerStatus(player, Status.Ready);
    }


    public void GainMediaSupport(Player player, Region region, int amount)
    {
        MediaSupportComponent.GainSupport(player, region, amount);
    }

    public void LoseMediaSupport(Player player, Region region, int amount)
    {
        MediaSupportComponent.LoseSupport(player, region, amount);
    }

    public IEnumerable<Card> GetCardsInZone(CardZone zone, Player player)
    {
        return CardZoneComponent.GetCardsInZone(zone, player);
    }

    public void AddCardsToZone(IEnumerable<Card> cards, CardZone zone, Player player)
    {
        CardZoneComponent.AddCardsToZone(cards, zone, player);
    }

    public void MoveCardFromOneZoneToAnother(Player player, Card cardToMove, CardZone source, CardZone destination)
    {
        cardZoneComponent.MoveCardFromOneZoneToAnother(player, cardToMove, source, destination);
    }

    public void ReturnCardFromDiscardPileToPlayerHandIfAvailable(Player player, Card cardToRecover)
    {
        var cardsInDiscardPile = CardZoneComponent.GetCardsInZone(CardZone.DiscardPile, player);
        if (cardsInDiscardPile.Contains(cardToRecover))
        {
            cardZoneComponent.MoveCardFromOneZoneToAnother(player, cardToRecover,
                CardZone.DiscardPile, CardZone.Hand);
        }
    }
}