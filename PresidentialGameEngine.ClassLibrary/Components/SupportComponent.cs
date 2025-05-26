using PresidentialGameEngine.ClassLibrary.Data;
using PresidentialGameEngine.ClassLibrary.Enums;
using PresidentialGameEngine.ClassLibrary.Interfaces;

namespace PresidentialGameEngine.ClassLibrary.Components
{
    public class SupportComponent<PlayersEnum, LeadersEnum, SubjectEnum> :
        ISupportComponent<PlayersEnum, LeadersEnum, SubjectEnum>
        where PlayersEnum : Enum
        where LeadersEnum : Enum
        where SubjectEnum : Enum
         
    {
        private readonly LeadersEnum defaultLeader = (LeadersEnum) Enum.ToObject(typeof(LeadersEnum), 0);

        public Dictionary<SubjectEnum, NewSupportStatus<LeadersEnum>> SubjectContests { get; init; }

        public SupportComponent()
        {
            SubjectContests = [];

            var subjectValues = Enum.GetValues(typeof(SubjectEnum)).OfType<SubjectEnum>().ToList();
            var valueOfNone = (SubjectEnum)Enum.ToObject(typeof(SubjectEnum), 0);
            subjectValues.Remove(valueOfNone);

            foreach (SubjectEnum subject in subjectValues)
            {
                SubjectContests.Add(subject, new NewSupportStatus<LeadersEnum>());
            }
        }

        public void GainSupport(PlayersEnum player, SubjectEnum state, int amount)
        {
            var stateContest = SubjectContests[state];

            var playerAsInt = Convert.ToInt32(player);
            var leaderAsInt = Convert.ToInt32(stateContest.Leader);

            if (playerAsInt == leaderAsInt) 
            {
                stateContest.Amount += amount;
            }
            else if(leaderAsInt != 0 && amount == stateContest.Amount) 
            {
                stateContest.Amount = 0;
                stateContest.Leader = defaultLeader;
            }
            else 
            {
                if(amount < stateContest.Amount) 
                {
                    stateContest.Amount -= amount;
                }
                else 
                {
                    stateContest.Amount += amount;
                    stateContest.Leader = (LeadersEnum)Enum.ToObject(typeof(LeadersEnum), playerAsInt);
                }
            }
        }

        public void LoseSupport(PlayersEnum player, SubjectEnum state, int amount)
        {
            var targetContest = SubjectContests[state];

            var playerAsInt = Convert.ToInt32(player);
            var leaderAsInt = Convert.ToInt32(targetContest.Leader);

            if(playerAsInt == leaderAsInt) 
            {
                targetContest.Amount -= amount;
                if (targetContest.Amount <= 0)
                {
                    targetContest.Leader = defaultLeader;
                    targetContest.Amount = 0;
                }
            }
        }
    }

    public class NewSupportStatus<LeadersEnum> where LeadersEnum : Enum
    {
        public int Amount { get; set; }
        public LeadersEnum Leader { get; set; }

        public NewSupportStatus()
        {
            Leader = (LeadersEnum)Enum.ToObject(typeof(LeadersEnum), 0);
        }
    }

    public class NewSupportChange<PlayersEnum, TargetEnum>(PlayersEnum player, TargetEnum target, int change)
        where PlayersEnum : Enum 
        where TargetEnum : Enum
    {
        public PlayersEnum Player { get; init; } = player;

        public TargetEnum Target { get; init; } = target;

        public int Change { get; init; } = change;
    }

    public class NewPlayerChosenChanges<PlayersEnum, IssuesEnum, StatesEnum>
        where PlayersEnum : Enum
        where IssuesEnum : Enum
        where StatesEnum : Enum
    {
        public List<NewSupportChange<PlayersEnum, IssuesEnum>> IssueChanges { get; internal set; }
        public List<NewSupportChange<PlayersEnum, StatesEnum>> StateChanges { get; internal set; }

        public NewPlayerChosenChanges()
        {
            IssueChanges = [];
            StateChanges = [];
        }
        public int TotalIssueChanges
        {
            get { return IssueChanges.Select(x => x.Change).Sum(); }

        }
        public int TotalStateChanges
        {
            get { return StateChanges.Select(x => x.Change).Sum(); }

        }

        public int HighestStateChange
        {
            get { return StateChanges.Max(x => x.Change); }
        }

        public bool ContainsOnlyTheseChangeTypes(List<NewChangeType> changeTypes)
        {
            bool returnValue = true;

            foreach (NewChangeType changeType in Enum.GetValues(typeof(NewChangeType)))
            {
                bool included = changeTypes.Contains(changeType);

                //What are we doing here?
                //We want true if both are true or both are false
                // ^ is the XOR operator, so it's false if both are true or both are false.
                //So we invert that with the ! operator.
                switch (changeType)
                {
                    case NewChangeType.IssueSupport:
                        returnValue = returnValue && !(included ^ IssueChanges.Count != 0);
                        break;
                    case NewChangeType.StateSupport:
                        returnValue = returnValue && !(included ^ StateChanges.Count != 0);
                        break;

                }

            }

            return returnValue;
        }
        

    }

    public enum NewChangeType
    {
        IssueSupport,
        StateSupport,
    }

}


