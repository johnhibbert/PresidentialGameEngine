using PresidentialGameEngine.ClassLibrary.Enums;
using PresidentialGameEngine.ClassLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PresidentialGameEngine.ClassLibrary
{
    public  class PoliticalCapitalBag<T> where T : Enum
    {
        Dictionary<T, int> cubes;

        readonly IRandomnessProvider rng;

        readonly int InitialCubePopulation;

        public PoliticalCapitalBag(IRandomnessProvider random, int initialPopulation)
        {
            rng = random;
            InitialCubePopulation = initialPopulation;
            cubes = [];

            RefillBag();
        }

        public int TotalCubesInBag
        {
            get 
            {
                return cubes.Values.Sum();
            }
        }


        private void RefillBag() 
        {

            foreach (T val in (T[])Enum.GetValues(typeof(T)))
            {
                cubes[val] = InitialCubePopulation;
            }
        }

        public void AddCubes(T candidate, int amount) 
        {
            cubes[candidate] += amount;
        }

        public T InitiativeCheck()
        {
            T firstDraw = DrawCube();
            T secondDraw = DrawCube();

            if(EqualityComparer<T>.Default.Equals(firstDraw, secondDraw))
            {
                return firstDraw;
            }
            else { return DrawCube();}
        }

        public T DrawCube() 
        {
            int fullSum = TotalCubesInBag;

            var num = rng.GetRandomNumber(fullSum);
            var valueFound = false;

            foreach (T val in (T[])Enum.GetValues(typeof(T)))
            {
                num -= cubes[val];
                if (valueFound == false && num <= 0) 
                {
                    cubes[val]--;

                    if(TotalCubesInBag == 0) 
                    {
                        RefillBag();
                    }

                    return val;
                }
            }

            //What kind should this be?  This shouldn't be possible, at first glance.
            throw new Exception();

        }
    }
}
