using PresidentialGameEngine.ClassLibrary.Data;
using PresidentialGameEngine.ClassLibrary.Interfaces;

namespace PresidentialGameEngine.ClassLibrary.Components
{
    public class PoliticalCapitalComponent<PlayersEnum> : IPoliticalCapitalComponent<PlayersEnum>
        where PlayersEnum : Enum
    {
        readonly Dictionary<PlayersEnum, int> cubes;

        readonly IRandomnessProvider rng;

        readonly int InitialCubePopulation;

        public PoliticalCapitalComponent(IRandomnessProvider random, int initialPopulationPerPlayer)
        {
            rng = random;
            InitialCubePopulation = initialPopulationPerPlayer;
            cubes = [];

            RefillBag();
        }

        public int TotalNumberOfCubesInBag
        {
            get
            {
                return cubes.Values.Sum();
            }
        }

        public IDictionary<PlayersEnum, int> Peek() { return cubes; }

        public PlayersEnum InitiativeCheck()
        {
            PlayersEnum firstDraw = DrawCube();
            PlayersEnum secondDraw = DrawCube();

            if (EqualityComparer<PlayersEnum>.Default.Equals(firstDraw, secondDraw))
            {
                return firstDraw;
            }
            else { return DrawCube(); }
        }

        public SupportCheckResult SupportCheck(PlayersEnum player, int checkAmount)
        {
            int successes = 0;

            int index = 0;

            while (index < checkAmount)
            {
                if (EqualityComparer<PlayersEnum>.Default.Equals(DrawCube(), player))
                {
                    successes++;
                }
                index++;
            }

            return new SupportCheckResult(successes, checkAmount-successes);

        }

        public void AddCubes(PlayersEnum player, int amount)
        {
            cubes[player] += amount;
        }

        private PlayersEnum DrawCube()
        {
            int fullSum = TotalNumberOfCubesInBag;

            var num = rng.GetRandomNumber(fullSum);
            var valueFound = false;

            foreach (PlayersEnum val in (PlayersEnum[])Enum.GetValues(typeof(PlayersEnum)))
            {
                num -= cubes[val];
                if (valueFound == false && num <= 0)
                {
                    cubes[val]--;

                    if (TotalNumberOfCubesInBag == 0)
                    {
                        RefillBag();
                    }

                    return val;
                }
            }

            //What kind should this be?  This shouldn't be possible, at first glance.
            throw new Exception();
        }

        private void RefillBag()
        {
            foreach (PlayersEnum val in (PlayersEnum[])Enum.GetValues(typeof(PlayersEnum)))
            {
                cubes[val] = InitialCubePopulation;
            }
        }
    }
}
