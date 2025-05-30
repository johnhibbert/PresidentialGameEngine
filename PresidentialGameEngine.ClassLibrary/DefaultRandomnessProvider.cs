using PresidentialGameEngine.ClassLibrary.Interfaces;

namespace PresidentialGameEngine.ClassLibrary
{
    public class DefaultRandomnessProvider : IRandomnessProvider
    {
        private readonly Random Random;

        public DefaultRandomnessProvider(int seed = 0)
        {
            if (seed == 0)
            {
                Random = new Random();
            }
            else 
            {
                Random = new Random(seed);
            }
        }

        public int GetRandomNumber(int maxValue)
        {
            return Random.Next(maxValue);
        }

        public int GetRandomNumber(int minvalue, int maxValue)
        {
            return Random.Next(minvalue, maxValue);
        }
    }
}
