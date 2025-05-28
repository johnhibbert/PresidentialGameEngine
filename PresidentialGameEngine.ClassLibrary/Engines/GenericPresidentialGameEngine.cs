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
#pragma warning restore CS8601 // Possible null reference assignment.
            }
            else throw new ArgumentException("At least one necessary property on the ComponentCollection is null.");
        }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        public IssuesEnum[] IssueOrder
        {
            get { return IssuePositioningComponent.GetSubjectOrder.ToArray(); }
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

        public int SupportCheck(PlayersEnum player, int checkAmount)
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

    }
}
