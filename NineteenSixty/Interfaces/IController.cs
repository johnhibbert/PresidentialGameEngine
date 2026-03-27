using NineteenSixty.Data;
using NineteenSixty.Enums;

namespace NineteenSixty.Interfaces;

public interface IController
{

    GameTime GetGameTime();
    GameState GetGameState();
    void SetUpBoard();
    InitiativeCheckResult ConductInitiativeCheck();

    void PlayCardAsEvent(Card card, SetOfChanges changes, Player player);

    void SetFirstPlayerForActivityPhase(Player player);

    void DrawCards(Player player, int numberToDraw);
    
    IEnumerable<Card> GetCardsInHand(Player player);

}