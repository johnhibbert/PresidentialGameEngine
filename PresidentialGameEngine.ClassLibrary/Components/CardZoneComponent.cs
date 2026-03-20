using PresidentialGameEngine.ClassLibrary.Data;
using PresidentialGameEngine.ClassLibrary.Interfaces;

namespace PresidentialGameEngine.ClassLibrary.Components;

public class CardZoneComponent<TPlayer, TZone, TCard> : ICardZoneComponent<TPlayer, TZone, TCard>
    where TPlayer : Enum
    where TZone : Enum
{
    
    private IDictionary<TZone, List<TCard>> PublicZones { get; init; }
    
    private IDictionary<TPlayer, IDictionary<TZone, List<TCard>>> PrivateZones { get; init; }


    public CardZoneComponent(HashSet<TZone> publicZones, HashSet<TZone> privateZones)
    {
        if (Enum.GetValues(typeof(TZone)).Length != (publicZones.Count + privateZones.Count))
        {
            throw new ArgumentException("Public and private zones must cover all zones.");
        }
        
        PublicZones = new Dictionary<TZone, List<TCard>>();
        PrivateZones = new Dictionary<TPlayer, IDictionary<TZone, List<TCard>>>();
        
        foreach (var zone in publicZones)
        {
            PublicZones.Add(zone, []);
        }
        
        foreach (var player in (TPlayer[])Enum.GetValues(typeof(TPlayer)))
        {
            var dict =  new Dictionary<TZone, List<TCard>>();
            foreach (var zone in privateZones)
            {
                dict.Add(zone, []);
            }
            PrivateZones.Add(player, dict);
        }

        int i = 0;

    }
    
    public IEnumerable<TCard> GetCardsInPublicZone(TZone zone)
    {
        return PublicZones[zone];
    }

    public IEnumerable<TCard> GetCardsInPrivateZone(TZone zone, TPlayer player)
    {
        return PrivateZones[player][zone];
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