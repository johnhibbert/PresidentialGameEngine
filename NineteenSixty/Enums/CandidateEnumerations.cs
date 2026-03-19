namespace NineteenSixty.Enums
{
    ///These three enumerations are related and reflect 'candidates' in different ways
    ///To keep conversions simple, the enum numbers should be shared across concepts.
    ///Kennedy is always 1, Nixon is always 2, etc.

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
    /// The leader in a specific contest, issue, state, etc.
    /// This can be none at any given moment but not both.
    /// </summary>
    public enum Leader 
    {
        None = 0,
        Kennedy = 1,
        Nixon = 2,
    }

    /// <summary>
    /// Player refers strictly to the two players of the game
    /// with no option for none or both.
    /// </summary>
    public enum Player
    {
        Kennedy = 1,
        Nixon = 2,
    }
    
    /// <summary>
    /// These are methods used to convert and swap these enums with each other
    /// Most of the time this is to get a player's opponent or get a player from a leader
    /// </summary>
    public static class CandidateEnumerationExtensions
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

        public static Leader ToOpponent(this Leader value)
        {
            return value switch
            {
                Leader.Nixon => Leader.Kennedy,
                Leader.Kennedy => Leader.Nixon,
                _ => throw new ArgumentException("Opponent Not Found"),
            };
        }

        public static Affiliation ToOpponent(this Affiliation value)
        {
            return value switch
            {
                Affiliation.Nixon => Affiliation.Kennedy,
                Affiliation.Kennedy => Affiliation.Nixon,
                _ => throw new ArgumentException("Opponent Not Found"),
            };
        }

        public static Player ToPlayer(this Leader value)
        {
            return value switch
            {
                Leader.Kennedy => Player.Kennedy,
                Leader.Nixon => Player.Nixon,
                _ => throw new ArgumentException("Leader Value Cannot Be Converted To Player"),
            };
        }

        public static Player ToPlayer(this Affiliation value)
        {
            return value switch
            {
                Affiliation.Kennedy => Player.Kennedy,
                Affiliation.Nixon => Player.Nixon,
                _ => throw new ArgumentException("Affiliation Value Cannot Be Converted To Player"),
            };
        }

        public static Leader ToLeader(this Player value)
        {
            return value switch
            {
                Player.Kennedy => Leader.Kennedy,
                Player.Nixon => Leader.Nixon,
                _ => throw new ArgumentException("Player Value Cannot Be Converted To Player"),
            };
        }

        public static Leader ToLeader(this Affiliation value)
        {
            return value switch
            {
                Affiliation.Kennedy => Leader.Kennedy,
                Affiliation.Nixon => Leader.Nixon,
                _ => throw new ArgumentException("Affiliation Value Cannot Be Converted To Player"),
            };
        }

        public static Affiliation ToAffiliation(this Player value)
        {
            return value switch
            {
                Player.Kennedy => Affiliation.Kennedy,
                Player.Nixon => Affiliation.Nixon,
                _ => throw new ArgumentException("Player Value Cannot Be Converted To Player"),
            };
        }

        public static Affiliation ToAffiliation(this Leader value)
        {
            return value switch
            {
                Leader.Kennedy => Affiliation.Kennedy,
                Leader.Nixon => Affiliation.Nixon,
                _ => throw new ArgumentException("Leader Value Cannot Be Converted To Player"),
            };
        }
    }
    
}
