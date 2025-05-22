using Microsoft.VisualStudio.TestTools.UnitTesting;
using PresidentialGameEngine.ClassLibrary.Enums;

namespace PresidentialGameEngine.ClassLibrary.Tests
{
    [TestClass]
    public class NineteenSixtyGameEngineTests
    {

        //GetPlayerMomentum
        //GainMomentum
        //LoseMomentum

        //GainIssueSupport
        //LoseIssueSupport

        //Not exactly a terrific test
        #region GetPlayerMomentum Tests

        [TestMethod]
        [DataRow(Player.Nixon, 1)]
        [DataRow(Player.Kennedy, 3)]
        public void GetPlayerMomentum_PlayerMomentumReturnsExpected(Player player, int amount)
        {
            NineteenSixtyGameEngine engine = new();
            var playerStartingMomentum = engine.GetPlayerMomentum(player);
            engine.GainMomentum(player, amount);
            Assert.AreEqual(engine.GetPlayerMomentum(player), playerStartingMomentum + amount);
        }

        #endregion

        #region GainMomentum Tests
        [TestMethod]
        [DataRow(Player.Nixon, 1)]
        [DataRow(Player.Kennedy, 3)]
        public void GainMomentum_MomentumGainedAsExpected(Player player, int amount)
        {
            NineteenSixtyGameEngine engine = new();
            var playerStartingMomentum = engine.GetPlayerMomentum(player);
            engine.GainMomentum(player, amount);
            Assert.AreEqual(engine.GetPlayerMomentum(player), playerStartingMomentum + amount);
        }
        #endregion

        #region LoseMomentum Tests
        [TestMethod]
        [DataRow(Player.Nixon, 1)]
        [DataRow(Player.Kennedy, 3)]
        public void LoseMomentum_MomentumGainedAsExpected(Player player, int amount)
        {
            //NineteenSixtyGameEngine engine = new();
            //var playerStartingMomentum = engine.GetPlayerMomentum(player);
            //engine.GainMomentum(player, amount);
            //Assert.AreEqual(engine.GetPlayerMomentum(player), playerStartingMomentum + amount);
        }
        #endregion

    }

}