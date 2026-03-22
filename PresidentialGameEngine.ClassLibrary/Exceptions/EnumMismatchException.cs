namespace PresidentialGameEngine.ClassLibrary.Exceptions;

public class EnumMismatchException : ApplicationException
{
    public EnumMismatchException() : base() { }
    public EnumMismatchException(string? message) : base(message) { }
}