namespace PresidentialGameEngine.ClassLibrary.Interfaces;

public interface IBlindBagComponent<TPlayer>
    where TPlayer : Enum
{
    public void FillBag();

    public IDictionary<TPlayer, int> PeekIntoBag();
    
    public TPlayer DrawCube();

    public void StopAutomaticallyRefillingBag();
}