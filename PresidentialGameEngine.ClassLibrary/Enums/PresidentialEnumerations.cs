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
    /// This can be none at any given moment but not both.
    /// </summary>
    public enum Leader 
    {
        None = 0,
        Kennedy = 1,
        Nixon = 2,
    }

    /// <summary>
    /// Player refers strictly to the two players
    /// with no option for none or both.
    /// </summary>
    public enum Player
    {
        Kennedy = 1,
        Nixon = 2,
    }
}
