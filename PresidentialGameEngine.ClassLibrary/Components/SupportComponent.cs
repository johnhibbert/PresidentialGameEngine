using PresidentialGameEngine.ClassLibrary.Data;
using PresidentialGameEngine.ClassLibrary.Enums;
using PresidentialGameEngine.ClassLibrary.Interfaces;

namespace PresidentialGameEngine.ClassLibrary.Components
{
    public class SupportComponent<PlayersEnum, LeadersEnum, SubjectEnum> :
        ISupportComponent<PlayersEnum, LeadersEnum, SubjectEnum>
        where PlayersEnum : Enum
        where LeadersEnum : Enum
        where SubjectEnum : Enum
         
    {
        private readonly LeadersEnum defaultLeader = (LeadersEnum) Enum.ToObject(typeof(LeadersEnum), 0);

        public Dictionary<SubjectEnum, SupportStatus<LeadersEnum>> SubjectContests { get; init; }

        public SupportComponent()
        {
            SubjectContests = [];

            var subjectValues = Enum.GetValues(typeof(SubjectEnum)).OfType<SubjectEnum>().ToList();
            var valueOfNone = (SubjectEnum)Enum.ToObject(typeof(SubjectEnum), 0);
            subjectValues.Remove(valueOfNone);

            foreach (SubjectEnum subject in subjectValues)
            {
                SubjectContests.Add(subject, new SupportStatus<LeadersEnum>());
            }
        }

        public LeadersEnum GetLeader(SubjectEnum subject) 
        {
            return SubjectContests[subject].Leader;
        }

        public int GetSupportAmount(SubjectEnum subject) 
        {
            return SubjectContests[subject].Amount;
        }

        public void GainSupport(PlayersEnum player, SubjectEnum state, int amount)
        {
            var stateContest = SubjectContests[state];

            var playerAsInt = Convert.ToInt32(player);
            var leaderAsInt = Convert.ToInt32(stateContest.Leader);

            if (playerAsInt == leaderAsInt) 
            {
                stateContest.Amount += amount;
            }
            else if(leaderAsInt != 0 && amount == stateContest.Amount) 
            {
                stateContest.Amount = 0;
                stateContest.Leader = defaultLeader;
            }
            else 
            {
                if(amount < stateContest.Amount) 
                {
                    stateContest.Amount -= amount;
                }
                else 
                {
                    stateContest.Amount = amount - stateContest.Amount;
                    stateContest.Leader = (LeadersEnum)Enum.ToObject(typeof(LeadersEnum), playerAsInt);
                }
            }
        }

        public void LoseSupport(PlayersEnum player, SubjectEnum state, int amount)
        {
            var targetContest = SubjectContests[state];

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


