using PresidentialGameEngine.ClassLibrary.Enums;

namespace PresidentialGameEngine.ClassLibrary.Data
{
    public class BatteryOfChanges
    {
        public BatteryOfChanges()
        {
            IssueChanges = [];
            StateChanges = [];
        }

        public List<SupportChange<Issue>> IssueChanges { get; internal set; }
        public List<SupportChange<State>> StateChanges { get; internal set; }
        
    }

}
