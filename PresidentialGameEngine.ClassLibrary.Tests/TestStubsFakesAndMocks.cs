using PresidentialGameEngine.ClassLibrary.Interfaces;

namespace PresidentialGameEngine.ClassLibrary.Tests
{
    public class TestStubsFakesAndMocks
    {
        public class SeededRandomnessProviderForTesting : IRandomnessProvider
        {
            //Seeded for consistency
            private readonly Random Random = new(60);

            public int GetRandomNumber(int maxValue)
            {
                return Random.Next(maxValue);
            }
        }
    }
}
