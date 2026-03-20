namespace PresidentialGameEngine.ClassLibrary.Interfaces;

public interface ICardZoneComponent<TPlayer, TZone, TCard>
    where TPlayer : Enum
    where TZone : Enum
{
    
   
    public IEnumerable<TCard> GetCardsInPublicZone(TZone zone);
    
    public IEnumerable<TCard> GetCardsInPrivateZone(TZone zone, TPlayer player);

    void MoveCardFromOneZoneToAnother(TPlayer player, TCard cardToMove,
        TZone source, TZone destination);
}