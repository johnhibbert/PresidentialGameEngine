namespace PresidentialGameEngine.ClassLibrary.Exceptions;

public class EmptyAndNonRefillableException : ApplicationException
{
    public EmptyAndNonRefillableException() : base()
    {
    }

    public EmptyAndNonRefillableException(string? message) : base(message)
    {
    }
}
    
