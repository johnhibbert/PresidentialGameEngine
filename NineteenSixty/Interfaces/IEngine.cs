using NineteenSixty.Data;
using NineteenSixty.Enums;

namespace NineteenSixty.Interfaces;

public interface IEngine: IMomentumEngine, IStateSupportEngine, IIssueSupportEngine, IIssuePositioningEngine
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