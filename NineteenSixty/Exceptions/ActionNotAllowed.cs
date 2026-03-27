namespace NineteenSixty.Exceptions;

public class ActionNotAllowed :  ApplicationException
{
    public ActionNotAllowed() : base() { }
    public ActionNotAllowed(string? message) : base(message) { }
}