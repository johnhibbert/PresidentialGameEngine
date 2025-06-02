using PresidentialGameEngine.ClassLibrary.Components;
using PresidentialGameEngine.ClassLibrary.Data;
using PresidentialGameEngine.ClassLibrary.Enums;
using PresidentialGameEngine.ClassLibrary.Exceptions;
using PresidentialGameEngine.ClassLibrary.Interfaces;

namespace PresidentialGameEngine.ClassLibrary.Engines
{
    public class GenericPresidentialGameEngine<PlayersEnum, LeadersEnum, IssuesEnum, StatesEnum, RegionsEnum, CardClass>
       where PlayersEnum : Enum
        where LeadersEnum : Enum
        where IssuesEnum : Enum
        where StatesEnum : Enum
        where RegionsEnum : Enum
        where CardClass : class
    {
        IAccumulatingComponent<PlayersEnum> MomentumComponent { get; init; }
        ISupportComponent<PlayersEnum, LeadersEnum, IssuesEnum> IssueSupportComponent { get; init; }
        ISupportComponent<PlayersEnum, LeadersEnum, StatesEnum> StateSupportComponent { get; init; }
        IPositioningComponent<IssuesEnum> IssuePositioningComponent { get; init; }
        IPoliticalCapitalComponent<PlayersEnum> PoliticalCapitalComponent { get; init; }
        IPlayerLocationComponent<PlayersEnum, StatesEnum> PlayerLocationComponent { get; init; }
        IAccumulatingComponent<PlayersEnum> RestComponent { get; init; }
        ISupportComponent<PlayersEnum, LeadersEnum, RegionsEnum> EndorsementComponent { get; init; }
        ISupportComponent<PlayersEnum, LeadersEnum, RegionsEnum> MediaSupportComponent { get; init; }
        IExhaustionComponent<PlayersEnum> ExhaustionComponent { get; init; }
        ICardComponent<PlayersEnum, CardClass> CardComponent { get; init; }
        IStaticDataComponent<StatesEnum, PlayersEnum, RegionsEnum> StaticDataComponent { get; init; }

        //Not sure I really want to be supressing warnings like this
        //but the object is intentionally nullable to use methods instead of a huge constructor
        //and guarded by the IsReady method.
        //So it should be fine?
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public GenericPresidentialGameEngine
            (ComponentCollection<PlayersEnum, LeadersEnum, IssuesEnum,
                StatesEnum, RegionsEnum, CardClass> collection)
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

            foreach (PlayersEnum player in (PlayersEnum[])Enum.GetValues(typeof(PlayersEnum)))
            {
                MomentumComponent.GainAmount(player, 2);
                //CardComponent.DrawCards(player, 6);
            }

            

        }

        

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


        public void ImplementChanges(PlayerChosenChanges<PlayersEnum, IssuesEnum, StatesEnum, RegionsEnum> changes)
        {
            foreach (SupportChange<PlayersEnum, IssuesEnum> issueChange in changes.IssueChanges)
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

            foreach (SupportChange<PlayersEnum, StatesEnum> stateChange in changes.StateChanges)
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

            foreach (SupportChange<PlayersEnum, RegionsEnum> mediaChange in changes.MediaSupportChanges)
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

            foreach (SupportChange<PlayersEnum, RegionsEnum> endorsementChange in changes.EndorsementChanges)
            {
                //FIXME: endorsements currently only change one at a time.
                GainEndorsement(endorsementChange.Player, endorsementChange.Target);
            }

            if (changes.NewIssuesOrder.Count > 0)
            {
                IssuePositioningComponent.SetSubjectOrder(changes.NewIssuesOrder);
            }

        }

        public StatesEnum GetPlayerState(PlayersEnum player)
        {
            return PlayerLocationComponent.GetPlayerState(player);
        }

        public void MovePlayerToState(PlayersEnum player, StatesEnum states)
        {
            PlayerLocationComponent.MovePlayerToState(player, states);
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

        public SupportCheckResult GainMediaSupport(PlayersEnum player, RegionsEnum region, int amount)
        {
            var result = SupportCheck(player, amount);
            MediaSupportComponent.GainSupport(player, region, result.Successes);
            return result;
        }

        public void GainMediaSupportWithoutSupportCheck(PlayersEnum player, RegionsEnum region, int amount)
        {
            MediaSupportComponent.GainSupport(player, region, amount);
        }

        public void LoseMediaSupport(PlayersEnum player, RegionsEnum region, int amount)
        {
            MediaSupportComponent.LoseSupport(player, region, amount);
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


        public IEnumerable<CardClass> GetPlayerHand(PlayersEnum player)
        {
            return CardComponent.GetPlayerHand(player);
        }


        public int CountCardsLeftInDeck() 
        {
            return CardComponent.CountCardsLeftInDeck();
        }

        public void DiscardCardFromHand(PlayersEnum player, CardClass card)
        {
            CardComponent.MoveCardFromOneZoneToAnother(player, card, CardZone.Hand, CardZone.Discard);
        }

        public void DrawCards(PlayersEnum player, int numberToDraw)
        {
            CardComponent.DrawCards(player, numberToDraw);
        }

        public void MoveCardFromHandToCampaignStrategyPile(PlayersEnum player, CardClass card)
        {
            CardComponent.MoveCardFromOneZoneToAnother(player, card, CardZone.Hand, CardZone.CampaignStrategy);
        }

        public void MoveCardFromHandToRemovedPile(PlayersEnum player, CardClass card)
        {
            CardComponent.MoveCardFromOneZoneToAnother(player, card, CardZone.Hand, CardZone.Removed);
        }

        public void RetrieveCardFromDiscardPile(PlayersEnum player, CardClass card, bool okayIfCardNotFound = false)
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

        public void MoveCardFromOneZoneToAnother(PlayersEnum player, CardClass cardToMove,
            CardZone source, CardZone destination)
        {
            CardComponent.MoveCardFromOneZoneToAnother(player, cardToMove, source, destination);
        }


        public GameState<PlayersEnum, LeadersEnum, IssuesEnum, StatesEnum, RegionsEnum> GetGameState() 
        {
            return new GameState<PlayersEnum, LeadersEnum, IssuesEnum, StatesEnum, RegionsEnum>()
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


    public class GameState<PlayersEnum, LeadersEnum, IssuesEnum, StatesEnum, RegionsEnum>
       where PlayersEnum : Enum
        where LeadersEnum : Enum
        where IssuesEnum : Enum
        where StatesEnum : Enum
        where RegionsEnum : Enum
    {
        public required IDictionary<PlayersEnum, int> Momentum { get; init; }

        public required IDictionary<PlayersEnum, int> RestCubes { get; init; }

        public required IDictionary<IssuesEnum, SupportStatus<LeadersEnum>> IssueContests { get; init; }

        public required IDictionary<StatesEnum, SupportStatus<LeadersEnum>> StateContests { get; init; }

        public required IList<IssuesEnum> IssueOrder { get; init; }

        public required IDictionary<RegionsEnum, SupportStatus<LeadersEnum>> Endorsements { get; init; }

        public required IDictionary<RegionsEnum, SupportStatus<LeadersEnum>> MediaSupportLevels { get; init; }

        public required IDictionary<PlayersEnum, StatesEnum> PlayerLocations { get; init; }

        public required IDictionary<PlayersEnum, bool> Exhaustion { get; init; }

    }
}
