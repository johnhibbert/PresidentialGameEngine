using PresidentialGameEngine.ClassLibrary.Interfaces;

namespace PresidentialGameEngine.ClassLibrary.Components
{
    public class CarriableSupportComponent<PlayersEnum, LeadersEnum, SubjectEnum> 
        : SupportComponent<PlayersEnum, LeadersEnum, SubjectEnum>,
        ICarriableSupportComponent<PlayersEnum, LeadersEnum, SubjectEnum>
        where PlayersEnum : Enum
        where LeadersEnum : Enum
        where SubjectEnum : Enum
    {
        public int Threshold { get; init; }

        public CarriableSupportComponent(int threshold = 4)
        {
            Threshold = threshold;
        }

        public bool IsCarried(SubjectEnum subject) 
        {
            return SubjectContests[subject].Amount >= Threshold;
        }
    }

    public interface ICarriableSupportComponent<PlayersEnum, LeadersEnum, SubjectEnum> : ISupportComponent<PlayersEnum, LeadersEnum, SubjectEnum>
        where PlayersEnum : Enum
        where LeadersEnum : Enum
        where SubjectEnum : Enum
    {
        public int Threshold { get; init; }
    }
}
