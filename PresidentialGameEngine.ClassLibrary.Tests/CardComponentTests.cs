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

            Assert.AreEqual(0, sut.LookAtPlayerHand(FakeEnumWithTwo.ElementOne).Count());
            Assert.AreEqual(0, sut.LookAtPlayerHand(FakeEnumWithTwo.ElementTwo).Count());
        }

        [TestMethod]
        public void Constructor_AllEntriesCreated_SizeThreeEnum()
        {
            SeededRandomnessProviderForTesting seed = new();

            CardComponent<FakeEnumWithFive, FakeCardClass> sut
                = new(seed, FakeManifest);

            Assert.AreEqual(0, sut.LookAtPlayerHand(FakeEnumWithFive.ElementOne).Count());
            Assert.AreEqual(0, sut.LookAtPlayerHand(FakeEnumWithFive.ElementTwo).Count());
            Assert.AreEqual(0, sut.LookAtPlayerHand(FakeEnumWithFive.ElementThree).Count());
        }

        [TestMethod]
        public void Constructor_AllEntriesCreated_SizeFiveEnum()
        {
            SeededRandomnessProviderForTesting seed = new();

            CardComponent<FakeEnumWithFive, FakeCardClass> sut
                = new(seed, FakeManifest);

            Assert.AreEqual(0, sut.LookAtPlayerHand(FakeEnumWithFive.ElementOne).Count());
            Assert.AreEqual(0, sut.LookAtPlayerHand(FakeEnumWithFive.ElementTwo).Count());
            Assert.AreEqual(0, sut.LookAtPlayerHand(FakeEnumWithFive.ElementThree).Count());
            Assert.AreEqual(0, sut.LookAtPlayerHand(FakeEnumWithFive.ElementFour).Count());
            Assert.AreEqual(0, sut.LookAtPlayerHand(FakeEnumWithFive.ElementFive).Count());
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

        #region LookAtPlayerHand Tests

        [TestMethod]
        public void LookAtPlayerHand_ExpectedCardsFound()
        {
            SeededRandomnessProviderForTesting seed = new();

            CardComponent<FakePlayer, FakeCardClass> sut
                = new(seed, FakeManifest);

            sut.DrawCards(FakePlayer.PlayerOne, 2);

            var result = sut.LookAtPlayerHand(FakePlayer.PlayerOne).ToList();

            Assert.AreEqual(result[0], FakeManifest[1]);
            Assert.AreEqual(result[1], FakeManifest[3]);
        }

        #endregion

        #region LookAtDiscardPile Tests

        [TestMethod]
        public void LookAtDiscardPile_ExpectedCardFound()
        {
            SeededRandomnessProviderForTesting seed = new();

            CardComponent<FakePlayer, FakeCardClass> sut
                = new(seed, FakeManifest);

            sut.DrawCards(FakePlayer.PlayerOne, 1);
            var card = sut.LookAtPlayerHand(FakePlayer.PlayerOne).First();

            sut.DiscardCardFromHand(FakePlayer.PlayerOne, card);

            var result = sut.LookAtDiscardPile().First();

            Assert.AreEqual(result, card);
        }

        #endregion

        #region LookAtCampaignStrategyPile Tests

        [TestMethod]
        public void LookAtCampaignStrategyPile_ExpectedCardsFound()
        {
            SeededRandomnessProviderForTesting seed = new();

            CardComponent<FakePlayer, FakeCardClass> sut
                = new(seed, FakeManifest);

            sut.DrawCards(FakePlayer.PlayerOne, 1);
            var card = sut.LookAtPlayerHand(FakePlayer.PlayerOne).First();
            sut.MoveCardFromHandToCampaignStrategyPile(FakePlayer.PlayerOne, card);

            var result = sut.LookAtPlayerCampaignStrategyPile(FakePlayer.PlayerOne).First();

            Assert.AreEqual(result, card);
        }

        #endregion

        #region LookAtRemovedPile Tests

        [TestMethod]
        public void LookAtRemovedPile_ExpectedCardsFound()
        {
            SeededRandomnessProviderForTesting seed = new();

            CardComponent<FakePlayer, FakeCardClass> sut
                = new(seed, FakeManifest);

            sut.DrawCards(FakePlayer.PlayerOne, 1);
            var card = sut.LookAtPlayerHand(FakePlayer.PlayerOne).First();
            sut.MoveCardFromHandToRemovedPile(FakePlayer.PlayerOne, card);

            var result = sut.LookAtRemovedPile().First();

            Assert.AreEqual(result, card);
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

            var resultOne = sut.LookAtPlayerHand(FakePlayer.PlayerOne).First();
            var resultTwo = sut.LookAtPlayerHand(FakePlayer.PlayerTwo).First();

            Assert.AreEqual(resultOne, FakeManifest[1]);
            Assert.AreEqual(resultTwo, FakeManifest[3]);
        }

        #endregion

        #region DiscardCardFromHand Tests

        [TestMethod]
        public void DiscardCardFromHand_CardNoLongerInHand()
        {
            SeededRandomnessProviderForTesting seed = new();

            CardComponent<FakePlayer, FakeCardClass> sut
                = new(seed, FakeManifest);

            sut.DrawCards(FakePlayer.PlayerOne, 2);
            var card = sut.LookAtPlayerHand(FakePlayer.PlayerOne).First();

            sut.DiscardCardFromHand(FakePlayer.PlayerOne, card);

            var result = sut.LookAtPlayerHand(FakePlayer.PlayerOne).First();

            Assert.AreEqual(result, FakeManifest[3]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DiscardCardFromHand_ThrowsIfCardNotInHand()
        {
            SeededRandomnessProviderForTesting seed = new();

            CardComponent<FakePlayer, FakeCardClass> sut
                = new(seed, FakeManifest);

            sut.DrawCards(FakePlayer.PlayerOne, 2);
            var card = FakeManifest[5];

            sut.DiscardCardFromHand(FakePlayer.PlayerOne, card);
        }

        #endregion

        #region RetrieveCardFromDiscardPile Tests

        [TestMethod]
        public void RetrieveCardFromDiscardPile_CardRetrievedAsExpected()
        {
            SeededRandomnessProviderForTesting seed = new();

            CardComponent<FakePlayer, FakeCardClass> sut
                = new(seed, FakeManifest);

            sut.DrawCards(FakePlayer.PlayerOne, 1);
            var card = sut.LookAtPlayerHand(FakePlayer.PlayerOne).First();

            sut.DiscardCardFromHand(FakePlayer.PlayerOne, card);

            sut.RetrieveCardFromDiscardPile(FakePlayer.PlayerTwo, card);

            var result = sut.LookAtPlayerHand(FakePlayer.PlayerTwo).First();

            Assert.AreEqual(result, FakeManifest[1]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RetrieveCardFromDiscardPile_ThrowsIfCardNotFound()
        {
            SeededRandomnessProviderForTesting seed = new();

            CardComponent<FakePlayer, FakeCardClass> sut
                = new(seed, FakeManifest);

            sut.DrawCards(FakePlayer.PlayerOne, 1);
            var card = sut.LookAtPlayerHand(FakePlayer.PlayerOne).First();
            sut.RetrieveCardFromDiscardPile(FakePlayer.PlayerTwo, card);
        }

        #endregion

        #region MoveCardFromHandToCampaignStrategyPile Tests

        [TestMethod]
        public void MoveCardFromHandToCampaignStrategyPile_CardMovedAsExpected()
        {
            SeededRandomnessProviderForTesting seed = new();

            CardComponent<FakePlayer, FakeCardClass> sut
                = new(seed, FakeManifest);

            sut.DrawCards(FakePlayer.PlayerOne, 1);
            var card = sut.LookAtPlayerHand(FakePlayer.PlayerOne).First();

            sut.MoveCardFromHandToCampaignStrategyPile(FakePlayer.PlayerOne, card);

            var result = sut.LookAtPlayerCampaignStrategyPile(FakePlayer.PlayerOne).First();

            Assert.AreEqual(result, FakeManifest[1]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MoveCardFromHandToCampaignStrategyPile_ThrowsIfCardNotFound()
        {
            SeededRandomnessProviderForTesting seed = new();

            CardComponent<FakePlayer, FakeCardClass> sut
                = new(seed, FakeManifest);

            sut.DrawCards(FakePlayer.PlayerOne, 1);
            var card = sut.LookAtPlayerHand(FakePlayer.PlayerOne).First();
            sut.DiscardCardFromHand(FakePlayer.PlayerOne, card);

            sut.MoveCardFromHandToCampaignStrategyPile(FakePlayer.PlayerOne, card);
        }

        #endregion

    }
}
