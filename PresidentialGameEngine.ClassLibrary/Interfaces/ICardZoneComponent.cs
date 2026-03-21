namespace PresidentialGameEngine.ClassLibrary.Interfaces;

public interface ICardZoneComponent<TZone, TPublicZone, TPrivateZone, TPlayer, TCard>
    where TZone : Enum
    where TPublicZone : Enum
    where TPrivateZone : Enum
    where TPlayer : Enum
{
    public IEnumerable<TCard> GetCardsInPublicZone(TPublicZone publicZone);
    
    public IEnumerable<TCard> GetCardsInPrivateZone(TPrivateZone privateZone, TPlayer player);

    public void AddCardToPublicZone(TCard card, TPublicZone publicZone);
    
    public void AddCardPrivateZone(TCard card, TPrivateZone privateZone, TPlayer player);
    
    void MoveCardFromOneZoneToAnother(TPlayer player, TCard cardToMove,
        TZone source, TZone destination);
}