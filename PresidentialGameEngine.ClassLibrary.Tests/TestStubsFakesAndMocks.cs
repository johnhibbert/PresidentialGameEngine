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

        public enum FakePlayer 
        {
        
            PlayerOne = 1,
            PlayerTwo = 2,
            PlayerThree = 3,
        }

        public enum FakeLeader 
        {
            NoLeader = 0,
            Leader = 1,
            Leader2 = 2,
            Leader3 = 3,
        }

        public enum FakeSubject
        {
            NoSubject = 0,
            SubjectOne = 1,
            SubjectTwo = 2,
            SubjectThree = 3,
            SubjectFour = 4,
            SubjectFive = 5,
        }

        public enum FakeState 
        {
            Being,
            Denial,
            Mind,
            OfTheArt,
        }

        public enum FakeRegion 
        {
            North,
            SouthEast,
        }

        public enum FakeIssue
        {
            KetchupOnHotDogs,
            PepsiOrCoke,
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
