using System.Reflection;
using NineteenSixty.Data;
using NineteenSixty.Enums;
using NineteenSixty.Interfaces;
using PresidentialGameEngine.ClassLibrary.Data;
using Card = NineteenSixty.Data.Card;

namespace NineteenSixty;

public class Controller(IEngine engine, GameEdition gameEdition) : IController
{
    private IEngine _engine = engine;

    public GameEdition GameEdition { get; init; } = gameEdition;
    
    private Phase _currentPhase = Phase.Setup;

    
    public GameState GetGameState()
    {
        return _engine.GetGameState();
    }

    [ValidOnlyInCertainPhases([Phase.Setup])]
    public void SetUpBoard()
    {
        ActionValidator.ThrowIfActionNotAllowed(_currentPhase);
        
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

        _currentPhase =  Phase.Initiative;
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

    public void PlayCardAsEvent(Player player, Card card)
    {
        throw new NotImplementedException();
    }
}

