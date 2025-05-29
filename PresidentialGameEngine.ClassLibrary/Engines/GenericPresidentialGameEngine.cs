using PresidentialGameEngine.ClassLibrary.Data;
using PresidentialGameEngine.ClassLibrary.Interfaces;

namespace PresidentialGameEngine.ClassLibrary.Engines
{
    public class GenericPresidentialGameEngine<PlayersEnum, LeadersEnum, IssuesEnum, StatesEnum, RegionsEnum>
       where PlayersEnum : Enum
        where LeadersEnum : Enum
        where IssuesEnum : Enum
        where StatesEnum : Enum
        where RegionsEnum : Enum
    {
        IAccumulatingComponent<PlayersEnum> MomentumComponent { get; init; }
        ISupportComponent<PlayersEnum, LeadersEnum, IssuesEnum> IssueSupportComponent { get; init; }
        ISupportComponent<PlayersEnum, LeadersEnum, StatesEnum> StateSupportComponent { get; init; }
        IPositioningComponent<IssuesEnum> IssuePositioningComponent { get; init; }
        IPoliticalCapitalComponent<PlayersEnum> PoliticalCapitalComponent { get; init; }
        IRegionalComponent<StatesEnum, RegionsEnum, PlayersEnum> RegionalComponent { get; init; }
        IAccumulatingComponent<PlayersEnum> RestComponent { get; init; }
        ISupportComponent<PlayersEnum, LeadersEnum, RegionsEnum> EndorsementComponent { get; init; }
        ISupportComponent<PlayersEnum, LeadersEnum, RegionsEnum> MediaSupportComponent { get; init; }
        IExhaustionComponent<PlayersEnum> ExhaustionComponent { get; init; }

        //Not sure I really want to be supressing warnings like this
        //but the object is intentionally nullable to use methods instead of a huge constructor
        //and guarded by the IsReady method.
        //So it should be fine?
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public GenericPresidentialGameEngine(ComponentCollection<PlayersEnum, LeadersEnum, IssuesEnum, StatesEnum, RegionsEnum> collection)
        {
            if (collection.IsReady())
            {
#pragma warning disable CS8601 // Possible null reference assignment.
                MomentumComponent = collection.MomentumComponent;
                IssueSupportComponent = collection.IssueSupportComponent;
                StateSupportComponent = collection.StateSupportComponent;
                IssuePositioningComponent = collection.IssuePositioningComponent;
                PoliticalCapitalComponent = collection.PoliticalCapitalComponent;
                RegionalComponent = collection.RegionalComponent;
                RestComponent = collection.RestComponent;
                EndorsementComponent = collection.EndorsementComponent;
                MediaSupportComponent = collection.MediaSupportComponent;
#pragma warning restore CS8601 // Possible null reference assignment.
            }
            else throw new ArgumentException("At least one necessary property on the ComponentCollection is null.");
        }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        public IssuesEnum[] GetIssueOrder
        {
            get { return IssuePositioningComponent.GetSubjectOrder; }
        }

        public void GainMomentum(PlayersEnum player, int amount)
        {
            MomentumComponent.GainAmount(player, amount);
        }

        public int GetPlayerMomentum(PlayersEnum player)
        {
            return MomentumComponent.GetPlayerAmount(player);
        }

        public void LoseMomentum(PlayersEnum player, int amount)
        {
            MomentumComponent.LoseAmount(player, amount);
        }

        public void GainRest(PlayersEnum player, int amount)
        {
            RestComponent.GainAmount(player, amount);
        }

        public int GetPlayerRest(PlayersEnum player)
        {
            return RestComponent.GetPlayerAmount(player);
        }

        public void EmptyRest(PlayersEnum player)
        {
            RestComponent.LoseAmount(player, RestComponent.GetPlayerAmount(player));
        }

        public void GainSupport(PlayersEnum player, IssuesEnum issue, int amount)
        {
            IssueSupportComponent.GainSupport(player, issue, amount);
        }

        public void LoseSupport(PlayersEnum player, IssuesEnum issue, int amount)
        {
            IssueSupportComponent.LoseSupport(player, issue, amount);
        }

        public void GainSupport(PlayersEnum player, StatesEnum state, int amount)
        {
            StateSupportComponent.GainSupport(player, state, amount);
        }

        public void LoseSupport(PlayersEnum player, StatesEnum state, int amount)
        {
            StateSupportComponent.LoseSupport(player, state, amount);
        }

        public void SetIssueOrder(IEnumerable<IssuesEnum> orderedIssues)
        {
            IssuePositioningComponent.SetSubjectOrder(orderedIssues);
        }
        public void MoveIssueUp(IssuesEnum issue)
        {
            IssuePositioningComponent.MoveSubjectUp(issue);
        }

        public LeadersEnum GetLeader(IssuesEnum issue)
        {
            return IssueSupportComponent.GetLeader(issue);
        }

        public LeadersEnum GetLeader(StatesEnum state)
        {
            return StateSupportComponent.GetLeader(state);
        }

        public int GetSupportAmount(IssuesEnum issue)
        {
            return IssueSupportComponent.GetSupportAmount(issue);
        }

        public int GetSupportAmount(StatesEnum state)
        {
            return StateSupportComponent.GetSupportAmount(state);
        }

        public PlayersEnum InitiativeCheck()
        {
            return PoliticalCapitalComponent.InitiativeCheck();
        }

        public SupportCheckResult SupportCheck(PlayersEnum player, int checkAmount)
        {
            return PoliticalCapitalComponent.SupportCheck(player, checkAmount);
        }

        public void AddCubesToBag(PlayersEnum player, int amount)
        {
            PoliticalCapitalComponent.AddCubes(player, amount);
        }


        public void ImplementChanges(PlayerChosenChanges<PlayersEnum, IssuesEnum, StatesEnum> changes)
        {
            foreach (SupportChange<PlayersEnum, IssuesEnum> issueChange in changes.IssueChanges)
            {
                GainSupport(issueChange.Player, issueChange.Target, issueChange.Change);
            }

            foreach (SupportChange<PlayersEnum, StatesEnum> stateChange in changes.StateChanges)
            {
                GainSupport(stateChange.Player, stateChange.Target, stateChange.Change);
            }
        }

        public RegionsEnum GetRegion(StatesEnum state)
        {
            return RegionalComponent.GetRegionByState(state);
        }

        public IEnumerable<StatesEnum> GetStatesInRegion(RegionsEnum region)
        {
            return RegionalComponent.GetStatesWithinRegion(region);
        }

        public StatesEnum GetPlayerState(PlayersEnum player)
        {
            return RegionalComponent.GetPlayerState(player);
        }

        public void MovePlayerToState(PlayersEnum player, StatesEnum states)
        {
            RegionalComponent.MovePlayerToState(player, states);
        }

        public void GainEndorsement(PlayersEnum player, RegionsEnum region)
        {
            EndorsementComponent.GainSupport(player, region, 1);
        }

        public LeadersEnum GetEndorsementLeader(RegionsEnum region) 
        {
            return EndorsementComponent.GetLeader(region);
        }

        public int GetNumberOfEndorsements(RegionsEnum region)
        {
            return EndorsementComponent.GetSupportAmount(region);
        }

        public void GainMediaSupport(PlayersEnum player, RegionsEnum region, int amount)
        {
            var result = SupportCheck(player, amount);
            MediaSupportComponent.GainSupport(player, region, result.Successes);
        }

        public LeadersEnum GetMediaSupportLeader(RegionsEnum region)
        {
            return MediaSupportComponent.GetLeader(region);
        }

        public int GetMediaSupportAmount(RegionsEnum region)
        {
            return MediaSupportComponent.GetSupportAmount(region);
        }

        public void ExhaustPlayer(PlayersEnum player) 
        {
            ExhaustionComponent.ExhaustPlayer(player);
        }

        public void UnexhaustPlayer(PlayersEnum player)
        {
            ExhaustionComponent.UnexhaustPlayer(player);
        }

        public bool IsPlayerReady(PlayersEnum player)
        {
            return ExhaustionComponent.IsPlayerReady(player);
        }

    }
}
