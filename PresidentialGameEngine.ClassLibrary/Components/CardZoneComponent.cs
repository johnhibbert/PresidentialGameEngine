using PresidentialGameEngine.ClassLibrary.Data;
using PresidentialGameEngine.ClassLibrary.Interfaces;

namespace PresidentialGameEngine.ClassLibrary.Components;

public class CardZoneComponent<TZone, TPublicZone, TPrivateZone, TPlayer, TCard>
    : ICardZoneComponent<TZone, TPublicZone, TPrivateZone, TPlayer, TCard>
    where TZone : Enum
    where TPublicZone : Enum
    where TPrivateZone : Enum
    where TPlayer : Enum
{
    
    private IDictionary<TPublicZone, List<TCard>> PublicZones { get; init; }
    
    private IDictionary<TPlayer, IDictionary<TPrivateZone, List<TCard>>> PrivateZones { get; init; }


    //public CardZoneComponent(HashSet<TPublicZone> publicZones, HashSet<TPrivateZone> privateZones)
    public CardZoneComponent()
    {
        PublicZones = new Dictionary<TPublicZone, List<TCard>>();
        PrivateZones = new Dictionary<TPlayer, IDictionary<TPrivateZone, List<TCard>>>();
        
        foreach (var zone in (TPublicZone[])Enum.GetValues(typeof(TPublicZone)))
        {
            PublicZones.Add(zone, []);
        }
        
        foreach (var player in (TPlayer[])Enum.GetValues(typeof(TPlayer)))
        {
            var dict =  new Dictionary<TPrivateZone, List<TCard>>();
            foreach (var zone in (TPrivateZone[])Enum.GetValues(typeof(TPrivateZone)))
            {
                dict.Add(zone, []);
            }
            PrivateZones.Add(player, dict);
        }
    }
    
    // public IEnumerable<TCard> GetCardsInPublicZone(TZone zone)
    // {
    //     return PublicZones[zone];
    // }
    //
    // public IEnumerable<TCard> GetCardsInPrivateZone(TZone zone, TPlayer player)
    // {
    //     return PrivateZones[player][zone];
    // }

    // public IEnumerable<TCard> GetCardsInZone(TZone zone)
    // {
    //     throw new NotImplementedException();
    // }

    public IEnumerable<TCard> GetCardsInPublicZone(TPublicZone publicZone)
    {
        return PublicZones[publicZone];
    }

    public IEnumerable<TCard> GetCardsInPrivateZone(TPrivateZone privateZone, TPlayer player)
    {
        return PrivateZones[player][privateZone];
    }

    public void MoveCardFromOneZoneToAnother(TPlayer player, TCard cardToMove, TZone source, TZone destination)
    {
        throw new NotImplementedException();
    }
    
    
    //Shared locations: Deck, Discard, Removed (Played events), Prevention, Debate, ElectionDay
    //Non-Shared: Hand, Campaign Strategy Pile
    
    /*
     *     public enum CardZone
       {
           Deck,
           Hand,
           Discard,
           Removed,
           CampaignStrategy
       }
     */
    
    
}