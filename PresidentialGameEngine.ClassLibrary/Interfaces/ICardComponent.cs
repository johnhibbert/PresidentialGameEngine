using PresidentialGameEngine.ClassLibrary.Components;

namespace PresidentialGameEngine.ClassLibrary.Interfaces
{
    public interface ICardComponent<PlayersEnum, CardClass>
        where PlayersEnum : Enum
        where CardClass : class
    {
        int CountCardsLeftInDeck();
        void DiscardCardFromHand(PlayersEnum player, CardClass card);
        void DrawCards(PlayersEnum player, int numberToDraw);
        IEnumerable<CardClass> GetPlayerHand(PlayersEnum player);
        void MoveCardFromHandToCampaignStrategyPile(PlayersEnum player, CardClass card);
        void MoveCardFromHandToRemovedPile(PlayersEnum player, CardClass card);
        void RetrieveCardFromDiscardPile(PlayersEnum player, CardClass card);

        public IEnumerable<CardClass> ViewCardsInZone(CardZone zone, PlayersEnum player);
        void MoveCardFromOneZoneToAnother(PlayersEnum player, CardClass cardToMove,
            CardZone source, CardZone destination);
    }
}