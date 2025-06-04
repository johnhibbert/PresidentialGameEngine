namespace PresidentialGameEngine.ClassLibrary.Data
{
    public record SupportStatus<LeadersEnum>(LeadersEnum Leader, int Support)
        where LeadersEnum : Enum
    { }
}
