using PresidentialGameEngine.ClassLibrary.Interfaces;

namespace PresidentialGameEngine.ClassLibrary.Components
{
    public class CarriableSupportComponent<TPlayer, TLeader, TSubject> 
        : SupportComponent<TPlayer, TLeader, TSubject>,
        ICarriableSupportComponent<TPlayer, TLeader, TSubject>
        where TPlayer : Enum
        where TLeader : Enum
        where TSubject : Enum
    {
        public int Threshold { get; init; }

        public CarriableSupportComponent(int threshold = 4)
        {
            if (threshold < 1) { throw new ArgumentException("Threshold must be greater than 1"); };
            Threshold = threshold;
        }

        public bool IsCarried(TSubject subject) 
        {
            return SubjectContests[subject].Amount >= Threshold;
        }
    }

    public interface ICarriableSupportComponent<TPlayer, TLeader, TSubject> : ISupportComponent<TPlayer, TLeader, TSubject>
        where TPlayer : Enum
        where TLeader : Enum
        where TSubject : Enum
    {
        public int Threshold { get; init; }

        bool IsCarried(TSubject subject);
    }
}
