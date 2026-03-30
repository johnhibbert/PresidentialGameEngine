using System.Reflection;
using NineteenSixty.Data;
using NineteenSixty.Enums;
using NineteenSixty.Exceptions;
using NineteenSixty.Interfaces;
using PresidentialGameEngine.ClassLibrary.Data;
using Card = NineteenSixty.Data.Card;

namespace NineteenSixty;

public class Controller(IEngine engine, GameEdition gameEdition) : IController
{
    private IEngine _engine = engine;

    public GameEdition GameEdition { get; init; } = gameEdition;

    private int TurnNumber { get; set; } = 0;
    private Phase CurrentPhase { get; set; } = Phase.Setup;
    private int ActivityPhaseNumber { get; set; }
    private Player FirstPlayer { get; set; }
    private Player CurrentPlayer { get; set; }
    
    
    public GameTime GetGameTime()
    {
        return new  GameTime()
        {
            TurnNumber =  TurnNumber,
            CurrentPhase = CurrentPhase,
            ActivityPhaseNumber = ActivityPhaseNumber,
            FirstPlayer = FirstPlayer,
            ActivePlayer = CurrentPlayer,
        };
    }

    public void SwitchActivePlayer()
    {
        CurrentPlayer = CurrentPlayer.ToOpponent();
        if (CurrentPlayer == FirstPlayer)
        {
            if (TurnNumber == 5)
            {
                TurnNumber = 0;
                CurrentPhase++;
            }
            else
            {
                TurnNumber++;
            }
        }
    }
    
    public GameState GetGameState()
    {
        return _engine.GetGameState();
    }

    [ValidOnlyInCertainPhases([Phase.Setup])]
    public void SetUpBoard()
    {
        ActionValidator.ThrowIfActionNotAllowed(CurrentPhase);
        
        foreach (var ff in Manifest.StateData)
        {
            if (ff.Value.StartingSupport > 0)
            {
                _engine.GainSupport(ff.Value.Tilt, ff.Value.State, ff.Value.StartingSupport);
            }
        }
        
        //Place the issue tiles on their indicated spaces on the Issues Track.
        _engine.SetIssueOrder([Issue.Defense, Issue.Economy, Issue.CivilRights]);
        
        //Each player should choose a side and take: Two momentum markers.
        _engine.GainMomentum(Player.Kennedy, 2);
        _engine.GainMomentum(Player.Nixon, 2);

        //Shuffle the Campaign Card deck and place it facedown near the board.
        switch (gameEdition)
        {
            case GameEdition.FirstEditionByZMan:
                _engine.AddCardsToZone(Manifest.ZManCards.Values, CardZone.Deck, Player.Kennedy);
                break;

            case GameEdition.SecondEditionByGmt:
            default:
                _engine.AddCardsToZone(Manifest.GMTCards.Values, CardZone.Deck, Player.Kennedy);
                break;
        }
        
        _engine.ShufflePublicZone(CardZone.Deck);


        TurnNumber++;
        CurrentPhase =  Phase.Initiative;
    }

    public InitiativeCheckResult ConductInitiativeCheck()
    {
        var cubesDrawn = new Dictionary<Player, int>()
        {
            [Player.Kennedy] = 0,
            [Player.Nixon] = 0,
        };

        var thresholdMet = false;
        var playerWithInitiative = Player.Kennedy;
        
        while (!thresholdMet)
        {
            var player = _engine.DrawCubeFromPoliticalCapitalBag();
            cubesDrawn[player]++;

            if (cubesDrawn[player] == 2)
            {
                thresholdMet = true;
                playerWithInitiative = player;
            }
        }

        return new InitiativeCheckResult()
        {
            CubesDrawn = cubesDrawn,
            PlayerWithInitiative = playerWithInitiative,
        };
    }

    [ValidOnlyInCertainPhases([Phase.Activity])]
    public void PlayCardAsEvent(Card card, SetOfChanges changes, Player player)
    {
        ActionValidator.ThrowIfActionNotAllowed(CurrentPhase);
        
        ActionPlan plan = new ActionPlan()
        {
            Engine = _engine,
            Changes = changes,
        };
        
        if(!card.AreChangesValid(plan))
        {
            throw new InvalidPlayerChoices($"The selected choices are invalid for this card: {card.Index} {card.Title}.");
        }
        
        card.Event(plan, player);
        _engine.MoveCardFromOneZoneToAnother(player, card, CardZone.Hand, CardZone.Removed);
        SwitchActivePlayer();
    }

    public void CampaignInStates(Card card, SetOfChanges changes, Player player)
    {
        throw new NotImplementedException();
    }

    [ValidOnlyInCertainPhases([Phase.Initiative])]
    public void SetFirstPlayerForActivityPhase(Player player)
    {
        ActionValidator.ThrowIfActionNotAllowed(CurrentPhase);
        
        CurrentPhase = Phase.Activity;
        ActivityPhaseNumber = 1;
        FirstPlayer = player;
        CurrentPlayer = player;
    }

    public void DrawCards(Player player, int numberToDraw)
    {
        _engine.DrawCards(player, numberToDraw);
    }

    public IEnumerable<Card> GetCardsInHand(Player player)
    {
        return _engine.GetCardsInZone(CardZone.Hand, player);
    }
}

