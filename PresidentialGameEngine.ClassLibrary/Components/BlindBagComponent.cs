using PresidentialGameEngine.ClassLibrary.Interfaces;

namespace PresidentialGameEngine.ClassLibrary.Components;

public class BlindBagComponent<TOption>
    : IBlindBagComponent<TOption>
    where TOption : Enum
{
    private readonly int _initialPopulationPerOption;
    private readonly Dictionary<TOption, int> _cubes;
    private readonly IRandomnessProvider _rng;

    private bool _refillBagAutomatically = true;

    public BlindBagComponent(int initialPopulationPerOption, IRandomnessProvider rng)
    {
        _initialPopulationPerOption = initialPopulationPerOption;
        _rng = rng;
        
        _cubes =  new Dictionary<TOption, int>();
        
        foreach (var val in (TOption[])Enum.GetValues(typeof(TOption)))
        {
            _cubes.Add(val, 0);
        }
        
    }
    
    public void FillBag()
    {
        foreach (var val in (TOption[])Enum.GetValues(typeof(TOption)))
        {
            _cubes[val] = _initialPopulationPerOption;
        }
    }

    public IDictionary<TOption, int> PeekIntoBag()
    {
        return _cubes;
    }

    public TOption Draw()
    {
        var fullSum = _cubes.Values.Sum();
        
        if (fullSum == 0 && _refillBagAutomatically)
        {
            FillBag();
            fullSum = _cubes.Values.Sum();
        }
        
        var num = _rng.GetRandomNumber(fullSum);
        
        var stillHasCubes = _cubes.Where(v => v.Value > 0).Select(k => k.Key).ToArray();
        var remainder = (num % stillHasCubes.Length);
        var playerDrawn = stillHasCubes[remainder];

        _cubes[playerDrawn]--;
        
        return playerDrawn;
    }

    public void StopAutomaticallyRefillingBag()
    {
        _refillBagAutomatically = false;
    }
}

