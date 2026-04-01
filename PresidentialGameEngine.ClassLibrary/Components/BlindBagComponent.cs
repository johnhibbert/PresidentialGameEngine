using PresidentialGameEngine.ClassLibrary.Interfaces;

namespace PresidentialGameEngine.ClassLibrary.Components;

public class BlindBagComponent<TCubeOption>
    : IBlindBagComponent<TCubeOption>
    where TCubeOption : Enum
{
    private readonly int _initialPopulationPerPlayer;
    private readonly Dictionary<TCubeOption, int> _cubes;
    private readonly IRandomnessProvider _rng;

    private bool _refillBagAutomatically = true;

    public BlindBagComponent(int initialPopulationPerPlayer, IRandomnessProvider rng)
    {
        _initialPopulationPerPlayer = initialPopulationPerPlayer;
        _rng = rng;
        
        _cubes =  new Dictionary<TCubeOption, int>();
        
        foreach (var val in (TCubeOption[])Enum.GetValues(typeof(TCubeOption)))
        {
            _cubes.Add(val, 0);
        }
        
    }
    
    public void FillBag()
    {
        foreach (var val in (TCubeOption[])Enum.GetValues(typeof(TCubeOption)))
        {
            _cubes[val] = _initialPopulationPerPlayer;
        }
    }

    public IDictionary<TCubeOption, int> PeekIntoBag()
    {
        return _cubes;
    }

    public TCubeOption DrawCube()
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

