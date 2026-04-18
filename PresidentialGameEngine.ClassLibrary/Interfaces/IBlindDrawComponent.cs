namespace PresidentialGameEngine.ClassLibrary.Interfaces;

/// <summary>
/// IBlindBagComponent represents a bag with choices to be drawn randomly that reduce the included population
/// For 1960, this would cover the political capital bag.
/// </summary>
/// <typeparam name="TOption">The enumeration of options in the bag</typeparam>
public interface IBlindDrawComponent<TOption>
    where TOption : Enum
{
    public void RefillInitialPopulation();

    public IDictionary<TOption, int> PeekAtPopulation();
    
    public TOption Draw();

    public void StopAutomaticallyRefillingPopulation();
}