using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresidentialGameEngine.ClassLibrary.Interfaces
{
    public interface IRandomnessProvider
    {
        int GetRandomNumber(int maxValue);
        int GetRandomNumber(int minValue, int maxValue);
    }
}
