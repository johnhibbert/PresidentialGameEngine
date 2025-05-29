using PresidentialGameEngine.ClassLibrary.Interfaces;

namespace PresidentialGameEngine.ClassLibrary.Components
{
    public class ExhaustionComponent<PlayersEnum> : IExhaustionComponent<PlayersEnum>
        where PlayersEnum : Enum
    {
        private Dictionary<PlayersEnum, bool> PlayerStatuses { get; init; }

        public ExhaustionComponent()
        {
            PlayerStatuses = [];

            foreach (PlayersEnum player in (PlayersEnum[])Enum.GetValues(typeof(PlayersEnum)))
            {
                PlayerStatuses.Add(player, true);
            }
        }

        public bool IsPlayerReady(PlayersEnum player)
        {
            return PlayerStatuses[player];
        }

        public void ExhaustPlayer(PlayersEnum player)
        {
            PlayerStatuses[player] = false;
        }

        public void UnexhaustPlayer(PlayersEnum player)
        {
            PlayerStatuses[player] = true;
        }
    }
}
