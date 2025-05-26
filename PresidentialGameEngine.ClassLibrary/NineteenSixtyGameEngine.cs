using PresidentialGameEngine.ClassLibrary.Components;
using PresidentialGameEngine.ClassLibrary.Data;
using PresidentialGameEngine.ClassLibrary.Enums;
using PresidentialGameEngine.ClassLibrary.Interfaces;
using System.Runtime;

namespace PresidentialGameEngine.ClassLibrary
{

    public class NineteenSixtyGameEngine : GenericPresidentialGameEngine<Player, Leader, Issue, State>
    {
        public NineteenSixtyGameEngine(IMomentumComponent<Player> momentumComponent, ISupportComponent<Player, Leader, Issue> issueSupportComponent, ISupportComponent<Player, Leader, State> stateSupportComponent, IPositioningComponent<Issue> issuePositioningComponent, IPoliticalCapitalComponent<Player> politicalCapitalComponent) : base(momentumComponent, issueSupportComponent, stateSupportComponent, issuePositioningComponent, politicalCapitalComponent)
        {
        }
    }


    //We don't want to directly inherit SupportComponent because that's doing double duty.
    //So direct inheritance isn't really doing a ton in this particular spot except for mild clarity?
    public class GenericPresidentialGameEngine<PlayersEnum, LeadersEnum, IssuesEnum, StatesEnum>
       where PlayersEnum : Enum
        where LeadersEnum : Enum
        where IssuesEnum : Enum
        where StatesEnum : Enum

    {

        readonly IMomentumComponent<PlayersEnum> MomentumComponent;
        readonly ISupportComponent<PlayersEnum, LeadersEnum, IssuesEnum> IssueSupportComponent;
        readonly ISupportComponent<PlayersEnum, LeadersEnum, StatesEnum> StateSupportComponent;
        readonly IPositioningComponent<IssuesEnum> IssuePositioningComponent;
        readonly IPoliticalCapitalComponent<PlayersEnum> PoliticalCapitalComponent;

        public GenericPresidentialGameEngine(
            IMomentumComponent<PlayersEnum> momentumComponent,
            ISupportComponent<PlayersEnum, LeadersEnum, IssuesEnum> issueSupportComponent,
            ISupportComponent<PlayersEnum, LeadersEnum, StatesEnum> stateSupportComponent,
            IPositioningComponent<IssuesEnum> issuePositioningComponent,
            IPoliticalCapitalComponent<PlayersEnum> politicalCapitalComponent
            )
        {
            MomentumComponent = momentumComponent;
            IssueSupportComponent = issueSupportComponent;
            StateSupportComponent = stateSupportComponent;
            IssuePositioningComponent = issuePositioningComponent;
            PoliticalCapitalComponent = politicalCapitalComponent;
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


        public void ImplementChanges(NewPlayerChosenChanges<PlayersEnum, IssuesEnum, StatesEnum> changes)
        {
            foreach (NewSupportChange<PlayersEnum, IssuesEnum> issueChange in changes.IssueChanges)
            {
                GainSupport(issueChange.Player, issueChange.Target, issueChange.Change);
            }

            foreach (NewSupportChange<PlayersEnum, StatesEnum> stateChange in changes.StateChanges)
            {
                GainSupport(stateChange.Player, stateChange.Target, stateChange.Change);
            }
        }

    }
    /*
    public class NineteenSixtyGameEngine: MomentumComponent<Player>
    {
        public NineteenSixtyGameEngine(IRandomnessProvider randomnessProvider)
        {
            SupportOnIssues = new Dictionary<Issue, SupportContest>
            {
                {Issue.Defense, new SupportContest() },
                {Issue.Economy, new SupportContest() },
                {Issue.CivilRights, new SupportContest() }
            };

            IssueOrder =
            [
                Issue.Defense,
                Issue.Economy,
                Issue.CivilRights
            ];

            SupportInStates = [];
            foreach (State state in ((State[])Enum.GetValues(typeof(State))).Where(s => s != State.None))
            {
                SupportInStates.Add(state, new SupportContest());
            }

            PoliticalCapitalBag = new PoliticalCapitalBag<Player>(randomnessProvider, 12);
        }

        public void ImplementChanges(PlayerChosenChanges changes) 
        {
            foreach (SupportChange<Issue> issueChange in changes.IssueChanges) 
            {
                GainIssueSupport(issueChange.Player, issueChange.Target, issueChange.Change);
            }

            foreach (SupportChange<State> stateChange in changes.StateChanges) 
            {
                GainStateSupport(stateChange.Player, stateChange.Target, stateChange.Change);
            }
        }

        PoliticalCapitalBag<Player> PoliticalCapitalBag;

        public Player ConductInitiativeCheck() 
        {
            return PoliticalCapitalBag.InitiativeCheck();
        }

        public int ConductSupportCheck(Player player, int amount) 
        {
            return PoliticalCapitalBag.SupportCheck(player, amount);
        }

        public Dictionary<State, SupportContest> SupportInStates { get; private set; }

        public void GainStateSupport(Player player, State state, int amount)
        {
            SupportInStates[state].GainSupport(player, amount);
        }

        public void LoseStateSupport(Player player, State state, int amount)
        {
            SupportInStates[state].LoseSupport(player, amount);
        }

        //Possibly rename me?
        public Dictionary<Issue, SupportContest> SupportOnIssues { get; private set; }

        public void GainIssueSupport(Player player, Issue issue, int amount)
        {
            SupportOnIssues[issue].GainSupport(player, amount);
        }

        public void LoseIssueSupport(Player player, Issue issue, int amount)
        {
            SupportOnIssues[issue].LoseSupport(player, amount);
        }

        public List<Issue> IssueOrder { get; private set; }


        public void SetIssueOrder(Issue topIssue, Issue middleIssue, Issue bottomIssue)
        {
            IssueOrder = [topIssue, middleIssue, bottomIssue];
        }

        //https://stackoverflow.com/questions/450233/generic-list-moving-an-item-within-the-list
        public void MoveIssueUp(Issue issue)
        {
            var itemIndex = IssueOrder.IndexOf(issue);
            if (itemIndex > 0)
            {
                var item = IssueOrder[itemIndex];
                IssueOrder.Remove(item);
                IssueOrder.Insert(itemIndex - 1, item);
            }
        }

        public Leader GetIssueLeader(Issue issue)
        {
            return SupportOnIssues[issue].SupportStatus.Player;
        }

        public SupportStatus GetIssueSupportStatus(Issue issue)
        {
            return SupportOnIssues[issue].SupportStatus;
        }

    }


    //Rename me? This represents both states and issues
    public class SupportContest
    {
        public SupportContest()
        {
            SupportStatus = new SupportStatus();
        }

        public SupportStatus SupportStatus { get; internal set; }

        public void GainSupport(Player player, int amount)
        {
            var playerAsLeader = player.ToLeader();

            if (SupportStatus.Amount == 0 && SupportStatus.Player == Leader.None)
            {
                SupportStatus.Amount = amount;
                SupportStatus.Player = playerAsLeader;
            }
            else if (playerAsLeader == SupportStatus.Player)
            {
                SupportStatus.Amount += amount;
            }
            else
            {
                if (amount > SupportStatus.Amount)
                {
                    SupportStatus.Player = SupportStatus.Player.ToOpponent();
                    SupportStatus.Amount = Math.Abs(SupportStatus.Amount - amount);
                }
                else if (amount < SupportStatus.Amount)
                {
                    SupportStatus.Amount -= amount;
                }
                else
                {
                    SupportStatus.Player = Leader.None;
                    SupportStatus.Amount = 0;
                }
            }
        }
        public void LoseSupport(Player player, int amount)
        {
            var playerAsLeader = player.ToLeader();

            if (playerAsLeader == SupportStatus.Player)
            {
                SupportStatus.Amount -= amount;
                if (SupportStatus.Amount <= 0)
                {
                    SupportStatus.Player = Leader.None;
                    SupportStatus.Amount = 0;
                }
            }
        }

    }
    */
}
