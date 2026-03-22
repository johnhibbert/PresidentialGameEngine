using PresidentialGameEngine.ClassLibrary.Interfaces;

namespace PresidentialGameEngine.ClassLibrary.Components;

public class BlindBagComponent<TPlayer>
    : IBlindBagComponent<TPlayer>
    where TPlayer : Enum
{
    private readonly int _initialPopulationPerPlayer;
    private readonly Dictionary<TPlayer, int> _cubes;
    private readonly IRandomnessProvider _rng;

    private bool _refillBagAutomatically = true;

    public BlindBagComponent(int initialPopulationPerPlayer, IRandomnessProvider rng)
    {
        _initialPopulationPerPlayer = initialPopulationPerPlayer;
        _rng = rng;
        
        _cubes =  new Dictionary<TPlayer, int>();
        
        foreach (var val in (TPlayer[])Enum.GetValues(typeof(TPlayer)))
        {
            _cubes.Add(val, 0);
        }
        
    }
    
    public void FillBag()
    {
        foreach (var val in (TPlayer[])Enum.GetValues(typeof(TPlayer)))
        {
            _cubes[val] = _initialPopulationPerPlayer;
        }
    }

    public IDictionary<TPlayer, int> PeekIntoBag()
    {
        return _cubes;
    }

    public TPlayer DrawCube()
    {
        var fullSum = _cubes.Values.Sum();
        var num = _rng.GetRandomNumber(fullSum);
        
        var stillHasCubes = _cubes.Where(v => v.Value > 0).Select(k => k.Key).ToArray();
        var remainder = (num % stillHasCubes.Length);
        var playerDrawn = stillHasCubes[remainder];

        _cubes[playerDrawn]--;

        if (fullSum == 1 && _refillBagAutomatically)
        {
            FillBag();
        }
        
        return playerDrawn;
    }

    public void StopAutomaticallyRefillingBag()
    {
        _refillBagAutomatically = false;
    }
}

