using PresidentialGameEngine.ClassLibrary.Interfaces;

namespace PresidentialGameEngine.ClassLibrary.Tests
{
    public class TestStubsFakesAndMocks
    {
        public enum FakeEnumWithTwo
        {
            ElementOne,
            ElementTwo
        }

        public enum FakeEnumWithThree
        {
            ElementOne,
            ElementTwo,
            ElementThree
        }

        public enum FakeEnumWithFive
        {
            ElementOne,
            ElementTwo,
            ElementThree,
            ElementFour,
            ElementFive,
        }

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
