namespace PresidentialGameEngine.ClassLibrary.Interfaces;

public interface ICardZoneComponent<TZone, TPlayer, TCard>
    where TZone : Enum
    where TPlayer : Enum
{
    
    public IEnumerable<TCard> GetCardsInZone(TZone zone, TPlayer player);

    public void AddCardsToZone(IEnumerable<TCard> cards, TZone zone, TPlayer player);
    
    void MoveCardFromOneZoneToAnother(TPlayer player, TCard cardToMove,
        TZone source, TZone destination);
}