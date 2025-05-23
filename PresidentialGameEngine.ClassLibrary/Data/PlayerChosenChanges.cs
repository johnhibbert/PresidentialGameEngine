using PresidentialGameEngine.ClassLibrary.Enums;

namespace PresidentialGameEngine.ClassLibrary.Data
{
    public class PlayerChosenChanges
    {
        public PlayerChosenChanges()
        {
            IssueChanges = [];
            StateChanges = [];
        }

        public List<SupportChange<Issue>> IssueChanges { get; internal set; }
        public List<SupportChange<State>> StateChanges { get; internal set; }
        
    }

}
