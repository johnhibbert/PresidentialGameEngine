using PresidentialGameEngine.ClassLibrary.Data;
using PresidentialGameEngine.ClassLibrary.Exceptions;
using PresidentialGameEngine.ClassLibrary.Interfaces;

namespace PresidentialGameEngine.ClassLibrary.Components;

public class CardZoneComponent<TZone, TPublicZone, TPrivateZone, TPlayer, TCard>
    : ICardZoneComponent<TZone, TPublicZone, TPrivateZone, TPlayer, TCard>
    where TZone : Enum
    where TPublicZone : Enum
    where TPrivateZone : Enum
    where TPlayer : Enum
{
    
    //These don't need to be saved as their enum
    //private TPublicZone[] _publicZones;
    //private TPrivateZone[] _privateZones;

    private List<int> _publicZoneInts;
    private List<int> _privateZoneInts;
    
    private IDictionary<TPublicZone, List<TCard>> CardsInPublicZones { get; set; }
    
    private IDictionary<TPlayer, IDictionary<TPrivateZone, List<TCard>>> CardInPrivateZones { get; set; }
    
    public CardZoneComponent()
    {
        ExtractAndValidateValuesFromZoneEnums();

        PopulateLocalDictionaries();
    }

    private void ExtractAndValidateValuesFromZoneEnums()
    {
        _publicZoneInts = [];
        _privateZoneInts = [];
        
        var publicZones = (TPublicZone[])Enum.GetValues(typeof(TPublicZone));
        var privateZones = (TPrivateZone[])Enum.GetValues(typeof(TPrivateZone));
        var allZones = (TZone[])Enum.GetValues(typeof(TZone));

        Dictionary<int, string> publicAndPrivateZonesInOneDictionary = new();
        Dictionary<int, string> allZonesAsDictionary = new();

        try
        {
            foreach (var publicZone in publicZones)
            {
                var asInt = Convert.ToInt32(publicZone);
                _publicZoneInts.Add(asInt);
                publicAndPrivateZonesInOneDictionary.Add(asInt, publicZone.ToString());
            }
        
            foreach (var privateZone in privateZones)
            {
                var asInt = Convert.ToInt32(privateZone);
                _privateZoneInts.Add(asInt);
                publicAndPrivateZonesInOneDictionary.Add(Convert.ToInt32(privateZone), privateZone.ToString());
            }
        }
        catch(ArgumentException ex)
        {
            const string message = "The TPublicZone and TPrivateZone cannot contain any overlapping numbers.";
            throw new EnumMismatchException(message);
        }

        if (publicAndPrivateZonesInOneDictionary.Values.Distinct().Count() !=
            publicAndPrivateZonesInOneDictionary.Count)
        {
            const string message = "The TPublicZone and TPrivateZone cannot contain any overlapping constant names.";
            throw new EnumMismatchException(message);
        }
        
        foreach (var zone in allZones)
        {
            allZonesAsDictionary.Add(Convert.ToInt32(zone), zone.ToString());
        }

        //https://stackoverflow.com/questions/9547351/how-to-compare-two-dictionaries-in-c-sharp
        var dictionariesIdentical = allZonesAsDictionary.OrderBy(kvp => kvp.Key)
            .SequenceEqual(publicAndPrivateZonesInOneDictionary.OrderBy(kvp => kvp.Key));

        if (!dictionariesIdentical)
        {
            const string message = "The TZone Enum must contain only and exactly the same numbers and values as the TPublicZone and TPrivateZone combined with no others.";
            throw new EnumMismatchException(message);
        }
    }

    private void PopulateLocalDictionaries()
    {
        CardsInPublicZones = new Dictionary<TPublicZone, List<TCard>>();
        CardInPrivateZones = new Dictionary<TPlayer, IDictionary<TPrivateZone, List<TCard>>>();
        
        foreach (var zone in (TPublicZone[])Enum.GetValues(typeof(TPublicZone)))
        {
            CardsInPublicZones.Add(zone, []);
        }
        
        foreach (var player in (TPlayer[])Enum.GetValues(typeof(TPlayer)))
        {
            var dict =  new Dictionary<TPrivateZone, List<TCard>>();
            foreach (var zone in (TPrivateZone[])Enum.GetValues(typeof(TPrivateZone)))
            {
                dict.Add(zone, []);
            }
            CardInPrivateZones.Add(player, dict);
        }
    }
    
    
    public IEnumerable<TCard> GetCardsInPublicZone(TPublicZone publicZone)
    {
        return CardsInPublicZones[publicZone];
    }

    public IEnumerable<TCard> GetCardsInPrivateZone(TPrivateZone privateZone, TPlayer player)
    {
        return CardInPrivateZones[player][privateZone];
    }

    public void AddCardsToPublicZone(IEnumerable<TCard> cards, TPublicZone publicZone)
    {
        CardsInPublicZones[publicZone].AddRange(cards);
    }

    public void AddCardsToPrivateZone(IEnumerable<TCard> cards, TPrivateZone privateZone, TPlayer player)
    {
        CardInPrivateZones[player][privateZone].AddRange(cards);
    }


    public void MoveCardFromOneZoneToAnother(TPlayer player, TCard cardToMove, TZone source, TZone destination)
    {
        if (source.Equals(destination))
        {
            throw new ArgumentException("Source and destination cannot be the same.");
        }

        /*
        _publicZones = (TPublicZone[])Enum.GetValues(typeof(TPublicZone));
        _privateZones = (TPrivateZone[])Enum.GetValues(typeof(TPrivateZone));
        */

        var fff = CardsInPublicZones.Keys;

        var sourceInt = Convert.ToInt32(source);
        var destinationInt = Convert.ToInt32(destination);

        bool sourceIsPublic = _publicZoneInts.Contains(sourceInt);
        bool destinationIsPublic = _publicZoneInts.Contains(destinationInt);


        if (sourceIsPublic && destinationIsPublic)
        {

            // if (Enum.IsDefined(typeof(TPublicZone), sourceInt))
            // {
            //     var sourceAsPublic = (TPublicZone)1;
            // }
            //
            // //var sourceAsPublic = (TPublicZone)1;
            //
            // //Enum.Parse<TPublicZone>(sourceInt, out TPublicZone sourceAsPublic);
            // CardsInPublicZones[sourceAsPublic].Remove(cardToMove);
        }
        else if (sourceIsPublic && !destinationIsPublic)
        {
            
        }
        else if (!sourceIsPublic && destinationIsPublic)
        {
            
        }
        else if (!sourceIsPublic && destinationIsPublic)
        {
            
        }
            
        
        //if(fff.Contains(source))
        
        TPrivateZone asPrivate = (TPrivateZone)Enum.ToObject(typeof(TPrivateZone), Convert.ToInt32(source));
        TPublicZone asPublic  = (TPublicZone)Enum.ToObject(typeof(TPublicZone), Convert.ToInt32(source));
        
        
        /* ORIGINAL
          if(source == destination) 
                      {
                          throw new ArgumentException("Source and destination cannot be the same");
                      }

                      var originList = source switch
                      {
                          CardZone.Removed => throw new ArgumentException("Cards can never be taken from Removed Zone."),
                          CardZone.Deck => Deck,
                          CardZone.Discard => DiscardPile,
                          CardZone.Hand => PlayerHands[player],
                          CardZone.CampaignStrategy => PlayerCampaignStrategyPiles[player],
                          _ => throw new ArgumentException("Card source not understood."),
                      };
                      bool isCardInSource = originList.Contains(cardToMove);

                      List<TCard> targetList = destination switch
                      {
                          CardZone.Deck => throw new ArgumentException("Cards can never be returned to the Deck."),
                          CardZone.Removed => RemovedFromGame,
                          CardZone.Discard => DiscardPile,
                          CardZone.Hand => PlayerHands[player],
                          CardZone.CampaignStrategy => PlayerCampaignStrategyPiles[player],
                          _ => throw new ArgumentException("Card destination not understood."),
                      };

                      if (isCardInSource == false) 
                      {
                          throw new CardNotFoundException("Card not in source.");
                      }
                      else
                      {
                          originList.Remove(cardToMove);
                          targetList.Add(cardToMove);
                      }
         */
        
        
        
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