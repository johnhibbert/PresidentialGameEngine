using PresidentialGameEngine.ClassLibrary.Interfaces;

namespace PresidentialGameEngine.ClassLibrary.Extensions;

public static class EnumerableExtensions
{
    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, IRandomnessProvider rng)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(rng);

        return source.ShuffleIterator(rng);
    }

    //https://stackoverflow.com/questions/33643104/shuffling-a-stackt
    //'Borrowed' from here.
    //https://stackoverflow.com/questions/17530306/getting-random-numbers-from-a-list-of-integers
    //Fisher-Yates Shuffle
    
    private static IEnumerable<T> ShuffleIterator<T>(
        this IEnumerable<T> source, IRandomnessProvider rng)
    {
        List<T> buffer = source.ToList();
        for (int i = 0; i < buffer.Count; i++)
        {
            int j = rng.GetRandomNumber(i, buffer.Count);
            yield return buffer[j];

            buffer[j] = buffer[i];
        }
    }
}