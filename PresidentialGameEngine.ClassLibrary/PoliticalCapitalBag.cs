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
                cubes.Add(val, InitialCubePopulation);
            }
        }

        public void AddCubes(T candidate, int amount) 
        {
            cubes[candidate] += amount;
        }

        public T InitiativeCheck()
        {
            throw new NotImplementedException();
            //var drawOne = 
        }

        public T DrawCube() 
        {
            //Hmm how to iterate over this and get what we expect.

            //T ReturnValue;

            int fullSum = 0;

            foreach (T val in (T[])Enum.GetValues(typeof(T)))
            {
                fullSum += cubes[val];
            }

            var num = rng.GetRandomNumber(fullSum);
            var valueFound = false;

            foreach (T val in (T[])Enum.GetValues(typeof(T)))
            {
                num -= cubes[val];
                if (valueFound == false && num <= 0) 
                {
                    cubes[val]--;
                    return val;
                }
            }

            //What kind should this be?  This shouldn't be possible, at first glance.
            throw new Exception();
           // return ReturnValue;

        }

        //public Candidate DrawCube() 
        //{
        //    //var drawnCube = rng.Next(NixonCubes + KennedyCubes) < NixonCubes ? Candidate.Nixon : Candidate.Kennedy;



        //    //I think this would work?
        //    //return rng.Next(NixonCubes + KennedyCubes) < NixonCubes ? Candidate.Nixon: Candidate.Kennedy;

        //}


    }
}
