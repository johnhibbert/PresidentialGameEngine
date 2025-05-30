using PresidentialGameEngine.ClassLibrary.Components;
using static PresidentialGameEngine.ClassLibrary.Tests.TestStubsFakesAndMocks;

namespace PresidentialGameEngine.ClassLibrary.Tests
{
    [TestClass]
    public class CardComponentTests
    {

        #region Constructor Tests
        [TestMethod]
        public void Constructor_AllEntriesCreated_SizeTwoEnum()
        {
            SeededRandomnessProviderForTesting seed = new();

            CardComponent<FakeEnumWithTwo, FakeCardClass> sut
                = new(seed, FakeManifest);

            Assert.AreEqual(0, sut.ViewCardsInZone(CardZone.Hand, FakeEnumWithTwo.ElementOne).Count());
            Assert.AreEqual(0, sut.ViewCardsInZone(CardZone.Hand, FakeEnumWithTwo.ElementTwo).Count());
        }

        [TestMethod]
        public void Constructor_AllEntriesCreated_SizeThreeEnum()
        {
            SeededRandomnessProviderForTesting seed = new();

            CardComponent<FakeEnumWithThree, FakeCardClass> sut
                = new(seed, FakeManifest);

            Assert.AreEqual(0, sut.ViewCardsInZone(CardZone.Hand, FakeEnumWithThree.ElementOne).Count());
            Assert.AreEqual(0, sut.ViewCardsInZone(CardZone.Hand, FakeEnumWithThree.ElementTwo).Count());
            Assert.AreEqual(0, sut.ViewCardsInZone(CardZone.Hand, FakeEnumWithThree.ElementThree).Count());
        }

        [TestMethod]
        public void Constructor_AllEntriesCreated_SizeFiveEnum()
        {
            SeededRandomnessProviderForTesting seed = new();

            CardComponent<FakeEnumWithFive, FakeCardClass> sut
                = new(seed, FakeManifest);

            Assert.AreEqual(0, sut.ViewCardsInZone(CardZone.Hand, FakeEnumWithFive.ElementOne).Count());
            Assert.AreEqual(0, sut.ViewCardsInZone(CardZone.Hand, FakeEnumWithFive.ElementTwo).Count());
            Assert.AreEqual(0, sut.ViewCardsInZone(CardZone.Hand, FakeEnumWithFive.ElementThree).Count());
            Assert.AreEqual(0, sut.ViewCardsInZone(CardZone.Hand, FakeEnumWithFive.ElementFour).Count());
            Assert.AreEqual(0, sut.ViewCardsInZone(CardZone.Hand, FakeEnumWithFive.ElementFive).Count());
        }

        [TestMethod]
        public void Constructor_ManifestPopulatedIntoDeck()
        {
            SeededRandomnessProviderForTesting seed = new();

            CardComponent<FakeEnumWithTwo, FakeCardClass> sut
                = new(seed, FakeManifest);

            var result = sut.CountCardsLeftInDeck();

            Assert.AreEqual(FakeManifest.Count, result);

        }
        #endregion

        #region CountCardsLeftInDeck Tests

        [TestMethod]
        public void CountCardsLeftInDeck_InitialCountMatchesManifest()
        {
            SeededRandomnessProviderForTesting seed = new();

            CardComponent<FakeEnumWithTwo, FakeCardClass> sut
                = new(seed, FakeManifest);

            var result = sut.CountCardsLeftInDeck();

            Assert.AreEqual(FakeManifest.Count, result);
        }

        [TestMethod]
        public void CountCardsLeftInDeck_CountReducedAfterDraws()
        {
            SeededRandomnessProviderForTesting seed = new();

            CardComponent<FakeEnumWithTwo, FakeCardClass> sut
                = new(seed, FakeManifest);

            sut.DrawCards(FakeEnumWithTwo.ElementOne, 1);

            var result = sut.CountCardsLeftInDeck();

            Assert.AreEqual(FakeManifest.Count - 1, result);
        }

        #endregion

        #region DrawCards Tests

        [TestMethod]
        public void DrawCards_CardsDistributedAsExpected()
        {
            SeededRandomnessProviderForTesting seed = new();

            CardComponent<FakePlayer, FakeCardClass> sut
                = new(seed, FakeManifest);

            sut.DrawCards(FakePlayer.PlayerOne, 1);
            sut.DrawCards(FakePlayer.PlayerTwo, 1);

            var resultOne = sut.ViewCardsInZone(CardZone.Hand, FakePlayer.PlayerOne).First();
            var resultTwo = sut.ViewCardsInZone(CardZone.Hand, FakePlayer.PlayerTwo).First();

            Assert.AreEqual(resultOne, FakeManifest[1]);
            Assert.AreEqual(resultTwo, FakeManifest[3]);
        }

        #endregion

        #region GetPlayerHand Tests
        [TestMethod]
        public void GetPlayerHand_HandsAsExpected()
        {
            SeededRandomnessProviderForTesting seed = new();

            CardComponent<FakePlayer, FakeCardClass> sut
                = new(seed, FakeManifest);

            sut.DrawCards(FakePlayer.PlayerOne, 1);
            sut.DrawCards(FakePlayer.PlayerTwo, 3);

            var resultOne = sut.ViewCardsInZone(CardZone.Hand, FakePlayer.PlayerOne);
            var resultTwo = sut.ViewCardsInZone(CardZone.Hand, FakePlayer.PlayerTwo);

            Assert.AreEqual(1, resultOne.Count());
            Assert.AreEqual(3, resultTwo.Count());
        }
        #endregion

        #region MoveCardFromOneZoneToAnother Tests

        [TestMethod]
        [DataRow(FakePlayer.PlayerOne, CardZone.Hand)]
        [DataRow(FakePlayer.PlayerOne, CardZone.Discard)]
        [DataRow(FakePlayer.PlayerOne, CardZone.Removed)]
        [DataRow(FakePlayer.PlayerOne, CardZone.CampaignStrategy)]

        public void MoveCardFromOneZoneToAnother_MoveFromDeckSuccessful(FakePlayer player, CardZone destinationZone)
        {
            CardZone sourceZone = CardZone.Deck;
            SeededRandomnessProviderForTesting seed = new();

            CardComponent<FakePlayer, FakeCardClass> sut
                = new(seed, FakeManifest);

            var card = FakeManifest[1];

            sut.MoveCardFromOneZoneToAnother(player, card, sourceZone, destinationZone);

            var result = sut.ViewCardsInZone(destinationZone, player).First();

            Assert.AreEqual(result, card);
        }

        [TestMethod]
        [DataRow(FakePlayer.PlayerOne, CardZone.Discard)]
        [DataRow(FakePlayer.PlayerOne, CardZone.Removed)]
        [DataRow(FakePlayer.PlayerOne, CardZone.CampaignStrategy)]

        public void MoveCardFromOneZoneToAnother_MoveFromHandSuccessful(FakePlayer player, CardZone destinationZone)
        {
            CardZone sourceZone = CardZone.Hand;
            SeededRandomnessProviderForTesting seed = new();

            CardComponent<FakePlayer, FakeCardClass> sut
                = new(seed, FakeManifest);

            var card = FakeManifest[1];
            sut.MoveCardFromOneZoneToAnother(player, card, CardZone.Deck, sourceZone);

            sut.MoveCardFromOneZoneToAnother(player, card, sourceZone, destinationZone);

            var result = sut.ViewCardsInZone(destinationZone, player).First();

            Assert.AreEqual(result, card);
        }

        [TestMethod]
        [DataRow(FakePlayer.PlayerOne, CardZone.Hand)]
        [DataRow(FakePlayer.PlayerOne, CardZone.Removed)]
        [DataRow(FakePlayer.PlayerOne, CardZone.CampaignStrategy)]

        public void MoveCardFromOneZoneToAnother_MoveFromDiscardSuccessful(FakePlayer player, CardZone destinationZone)
        {
            CardZone sourceZone = CardZone.Discard;
            SeededRandomnessProviderForTesting seed = new();

            CardComponent<FakePlayer, FakeCardClass> sut
                = new(seed, FakeManifest);

            var card = FakeManifest[1];
            sut.MoveCardFromOneZoneToAnother(player, card, CardZone.Deck, sourceZone);

            sut.MoveCardFromOneZoneToAnother(player, card, sourceZone, destinationZone);

            var result = sut.ViewCardsInZone(destinationZone, player).First();

            Assert.AreEqual(result, card);
        }

        [TestMethod]
        [DataRow(FakePlayer.PlayerOne, CardZone.Hand)]
        [DataRow(FakePlayer.PlayerOne, CardZone.Removed)]
        [DataRow(FakePlayer.PlayerOne, CardZone.Discard)]

        public void MoveCardFromOneZoneToAnother_MoveFromCampgainStrategySuccessful(FakePlayer player, CardZone destinationZone)
        {
            CardZone sourceZone = CardZone.CampaignStrategy;
            SeededRandomnessProviderForTesting seed = new();

            CardComponent<FakePlayer, FakeCardClass> sut
                = new(seed, FakeManifest);

            var card = FakeManifest[1];
            sut.MoveCardFromOneZoneToAnother(player, card, CardZone.Deck, sourceZone);

            sut.MoveCardFromOneZoneToAnother(player, card, sourceZone, destinationZone);

            var result = sut.ViewCardsInZone(destinationZone, player).First();

            Assert.AreEqual(result, card);
        }

        [TestMethod]
        [DataRow(FakePlayer.PlayerOne, CardZone.Hand)]
        [DataRow(FakePlayer.PlayerOne, CardZone.Discard)]
        [DataRow(FakePlayer.PlayerOne, CardZone.Removed)]
        [DataRow(FakePlayer.PlayerOne, CardZone.CampaignStrategy)]
        [ExpectedException(typeof(ArgumentException))]
        public void MoveCardFromOneZoneToAnother_MoveToDeckAlwaysUnsuccessful(FakePlayer player, CardZone sourceZone)
        {
            CardZone destinationZone = CardZone.Deck;
            SeededRandomnessProviderForTesting seed = new();

            CardComponent<FakePlayer, FakeCardClass> sut
                = new(seed, FakeManifest);

            var card = FakeManifest[1];
            sut.MoveCardFromOneZoneToAnother(player, card, CardZone.Deck, sourceZone);

            sut.MoveCardFromOneZoneToAnother(player, card, sourceZone, destinationZone);

            var result = sut.ViewCardsInZone(destinationZone, player).First();

            Assert.AreEqual(result, card);
        }

        [TestMethod]
        [DataRow(FakePlayer.PlayerOne, CardZone.Hand)]
        [DataRow(FakePlayer.PlayerOne, CardZone.Discard)]
        [DataRow(FakePlayer.PlayerOne, CardZone.Removed)]
        [DataRow(FakePlayer.PlayerOne, CardZone.CampaignStrategy)]
        [ExpectedException(typeof(ArgumentException))]
        public void MoveCardFromOneZoneToAnother_SourceAndDestinationCannotBeTheSame(FakePlayer player, CardZone sourceZone)
        {
            CardZone destinationZone = sourceZone;
            SeededRandomnessProviderForTesting seed = new();

            CardComponent<FakePlayer, FakeCardClass> sut
                = new(seed, FakeManifest);

            var card = FakeManifest[1];
            sut.MoveCardFromOneZoneToAnother(player, card, CardZone.Deck, sourceZone);

            sut.MoveCardFromOneZoneToAnother(player, card, sourceZone, destinationZone);

        }

        #endregion

        #region ViewCardsInZone Tests


        [TestMethod]
        public void ViewCardsInZone_ExpectedCardFoundInHand()
        {
            SeededRandomnessProviderForTesting seed = new();

            CardComponent<FakePlayer, FakeCardClass> sut
                = new(seed, FakeManifest);

            var card = FakeManifest[1];
            sut.MoveCardFromOneZoneToAnother(FakePlayer.PlayerOne, card, CardZone.Deck, CardZone.Hand);

            var result = sut.ViewCardsInZone(CardZone.Hand, FakePlayer.PlayerOne).First();

            Assert.AreEqual(result, card);
        }


        [TestMethod]
        public void ViewCardsInZone_ExpectedCardFoundInDiscardPile()
        {
            SeededRandomnessProviderForTesting seed = new();

            CardComponent<FakePlayer, FakeCardClass> sut
                = new(seed, FakeManifest);

            var card = FakeManifest[1];
            sut.MoveCardFromOneZoneToAnother(FakePlayer.PlayerOne, card, CardZone.Deck, CardZone.Discard);

            var result = sut.ViewCardsInZone(CardZone.Discard, FakePlayer.PlayerOne).First();

            Assert.AreEqual(result, card);
        }


        [TestMethod]
        public void ViewCardsInZone_ExpectedCardsFoundInCampaignStrategyPile()
        {
            SeededRandomnessProviderForTesting seed = new();

            CardComponent<FakePlayer, FakeCardClass> sut
                = new(seed, FakeManifest);

            var card = FakeManifest[2];
            sut.MoveCardFromOneZoneToAnother(FakePlayer.PlayerOne, card, CardZone.Deck, CardZone.CampaignStrategy);

            var result = sut.ViewCardsInZone(CardZone.CampaignStrategy, FakePlayer.PlayerOne).First();

            Assert.AreEqual(result, card);
        }

        [TestMethod]
        public void ViewCardsInZone_ExpectedCardsFoundInRemovedPile()
        {
            SeededRandomnessProviderForTesting seed = new();

            CardComponent<FakePlayer, FakeCardClass> sut
                = new(seed, FakeManifest);

            var card = FakeManifest[3];
            sut.MoveCardFromOneZoneToAnother(FakePlayer.PlayerOne, card, CardZone.Deck, CardZone.Removed);

            var result = sut.ViewCardsInZone(CardZone.Removed, FakePlayer.PlayerOne).First();

            Assert.AreEqual(result, card);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ViewCardsInZone_LookingAtDeckIsDisallowed()
        {
            SeededRandomnessProviderForTesting seed = new();

            CardComponent<FakePlayer, FakeCardClass> sut
                = new(seed, FakeManifest);

            sut.ViewCardsInZone(CardZone.Deck, FakePlayer.PlayerOne);
        }

        #endregion

    }
}
