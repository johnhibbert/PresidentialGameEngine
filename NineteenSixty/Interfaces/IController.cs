using NineteenSixty.Data;
using NineteenSixty.Enums;

namespace NineteenSixty.Interfaces;

public interface IController
{

    GameState GetGameState();
    void SetUpBoard();
    InitiativeCheckResult ConductInitiativeCheck();

    void PlayCardAsEvent(Card card, ActionPlan plan, Player player);


}