using PresidentialGameEngine.ClassLibrary.Interfaces;

namespace PresidentialGameEngine.ClassLibrary.Randomness
{
    public class DefaultRandomnessProvider : IRandomnessProvider
    {
        private readonly int _seed;
        private readonly Random _random;

        public DefaultRandomnessProvider(int seed = 0)
        {
            _seed = seed;
            if (_seed == 0)
            {
                _random = new Random();
            }
            else
            {
                _random = new Random(_seed);
            }
        }

        public int GetSeed()
        {
            return _seed;
        }

        public int GetRandomNumber(int maxValue)
        {
            return _random.Next(maxValue);
        }

        public int GetRandomNumber(int minvalue, int maxValue)
        {
            return _random.Next(minvalue, maxValue);
        }
    }
}
