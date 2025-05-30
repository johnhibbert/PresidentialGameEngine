namespace PresidentialGameEngine.ClassLibrary.Exceptions
{
    public class CardNotFoundException : ArgumentException
    {
        public CardNotFoundException() : base() { }
        public CardNotFoundException(string? message) : base(message) { }
    }
}
