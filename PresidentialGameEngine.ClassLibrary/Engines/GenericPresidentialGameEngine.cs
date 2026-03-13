using PresidentialGameEngine.ClassLibrary.Components;
using PresidentialGameEngine.ClassLibrary.Data;
using PresidentialGameEngine.ClassLibrary.Enums;
using PresidentialGameEngine.ClassLibrary.Exceptions;
using PresidentialGameEngine.ClassLibrary.Interfaces;

namespace PresidentialGameEngine.ClassLibrary.Engines
{
    public class GenericPresidentialGameEngine<TPlayer, TLeader, TIssue, TState, TRegion, TCard>
       where TPlayer : Enum
        where TLeader : Enum
        where TIssue : Enum
        where TState : Enum
        where TRegion : Enum
        where TCard : ICard
    {
        IAccumulatingComponent<TPlayer> MomentumComponent { get; init; }
        ISupportComponent<TPlayer, TLeader, TIssue> IssueSupportComponent { get; init; }
        ICarriableSupportComponent<TPlayer, TLeader, TState> StateSupportComponent { get; init; }
        IPositioningComponent<TIssue> IssuePositioningComponent { get; init; }
        IPoliticalCapitalComponent<TPlayer> PoliticalCapitalComponent { get; init; }
        IPlayerLocationComponent<TPlayer, TState> PlayerLocationComponent { get; init; }
        IAccumulatingComponent<TPlayer> RestComponent { get; init; }
        ISupportComponent<TPlayer, TLeader, TRegion> EndorsementComponent { get; init; }
        ISupportComponent<TPlayer, TLeader, TRegion> MediaSupportComponent { get; init; }
        IExhaustionComponent<TPlayer> ExhaustionComponent { get; init; }
        ICardComponent<TPlayer, TCard> CardComponent { get; init; }
        IStaticDataComponent<TState, TPlayer, TRegion> StaticDataComponent { get; init; }

        //Not sure I really want to be supressing warnings like this
        //but the object is intentionally nullable to use methods instead of a huge constructor
        //and guarded by the IsReady method.
        //So it should be fine?
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public GenericPresidentialGameEngine
            (ComponentCollection<TPlayer, TLeader, TIssue,
                TState, TRegion, TCard> collection)
        {
            if (collection.IsReady())
            {
#pragma warning disable CS8601 // Possible null reference assignment.
                MomentumComponent = collection.MomentumComponent;
                IssueSupportComponent = collection.IssueSupportComponent;
                StateSupportComponent = collection.StateSupportComponent;
                IssuePositioningComponent = collection.IssuePositioningComponent;
                PoliticalCapitalComponent = collection.PoliticalCapitalComponent;
                PlayerLocationComponent = collection.PlayerLocationComponent;
                RestComponent = collection.RestComponent;
                EndorsementComponent = collection.EndorsementComponent;
                MediaSupportComponent = collection.MediaSupportComponent;
                ExhaustionComponent = collection.ExhaustionComponent;
                CardComponent = collection.CardComponent;
                StaticDataComponent = collection.StaticDataComponent;
#pragma warning restore CS8601 // Possible null reference assignment.
            }
            else throw new ArgumentException("At least one necessary property on the ComponentCollection is null.");
        }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        public void DoInitialSetup()
        {
            var holder = StaticDataComponent.GetRawData().Values;

            foreach (var item in holder)
            {
                StateSupportComponent.GainSupport(item.Tilt, item.State, item.StartingSupport);
            }

            foreach (TPlayer player in (TPlayer[])Enum.GetValues(typeof(TPlayer)))
            {
                MomentumComponent.GainAmount(player, 2);
                //CardComponent.DrawCards(player, 6);
            }

            

        }

        

        public TIssue[] GetIssueOrder
        {
            get { return IssuePositioningComponent.GetSubjectOrder; }
        }

        public void GainMomentum(TPlayer player, int amount)
        {
            MomentumComponent.GainAmount(player, amount);
        }

        public int GetPlayerMomentum(TPlayer player)
        {
            return MomentumComponent.GetPlayerAmount(player);
        }

        public void LoseMomentum(TPlayer player, int amount)
        {
            MomentumComponent.LoseAmount(player, amount);
        }

        public void GainRest(TPlayer player, int amount)
        {
            RestComponent.GainAmount(player, amount);
        }

        public int GetPlayerRest(TPlayer player)
        {
            return RestComponent.GetPlayerAmount(player);
        }

        public void EmptyRest(TPlayer player)
        {
            RestComponent.LoseAmount(player, RestComponent.GetPlayerAmount(player));
        }

        public void GainSupport(TPlayer player, TIssue issue, int amount)
        {
            IssueSupportComponent.GainSupport(player, issue, amount);
        }

        public void LoseSupport(TPlayer player, TIssue issue, int amount)
        {
            IssueSupportComponent.LoseSupport(player, issue, amount);
        }

        public void GainSupport(TPlayer player, TState state, int amount)
        {
            StateSupportComponent.GainSupport(player, state, amount);
        }

        public void LoseSupport(TPlayer player, TState state, int amount)
        {
            StateSupportComponent.LoseSupport(player, state, amount);
        }

        public void SetIssueOrder(IEnumerable<TIssue> orderedIssues)
        {
            IssuePositioningComponent.SetSubjectOrder(orderedIssues);
        }
        public void MoveIssueUp(TIssue issue)
        {
            IssuePositioningComponent.MoveSubjectUp(issue);
        }

        public TLeader GetLeader(TIssue issue)
        {
            return IssueSupportComponent.GetLeader(issue);
        }

        public TLeader GetLeader(TState state)
        {
            return StateSupportComponent.GetLeader(state);
        }

        public int GetSupportAmount(TIssue issue)
        {
            return IssueSupportComponent.GetSupportStatus(issue).Support;
        }

        public int GetSupportAmount(TState state)
        {
            return StateSupportComponent.GetSupportStatus(state).Support;
        }

        public TPlayer InitiativeCheck()
        {
            return PoliticalCapitalComponent.InitiativeCheck();
        }

        public SupportCheckResult SupportCheck(TPlayer player, int checkAmount)
        {
            return PoliticalCapitalComponent.SupportCheck(player, checkAmount);
        }

        public void AddCubesToBag(TPlayer player, int amount)
        {
            PoliticalCapitalComponent.AddCubes(player, amount);
        }


        public void NEWImplementChanges(NEW_ChangeBattery<TPlayer, TIssue, TState, TRegion> changeBattery) 
        {
            foreach (NEW_SupportChange<TPlayer, TState> stateChange in changeBattery.StateChanges)
            {
                switch (stateChange.GainOrLoss) 
                {
                    case NEW_ChangeDirection.Gain:
                        GainSupport(stateChange.Player, stateChange.Target, stateChange.Change);
                        break;
                    case NEW_ChangeDirection.Loss:
                        LoseSupport(stateChange.Player, stateChange.Target, stateChange.Change);
                        break;
                }
            }

            foreach (NEW_SupportChange<TPlayer, TIssue> issueChange in changeBattery.IssueChanges)
            {
                switch (issueChange.GainOrLoss)
                {
                    case NEW_ChangeDirection.Gain:
                        GainSupport(issueChange.Player, issueChange.Target, issueChange.Change);
                        break;
                    case NEW_ChangeDirection.Loss:
                        LoseSupport(issueChange.Player, issueChange.Target, issueChange.Change);
                        break;
                }
            }

            foreach (NEW_SupportChange<TPlayer, TRegion> endorsementChange in changeBattery.EndorsementChanges)
            {
                switch (endorsementChange.GainOrLoss)
                {
                    case NEW_ChangeDirection.Gain:
                        EndorsementComponent.GainSupport(endorsementChange.Player, endorsementChange.Target, endorsementChange.Change);
                        break;
                    case NEW_ChangeDirection.Loss:
                        EndorsementComponent.LoseSupport(endorsementChange.Player, endorsementChange.Target, endorsementChange.Change);
                        break;
                }
            }

            foreach (NEW_SupportChange<TPlayer, TRegion> mediaChange in changeBattery.MediaSupportChanges)
            {
                switch (mediaChange.GainOrLoss)
                {
                    case NEW_ChangeDirection.Gain:
                        MediaSupportComponent.GainSupport(mediaChange.Player, mediaChange.Target, mediaChange.Change);
                        break;
                    case NEW_ChangeDirection.Loss:
                        MediaSupportComponent.LoseSupport(mediaChange.Player, mediaChange.Target, mediaChange.Change);
                        break;
                }
            }

            foreach (NEW_AccumulationChange<TPlayer> momentumChange in changeBattery.MomentumChanges)
            {
                switch (momentumChange.GainOrLoss)
                {
                    case NEW_ChangeDirection.Gain:
                        MomentumComponent.GainAmount(momentumChange.Player, momentumChange.Change);
                        break;
                    case NEW_ChangeDirection.Loss:
                        MomentumComponent.LoseAmount(momentumChange.Player, momentumChange.Change);
                        break;
                }
            }

            foreach (NEW_AccumulationChange<TPlayer> restChange in changeBattery.RestChanges)
            {
                switch (restChange.GainOrLoss)
                {
                    case NEW_ChangeDirection.Gain:
                        RestComponent.GainAmount(restChange.Player, restChange.Change);
                        break;
                    case NEW_ChangeDirection.Loss:
                        RestComponent.LoseAmount(restChange.Player, restChange.Change);
                        break;
                }
            }

            foreach (NEW_PlayerLocationChange<TPlayer, TState> playerLocationChange in changeBattery.PlayerLocationChanges)
            {
                PlayerLocationComponent.MovePlayerToState(playerLocationChange.Player, playerLocationChange.State);
            }

            TIssue defaultIssue = (TIssue)Enum.ToObject(typeof(TIssue), 0);
            bool issueToElevateIsNotDefault = EqualityComparer<TIssue>.Default.Equals(changeBattery.IssueToElevate, defaultIssue) == false;

            bool hasNewIssueOrder = changeBattery.NewIssuesOrder.Count == (Enum.GetValues(typeof(TIssue)).Length - 1);

            if (issueToElevateIsNotDefault)
            {
                IssuePositioningComponent.MoveSubjectUp(changeBattery.IssueToElevate);
            }
            else if(hasNewIssueOrder)
            {
                IssuePositioningComponent.SetSubjectOrder(changeBattery.NewIssuesOrder);
            }

        }


        public void ImplementChanges(PlayerChosenChanges<TPlayer, TIssue, TState, TRegion> changes)
        {
            foreach (SupportChange<TPlayer, TIssue> issueChange in changes.IssueChanges)
            {
                if (issueChange.Change > 0)
                {
                    GainSupport(issueChange.Player, issueChange.Target, issueChange.Change);
                }
                else 
                {
                    LoseSupport(issueChange.Player, issueChange.Target, Math.Abs(issueChange.Change));
                }
            }

            foreach (SupportChange<TPlayer, TState> stateChange in changes.StateChanges)
            {
                if (stateChange.Change > 0)
                {
                    GainSupport(stateChange.Player, stateChange.Target, stateChange.Change);
                }
                else
                {
                    LoseSupport(stateChange.Player, stateChange.Target, Math.Abs(stateChange.Change));
                }
            }

            foreach (SupportChange<TPlayer, TRegion> mediaChange in changes.MediaSupportChanges)
            {
                if (mediaChange.Change > 0)
                {
                    GainMediaSupportWithoutSupportCheck(mediaChange.Player, mediaChange.Target, mediaChange.Change);
                }
                else
                {
                    LoseMediaSupport(mediaChange.Player, mediaChange.Target, Math.Abs(mediaChange.Change));
                }               
            }

            foreach (SupportChange<TPlayer, TRegion> endorsementChange in changes.EndorsementChanges)
            {
                GainEndorsement(endorsementChange.Player, endorsementChange.Target, endorsementChange.Change);
            }

            if (changes.NewIssuesOrder.Count > 0)
            {
                IssuePositioningComponent.SetSubjectOrder(changes.NewIssuesOrder);
            }

        }

        public TState GetPlayerState(TPlayer player)
        {
            return PlayerLocationComponent.GetPlayerState(player);
        }

        public void MovePlayerToState(TPlayer player, TState states)
        {
            PlayerLocationComponent.MovePlayerToState(player, states);
        }

        public void GainEndorsement(TPlayer player, TRegion region, int amount)
        {
            EndorsementComponent.GainSupport(player, region, amount);
        }

        public void LoseEndorsement(TPlayer player, TRegion region, int amount)
        {
            EndorsementComponent.LoseSupport(player, region, amount);
        }

        public TLeader GetEndorsementLeader(TRegion region) 
        {
            return EndorsementComponent.GetLeader(region);
        }

        public int GetNumberOfEndorsements(TRegion region)
        {
            return EndorsementComponent.GetSupportStatus(region).Support;
        }

        public SupportCheckResult GainMediaSupport(TPlayer player, TRegion region, int amount)
        {
            var result = SupportCheck(player, amount);
            MediaSupportComponent.GainSupport(player, region, result.Successes);
            return result;
        }

        public void GainMediaSupportWithoutSupportCheck(TPlayer player, TRegion region, int amount)
        {
            MediaSupportComponent.GainSupport(player, region, amount);
        }

        public void LoseMediaSupport(TPlayer player, TRegion region, int amount)
        {
            MediaSupportComponent.LoseSupport(player, region, amount);
        }

        public TLeader GetMediaSupportLeader(TRegion region)
        {
            return MediaSupportComponent.GetLeader(region);
        }

        public int GetMediaSupportAmount(TRegion region)
        {
            return MediaSupportComponent.GetSupportStatus(region).Support;
        }

        public void ExhaustPlayer(TPlayer player) 
        {
            ExhaustionComponent.ExhaustPlayer(player);
        }

        public void UnexhaustPlayer(TPlayer player)
        {
            ExhaustionComponent.UnexhaustPlayer(player);
        }

        public bool IsPlayerReady(TPlayer player)
        {
            return ExhaustionComponent.IsPlayerReady(player);
        }


        public IEnumerable<TCard> GetPlayerHand(TPlayer player)
        {
            return CardComponent.GetPlayerHand(player);
        }


        public int CountCardsLeftInDeck() 
        {
            return CardComponent.CountCardsLeftInDeck();
        }

        public void DiscardCardFromHand(TPlayer player, TCard card)
        {
            CardComponent.MoveCardFromOneZoneToAnother(player, card, CardZone.Hand, CardZone.Discard);
        }

        public void DrawCards(TPlayer player, int numberToDraw)
        {
            CardComponent.DrawCards(player, numberToDraw);
        }

        public void MoveCardFromHandToCampaignStrategyPile(TPlayer player, TCard card)
        {
            CardComponent.MoveCardFromOneZoneToAnother(player, card, CardZone.Hand, CardZone.CampaignStrategy);
        }

        public void MoveCardFromHandToRemovedPile(TPlayer player, TCard card)
        {
            CardComponent.MoveCardFromOneZoneToAnother(player, card, CardZone.Hand, CardZone.Removed);
        }

        public void RetrieveCardFromDiscardPile(TPlayer player, TCard card, bool okayIfCardNotFound = false)
        {
            try
            {
                CardComponent.MoveCardFromOneZoneToAnother(player, card, CardZone.Discard, CardZone.Hand);
            }
            catch(CardNotFoundException) 
            {
                //Ignore it if we know it's okay
                if (okayIfCardNotFound == false) 
                {
                    throw;
                }
            }
        }

        public void MoveCardFromOneZoneToAnother(TPlayer player, TCard cardToMove,
            CardZone source, CardZone destination)
        {
            CardComponent.MoveCardFromOneZoneToAnother(player, cardToMove, source, destination);
        }


        public GameState<TPlayer, TLeader, TIssue, TState, TRegion> GetGameState() 
        {
            return new GameState<TPlayer, TLeader, TIssue, TState, TRegion>()
            {
                Momentum = MomentumComponent.GetRawData(),
                RestCubes = RestComponent.GetRawData(),
                IssueContests = IssueSupportComponent.GetRawData(),
                IssueOrder = IssuePositioningComponent.GetSubjectOrder,
                Endorsements = EndorsementComponent.GetRawData(),
                Exhaustion = ExhaustionComponent.GetRawData(),
                MediaSupportLevels = MediaSupportComponent.GetRawData(),
                PlayerLocations = PlayerLocationComponent.GetRawData(),
                StateContests = StateSupportComponent.GetRawData(),
            };
        }
    }


    public class GameState<TPlayer, TLeader, TIssue, TState, TRegion>
       where TPlayer : Enum
        where TLeader : Enum
        where TIssue : Enum
        where TState : Enum
        where TRegion : Enum
    {
        public required IDictionary<TPlayer, int> Momentum { get; init; }

        public required IDictionary<TPlayer, int> RestCubes { get; init; }

        public required IDictionary<TIssue, SupportContest<TLeader>> IssueContests { get; init; }

        public required IDictionary<TState, SupportContest<TLeader>> StateContests { get; init; }

        public required IList<TIssue> IssueOrder { get; init; }

        public required IDictionary<TRegion, SupportContest<TLeader>> Endorsements { get; init; }

        public required IDictionary<TRegion, SupportContest<TLeader>> MediaSupportLevels { get; init; }

        public required IDictionary<TPlayer, TState> PlayerLocations { get; init; }

        public required IDictionary<TPlayer, bool> Exhaustion { get; init; }

    }
}
