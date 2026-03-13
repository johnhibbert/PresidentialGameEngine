using PresidentialGameEngine.ClassLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresidentialGameEngine.ClassLibrary.Interfaces
{
    public interface IPoliticalCapitalComponent<TPlayer>
        where TPlayer : Enum
    {
        IDictionary<TPlayer, int> Peek();

        TPlayer InitiativeCheck();

        SupportCheckResult SupportCheck(TPlayer player, int checkAmount);

        void AddCubes(TPlayer player, int amount);
    }
}
