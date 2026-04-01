namespace PresidentialGameEngine.ClassLibrary.Interfaces;

/// <summary>
/// IBlindBagComponent represents a bag with choices to be drawn randomly that reduce the included population
/// For 1960, this would cover the political capital bag.
/// </summary>
/// <typeparam name="TCubeOption">The enumeration of options in the bag</typeparam>
public interface IBlindBagComponent<TCubeOption>
    where TCubeOption : Enum
{
    public void FillBag();

    public IDictionary<TCubeOption, int> PeekIntoBag();
    
    public TCubeOption DrawCube();

    public void StopAutomaticallyRefillingBag();
}