namespace NineteenSixty.Exceptions;

public class InvalidPlayerChoices :  ApplicationException
{
    public InvalidPlayerChoices() : base() { }
    public InvalidPlayerChoices(string? message) : base(message) { }
}