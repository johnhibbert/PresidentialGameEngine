using PresidentialGameEngine.ClassLibrary.Data;
using PresidentialGameEngine.ClassLibrary.Interfaces;

namespace PresidentialGameEngine.ClassLibrary.Components
{
    public class PoliticalCapitalComponent<TPlayer> : IPoliticalCapitalComponent<TPlayer>
        where TPlayer : Enum
    {
        readonly Dictionary<TPlayer, int> cubes;

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

        public IDictionary<TPlayer, int> Peek() { return cubes; }

        public TPlayer InitiativeCheck()
        {
            TPlayer firstDraw = DrawCube();
            TPlayer secondDraw = DrawCube();

            if (EqualityComparer<TPlayer>.Default.Equals(firstDraw, secondDraw))
            {
                return firstDraw;
            }
            else { return DrawCube(); }
        }

        public SupportCheckResult SupportCheck(TPlayer player, int checkAmount)
        {
            int successes = 0;

            int index = 0;

            while (index < checkAmount)
            {
                if (EqualityComparer<TPlayer>.Default.Equals(DrawCube(), player))
                {
                    successes++;
                }
                index++;
            }

            return new SupportCheckResult(successes, checkAmount-successes);

        }

        public void AddCubes(TPlayer player, int amount)
        {
            cubes[player] += amount;
        }

        private TPlayer DrawCube()
        {
            int fullSum = TotalNumberOfCubesInBag;

            var num = rng.GetRandomNumber(fullSum);
            var valueFound = false;

            foreach (TPlayer val in (TPlayer[])Enum.GetValues(typeof(TPlayer)))
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
            foreach (TPlayer val in (TPlayer[])Enum.GetValues(typeof(TPlayer)))
            {
                cubes[val] = InitialCubePopulation;
            }
        }
    }
}
