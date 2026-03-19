using NineteenSixty.Data;
using NineteenSixty.Enums;

namespace NineteenSixty.Interfaces;

public interface IEngine: IMomentumEngine
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