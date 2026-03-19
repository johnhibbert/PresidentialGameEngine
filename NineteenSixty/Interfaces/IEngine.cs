using NineteenSixty.Data;

namespace NineteenSixty.Interfaces;

public interface IEngine
{
    GameState GetGameState();

    public void ImplementChanges(SetOfChanges changes);

}