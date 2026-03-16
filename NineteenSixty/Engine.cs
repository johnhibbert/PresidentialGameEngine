using PresidentialGameEngine.ClassLibrary.Enums;
using PresidentialGameEngine.ClassLibrary.Engines;
using PresidentialGameEngine.ClassLibrary.Data;
using PresidentialGameEngine.ClassLibrary.Components;
using PresidentialGameEngine.ClassLibrary.Interfaces;
/*
using Issue = NineteenSixty.Enums.Issue;
using Leader = NineteenSixty.Enums.Leader;
using Player = NineteenSixty.Enums.Player;
using Region = NineteenSixty.Enums.Region;
using State = NineteenSixty.Enums.State;
*/

namespace NineteenSixty;

public class Engine :IEngine<Player, Leader, Issue, State, Region>
{

    //GetGameState() 
    //ImplementChanges(PlayerChosenChanges<TPlayer, TIssue, TState, TRegion> changes)
    
    
    IAccumulatingComponent<Player> MomentumComponent { get; init; }
   ISupportComponent<Player, Leader, Issue> IssueSupportComponent { get; init; }
    ICarriableSupportComponent<Player, Leader, State> StateSupportComponent { get; init; }
    IPositioningComponent<Issue> IssuePositioningComponent { get; init; }
    IPoliticalCapitalComponent<Player> PoliticalCapitalComponent { get; init; }
    IPlayerLocationComponent<Player, State> PlayerLocationComponent { get; init; }
    IAccumulatingComponent<Player> RestComponent { get; init; }
    ISupportComponent<Player, Leader, Region> EndorsementComponent { get; init; }
    ISupportComponent<Player, Leader, Region> MediaSupportComponent { get; init; }
    IExhaustionComponent<Player> ExhaustionComponent { get; init; }
    ICardComponent<Player, Card> CardComponent { get; init; }
    IStaticDataComponent<State, Player, Region> StaticDataComponent { get; init; }
    
    public GameState<Player, Leader, Issue, State, Region> GetGameState()
    {
        return new GameState<Player, Leader, Issue, State, Region>()
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
        foreach (SupportChange<Player, Issue> issueChange in changes.IssueChanges)
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

        foreach (SupportChange<Player, State> stateChange in changes.StateChanges)
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

        /*
        foreach (SupportChange<Player, Region> mediaChange in changes.MediaSupportChanges)
        {
            if (mediaChange.Change > 0)
            {
                MediaSupportComponent. GainMediaSupportWithoutSupportCheck(mediaChange.Player, mediaChange.Target, mediaChange.Change);
            }
            else
            {
                LoseMediaSupport(mediaChange.Player, mediaChange.Target, Math.Abs(mediaChange.Change));
            }               
        }

        foreach (SupportChange<Player, Region> endorsementChange in changes.EndorsementChanges)
        {
            GainEndorsement(endorsementChange.Player, endorsementChange.Target, endorsementChange.Change);
        }
*/
        if (changes.NewIssuesOrder.Count > 0)
        {
            IssuePositioningComponent.SetSubjectOrder(changes.NewIssuesOrder);
        }


    }

    //public void ImplementChanges(PlayerChosenChanges<TPlayer, TIssue, TState, TRegion> changes)
}
//GenericPresidentialGameEngine<Player, Leader, Issue, State, Region, Card>(componentCollection);

public interface IEngine<TPlayer, TLeader, TIssue, TState, TRegion>
    where TPlayer : Enum
    where TLeader : Enum
    where TIssue : Enum
    where TState : Enum
    where TRegion : Enum
{
    GameState<TPlayer, TLeader, TIssue, TState, TRegion> GetGameState();

    public void ImplementChanges(PlayerChosenChanges<TPlayer, TIssue, TState, TRegion> changes);

}
