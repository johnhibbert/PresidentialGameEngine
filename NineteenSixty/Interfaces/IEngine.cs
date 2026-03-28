using NineteenSixty.Data;
using NineteenSixty.Enums;

namespace NineteenSixty.Interfaces;

public interface IEngine: IMomentumEngine, IStateSupportEngine, IIssueSupportEngine, IIssuePositioningEngine,
    IPlayerPositionEngine, IEndorsementEngine, IExhaustionEngine, IMediaSupportEngine, ICardZoneEngine,
    IPoliticalCapitalEngine
{
    GameState GetGameState();

    public void ImplementChanges(SetOfChanges changes);

}

public interface IMomentumEngine
{
    public void GainMomentum(Player player, int amount);

    public int GetPlayerMomentum(Player player);

    public void LoseMomentum(Player player, int amount);
    
}

public interface IStateSupportEngine
{
    public Leader GetLeader(State state);
    public int GetSupportAmount(State state);
    public void GainSupport(Player player, State state, int amount);
    public void LoseSupport(Player player, State state, int amount);
}

public interface IIssueSupportEngine
{
    public Leader GetLeader(Issue issue);
    public int GetSupportAmount(Issue issue);
    public void GainSupport(Player player, Issue issue, int amount);
    public void LoseSupport(Player player, Issue issue, int amount);
}

public interface IIssuePositioningEngine
{
    public void SetIssueOrder(IEnumerable<Issue> orderedIssues);
    public void MoveIssueUp(Issue issue);
}

public interface IPlayerPositionEngine
{
    public State GetPlayerState(Player player);
    public void MovePlayerToState(Player player, State state);
}

public interface IEndorsementEngine
{
    public void GainEndorsement(Player player, Region region, int amount);
    public void LoseEndorsement(Player player, Region region, int amount);
    public Leader GetEndorsementLeader(Region region);
    public int GetNumberOfEndorsements(Region region);
}

public interface IExhaustionEngine
{
    public void ExhaustPlayer(Player player);
    public void UnexhaustPlayer(Player player);
}

public interface IMediaSupportEngine
{
    public void GainMediaSupport(Player player, Region region, int amount);
    public void LoseMediaSupport(Player player, Region region, int amount);
}

public interface ICardZoneEngine
{
    public IEnumerable<Card> GetCardsInZone(CardZone zone, Player player);
    
    public void AddCardsToZone(IEnumerable<Card> cards, CardZone zone, Player player);
    
    public void MoveCardFromOneZoneToAnother(Player player, Card cardToMove, CardZone source, CardZone destination);

    public void ReturnCardFromDiscardPileToPlayerHandIfAvailable(Player player, Card cardToRecover);
    
    public void DrawCards(Player player, int numberToDraw);
    
    public void ShufflePublicZone(CardZone zone);
}

public interface IPoliticalCapitalEngine
{
    public Player DrawCubeFromPoliticalCapitalBag();
}