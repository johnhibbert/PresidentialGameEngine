using NineteenSixty.Data;
using NineteenSixty.Enums;

namespace NineteenSixty.Interfaces;

public interface IController
{

    GameTime GetGameTime();
    GameState GetGameState();
    void SetUpBoard();

    bool ConductSupportCheck(Player player);
    
    InitiativeCheckResult ConductInitiativeCheck();

    void PlayCardAsEvent(Card card, SetOfChanges changes, Player player);
    
    void PlayCardToCampaignInStates(Card card, SetOfChanges changes, Player player);

    void SetFirstPlayerForActivityPhase(Player player);

    void DrawCards(Player player, int numberToDraw);
    
    IEnumerable<Card> GetCardsInHand(Player player);

    void DecayMomentum();

    Leader GetLeaderInMediaSupportForIssueShift();
    
    void IssueShift(Issue issueToElevate, Player leadingPlayer);

    Endorsement GainRandomEndorsement();    
    
    void DecayIssueSupport();

}