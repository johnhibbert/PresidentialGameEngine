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
        PlayersEnum InitiativeCheck();

        int SupportCheck(PlayersEnum player, int checkAmount);

        void AddCubes(PlayersEnum player, int amount);
    }
}
