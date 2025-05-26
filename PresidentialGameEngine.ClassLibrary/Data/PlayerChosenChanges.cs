using PresidentialGameEngine.ClassLibrary.Enums;

namespace PresidentialGameEngine.ClassLibrary.Data
{
    public class PlayerChosenChanges<PlayersEnum, IssuesEnum, StatesEnum>
        where PlayersEnum : Enum
        where IssuesEnum : Enum
        where StatesEnum : Enum
    {
        public List<SupportChange<PlayersEnum, IssuesEnum>> IssueChanges { get; internal set; }
        public List<SupportChange<PlayersEnum, StatesEnum>> StateChanges { get; internal set; }

        public PlayerChosenChanges()
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

        public bool ContainsOnlyTheseChangeTypes(List<ChangeType> changeTypes)
        {
            bool returnValue = true;

            foreach (ChangeType changeType in Enum.GetValues(typeof(ChangeType)))
            {
                bool included = changeTypes.Contains(changeType);

                //What are we doing here?
                //We want true if both are true or both are false
                // ^ is the XOR operator, so it's false if both are true or both are false.
                //So we invert that with the ! operator.
                switch (changeType)
                {
                    case ChangeType.IssueSupport:
                        returnValue = returnValue && !(included ^ IssueChanges.Count != 0);
                        break;
                    case ChangeType.StateSupport:
                        returnValue = returnValue && !(included ^ StateChanges.Count != 0);
                        break;

                }

            }

            return returnValue;
        }


    }

}


