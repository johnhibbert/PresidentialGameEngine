using PresidentialGameEngine.ClassLibrary.Enums;

namespace PresidentialGameEngine.ClassLibrary.Data
{
    public class SupportChange<T>(Player player, T target, int change) where T: Enum 
    {
        public Player Player { get; internal set; } = player;

        public T Target { get; internal set; } = target;

        public int Change { get; internal set; } = change;
    }

}
