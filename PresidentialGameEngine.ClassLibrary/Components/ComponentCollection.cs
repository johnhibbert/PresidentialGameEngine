using PresidentialGameEngine.ClassLibrary.Interfaces;
using System.Reflection;

namespace PresidentialGameEngine.ClassLibrary.Components
{
    public class ComponentCollection<PlayersEnum, LeadersEnum, IssuesEnum, StatesEnum, RegionsEnum, CardClass>()
        : IComponentCollection<PlayersEnum, LeadersEnum, IssuesEnum, StatesEnum, RegionsEnum, CardClass>
        where PlayersEnum : Enum
        where LeadersEnum : Enum
        where IssuesEnum : Enum
        where StatesEnum : Enum
        where RegionsEnum : Enum
        where CardClass : class
    {
        public IAccumulatingComponent<PlayersEnum>? MomentumComponent { get; set; }
        public ISupportComponent<PlayersEnum, LeadersEnum, IssuesEnum>? IssueSupportComponent { get; set; }
        public ICarriableSupportComponent<PlayersEnum, LeadersEnum, StatesEnum>? StateSupportComponent { get; set; }
        public IPositioningComponent<IssuesEnum>? IssuePositioningComponent { get; set; }
        public IPoliticalCapitalComponent<PlayersEnum>? PoliticalCapitalComponent { get; set; }
        public IPlayerLocationComponent<PlayersEnum, StatesEnum>? PlayerLocationComponent { get; set; }
        public IAccumulatingComponent<PlayersEnum>? RestComponent { get; set; }
        public ISupportComponent<PlayersEnum, LeadersEnum, RegionsEnum>? EndorsementComponent { get; set; }
        public ISupportComponent<PlayersEnum, LeadersEnum, RegionsEnum>? MediaSupportComponent { get; set; }
        public IExhaustionComponent<PlayersEnum>? ExhaustionComponent { get; set; }
        public ICardComponent<PlayersEnum, CardClass>? CardComponent { get; set; }
        public IStaticDataComponent<StatesEnum, PlayersEnum, RegionsEnum>? StaticDataComponent { get; set; }

        public bool IsReady()
        {
            //A slightly overengineered way to do this,
            //but it will work automatically when we add more props
            //Modified from
            //https://stackoverflow.com/questions/22683040/how-to-check-whether-all-properties-of-an-object-are-null-or-empty

            foreach (PropertyInfo pi in GetType().GetProperties())
            {
                object? value = pi.GetValue(this);
                if (value is null)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
