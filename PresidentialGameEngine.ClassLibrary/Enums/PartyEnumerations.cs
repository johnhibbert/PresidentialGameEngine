namespace PresidentialGameEngine.ClassLibrary.Enums
{
    /// <summary>
    /// Affiliation is the party symbol found on a card
    /// The donkey, elephant, both symbols
    /// or none for the Gathering Momentum cards
    /// </summary>
    public enum Affiliation
    {
        None = 0,
        Kennedy = 1,
        Nixon = 2,
        Both = 3,
    }

    /// <summary>
    /// The leader in a specific contest, issue, state, etc
    /// This can be none at any given moment.
    /// </summary>
    public enum Leader 
    {
        None = 0,
        Kennedy = 1,
        Nixon = 2,
    }

    /// <summary>
    /// Player refers strictly to the two players
    /// with no option for none
    /// </summary>
    public enum Player
    {
        Kennedy = 1,
        Nixon = 2,
    }

    public static class PartyEnumerationExtensions
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

        //Remove me later? It's unclear if this will have use.
        public static Leader ToOpponent(this Leader value)
        {
            return value switch
            {
                Leader.Nixon => Leader.Kennedy,
                Leader.Kennedy => Leader.Nixon,
                _ => throw new ArgumentException("Opponent Not Found"),
            };
        }




        public static Leader ToLeader(this Player value)
        {
            return value switch
            {
                Player.Kennedy => Leader.Kennedy,
                Player.Nixon => Leader.Nixon,
                _ => throw new ArgumentException("Candidate Value Cannot Be Converted To Player"),
            };
        }



        public static Player ToPlayer(this Leader value)
        {
            return value switch
            {
                Leader.Kennedy => Player.Kennedy,
                Leader.Nixon => Player.Nixon,
                _ => throw new ArgumentException("Candidate Value Cannot Be Converted To Player"),
            };
        }

    }

    //https://stackoverflow.com/questions/1818131/convert-an-enum-to-another-type-of-enum
    public static class CandidateToPlayerExtensions
    {
        public static Leader ToPlayer(this Affiliation value)
        {
            return value switch
            {
                Affiliation.Kennedy => Leader.Kennedy,
                Affiliation.Nixon => Leader.Nixon,
                _ => throw new ArgumentException("Candidate Value Cannot Be Converted To Player"),
            };
        }
    }

    //https://stackoverflow.com/questions/1818131/convert-an-enum-to-another-type-of-enum
    public static class PlayerToCandidateExtensions
    {
        public static Affiliation ToCandidate(this Leader value)
        {
            return value switch
            {
                Leader.Kennedy => Affiliation.Kennedy,
                Leader.Nixon => Affiliation.Nixon,
                _ => throw new ArgumentException("Player Value Cannot Be Converted To Candidate"),
            };
        }
    }

}
