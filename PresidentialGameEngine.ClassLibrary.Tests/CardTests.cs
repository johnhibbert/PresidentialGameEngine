using PresidentialGameEngine.ClassLibrary.Data;

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
    }
}
