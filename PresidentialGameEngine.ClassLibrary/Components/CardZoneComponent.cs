using PresidentialGameEngine.ClassLibrary.Data;
using PresidentialGameEngine.ClassLibrary.Exceptions;
using PresidentialGameEngine.ClassLibrary.Interfaces;

namespace PresidentialGameEngine.ClassLibrary.Components;

public class CardZoneComponent<TZone, TPlayer, TCard>
    : ICardZoneComponent<TZone, TPlayer, TCard>
    where TZone : Enum
    where TPlayer : Enum
{

    private readonly HashSet<TZone> _privateZones;
    
    private IDictionary<TZone, List<TCard>> CardsInPublicZones { get; set; }
    
    private IDictionary<TPlayer, IDictionary<TZone, List<TCard>>> CardsInPrivateZones { get; set; }
    
    public CardZoneComponent(HashSet<TZone> privateZones)
    {
        var allZones = (TZone[])Enum.GetValues(typeof(TZone));
        var allPrivateZones = allZones.Where(x => privateZones.Contains(x)).ToList();
        var allPublicZones = allZones.Where(x => !privateZones.Contains(x)).ToList();

        var players = (TPlayer[])Enum.GetValues(typeof(TPlayer));
        
        _privateZones = privateZones;
        
        CardsInPublicZones = new Dictionary<TZone, List<TCard>>();
        CardsInPrivateZones = new Dictionary<TPlayer, IDictionary<TZone, List<TCard>>>();
        
        foreach (var zone in allPublicZones)
        {
            CardsInPublicZones.Add(zone, []);
        }
        
        foreach (var player in players)
        {
            var dict =  new Dictionary<TZone, List<TCard>>();
            foreach (var zone in allPrivateZones)
            {
                dict.Add(zone, []);
            }
            CardsInPrivateZones.Add(player, dict);
        }
    }


    public IEnumerable<TCard> GetCardsInZone(TZone zone, TPlayer player)
    {
        if (_privateZones.Contains(zone))
        {
            return CardsInPrivateZones[player][zone];
        }
        else
        {
            return CardsInPublicZones[zone];
        }
    }

    public void AddCardsToZone(IEnumerable<TCard> cards, TZone zone, TPlayer player)
    {
        if (_privateZones.Contains(zone))
        {
            CardsInPrivateZones[player][zone].AddRange(cards);
        }
        else
        {
            CardsInPublicZones[zone].AddRange(cards);
        }
    }

    public void MoveCardFromOneZoneToAnother(TPlayer player, TCard cardToMove, TZone source, TZone destination)
    {
        if (source.Equals(destination))
        {
            throw new ArgumentException("Source and destination zones cannot be the same.");
        }

        if (_privateZones.Contains(source))
        {
            CardsInPrivateZones[player][source].Remove(cardToMove);
        }
        else
        {
            CardsInPublicZones[source].Remove(cardToMove);
        }
        
        if (_privateZones.Contains(destination))
        {
            CardsInPrivateZones[player][destination].Add(cardToMove);
        }
        else
        {
            CardsInPublicZones[destination].Add(cardToMove);
        }
    }
}