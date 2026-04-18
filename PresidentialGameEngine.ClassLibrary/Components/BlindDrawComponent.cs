using PresidentialGameEngine.ClassLibrary.Exceptions;
using PresidentialGameEngine.ClassLibrary.Interfaces;

namespace PresidentialGameEngine.ClassLibrary.Components;

public class BlindDrawComponent<TOption>
    : IBlindDrawComponent<TOption>
    where TOption : Enum
{
    private readonly IDictionary<TOption, int> _initialPopulation;
    private readonly IDictionary<TOption, int> _optionPopulations;
    private readonly IRandomnessProvider _rng;

    private bool _refillPopulationAutomatically = true;

    public BlindDrawComponent(int initialPopulationPerOption, IRandomnessProvider rng) 
        : this(((TOption[])Enum.GetValues(typeof(TOption))).ToDictionary(val => val, val => initialPopulationPerOption), rng)
    {

    }

    public BlindDrawComponent(IDictionary<TOption, int> initialPopulation, IRandomnessProvider rng)
    {
        _initialPopulation = initialPopulation;
        
        _optionPopulations =  new Dictionary<TOption, int>();
        foreach (var kvp in _initialPopulation)
        {
            _optionPopulations.Add(kvp.Key, 0);
        }
        
        _rng = rng;
    }
    
    public void RefillInitialPopulation()
    {
        foreach (var kvp in _optionPopulations)
        {
            _optionPopulations[kvp.Key] = _initialPopulation[kvp.Key];
        }
    }

    public IDictionary<TOption, int> PeekAtPopulation()
    {
        return _optionPopulations;
    }

    public TOption Draw()
    {
        var fullSum = _optionPopulations.Values.Sum();
        
        if (fullSum == 0 && _refillPopulationAutomatically)
        {
            RefillInitialPopulation();
            fullSum = _optionPopulations.Values.Sum();
        }
        else if (fullSum == 0 && !_refillPopulationAutomatically)
        {
            throw new EmptyAndNonRefillableException();
        }
        
        var num = _rng.GetRandomNumber(fullSum);
        
        var optionsStillAvailable = _optionPopulations.Where(v => v.Value > 0).Select(k => k.Key).ToArray();
        var remainder = (num % optionsStillAvailable.Length);
        var playerDrawn = optionsStillAvailable[remainder];

        _optionPopulations[playerDrawn]--;
        
        return playerDrawn;
    }

    public void StopAutomaticallyRefillingPopulation()
    {
        _refillPopulationAutomatically = false;
    }
}

