using PresidentialGameEngine.ClassLibrary.Data;
using PresidentialGameEngine.ClassLibrary.Enums;
using PresidentialGameEngine.ClassLibrary.Interfaces;

namespace PresidentialGameEngine.ClassLibrary
{
    public class NineteenSixtyGameEngine
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

            int i = 0;

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

        public int NixonMomentum { get; private set; }

        public int KennedyMomentum { get; private set; }

        public int GetPlayerMomentum(Player player)
        {
            switch (player)
            {
                case Player.Kennedy:
                    return KennedyMomentum;
                default:
                    return NixonMomentum;

            }
        }

        public void GainMomentum(Player player, int amount)
        {
            switch (player)
            {
                case Player.Nixon:
                    NixonMomentum += amount;
                    break;
                case Player.Kennedy:
                    KennedyMomentum += amount;
                    break;
            }
        }

        public void LoseMomentum(Player player, int amount)
        {
            switch (player)
            {
                case Player.Nixon:
                    NixonMomentum -= amount;
                    if (NixonMomentum < 0)
                    {
                        NixonMomentum = 0;
                    }
                    break;
                case Player.Kennedy:
                    KennedyMomentum -= amount;
                    if (KennedyMomentum < 0)
                    {
                        KennedyMomentum = 0;
                    }
                    break;
            }
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

}
