namespace PresidentialGameEngine.ClassLibrary.Data
{
    //We could change this to be just Game State Changes or something, unsure if that's more clear
    public class PlayerChosenChanges<TPlayer, TIssue, TState, TRegion>
     where TPlayer : Enum
     where TIssue : Enum
     where TState : Enum
     where TRegion : Enum
    {

        //This is going to be a mess.
        public List<SupportChange<TPlayer, TIssue>> IssueChanges { get; internal set; }
        public List<SupportChange<TPlayer, TState>> StateChanges { get; internal set; }
        public List<SupportChange<TPlayer, TRegion>> EndorsementChanges { get; internal set; }
        public List<SupportChange<TPlayer, TRegion>> MediaSupportChanges { get; internal set; }

        //not internal set because we are not adding to a list, but providing a list.
        //This should probably change.
        public List<TIssue> NewIssuesOrder { get; set; }



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
        
        public bool ContainsOnlyExactlyTheseChangeTypes(IEnumerable<ChangeType> changeTypes)
        {
            var givenTypesAsArray = changeTypes as ChangeType[] ?? changeTypes.ToArray();
            
            var issueChangeTypeExpected = givenTypesAsArray.Any(x => x == ChangeType.IssueSupport);
            var stateChangeTypeExpected = givenTypesAsArray.Any(x => x == ChangeType.StateSupport);
            var endorsementTypeExpected = givenTypesAsArray.Any(x => x == ChangeType.Endorsement);
            var mediaSupportTypeExpected = givenTypesAsArray.Any(x => x == ChangeType.MediaSupport);
            var newIssueOrderTypeExpected = givenTypesAsArray.Any(x => x == ChangeType.NewIssueOrder);
            
            var hasIssueChanges = TotalIssueChanges > 0;
            var hasStateChanges = TotalStateChanges > 0;
            var hasEndorsementChanges = TotalEndorsementChanges > 0;
            var hasMediaChanges = TotalMediaChanges > 0;
            var hasNewIssuesOrder = NewIssuesOrder.Count > 0;

            var issueChangeMatch = !(hasIssueChanges ^ issueChangeTypeExpected);
            var stateChangeMatch = !(hasStateChanges ^ stateChangeTypeExpected);
            var endorsementChangeMatch = !(hasEndorsementChanges ^ endorsementTypeExpected);
            var mediaSupportChangeMatch = !(hasMediaChanges ^ mediaSupportTypeExpected);
            var newIssueOrderMatch = !(hasNewIssuesOrder ^ newIssueOrderTypeExpected);

            return issueChangeMatch && stateChangeMatch && endorsementChangeMatch && mediaSupportChangeMatch &&
                   newIssueOrderMatch;
            
        }
    }



}

public enum ChangeType
{
        
    StateSupport,
    IssueSupport,
    MediaSupport,
    Endorsement,
    NewIssueOrder,
        
}

