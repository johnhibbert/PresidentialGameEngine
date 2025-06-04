namespace PresidentialGameEngine.ClassLibrary.Data
{
    public class SupportContest<LeadersEnum> where LeadersEnum : Enum
    {
        public int Amount { get; set; }
        public LeadersEnum Leader { get; set; }

        public SupportContest()
        {
            Leader = (LeadersEnum)Enum.ToObject(typeof(LeadersEnum), 0);
        }
    }

}


