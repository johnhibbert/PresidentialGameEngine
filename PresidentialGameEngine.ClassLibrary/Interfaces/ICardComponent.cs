using PresidentialGameEngine.ClassLibrary.Enums;

namespace PresidentialGameEngine.ClassLibrary.Interfaces
{
    public interface ICardComponent<TPlayer, TCard>
        where TPlayer : Enum
        where TCard : ICard
    {
        int CountCardsLeftInDeck();

        void DrawCards(TPlayer player, int numberToDraw);
        IEnumerable<TCard> GetPlayerHand(TPlayer player);

        public IEnumerable<TCard> ViewCardsInZone(CardZone zone, TPlayer player);

        void MoveCardFromOneZoneToAnother(TPlayer player, TCard cardToMove,
            CardZone source, CardZone destination);
    }
}