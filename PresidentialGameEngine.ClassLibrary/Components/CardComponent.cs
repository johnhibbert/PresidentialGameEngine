using PresidentialGameEngine.ClassLibrary.Interfaces;

namespace PresidentialGameEngine.ClassLibrary.Components
{
    public class CardComponent<PlayersEnum, CardClass>
        where PlayersEnum : Enum
        where CardClass : class
    {

        private Dictionary<PlayersEnum, List<CardClass>> PlayerHands { get; init; }
        private Dictionary<PlayersEnum, List<CardClass>> PlayerCampaignStrategyPiles { get; init; }

        private Dictionary<int, CardClass> CardManifest { get; init; }

        readonly IRandomnessProvider rng;

        readonly Stack<CardClass> deck;

        readonly List<CardClass> discardPile;

        readonly List<CardClass> removedFromGame;

        public CardComponent(IRandomnessProvider random, Dictionary<int, CardClass> cardManifest)
        {
            rng = random;
            CardManifest = cardManifest;

            PlayerHands = [];
            PlayerCampaignStrategyPiles = [];

            foreach (PlayersEnum player in (PlayersEnum[])Enum.GetValues(typeof(PlayersEnum)))
            {
                PlayerHands.Add(player, []);
                PlayerCampaignStrategyPiles.Add(player, []);
            }

            deck = [];
            discardPile = [];
            removedFromGame = [];

            PrepareShuffledDeck();
        }

        private void PrepareShuffledDeck() 
        {
            List<CardClass> cards = [.. CardManifest.Values];

            var shuffledList = cards.Shuffle(rng).ToList();
            
            //Why reverse?  Well, a list pushed to a stack becomes reversed
            //Both are random, so it won't matter in the long run
            //but this will probably make it easier to see during development
            shuffledList.Reverse();

            shuffledList.ForEach(x => deck.Push(x));
        }

        public int CountCardsLeftInDeck() 
        {
            return deck.Count;
        }

        public IEnumerable<CardClass> LookAtDiscardPile()
        {
            return discardPile;
        }

        public void DrawCards(PlayersEnum player, int numberToDraw) 
        {
            int counter = 1;
            while (counter <= numberToDraw)
            {
                PlayerHands[player].Add(deck.Pop());
                counter++;
            }
        }

        //Call this GetPlayerHand or LookAtPlayerHand?
        public IEnumerable<CardClass> LookAtPlayerHand(PlayersEnum player) 
        {
            return PlayerHands[player];
        }
        

        public void DiscardCardFromHand(PlayersEnum player, CardClass card) 
        {
            ThrowIfCardNotInPlayerHand(player, card);

            PlayerHands[player].Remove(card);
            discardPile.Add(card);
        }



        public IEnumerable<CardClass> LookAtRemovedPile()
        {
            return removedFromGame;
        }

        public void MoveCardFromHandToRemovedPile(PlayersEnum player, CardClass card)
        {
            ThrowIfCardNotInPlayerHand(player, card);

            PlayerHands[player].Remove(card);
            removedFromGame.Add(card);
        }



        public IEnumerable<CardClass> LookAtPlayerCampaignStrategyPile(PlayersEnum player)
        {
            return PlayerCampaignStrategyPiles[player];
        }

        public void MoveCardFromHandToCampaignStrategyPile(PlayersEnum player, CardClass card)
        {
            ThrowIfCardNotInPlayerHand(player, card);

            PlayerHands[player].Remove(card);
            PlayerCampaignStrategyPiles[player].Add(card);
        }




        public void RetrieveCardFromDiscardPile(PlayersEnum player, CardClass card) 
        {
            if(discardPile.Contains(card) == false) 
            {
                throw new ArgumentException("Card not in discard pile");
            }

            discardPile.Remove(card);
            PlayerHands[player].Add(card);
        }




        private void ThrowIfCardNotInPlayerHand(PlayersEnum player, CardClass card)
        {
            if (PlayerHands[player].Contains(card) == false)
            {
                throw new ArgumentException("Card not in player's hand");
            };
        }



    }





    //https://stackoverflow.com/questions/33643104/shuffling-a-stackt


    //'Borrowed' from here.
    //https://stackoverflow.com/questions/17530306/getting-random-numbers-from-a-list-of-integers
    //Fisher-Yates Shuffle
    public static class EnumerableExtensions
    {
        //public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        //{
        //    return source.Shuffle(new Random());
        //}

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
