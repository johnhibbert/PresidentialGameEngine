using PresidentialGameEngine.ClassLibrary.Enums;

namespace PresidentialGameEngine.ClassLibrary.Interfaces
{
    public interface ICardComponent<PlayersEnum, CardClass>
        where PlayersEnum : Enum
        where CardClass : ICard
    {
        int CountCardsLeftInDeck();

        void DrawCards(PlayersEnum player, int numberToDraw);
        IEnumerable<CardClass> GetPlayerHand(PlayersEnum player);

        public IEnumerable<CardClass> ViewCardsInZone(CardZone zone, PlayersEnum player);

        void MoveCardFromOneZoneToAnother(PlayersEnum player, CardClass cardToMove,
            CardZone source, CardZone destination);
    }
}