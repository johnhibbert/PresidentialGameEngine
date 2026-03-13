namespace PresidentialGameEngine.ClassLibrary.Data
{
    public class SupportContest<TLeader> where TLeader : Enum
    {
        public int Amount { get; set; }
        public TLeader Leader { get; set; }

        public SupportContest()
        {
            Leader = (TLeader)Enum.ToObject(typeof(TLeader), 0);
        }
    }

}


