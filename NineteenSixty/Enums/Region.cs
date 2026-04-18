namespace NineteenSixty.Enums
{
    public enum Region
    {
        East = 1,
        Midwest = 2,
        South = 3,
        West = 4,
    }

    public enum Endorsement
    {
        Major = 0,
        East = 1,
        Midwest = 2,
        South = 3,
        West = 4,
    }

    public static class RegionalEnumerationExtensions
    {
        public static Region ToRegion(this Endorsement value)
        {
            return value switch
            {
                Endorsement.East => Region.East,
                Endorsement.Midwest => Region.Midwest,
                Endorsement.South => Region.South,
                Endorsement.West => Region.West,
                _ => throw new ArgumentException("Endorsement Value Cannot Be Converted To Region"),
            };
        }
    }
}
