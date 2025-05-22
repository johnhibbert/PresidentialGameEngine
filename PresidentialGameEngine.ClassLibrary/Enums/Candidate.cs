namespace PresidentialGameEngine.ClassLibrary.Enums
{
    public enum Candidate
    {
        None = 0,
        Kennedy = 1,
        Nixon = 2,
        Both = 3,
    }

    public enum Player 
    {
        None = 0,
        Kennedy = 1,
        Nixon = 2,
    }

    public static class PlayerToOpponentExtensions
    {
        public static Player ToOpponent(this Player value)
        {
            return value switch
            {
                Player.Nixon => Player.Kennedy,
                Player.Kennedy => Player.Nixon,
                _ => throw new ArgumentException("Opponent Not Found"),
            };
        }
    }

    //https://stackoverflow.com/questions/1818131/convert-an-enum-to-another-type-of-enum
    public static class CandidateToPlayerExtensions
    {
        public static Player ToPlayer(this Candidate value)
        {
            return value switch
            {
                Candidate.Kennedy => Player.Kennedy,
                Candidate.Nixon => Player.Nixon,
                _ => throw new ArgumentException("Candidate Value Cannot Be Converted To Player"),
            };
        }
    }

    //https://stackoverflow.com/questions/1818131/convert-an-enum-to-another-type-of-enum
    public static class PlayerToCandidateExtensions
    {
        public static Candidate ToCandidate(this Player value)
        {
            return value switch
            {
                Player.Kennedy => Candidate.Kennedy,
                Player.Nixon => Candidate.Nixon,
                _ => throw new ArgumentException("Player Value Cannot Be Converted To Candidate"),
            };
        }
    }

}
