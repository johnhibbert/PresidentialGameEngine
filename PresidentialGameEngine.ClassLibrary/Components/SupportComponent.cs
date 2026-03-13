using PresidentialGameEngine.ClassLibrary.Data;
using PresidentialGameEngine.ClassLibrary.Interfaces;

namespace PresidentialGameEngine.ClassLibrary.Components
{
    public class SupportComponent<TPlayer, TLeader, TSubject> :
        ISupportComponent<TPlayer, TLeader, TSubject>
        where TPlayer : Enum
        where TLeader : Enum
        where TSubject : Enum
         
    {
        private readonly TLeader defaultLeader = (TLeader) Enum.ToObject(typeof(TLeader), 0);

        public IDictionary<TSubject, SupportContest<TLeader>> SubjectContests { get; init; }

        public IDictionary<TSubject, SupportContest<TLeader>> GetRawData() { return SubjectContests; }

        public SupportComponent()
        {
            SubjectContests = new Dictionary<TSubject, SupportContest<TLeader>>();

            var subjectValues = Enum.GetValues(typeof(TSubject)).OfType<TSubject>().ToList();
            var valueOfNone = (TSubject)Enum.ToObject(typeof(TSubject), 0);
            subjectValues.Remove(valueOfNone);

            foreach (TSubject subject in subjectValues)
            {
                SubjectContests.Add(subject, new SupportContest<TLeader>());
            }
        }

        //It's an open question if we should just make people go through the GetSupportStatus method.
        public TLeader GetLeader(TSubject subject) 
        {
            return SubjectContests[subject].Leader;
        }

        public SupportStatus<TLeader> GetSupportStatus(TSubject subject)
        {
            return new SupportStatus<TLeader>(SubjectContests[subject].Leader, SubjectContests[subject].Amount);
        }

        //public int GetSupportAmount(SubjectEnum subject) 
        //{
        //    return SubjectContests[subject].Amount;
        //}

        public void GainSupport(TPlayer player, TSubject subject, int amount)
        {
            var contest = SubjectContests[subject];

            var playerAsInt = Convert.ToInt32(player);
            var leaderAsInt = Convert.ToInt32(contest.Leader);

            if (playerAsInt == leaderAsInt) 
            {
                contest.Amount += amount;
            }
            else if(leaderAsInt != 0 && amount == contest.Amount) 
            {
                contest.Amount = 0;
                contest.Leader = defaultLeader;
            }
            else 
            {
                if(amount < contest.Amount) 
                {
                    contest.Amount -= amount;
                }
                else 
                {
                    contest.Amount = amount - contest.Amount;
                    contest.Leader = (TLeader)Enum.ToObject(typeof(TLeader), playerAsInt);
                }
            }
        }

        public void LoseSupport(TPlayer player, TSubject subject, int amount)
        {
            var targetContest = SubjectContests[subject];

            var playerAsInt = Convert.ToInt32(player);
            var leaderAsInt = Convert.ToInt32(targetContest.Leader);

            if(playerAsInt == leaderAsInt) 
            {
                targetContest.Amount -= amount;
                if (targetContest.Amount <= 0)
                {
                    targetContest.Leader = defaultLeader;
                    targetContest.Amount = 0;
                }
            }
        }


    }

}


