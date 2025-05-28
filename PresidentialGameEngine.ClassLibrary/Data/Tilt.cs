namespace PresidentialGameEngine.ClassLibrary.Data
{
    public class Tilt<PlayersEnum>
    {
        public required PlayersEnum Player { get; set; }
        public int StartingSupport { get; set; }
    }
}
