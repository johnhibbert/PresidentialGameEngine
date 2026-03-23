using NineteenSixty.Data;
using NineteenSixty.Enums;
using NineteenSixty.Interfaces;
using PresidentialGameEngine.ClassLibrary.Data;
using Card = NineteenSixty.Data.Card;

namespace NineteenSixty;

public class Controller(IEngine engine) : IController
{
    private IEngine _engine = engine;
    
    public void PlayCard(Card card)
    {
        
    }


    public GameState GetGameState()
    {
        return _engine.GetGameState();
    }

    public void SetUpBoard(GameEdition gameEdition)
    {

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
        
        
        //throw new NotImplementedException();
    }
}

