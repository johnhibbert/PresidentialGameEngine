using PresidentialGameEngine.ClassLibrary.Interfaces;
using System.Reflection;

namespace PresidentialGameEngine.ClassLibrary.Components
{
    public class ComponentCollection<TPlayer, TLeader, TIssue, TState, TRegion, TCard>()
        : IComponentCollection<TPlayer, TLeader, TIssue, TState, TRegion, TCard>
        where TPlayer : Enum
        where TLeader : Enum
        where TIssue : Enum
        where TState : Enum
        where TRegion : Enum
        where TCard : ICard
    {
        public IAccumulatingComponent<TPlayer>? MomentumComponent { get; set; }
        public ISupportComponent<TPlayer, TLeader, TIssue>? IssueSupportComponent { get; set; }
        public ICarriableSupportComponent<TPlayer, TLeader, TState>? StateSupportComponent { get; set; }
        public IPositioningComponent<TIssue>? IssuePositioningComponent { get; set; }
        public IPoliticalCapitalComponent<TPlayer>? PoliticalCapitalComponent { get; set; }
        public IPlayerLocationComponent<TPlayer, TState>? PlayerLocationComponent { get; set; }
        public IAccumulatingComponent<TPlayer>? RestComponent { get; set; }
        public ISupportComponent<TPlayer, TLeader, TRegion>? EndorsementComponent { get; set; }
        public ISupportComponent<TPlayer, TLeader, TRegion>? MediaSupportComponent { get; set; }
        public IExhaustionComponent<TPlayer>? ExhaustionComponent { get; set; }
        public ICardComponent<TPlayer, TCard>? CardComponent { get; set; }
        public IStaticDataComponent<TState, TPlayer, TRegion>? StaticDataComponent { get; set; }

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
