using PresidentialGameEngine.ClassLibrary.Components;

namespace PresidentialGameEngine.ClassLibrary.Interfaces
{
    public interface IComponentCollection<TPlayer, TLeader, TIssue, TState, TRegion, TCard>
        where TPlayer : Enum
        where TLeader : Enum
        where TIssue : Enum
        where TState : Enum
        where TRegion : Enum
        where TCard : ICard
    {
        ISupportComponent<TPlayer, TLeader, TRegion>? EndorsementComponent { get; set; }
        IPositioningComponent<TIssue>? IssuePositioningComponent { get; set; }
        ISupportComponent<TPlayer, TLeader, TIssue>? IssueSupportComponent { get; set; }
        ISupportComponent<TPlayer, TLeader, TRegion>? MediaSupportComponent { get; set; }
        IAccumulatingComponent<TPlayer>? MomentumComponent { get; set; }
        IPoliticalCapitalComponent<TPlayer>? PoliticalCapitalComponent { get; set; }
        IPlayerLocationComponent<TPlayer, TState>? PlayerLocationComponent { get; set; }
        IAccumulatingComponent<TPlayer>? RestComponent { get; set; }
        ICarriableSupportComponent<TPlayer, TLeader, TState>? StateSupportComponent { get; set; }
        ICardComponent<TPlayer, TCard>? CardComponent { get; set; }
        IStaticDataComponent<TState, TPlayer, TRegion>? StaticDataComponent { get; set; }

        bool IsReady();
    }
}