namespace PresidentialGameEngine.ClassLibrary.Data
{
    public record SupportStatus<TLeader>(TLeader Leader, int Support)
        where TLeader : Enum
    { }
}
