using NineteenSixty.Enums;

namespace NineteenSixty.Tests
{
    [TestClass]
    public class CandidateEnumerationExtensionTests
    {
        #region Player Tests
        [TestMethod]
        public void PlayerToOpponent_KennedyToNixonConversionWorks()
        {
            const Player player = Player.Kennedy;
            Assert.AreEqual(Player.Nixon, player.ToOpponent());
        }

        [TestMethod]
        public void PlayerToOpponent_NixonToKennedyConversionWorks()
        {
            const Player player = Player.Nixon;
            Assert.AreEqual(Player.Kennedy, player.ToOpponent());
        }

        [TestMethod]
        public void PlayerToLeader_KennedyConversionWorks()
        {
            const Player player = Player.Kennedy;
            Assert.AreEqual(Leader.Kennedy, player.ToLeader());
        }

        [TestMethod]
        public void PlayerToLeader_NixonConversionWorks()
        {
            const Player player = Player.Nixon;
            Assert.AreEqual(Leader.Nixon, player.ToLeader());
        }

        [TestMethod]
        public void PlayerToAffiliation_KennedyConversionWorks()
        {
            const Player player = Player.Kennedy;
            Assert.AreEqual(Affiliation.Kennedy, player.ToAffiliation());
        }

        [TestMethod]
        public void PlayerToAffiliation_NixonConversionWorks()
        {
            const Player player = Player.Nixon;
            Assert.AreEqual(Affiliation.Nixon, player.ToAffiliation());
        }
        #endregion

        #region Leader Tests
        [TestMethod]
        public void LeaderToOpponent_KennedyToNixonConversionWorks()
        {
            const Leader leader = Leader.Kennedy;
            Assert.AreEqual(Leader.Nixon, leader.ToOpponent());
        }

        [TestMethod]
        public void LeaderToOpponent_NixonToKennedyConversionWorks()
        {
            const Leader leader = Leader.Nixon;
            Assert.AreEqual(Leader.Kennedy, leader.ToOpponent());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LeaderToOpponent_InvalidConversionNone_Throws()
        {
            Leader.None.ToOpponent();
        }

        [TestMethod]
        public void LeaderToPlayer_KennedyConversionWorks()
        {
            const Leader leader = Leader.Kennedy;
            Assert.AreEqual(Player.Kennedy, leader.ToPlayer());
        }

        [TestMethod]
        public void LeaderToPlayer_NixonConversionWorks()
        {
            const Leader leader = Leader.Nixon;
            Assert.AreEqual(Player.Nixon, leader.ToPlayer());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LeaderToPlayer_InvalidConversion_Throws()
        {
            Leader.None.ToPlayer();
        }

        [TestMethod]
        public void LeaderToAffiliation_NixonConversionWorks()
        {
            const Leader leader = Leader.Nixon;
            Assert.AreEqual(Affiliation.Nixon, leader.ToAffiliation());
        }

        [TestMethod]
        public void LeaderToAffiliation_KennedyConversionWorks()
        {
            const Leader leader = Leader.Kennedy;
            Assert.AreEqual(Affiliation.Kennedy, leader.ToAffiliation());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LeaderToAffiliation_NoneConversionWorks()
        {
            const Leader leader = Leader.None;
            Assert.AreEqual(Affiliation.None, leader.ToAffiliation());
        }


        #endregion

        #region Affiliation Tests
        [TestMethod]
        public void AffiliationToOpponent_KennedyToNixonConversionWorks()
        {
            const Affiliation affiliation = Affiliation.Kennedy;
            Assert.AreEqual(Affiliation.Nixon, affiliation.ToOpponent());
        }

        [TestMethod]
        public void AffiliationToOpponent_NixonToKennedyConversionWorks()
        {
            const Affiliation affiliation = Affiliation.Nixon;
            Assert.AreEqual(Affiliation.Kennedy, affiliation.ToOpponent());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AffiliationToOpponent_InvalidConversionNone_Throws()
        {
            Affiliation.None.ToOpponent();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AffiliationToOpponent_InvalidConversionBoth_Throws()
        {
            Affiliation.Both.ToOpponent();
        }

        [TestMethod]
        public void AffiliationToPlayer_KennedyConversionWorks()
        {
            const Affiliation affiliation = Affiliation.Kennedy;
            Assert.AreEqual(Player.Kennedy, affiliation.ToPlayer());
        }

        [TestMethod]
        public void AffiliationToPlayer_NixonConversionWorks()
        {
            const Affiliation affiliation = Affiliation.Nixon;
            Assert.AreEqual(Player.Nixon, affiliation.ToPlayer());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AffiliationToPlayer_InvalidNoneConversion_Throws()
        {
            Affiliation.None.ToPlayer();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AffiliationToPlayer_InvalidBothConversion_Throws()
        {
            Affiliation.Both.ToPlayer();
        }

        [TestMethod]
        public void AffiliationToLeader_NixonConversionWorks()
        {
            const Affiliation affiliation = Affiliation.Nixon;
            Assert.AreEqual(Leader.Nixon, affiliation.ToLeader());
        }

        [TestMethod]
        public void AffiliationToLeader_KennedyConversionWorks()
        {
            const Affiliation affiliation = Affiliation.Kennedy;
            Assert.AreEqual(Leader.Kennedy, affiliation.ToLeader());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AffiliationToLeader_NoneConversionWorks()
        {
            const Affiliation affiliation = Affiliation.None;
            Assert.AreEqual(Leader.None, affiliation.ToLeader());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AffiliationToLeader_InvalidBothConversion_Throws()
        {
            Affiliation.Both.ToLeader();
        }

        #endregion
    }
}
