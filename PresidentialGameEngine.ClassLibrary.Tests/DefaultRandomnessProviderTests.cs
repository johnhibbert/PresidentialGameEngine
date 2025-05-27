using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PresidentialGameEngine.ClassLibrary.Tests
{
    [TestClass]
    public class DefaultRandomnessProviderTests
    {
        //These are mostly just here to make the test percentage higher
        //The class is very simple.

        [TestMethod]
        public void Constructor_Unseeded()
        {
           Assert.IsTrue(new DefaultRandomnessProvider().GetRandomNumber(10) >= 0);
        }

        [TestMethod]
        public void Constructor_Seeded()
        {
            Assert.IsTrue(new DefaultRandomnessProvider(1).GetRandomNumber(10) >= 0);
        }
    }
}
