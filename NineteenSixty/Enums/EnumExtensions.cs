namespace NineteenSixty.Enums
{
    // I have not made these with generics, since a theoretical 
    // multiplayer version would not have a singluar opponent.
    // These work for 1960 only for now.
    public static class PresidentialEnumerationExtensions
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
