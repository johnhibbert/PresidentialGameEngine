namespace PresidentialGameEngine.ClassLibrary.Interfaces
{
    public interface IComponentCollection<PlayersEnum, LeadersEnum, IssuesEnum, StatesEnum, RegionsEnum, CardClass>
        where PlayersEnum : Enum
        where LeadersEnum : Enum
        where IssuesEnum : Enum
        where StatesEnum : Enum
        where RegionsEnum : Enum
        where CardClass : class
    {
        ISupportComponent<PlayersEnum, LeadersEnum, RegionsEnum>? EndorsementComponent { get; set; }
        IPositioningComponent<IssuesEnum>? IssuePositioningComponent { get; set; }
        ISupportComponent<PlayersEnum, LeadersEnum, IssuesEnum>? IssueSupportComponent { get; set; }
        ISupportComponent<PlayersEnum, LeadersEnum, RegionsEnum>? MediaSupportComponent { get; set; }
        IAccumulatingComponent<PlayersEnum>? MomentumComponent { get; set; }
        IPoliticalCapitalComponent<PlayersEnum>? PoliticalCapitalComponent { get; set; }
        IPlayerLocationComponent<PlayersEnum, StatesEnum>? PlayerLocationComponent { get; set; }
        IAccumulatingComponent<PlayersEnum>? RestComponent { get; set; }
        ISupportComponent<PlayersEnum, LeadersEnum, StatesEnum>? StateSupportComponent { get; set; }
        ICardComponent<PlayersEnum, CardClass>? CardComponent { get; set; }
        IStaticDataComponent<StatesEnum, PlayersEnum, RegionsEnum>? StaticDataComponent { get; set; }

        bool IsReady();
    }
}