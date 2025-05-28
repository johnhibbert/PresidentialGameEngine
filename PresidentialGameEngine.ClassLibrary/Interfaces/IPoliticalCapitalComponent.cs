using PresidentialGameEngine.ClassLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresidentialGameEngine.ClassLibrary.Interfaces
{
    public interface IPoliticalCapitalComponent<PlayersEnum>
        where PlayersEnum : Enum
    {
        IDictionary<PlayersEnum, int> Peek();

        PlayersEnum InitiativeCheck();

        SupportCheckResult SupportCheck(PlayersEnum player, int checkAmount);

        void AddCubes(PlayersEnum player, int amount);
    }
}
