namespace PresidentialGameEngine.ClassLibrary.Data
{
    //We could change this to be just Game State Changes or something, unsure if that's more clear
    public class PlayerChosenChanges<PlayersEnum, IssuesEnum, StatesEnum, RegionsEnum>
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

        public List<IssuesEnum> NewIssuesOrder { get; internal set; }



        public PlayerChosenChanges()
        {
            IssueChanges = [];
            StateChanges = [];
            EndorsementChanges = [];
            MediaSupportChanges = [];
            NewIssuesOrder = [];
        }

        public int TotalIssueChanges
        {
            get { return IssueChanges.Select(x => x.Change).Sum(); }

        }
        public int HighestIssueChange
        {
            get 
            { 
                if(IssueChanges.Count == 0) 
                {
                    return 0;
                }
                else return IssueChanges.Max(x => x.Change);
            }
        }

        public int TotalStateChanges
        {
            get { return StateChanges.Select(x => x.Change).Sum(); }

        }
        public int HighestStateChange
        {
            get 
            {
                if (StateChanges.Count == 0)
                {
                    return 0;
                }
                else return StateChanges.Max(x => x.Change);
            }
        }

        public int TotalEndorsementChanges
        {
            get { return EndorsementChanges.Select(x => x.Change).Sum(); }

        }
        public int HighestEndorsementChange
        {
            get 
            {
                if (EndorsementChanges.Count == 0)
                {
                    return 0;
                }
                else return EndorsementChanges.Max(x => x.Change);
            }
        }

        public int TotalMediaChanges
        {
            get { return MediaSupportChanges.Select(x => x.Change).Sum(); }

        }
        public int HighestMediaChange
        {
            get 
            {
                if (MediaSupportChanges.Count == 0)
                {
                    return 0;
                }
                else return MediaSupportChanges.Max(x => x.Change);
            }
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
            if (NewIssuesOrder.Count > 0)
            {
                counter++;
            }

            return counter == 1;
        }
    }


}


