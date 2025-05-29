using PresidentialGameEngine.ClassLibrary.Data;
using PresidentialGameEngine.ClassLibrary.Enums;

namespace PresidentialGameEngine.ClassLibrary.Tests
{
    [TestClass]
    public class CardTests
    {
        [TestMethod]
        public void ToString_ReturnsExpectedInformation()
        {
            var Card = new Card() 
            {
                Index = 1,
                Title = "Test",
                Text = "Test",
                Event = (engine, player, choices) =>
                {
                    //Nothing
                },
                AreChangesValid = (choices) =>
                {
                    return true;
                },
            };

            Assert.IsTrue(Card.ToString().Contains(Card.Index.ToString()));
            Assert.IsTrue(Card.ToString().Contains(Card.Title.ToString()));
        }

        [TestMethod]
        public void ToLongString_ReturnsExpectedInformation()
        {
            var Card = new Card()
            {
                Index = 1,
                Title = "Test",
                Text = "This is a sentence",
                Event = (engine, player, choices) =>
                {
                    //Nothing
                },
                AreChangesValid = (choices) =>
                {
                    return true;
                },
            };

            Assert.IsTrue(Card.ToLongString().Contains(Card.Index.ToString()));
            Assert.IsTrue(Card.ToLongString().Contains(Card.Title.ToString()));
            Assert.IsTrue(Card.ToLongString().Contains(Card.Text.ToString()));
        }

        [TestMethod]
        public void CardType_UnassignedToNone()
        {
            var Card = new Card()
            {
                Index = 1,
                Title = "Test",
                Text = "Test",
                Event = (engine, player, choices) =>
                {
                    //Nothing
                },
                AreChangesValid = (choices) =>
                {
                    return true;
                },
            };

            Assert.AreEqual(Card.CardType, EventType.None);

        }
    }
}
