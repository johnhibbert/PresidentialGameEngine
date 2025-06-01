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


    //We could change this to be just Game State Changes or something, unsure if that's more clear
    public class NewMasterPlayerChosenChanges<PlayersEnum, IssuesEnum, StatesEnum, RegionsEnum>
     where PlayersEnum : Enum
     where IssuesEnum : Enum
     where StatesEnum : Enum
     where RegionsEnum : Enum
    {

        //This is going to be a mess.
        public List<SupportChange<PlayersEnum, IssuesEnum>> IssueChanges { get; internal set; }
        public List<SupportChange<PlayersEnum, StatesEnum>> StateChanges { get; internal set; }
        public List<SupportChange<PlayersEnum, RegionsEnum>> EndorsementChanges { get; internal set; }
        public List<SupportChange<PlayersEnum, RegionsEnum>> MediaSupportChanges { get; internal set; }



        public NewMasterPlayerChosenChanges()
        {
            IssueChanges = [];
            StateChanges = [];
            EndorsementChanges = [];
            MediaSupportChanges = [];
        }

        public int TotalIssueChanges
        {
            get { return IssueChanges.Select(x => x.Change).Sum(); }

        }
        public int HighestIssueChange
        {
            get { return IssueChanges.Max(x => x.Change); }
        }

        public int TotalStateChanges
        {
            get { return StateChanges.Select(x => x.Change).Sum(); }

        }
        public int HighestStateChange
        {
            get { return StateChanges.Max(x => x.Change); }
        }

        public int TotalEndorsementChanges
        {
            get { return EndorsementChanges.Select(x => x.Change).Sum(); }

        }
        public int HighestEndorsementChange
        {
            get { return EndorsementChanges.Max(x => x.Change); }
        }

        public int TotalMediaChanges
        {
            get { return MediaSupportChanges.Select(x => x.Change).Sum(); }

        }
        public int HighestMediaChange
        {
            get { return MediaSupportChanges.Max(x => x.Change); }
        }

        //The existing game does not appear to ever require
        //More than one type of change from players.
        public bool ContainsExactlyOneTypeOfChange() 
        {
            var counter = 0;

            if(TotalIssueChanges > 0) 
            {
                counter++;
            }
            if (TotalStateChanges > 0)
            {
                counter++;
            }
            if (TotalEndorsementChanges > 0)
            {
                counter++;
            }
            if (TotalMediaChanges >0)
            {
                counter++;
            }

            return counter == 1;
        }
    }


}


