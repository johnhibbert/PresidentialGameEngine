using PresidentialGameEngine.ClassLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
