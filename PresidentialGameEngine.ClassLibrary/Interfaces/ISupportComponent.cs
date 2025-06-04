using PresidentialGameEngine.ClassLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresidentialGameEngine.ClassLibrary.Interfaces
{
    public interface ISupportComponent<PlayersEnum, LeadersEnum, SubjectEnum>
        where PlayersEnum : Enum
        where LeadersEnum : Enum
        where SubjectEnum : Enum
    {
        public IDictionary<SubjectEnum, SupportContest<LeadersEnum>> GetRawData();

        public LeadersEnum GetLeader(SubjectEnum subject);

        public int GetSupportAmount(SubjectEnum subject);

        public SupportStatus<LeadersEnum> GetSupportStatus(SubjectEnum subject);

        public void GainSupport(PlayersEnum player, SubjectEnum state, int amount);

        public void LoseSupport(PlayersEnum player, SubjectEnum state, int amount);

        

    }
}
