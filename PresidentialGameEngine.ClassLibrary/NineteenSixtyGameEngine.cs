using PresidentialGameEngine.ClassLibrary.Data;
using PresidentialGameEngine.ClassLibrary.Enums;
using PresidentialGameEngine.ClassLibrary.Interfaces;
using System.Runtime;

namespace PresidentialGameEngine.ClassLibrary
{

    public class NineteenSixtyGameEngine : GenericPresidentialGameEngine<Player, Leader, Issue, State, Region>
    {
        public NineteenSixtyGameEngine
            (IMomentumComponent<Player> momentumComponent,
            ISupportComponent<Player, Leader, Issue> issueSupportComponent,
            ISupportComponent<Player, Leader, State> stateSupportComponent,
            IPositioningComponent<Issue> issuePositioningComponent,
            IPoliticalCapitalComponent<Player> politicalCapitalComponent,
            IRegionalComponent<State, Region, Player> regionalComponent
            )
            : base(momentumComponent, issueSupportComponent, stateSupportComponent,
                  issuePositioningComponent, politicalCapitalComponent, regionalComponent)
        {
        }
    }


    //We don't want to directly inherit SupportComponent because that's doing double duty.
    //So direct inheritance isn't really doing a ton in this particular spot except for mild clarity?
    public class GenericPresidentialGameEngine<PlayersEnum, LeadersEnum, IssuesEnum, StatesEnum, RegionsEnum>
       where PlayersEnum : Enum
        where LeadersEnum : Enum
        where IssuesEnum : Enum
        where StatesEnum : Enum
        where RegionsEnum : Enum
    {

        readonly IMomentumComponent<PlayersEnum> MomentumComponent;
        readonly ISupportComponent<PlayersEnum, LeadersEnum, IssuesEnum> IssueSupportComponent;
        readonly ISupportComponent<PlayersEnum, LeadersEnum, StatesEnum> StateSupportComponent;
        readonly IPositioningComponent<IssuesEnum> IssuePositioningComponent;
        readonly IPoliticalCapitalComponent<PlayersEnum> PoliticalCapitalComponent;
        readonly IRegionalComponent<StatesEnum, RegionsEnum, PlayersEnum> RegionalComponent;

        public GenericPresidentialGameEngine(
            IMomentumComponent<PlayersEnum> momentumComponent,
            ISupportComponent<PlayersEnum, LeadersEnum, IssuesEnum> issueSupportComponent,
            ISupportComponent<PlayersEnum, LeadersEnum, StatesEnum> stateSupportComponent,
            IPositioningComponent<IssuesEnum> issuePositioningComponent,
            IPoliticalCapitalComponent<PlayersEnum> politicalCapitalComponent,
            IRegionalComponent<StatesEnum, RegionsEnum, PlayersEnum> regionalComponent
            )
        {
            MomentumComponent = momentumComponent;
            IssueSupportComponent = issueSupportComponent;
            StateSupportComponent = stateSupportComponent;
            IssuePositioningComponent = issuePositioningComponent;
            PoliticalCapitalComponent = politicalCapitalComponent;
            RegionalComponent = regionalComponent;
        }

        public IssuesEnum[] IssueOrder
        {
            get { return IssuePositioningComponent.GetSubjectOrder.ToArray(); }
        }

        public void GainMomentum(PlayersEnum player, int amount)
        {
            MomentumComponent.GainMomentum(player, amount);
        }

        public int GetPlayerMomentum(PlayersEnum player)
        {
            return MomentumComponent.GetPlayerMomentum(player);
        }

        public void LoseMomentum(PlayersEnum player, int amount)
        {
            MomentumComponent.LoseMomentum(player, amount);
        }

        public void GainSupport(PlayersEnum player, IssuesEnum issue, int amount)
        {
            IssueSupportComponent.GainSupport(player, issue, amount);
        }

        public void LoseSupport(PlayersEnum player, IssuesEnum issue, int amount)
        {
            IssueSupportComponent.LoseSupport(player, issue, amount);
        }

        public void GainSupport(PlayersEnum player, StatesEnum state, int amount)
        {
            StateSupportComponent.GainSupport(player, state, amount);
        }

        public void LoseSupport(PlayersEnum player, StatesEnum state, int amount)
        {
            StateSupportComponent.LoseSupport(player, state, amount);
        }

        public void SetIssueOrder(IEnumerable<IssuesEnum> orderedIssues)
        {
            IssuePositioningComponent.SetSubjectOrder(orderedIssues);
        }

        public void MoveIssueUp(IssuesEnum issue)
        {
            IssuePositioningComponent.MoveSubjectUp(issue);
        }

        public LeadersEnum GetLeader(IssuesEnum issue)
        {
            return IssueSupportComponent.GetLeader(issue);
        }

        public LeadersEnum GetLeader(StatesEnum state)
        {
            return StateSupportComponent.GetLeader(state);
        }

        public int GetSupportAmount(IssuesEnum issue) 
        {
            return IssueSupportComponent.GetSupportAmount(issue);
        }

        public int GetSupportAmount(StatesEnum state)
        {
            return StateSupportComponent.GetSupportAmount(state);
        }

        public PlayersEnum InitiativeCheck()
        {
            return PoliticalCapitalComponent.InitiativeCheck();
        }

        public int SupportCheck(PlayersEnum player, int checkAmount)
        {
            return PoliticalCapitalComponent.SupportCheck(player, checkAmount);

        }

        public void AddCubes(PlayersEnum player, int amount)
        {
            PoliticalCapitalComponent.AddCubes(player, amount);
        }


        public void ImplementChanges(PlayerChosenChanges<PlayersEnum, IssuesEnum, StatesEnum> changes)
        {
            foreach (SupportChange<PlayersEnum, IssuesEnum> issueChange in changes.IssueChanges)
            {
                GainSupport(issueChange.Player, issueChange.Target, issueChange.Change);
            }

            foreach (SupportChange<PlayersEnum, StatesEnum> stateChange in changes.StateChanges)
            {
                GainSupport(stateChange.Player, stateChange.Target, stateChange.Change);
            }
        }

        public RegionsEnum GetRegion(StatesEnum state) 
        {
            return RegionalComponent.GetRegionByState(state);
        }

        public IEnumerable<StatesEnum> GetStatesInRegion(RegionsEnum region)
        {
            return RegionalComponent.GetStatesWithinRegion(region);
        }

        public StatesEnum GetPlayerState(PlayersEnum player)
        {
            return RegionalComponent.GetPlayerState(player);
        }

        public void MovePlayerToState(PlayersEnum player, StatesEnum states)
        {
            RegionalComponent.MovePlayerToState(player, states);
        }

    }
}
