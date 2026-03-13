using PresidentialGameEngine.ClassLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresidentialGameEngine.ClassLibrary.Interfaces
{
    public interface ISupportComponent<TPlayer, TLeader, TSubject>
        where TPlayer : Enum
        where TLeader : Enum
        where TSubject : Enum
    {
        public IDictionary<TSubject, SupportContest<TLeader>> GetRawData();

        public TLeader GetLeader(TSubject subject);

        //public int GetSupportAmount(SubjectEnum subject);

        public SupportStatus<TLeader> GetSupportStatus(TSubject subject);

        public void GainSupport(TPlayer player, TSubject state, int amount);

        public void LoseSupport(TPlayer player, TSubject state, int amount);

        

    }
}
