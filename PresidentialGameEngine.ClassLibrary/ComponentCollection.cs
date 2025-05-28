using PresidentialGameEngine.ClassLibrary.Interfaces;
using System.Reflection;

namespace PresidentialGameEngine.ClassLibrary
{
    public class ComponentCollection<PlayersEnum, LeadersEnum, IssuesEnum, StatesEnum, RegionsEnum>()
        where PlayersEnum : Enum
        where LeadersEnum : Enum
        where IssuesEnum : Enum
        where StatesEnum : Enum
        where RegionsEnum : Enum
    {
        public bool IsReady() 
        {
            //A slightly overengineered way to do this,
            //but it will work automatically when we add more props
            //Modified from
            //https://stackoverflow.com/questions/22683040/how-to-check-whether-all-properties-of-an-object-are-null-or-empty

            foreach (PropertyInfo pi in this.GetType().GetProperties())
            {
                object value = pi.GetValue(this);
                if (value is null)
                {
                    return false;
                }
           }
            return true;
        }

        public IAccumulatingComponent<PlayersEnum>? MomentumComponent { get; set; }
        public ISupportComponent<PlayersEnum, LeadersEnum, IssuesEnum>? IssueSupportComponent { get; set; }
        public ISupportComponent<PlayersEnum, LeadersEnum, StatesEnum>? StateSupportComponent { get; set; }
        public IPositioningComponent<IssuesEnum>? IssuePositioningComponent { get; set; }
        public IPoliticalCapitalComponent<PlayersEnum>? PoliticalCapitalComponent { get; set; }
        public IRegionalComponent<StatesEnum, RegionsEnum, PlayersEnum>? RegionalComponent { get; set; }
        public IAccumulatingComponent<PlayersEnum>? RestComponent { get; set; }
    }
}
