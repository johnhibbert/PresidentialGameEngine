namespace PresidentialGameEngine.ClassLibrary.Data
{
    public class SupportStatus<LeadersEnum> where LeadersEnum : Enum
    {
        public int Amount { get; set; }
        public LeadersEnum Leader { get; set; }

        public SupportStatus()
        {
            Leader = (LeadersEnum)Enum.ToObject(typeof(LeadersEnum), 0);
        }
    }

}


