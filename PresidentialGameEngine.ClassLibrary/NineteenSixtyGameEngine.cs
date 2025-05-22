using PresidentialGameEngine.ClassLibrary.Enums;

namespace PresidentialGameEngine.ClassLibrary
{
    public class NineteenSixtyGameEngine
    {
        public NineteenSixtyGameEngine()
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
            foreach(State state in ((State[])Enum.GetValues(typeof(State))).Where(s => s != State.None)) 
            {
                SupportInStates.Add(state, new SupportContest());
            }

            int i = 0;

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

        public Player GetIssueLeader(Issue issue) 
        {
            return SupportOnIssues[issue].SupportedPlayer;
        }

        public int GetIssueSupportAmount(Issue issue) 
        {
            return SupportOnIssues[issue].SupportAmount;
        }

    }




    //Rename me? This represents both states and issues
    public class SupportContest()
    {
        public int SupportAmount { get; set; }
        public Player SupportedPlayer { get; set; }

        public void GainSupport(Player player, int amount)
        {
            if(SupportAmount == 0 && SupportedPlayer == Player.None) 
            {
                SupportAmount = amount;
                SupportedPlayer = player;
            }
            else if (player == SupportedPlayer)
            {
                SupportAmount += amount;
            }
            else
            {
                if (amount > SupportAmount)
                {
                    SupportedPlayer = SupportedPlayer.ToOpponent();
                    SupportAmount = Math.Abs(SupportAmount - amount);
                }
                else if (amount < SupportAmount)
                {
                    SupportAmount -= amount;
                }
                else
                {
                    SupportedPlayer = Player.None;
                    SupportAmount = 0;
                }
            }
        }

        public void LoseSupport(Player player, int amount)
        {
            if (player == SupportedPlayer)
            {
                SupportAmount -= amount;
                if (SupportAmount <= 0)
                {
                    SupportedPlayer = Player.None;
                    SupportAmount = 0;
                }
            }
        }

    }

}
