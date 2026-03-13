using PresidentialGameEngine.ClassLibrary.Enums;
using PresidentialGameEngine.ClassLibrary.Exceptions;
using PresidentialGameEngine.ClassLibrary.Interfaces;

namespace PresidentialGameEngine.ClassLibrary.Components
{
    public class CardComponent<TPlayer, TCard> : ICardComponent<TPlayer, TCard> where TPlayer : Enum
        where TCard : ICard
    {
        private IRandomnessProvider Random { get; init; }

        private Dictionary<TPlayer, List<TCard>> PlayerHands { get; init; }
        private Dictionary<TPlayer, List<TCard>> PlayerCampaignStrategyPiles { get; init; }
        private Dictionary<int, TCard> CardManifest { get; init; }
        private List<TCard> Deck { get; init; }
        private List<TCard> DiscardPile { get; init; }
        private List<TCard> RemovedFromGame { get; init; }

        public CardComponent(IRandomnessProvider random, Dictionary<int, TCard> cardManifest)
        {
            Random = random;
            CardManifest = cardManifest;

            PlayerHands = [];
            PlayerCampaignStrategyPiles = [];

            foreach (TPlayer player in (TPlayer[])Enum.GetValues(typeof(TPlayer)))
            {
                PlayerHands.Add(player, []);
                PlayerCampaignStrategyPiles.Add(player, []);
            }

            Deck = [];

            List<TCard> cards = [.. CardManifest.Values];
            Deck = cards.Shuffle(Random).ToList();

            DiscardPile = [];
            RemovedFromGame = [];
        }

        public int CountCardsLeftInDeck()
        {
            return Deck.Count;
        }

        public void DrawCards(TPlayer player, int numberToDraw)
        {
            int counter = 1;
            while (counter <= numberToDraw)
            {
                var nextCard = Deck.First();
                PlayerHands[player].Add(nextCard);
                Deck.Remove(nextCard);
                counter++;
            }
        }

        //Redirect through main method?  This is the only specific one I want to keep.
        public IEnumerable<TCard> GetPlayerHand(TPlayer player)
        {
            return PlayerHands[player];
        }

        public void MoveCardFromOneZoneToAnother(TPlayer player, TCard cardToMove, 
            CardZone source, CardZone destination) 
        {
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
        }

        public IEnumerable<TCard> ViewCardsInZone(CardZone zone, TPlayer player)
        {
            return zone switch
            {
                CardZone.Deck => throw new ArgumentException("Looking at deck is not allowed."),
                CardZone.Removed => RemovedFromGame,
                CardZone.Discard => DiscardPile,
                CardZone.Hand => PlayerHands[player],
                CardZone.CampaignStrategy => PlayerCampaignStrategyPiles[player],
                _ => throw new ArgumentException("Zone Unknown."),
            };
        }
    }


    //https://stackoverflow.com/questions/33643104/shuffling-a-stackt
    //'Borrowed' from here.
    //https://stackoverflow.com/questions/17530306/getting-random-numbers-from-a-list-of-integers
    //Fisher-Yates Shuffle
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, IRandomnessProvider rng)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(rng);

            return source.ShuffleIterator(rng);
        }

        private static IEnumerable<T> ShuffleIterator<T>(
            this IEnumerable<T> source, IRandomnessProvider rng)
        {
            List<T> buffer = source.ToList();
            for (int i = 0; i < buffer.Count; i++)
            {
                int j = rng.GetRandomNumber(i, buffer.Count);
                yield return buffer[j];

                buffer[j] = buffer[i];
            }
        }
    }
}
