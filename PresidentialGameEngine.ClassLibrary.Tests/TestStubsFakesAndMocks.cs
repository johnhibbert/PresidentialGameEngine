using PresidentialGameEngine.ClassLibrary.Data;
using PresidentialGameEngine.ClassLibrary.Engines;
using PresidentialGameEngine.ClassLibrary.Enums;
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
            NoState = 0,
            Being = 1,
            Denial = 2,
            Mind = 3,
            OfTheArt = 4
        }

        public enum FakeRegion
        {
            North = 0,
            SouthEast = 1
        }

        public enum FakeIssue
        {
            KetchupOnHotDogs = 1,
            PepsiOrCoke = 2
        }

        public enum FakeStatus
        {
            Hungry = 0,
            Sleepy = 1,
            Bashful = 2,
        }

        public enum FakeCardZone
        {
            Time = 0,
            Phantom = 1, 
            Danger = 2,
        }

        public enum FakePublicZone
        {
            Time = 0,
            Danger = 2,
        }
        
        public enum FakePrivateZone
        {
            Phantom = 1, 
        }
        
        public class FakeCardClass : ICard
        {
            public int Index { get; init; }

            public string Title { get; init; }

            public string Text { get; init; }

            public int CampaignPoints { get; init; }

            public int RestCubes
            {
                get { return 4 - CampaignPoints; }
            }

            public Issue Issue { get; init; }

            public Affiliation Affiliation { get; init; }

            public State State { get; init; }

            public EventType EventType { get; init; }

            public bool RequiresPlayerInput { get; init; }

            public Predicate<PlayerChosenChanges<Player, Issue, State, Region>> AreChangesValid { get; init; }

            public Action<NineteenSixtyGameEngine, Player, PlayerChosenChanges<Player, Issue, State, Region>> Event { get; init; }

            public override string ToString()
            {
                return $"{Title} [{Index}]";
            }

            public string ToLongString()
            {
                return $"{ToString()}: {Text}";
            }
        };

        public static readonly Dictionary<int, ICard> FakeManifest = new Dictionary<int, ICard>()
        {
            {1, new FakeCardClass() { Index = 1 } },
            {2, new FakeCardClass() { Index = 2 } },
            {3, new FakeCardClass() { Index = 3 } },
            {4, new FakeCardClass() { Index = 4 } },
            {5, new FakeCardClass() { Index = 5 } },
        };


        public class SeededRandomnessProviderForTesting : IRandomnessProvider
        {
            //Seeded for consistency
            private readonly Random Random = new(60);

            public int GetRandomNumber(int maxValue)
            {
                return Random.Next(maxValue);
            }

            public int GetRandomNumber(int minValue, int maxValue)
            {
                return Random.Next(minValue, maxValue);
            }
        }
    }
}
