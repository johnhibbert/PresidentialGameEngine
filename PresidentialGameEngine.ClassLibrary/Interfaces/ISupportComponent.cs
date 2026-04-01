using PresidentialGameEngine.ClassLibrary.Data;

namespace PresidentialGameEngine.ClassLibrary.Interfaces
{
    /// <summary>
    /// ISupportComponent represents a game aspect where players control removes opponents control leading to one leader
    /// For 1960, this would cover state contests, issue contests, media support and endorsements.
    /// </summary>
    /// <typeparam name="TPlayer">The player enumeration</typeparam>
    /// <typeparam name="TLeader">The leader enumeration</typeparam>
    /// <typeparam name="TSubject">The subject enumeration being contested.</typeparam>
    public interface ISupportComponent<TPlayer, TLeader, TSubject>
        where TPlayer : Enum
        where TLeader : Enum
        where TSubject : Enum
    {
        public IDictionary<TSubject, SupportContest<TLeader>> GetRawData();

        public TLeader GetLeader(TSubject subject);
        
        public SupportStatus<TLeader> GetSupportStatus(TSubject subject);

        public void GainSupport(TPlayer player, TSubject state, int amount);

        public void LoseSupport(TPlayer player, TSubject state, int amount);

    }
}
