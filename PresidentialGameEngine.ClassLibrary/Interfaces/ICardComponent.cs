namespace PresidentialGameEngine.ClassLibrary.Interfaces
{
    public interface ICardComponent<PlayersEnum, CardClass>
        where PlayersEnum : Enum
        where CardClass : class
    {
        int CountCardsLeftInDeck();
        void DiscardCardFromHand(PlayersEnum player, CardClass card);
        void DrawCards(PlayersEnum player, int numberToDraw);
        IEnumerable<CardClass> LookAtDiscardPile();
        IEnumerable<CardClass> LookAtPlayerCampaignStrategyPile(PlayersEnum player);
        IEnumerable<CardClass> LookAtPlayerHand(PlayersEnum player);
        IEnumerable<CardClass> LookAtRemovedPile();
        void MoveCardFromHandToCampaignStrategyPile(PlayersEnum player, CardClass card);
        void MoveCardFromHandToRemovedPile(PlayersEnum player, CardClass card);
        void RetrieveCardFromDiscardPile(PlayersEnum player, CardClass card);
    }
}