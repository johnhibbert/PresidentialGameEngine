using Microsoft.VisualStudio.TestTools.UnitTesting;
using PresidentialGameEngine.ClassLibrary.Enums;
using System.Numerics;

namespace PresidentialGameEngine.ClassLibrary.Tests
{
    [TestClass]
    public class CardSpecificTests
    {

        #region #5 - Volunteers
        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void Volunteers_5_PlayerMomentumIsIncreasedByOne(Player player)
        {
            int cardIndex = 5;

            NineteenSixtyGameEngine engine = new();
            var playerStartingMomentum = engine.GetPlayerMomentum(player);

            var sut = CardManifests.TheMakingOfThePresidentGMTCards[cardIndex];

            sut.Event(engine, player);

            Assert.AreEqual(engine.GetPlayerMomentum(player), playerStartingMomentum + 1);
        }
        #endregion

        #region #8 - Soviet Economic Growth
        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void SovietEconomicGrowth_8_EconomyGoesUpOneSpace(Player player)
        {
            int cardIndex = 8;

            NineteenSixtyGameEngine engine = new();
            engine.SetIssueOrder(Issue.CivilRights, Issue.Defense, Issue.Economy);

            var sut = CardManifests.TheMakingOfThePresidentGMTCards[cardIndex];
            sut.Event(engine, player);

            Assert.AreEqual(Issue.Economy, engine.IssueOrder[1]);
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void SovietEconomicGrowth_8_EconomyAtTopRemainsAtTop(Player player)
        {
            int cardIndex = 8;

            NineteenSixtyGameEngine engine = new();
            engine.SetIssueOrder(Issue.Economy, Issue.Defense, Issue.CivilRights);

            var sut = CardManifests.TheMakingOfThePresidentGMTCards[cardIndex];
            sut.Event(engine, player);

            Assert.AreEqual(Issue.Economy, engine.IssueOrder[0]);
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void SovietEconomicGrowth_8_StateSupportGained(Player player)
        {
            int cardIndex = 8;

            NineteenSixtyGameEngine engine = new();
            engine.SetIssueOrder(Issue.Economy, Issue.Defense, Issue.CivilRights);
            engine.GainIssueSupport(player, Issue.Economy, 1);

            var sut = CardManifests.TheMakingOfThePresidentGMTCards[cardIndex];
            sut.Event(engine, player);

            Assert.AreEqual(1, engine.SupportInStates[State.NY].SupportAmount);
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void SovietEconomicGrowth_8_NoStateSupportGainedIfNoLeader(Player player)
        {
            int cardIndex = 8;

            NineteenSixtyGameEngine engine = new();
            engine.SetIssueOrder(Issue.Economy, Issue.Defense, Issue.CivilRights);

            var sut = CardManifests.TheMakingOfThePresidentGMTCards[cardIndex];
            sut.Event(engine, player);

            Assert.AreEqual(0, engine.SupportInStates[State.NY].SupportAmount);
        }

        #endregion

        #region #23 - Martin Luther King Arrested
        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void MartinLutherKingArrested_23_CivilRightsGoesUpOneSpace(Player player)
        {
            int cardIndex = 23;

            NineteenSixtyGameEngine engine = new();
            engine.SetIssueOrder(Issue.Economy, Issue.Defense, Issue.CivilRights);

            var sut = CardManifests.TheMakingOfThePresidentGMTCards[cardIndex];
            sut.Event(engine, player);

            Assert.AreEqual(Issue.CivilRights, engine.IssueOrder[1]);
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void MartinLutherKingArrested_23_EconomyAtTopRemainsAtTop(Player player)
        {
            int cardIndex = 23;

            NineteenSixtyGameEngine engine = new();
            engine.SetIssueOrder(Issue.CivilRights, Issue.Defense, Issue.Economy);

            var sut = CardManifests.TheMakingOfThePresidentGMTCards[cardIndex];
            sut.Event(engine, player);

            Assert.AreEqual(Issue.CivilRights, engine.IssueOrder[0]);
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void MartinLutherKingArrested_23_IssueSupportGained(Player player)
        {
            int cardIndex = 23;

            NineteenSixtyGameEngine engine = new();
            engine.SetIssueOrder(Issue.Economy, Issue.Defense, Issue.CivilRights);
            engine.GainIssueSupport(player, Issue.CivilRights, 1);

            var sut = CardManifests.TheMakingOfThePresidentGMTCards[cardIndex];
            sut.Event(engine, player);

            Assert.AreEqual(4, engine.SupportOnIssues[Issue.CivilRights].SupportAmount);
            Assert.AreEqual(player, engine.SupportOnIssues[Issue.CivilRights].SupportedPlayer);
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void MartinLutherKingArrested_23_StateSupportDecaysOpponent(Player player)
        {
            int cardIndex = 23;

            NineteenSixtyGameEngine engine = new();
            engine.SetIssueOrder(Issue.Economy, Issue.Defense, Issue.CivilRights);
            engine.GainIssueSupport(player.ToOpponent(), Issue.CivilRights, 2);

            var sut = CardManifests.TheMakingOfThePresidentGMTCards[cardIndex];
            sut.Event(engine, player);

            Assert.AreEqual(1, engine.SupportOnIssues[Issue.CivilRights].SupportAmount);
            Assert.AreEqual(player, engine.SupportOnIssues[Issue.CivilRights].SupportedPlayer);
        }
        #endregion

        #region #25 - 1960 Civil Rights Act
        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void CivilRightsAct_25_CivilRightsGoesUpOneSpace(Player player)
        {
            int cardIndex = 25;

            NineteenSixtyGameEngine engine = new();
            engine.SetIssueOrder(Issue.Economy, Issue.Defense, Issue.CivilRights);

            var sut = CardManifests.TheMakingOfThePresidentGMTCards[cardIndex];
            sut.Event(engine, player);

            Assert.AreEqual(Issue.CivilRights, engine.IssueOrder[1]);
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void CivilRightsAct_25_EconomyAtTopRemainsAtTop(Player player)
        {
            int cardIndex = 25;

            NineteenSixtyGameEngine engine = new();
            engine.SetIssueOrder(Issue.CivilRights, Issue.Defense, Issue.Economy);

            var sut = CardManifests.TheMakingOfThePresidentGMTCards[cardIndex];
            sut.Event(engine, player);

            Assert.AreEqual(Issue.CivilRights, engine.IssueOrder[0]);
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void CivilRightsAct_25_IssueSupportGained(Player player)
        {
            int cardIndex = 25;

            NineteenSixtyGameEngine engine = new();
            engine.SetIssueOrder(Issue.Economy, Issue.Defense, Issue.CivilRights);
            engine.GainIssueSupport(Player.Nixon, Issue.CivilRights, 1);

            var sut = CardManifests.TheMakingOfThePresidentGMTCards[cardIndex];
            sut.Event(engine, player);

            Assert.AreEqual(2, engine.SupportOnIssues[Issue.CivilRights].SupportAmount);
            Assert.AreEqual(Player.Nixon, engine.SupportOnIssues[Issue.CivilRights].SupportedPlayer);
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void CivilRightsAct_25_StateSupportDecaysOpponent(Player player)
        {
            int cardIndex = 25;

            NineteenSixtyGameEngine engine = new();
            engine.SetIssueOrder(Issue.Economy, Issue.Defense, Issue.CivilRights);
            engine.GainIssueSupport(Player.Kennedy, Issue.CivilRights, 2);

            var sut = CardManifests.TheMakingOfThePresidentGMTCards[cardIndex];
            sut.Event(engine, player);

            Assert.AreEqual(1, engine.SupportOnIssues[Issue.CivilRights].SupportAmount);
            Assert.AreEqual(Player.Kennedy, engine.SupportOnIssues[Issue.CivilRights].SupportedPlayer);
        }
        #endregion

        #region #48 - Rising Food Prices
        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void RisingFoodPrices_48_EconomyGoesUpOneSpace(Player player)
        {
            int cardIndex = 48;

            NineteenSixtyGameEngine engine = new();
            engine.SetIssueOrder(Issue.CivilRights, Issue.Defense, Issue.Economy);

            var sut = CardManifests.TheMakingOfThePresidentGMTCards[cardIndex];

            sut.Event(engine, player);

            Assert.AreEqual(Issue.Economy, engine.IssueOrder[1]);
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void RisingFoodPrices_48_EconomyAtTopRemainsAtTop(Player player)
        {
            int cardIndex = 48;

            NineteenSixtyGameEngine engine = new();
            engine.SetIssueOrder(Issue.Economy, Issue.Defense, Issue.CivilRights);

            var sut = CardManifests.TheMakingOfThePresidentGMTCards[cardIndex];

            sut.Event(engine, player);

            Assert.AreEqual(Issue.Economy, engine.IssueOrder[0]);
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void RisingFoodPrices_48_IssueSupportGained(Player player)
        {
            int cardIndex = 48;

            NineteenSixtyGameEngine engine = new();
            engine.SetIssueOrder(Issue.Economy, Issue.Defense, Issue.CivilRights);

            var sut = CardManifests.TheMakingOfThePresidentGMTCards[cardIndex];

            sut.Event(engine, player);

            Assert.AreEqual(Player.Nixon, engine.SupportOnIssues[Issue.Economy].SupportedPlayer);
            Assert.AreEqual(2, engine.SupportOnIssues[Issue.Economy].SupportAmount);
        }
        #endregion

        #region #51 - Missile Gap
        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void MissileGap_51_ExpectedSupportGained(Player player)
        {
            int cardIndex = 51;

            NineteenSixtyGameEngine engine = new();
            var sut = CardManifests.TheMakingOfThePresidentGMTCards[cardIndex];

            sut.Event(engine, player);

            Assert.AreEqual(Player.Kennedy, engine.SupportOnIssues[Issue.Defense].SupportedPlayer);
            Assert.AreEqual(3, engine.SupportOnIssues[Issue.Defense].SupportAmount);
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void MissileGap_51_SupportFromOpponentRemoved(Player player)
        {
            int cardIndex = 51;

            NineteenSixtyGameEngine engine = new();

            engine.GainIssueSupport(Player.Nixon, Issue.Defense, 2);
            var sut = CardManifests.TheMakingOfThePresidentGMTCards[cardIndex];

            sut.Event(engine, player);

            Assert.AreEqual(Player.Kennedy, engine.SupportOnIssues[Issue.Defense].SupportedPlayer);
            Assert.AreEqual(1, engine.SupportOnIssues[Issue.Defense].SupportAmount);
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void MissileGap_51_SupportChangedToZero(Player player)
        {
            int cardIndex = 51;

            NineteenSixtyGameEngine engine = new();
            engine.GainIssueSupport(Player.Nixon, Issue.Defense, 3);

            var sut = CardManifests.TheMakingOfThePresidentGMTCards[cardIndex];
            sut.Event(engine, player);

            Assert.AreEqual(Player.None, engine.SupportOnIssues[Issue.Defense].SupportedPlayer);
            Assert.AreEqual(0, engine.SupportOnIssues[Issue.Defense].SupportAmount);
        }

        #endregion

        #region #62 - The Trial of Gary Powers
        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void TheTrialOfGaryPowers_62_DefenseMovesUpTwoSpaces(Player player)
        {
            int cardIndex = 62;

            NineteenSixtyGameEngine engine = new();

            engine.SetIssueOrder(Issue.CivilRights, Issue.Economy, Issue.Defense);

            var sut = CardManifests.TheMakingOfThePresidentGMTCards[cardIndex];
            sut.Event(engine, player);

            Assert.AreEqual(Issue.Defense, engine.IssueOrder[0]);
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void TheTrialOfGaryPowers_62_DefenseAtTopUnchanged(Player player)
        {
            int cardIndex = 62;

            NineteenSixtyGameEngine engine = new();

            engine.SetIssueOrder(Issue.Defense, Issue.Economy, Issue.CivilRights);

            var sut = CardManifests.TheMakingOfThePresidentGMTCards[cardIndex];
            sut.Event(engine, player);

            Assert.AreEqual(Issue.Defense, engine.IssueOrder[0]);
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void TheTrialOfGaryPowers_62_LeaderAwardedMomentum(Player player)
        {
            int cardIndex = 62;

            NineteenSixtyGameEngine engine = new();
            
            engine.GainIssueSupport(player, Issue.Defense, 1);

            var sut = CardManifests.TheMakingOfThePresidentGMTCards[cardIndex];
            sut.Event(engine, player);

            Assert.AreEqual(1, engine.GetPlayerMomentum(player));
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void TheTrialOfGaryPowers_62_NoLeaderNoMomentumChange(Player player)
        {
            int cardIndex = 62;

            NineteenSixtyGameEngine engine = new();

            var sut = CardManifests.TheMakingOfThePresidentGMTCards[cardIndex];
            sut.Event(engine, player);

            Assert.AreEqual(0, engine.KennedyMomentum);
            Assert.AreEqual(0, engine.NixonMomentum);
        }

        #endregion

        #region #63 - “Give Me A Week”
        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void GiveMeAWeek_63_ExpectedSupportAndMomentumLost(Player player)
        {
            int cardIndex = 63;

            NineteenSixtyGameEngine engine = new();

            engine.GainMomentum(Player.Nixon, 5);
            engine.GainIssueSupport(Player.Nixon, Issue.Defense, 4);
            engine.GainIssueSupport(Player.Nixon, Issue.Economy, 3);
            engine.GainIssueSupport(Player.Nixon, Issue.CivilRights, 2);

            var sut = CardManifests.TheMakingOfThePresidentGMTCards[cardIndex];
            sut.Event(engine, player);

            Assert.AreEqual(3, engine.GetPlayerMomentum(Player.Nixon));
            Assert.AreEqual(3, engine.SupportOnIssues[Issue.Defense].SupportAmount);
            Assert.AreEqual(2, engine.SupportOnIssues[Issue.Economy].SupportAmount);
            Assert.AreEqual(1, engine.SupportOnIssues[Issue.CivilRights].SupportAmount);
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void GiveMeAWeek_63_SupportAndMomentumDoNotGoNegative(Player player)
        {
            int cardIndex = 63;

            NineteenSixtyGameEngine engine = new();

            engine.GainMomentum(Player.Nixon, 1);
            engine.GainIssueSupport(Player.Nixon, Issue.Defense, 0);
            engine.GainIssueSupport(Player.Nixon, Issue.Economy, 0);
            engine.GainIssueSupport(Player.Nixon, Issue.CivilRights, 0);

            var sut = CardManifests.TheMakingOfThePresidentGMTCards[cardIndex];
            sut.Event(engine, player);

            Assert.AreEqual(0, engine.GetPlayerMomentum(Player.Nixon));
            Assert.AreEqual(0, engine.SupportOnIssues[Issue.Defense].SupportAmount);
            Assert.AreEqual(0, engine.SupportOnIssues[Issue.Economy].SupportAmount);
            Assert.AreEqual(0, engine.SupportOnIssues[Issue.CivilRights].SupportAmount);
        }
        #endregion

        #region #64 - Stump Speech
        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void StumpSpeech_64_LowerMomentumIsGained(Player player)
        {
            int cardIndex = 64;

            NineteenSixtyGameEngine engine = new();

            engine.GainMomentum(player.ToOpponent(), 5);

            var sut = CardManifests.TheMakingOfThePresidentGMTCards[cardIndex];
            sut.Event(engine, player);

            Assert.AreEqual(engine.GetPlayerMomentum(player), engine.GetPlayerMomentum(player.ToOpponent()));
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void StumpSpeech_64_HigherMomentumNoChange(Player player)
        {
            int cardIndex = 64;

            NineteenSixtyGameEngine engine = new();

            engine.GainMomentum(player, 5);

            var sut = CardManifests.TheMakingOfThePresidentGMTCards[cardIndex];
            sut.Event(engine, player);

            Assert.AreEqual(5, engine.GetPlayerMomentum(player));
            Assert.AreEqual(0, engine.GetPlayerMomentum(player.ToOpponent()));
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void StumpSpeech_64_TiedMomentumNoChange(Player player)
        {
            int cardIndex = 64;

            NineteenSixtyGameEngine engine = new();

            var sut = CardManifests.TheMakingOfThePresidentGMTCards[cardIndex];
            sut.Event(engine, player);

            Assert.AreEqual(engine.GetPlayerMomentum(player), engine.GetPlayerMomentum(player.ToOpponent()));
        }

        #endregion

        #region #68 - "Peace Without Surrender"
        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void PeaceWithoutSurrender_68_DefenseGoesUpOneSpace(Player player)
        {
            int cardIndex = 68;

            NineteenSixtyGameEngine engine = new();
            engine.SetIssueOrder(Issue.CivilRights, Issue.Economy, Issue.Defense);

            var sut = CardManifests.TheMakingOfThePresidentGMTCards[cardIndex];

            sut.Event(engine, player);

            Assert.AreEqual(Issue.Defense, engine.IssueOrder[1]);
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void PeaceWithoutSurrender_68_EconomyAtTopRemainsAtTop(Player player)
        {
            int cardIndex = 68;

            NineteenSixtyGameEngine engine = new();
            engine.SetIssueOrder(Issue.Defense, Issue.Economy, Issue.CivilRights);

            var sut = CardManifests.TheMakingOfThePresidentGMTCards[cardIndex];

            sut.Event(engine, player);

            Assert.AreEqual(Issue.Defense, engine.IssueOrder[0]);
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void PeaceWithoutSurrender_68_IssueSupportGained(Player player)
        {
            int cardIndex = 68;

            NineteenSixtyGameEngine engine = new();
            engine.SetIssueOrder(Issue.Defense, Issue.Economy, Issue.CivilRights);

            var sut = CardManifests.TheMakingOfThePresidentGMTCards[cardIndex];

            sut.Event(engine, player);

            Assert.AreEqual(Player.Nixon, engine.SupportOnIssues[Issue.Defense].SupportedPlayer);
            Assert.AreEqual(1, engine.SupportOnIssues[Issue.Defense].SupportAmount);
        }
        #endregion

        #region #70 - The Old Nixon
        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void TheOldNixon_70_ExpectedAmountOfMomentumLost(Player player)
        {
            int cardIndex = 70;

            NineteenSixtyGameEngine engine = new();

            engine.GainMomentum(Player.Nixon, 5);
            engine.GainMomentum(Player.Kennedy, 5);

            var nixonStartingMomentum = engine.NixonMomentum;
            var kennedyStartingMomentum = engine.KennedyMomentum;

            var sut = CardManifests.TheMakingOfThePresidentGMTCards[cardIndex];

            sut.Event(engine, player);

            Assert.AreEqual(engine.NixonMomentum, nixonStartingMomentum - 1);
            Assert.AreEqual(engine.KennedyMomentum, kennedyStartingMomentum - 3);
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void TheOldNixon_70_MomentumDoesNotGoNegative(Player player)
        {
            int cardIndex = 70;

            NineteenSixtyGameEngine engine = new();

            engine.GainMomentum(Player.Nixon, 0);
            engine.GainMomentum(Player.Kennedy, 2);

            var sut = CardManifests.TheMakingOfThePresidentGMTCards[cardIndex];

            sut.Event(engine, player);

            Assert.AreEqual(engine.NixonMomentum, 0);
            Assert.AreEqual(engine.KennedyMomentum, 0);
        }

        #endregion

        #region #78 Stock Market In Decline

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void StockMarketInDecline_78_EconomyGoesUpTwoSpace(Player player)
        {
            int cardIndex = 78;

            NineteenSixtyGameEngine engine = new();
            engine.SetIssueOrder(Issue.CivilRights, Issue.Defense, Issue.Economy);

            var sut = CardManifests.TheMakingOfThePresidentGMTCards[cardIndex];
            sut.Event(engine, player);

            Assert.AreEqual(Issue.Economy, engine.IssueOrder[0]);
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void StockMarketInDecline_78_EconomyAtTopRemainsAtTop(Player player)
        {
            int cardIndex = 78;

            NineteenSixtyGameEngine engine = new();
            engine.SetIssueOrder(Issue.Economy, Issue.Defense, Issue.CivilRights);

            var sut = CardManifests.TheMakingOfThePresidentGMTCards[cardIndex];
            sut.Event(engine, player);

            Assert.AreEqual(Issue.Economy, engine.IssueOrder[0]);
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void StockMarketInDecline_78_StateSupportGained(Player player)
        {
            int cardIndex = 78;

            NineteenSixtyGameEngine engine = new();
            engine.SetIssueOrder(Issue.Economy, Issue.Defense, Issue.CivilRights);
            engine.GainIssueSupport(player, Issue.Economy, 1);

            var sut = CardManifests.TheMakingOfThePresidentGMTCards[cardIndex];
            sut.Event(engine, player);

            Assert.AreEqual(2, engine.SupportInStates[State.NY].SupportAmount);
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void StockMarketInDecline_78_NoStateSupportGainedIfNoLeader(Player player)
        {
            int cardIndex = 78;

            NineteenSixtyGameEngine engine = new();
            engine.SetIssueOrder(Issue.Economy, Issue.Defense, Issue.CivilRights);

            var sut = CardManifests.TheMakingOfThePresidentGMTCards[cardIndex];
            sut.Event(engine, player);

            Assert.AreEqual(0, engine.SupportInStates[State.NY].SupportAmount);
        }

        #endregion

        #region #82 - Fidel Castro

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void FidelCastro_82_LeaderGainsStateSupportAndMomentum(Player player)
        {
            int cardIndex = 82;

            NineteenSixtyGameEngine engine = new();
            engine.GainIssueSupport(player, Issue.Defense, 1);

            var sut = CardManifests.TheMakingOfThePresidentGMTCards[cardIndex];
            sut.Event(engine, player);

            Assert.AreEqual(1, engine.SupportInStates[State.FL].SupportAmount);
            Assert.AreEqual(1, engine.GetPlayerMomentum(player));
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void FidelCastro_82_TieAwardsNothing(Player player)
        {
            int cardIndex = 82;

            NineteenSixtyGameEngine engine = new();

            var sut = CardManifests.TheMakingOfThePresidentGMTCards[cardIndex];
            sut.Event(engine, player);

            Assert.AreEqual(0, engine.SupportInStates[State.FL].SupportAmount);
            Assert.AreEqual(0, engine.GetPlayerMomentum(player));
        }

        #endregion

        #region #89 - The New Nixon
        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void TheNewNixon_89_NixonMomentumIsIncreasedByOne(Player player)
        {
            int cardIndex = 89;

            NineteenSixtyGameEngine engine = new();
            var nixonStartingMomentum = engine.NixonMomentum;
            var kennedyStartingMomentum = engine.KennedyMomentum;

            var sut = CardManifests.TheMakingOfThePresidentGMTCards[cardIndex];

            sut.Event(engine, player);

            Assert.AreEqual(engine.NixonMomentum, nixonStartingMomentum + 1);
            Assert.AreEqual(engine.KennedyMomentum, kennedyStartingMomentum);
        }
        #endregion

        #region #93 - Experience Counts
        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void ExperienceCounts_93_NixonMomentumGainedAndKennedyIssueSupportLost(Player player)
        {
            int cardIndex = 93;

            NineteenSixtyGameEngine engine = new();

            engine.GainMomentum(Player.Nixon, 2);

            engine.GainIssueSupport(Player.Kennedy, Issue.CivilRights, 3);
            engine.GainIssueSupport(Player.Kennedy, Issue.Defense, 2);
            engine.GainIssueSupport(Player.Kennedy, Issue.Economy, 1);

            var sut = CardManifests.TheMakingOfThePresidentGMTCards[cardIndex];
            sut.Event(engine, player);

            Assert.AreEqual(engine.NixonMomentum, 3);
            Assert.AreEqual(2, engine.GetIssueSupportAmount(Issue.CivilRights));
            Assert.AreEqual(1, engine.GetIssueSupportAmount(Issue.Defense));
            Assert.AreEqual(0, engine.GetIssueSupportAmount(Issue.Economy));
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void ExperienceCounts_93_NixonAndNeutralSupportNotLost(Player player)
        {
            int cardIndex = 93;

            NineteenSixtyGameEngine engine = new();

            engine.GainIssueSupport(Player.Kennedy, Issue.CivilRights, 3);
            engine.GainIssueSupport(Player.Nixon, Issue.Defense, 2);

            var sut = CardManifests.TheMakingOfThePresidentGMTCards[cardIndex];
            sut.Event(engine, player);

            Assert.AreEqual(2, engine.GetIssueSupportAmount(Issue.CivilRights));
            Assert.AreEqual(2, engine.GetIssueSupportAmount(Issue.Defense));
            Assert.AreEqual(0, engine.GetIssueSupportAmount(Issue.Economy));
        }

        #endregion

        #region #96 - Medal Count
        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void MedalCount_96_SharedLeaderLosesMomentumAsWell(Player player)
        {
            int cardIndex = 96;

            NineteenSixtyGameEngine engine = new();
            engine.GainIssueSupport(player, Issue.CivilRights, 3);
            engine.GainIssueSupport(player, Issue.Defense, 2);
            engine.GainIssueSupport(player, Issue.Economy, 1);
            engine.GainMomentum(player, 1);

            var sut = CardManifests.TheMakingOfThePresidentGMTCards[cardIndex];

            sut.Event(engine, player);

            Assert.AreEqual(0, engine.GetPlayerMomentum(player));
            Assert.AreEqual(2, engine.GetIssueSupportAmount(Issue.CivilRights));
            Assert.AreEqual(2, engine.GetIssueSupportAmount(Issue.Defense));
            Assert.AreEqual(0, engine.GetIssueSupportAmount(Issue.Economy));
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void MedalCount_96_SplitLeaderNoMomentumLoss(Player player)
        {
            int cardIndex = 96;

            NineteenSixtyGameEngine engine = new();
            engine.GainIssueSupport(Player.Nixon, Issue.CivilRights, 3);
            engine.GainIssueSupport(Player.Nixon, Issue.Defense, 2);
            engine.GainIssueSupport(Player.Kennedy, Issue.Economy, 1);
            engine.GainMomentum(Player.Nixon, 1);
            engine.GainMomentum(Player.Kennedy, 1);

            var sut = CardManifests.TheMakingOfThePresidentGMTCards[cardIndex];

            sut.Event(engine, player);

            Assert.AreEqual(2, engine.GetIssueSupportAmount(Issue.CivilRights));
            Assert.AreEqual(2, engine.GetIssueSupportAmount(Issue.Defense));
            Assert.AreEqual(0, engine.GetIssueSupportAmount(Issue.Economy));
            Assert.AreEqual(1, engine.GetPlayerMomentum(Player.Nixon));
            Assert.AreEqual(1, engine.GetPlayerMomentum(Player.Kennedy));
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void MedalCount_96_NoLeaderNoMomentumLoss(Player player)
        {
            int cardIndex = 96;

            NineteenSixtyGameEngine engine = new();
            engine.GainIssueSupport(Player.Nixon, Issue.CivilRights, 3);
            engine.GainIssueSupport(Player.Nixon, Issue.Defense, 2);
            engine.GainIssueSupport(Player.Kennedy, Issue.Economy, 1);
            engine.GainMomentum(Player.Nixon, 1);
            engine.GainMomentum(Player.Kennedy, 1);

            var sut = CardManifests.TheMakingOfThePresidentGMTCards[cardIndex];

            sut.Event(engine, player);

            Assert.AreEqual(2, engine.GetIssueSupportAmount(Issue.CivilRights));
            Assert.AreEqual(2, engine.GetIssueSupportAmount(Issue.Defense));
            Assert.AreEqual(0, engine.GetIssueSupportAmount(Issue.Economy));
            Assert.AreEqual(1, engine.GetPlayerMomentum(Player.Nixon));
            Assert.AreEqual(1, engine.GetPlayerMomentum(Player.Kennedy));
        }

        #endregion

        #region #97 - Cassius Clay Wins Gold
        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void CassiusClayWinsGold_97_SharedLeaderLosesMomentumAsWell(Player player)
        {
            int cardIndex = 97;

            NineteenSixtyGameEngine engine = new();
            engine.GainIssueSupport(player, Issue.CivilRights, 3);
            engine.GainIssueSupport(player, Issue.Defense, 2);
            engine.GainIssueSupport(player, Issue.Economy, 1);
            engine.GainMomentum(player, 1);

            var sut = CardManifests.TheMakingOfThePresidentGMTCards[cardIndex];

            sut.Event(engine, player);

            Assert.AreEqual(0, engine.GetPlayerMomentum(player));
            Assert.AreEqual(3, engine.GetIssueSupportAmount(Issue.CivilRights));
            Assert.AreEqual(1, engine.GetIssueSupportAmount(Issue.Defense));
            Assert.AreEqual(0, engine.GetIssueSupportAmount(Issue.Economy));
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void CassiusClayWinsGold_97_SplitLeaderNoMomentumLoss(Player player)
        {
            int cardIndex = 97;

            NineteenSixtyGameEngine engine = new();
            engine.GainIssueSupport(Player.Nixon, Issue.CivilRights, 3);
            engine.GainIssueSupport(Player.Nixon, Issue.Defense, 2);
            engine.GainIssueSupport(Player.Kennedy, Issue.Economy, 1);
            engine.GainMomentum(Player.Nixon, 1);
            engine.GainMomentum(Player.Kennedy, 1);

            var sut = CardManifests.TheMakingOfThePresidentGMTCards[cardIndex];

            sut.Event(engine, player);

            Assert.AreEqual(3, engine.GetIssueSupportAmount(Issue.CivilRights));
            Assert.AreEqual(1, engine.GetIssueSupportAmount(Issue.Defense));
            Assert.AreEqual(0, engine.GetIssueSupportAmount(Issue.Economy));
            Assert.AreEqual(1, engine.GetPlayerMomentum(Player.Nixon));
            Assert.AreEqual(1, engine.GetPlayerMomentum(Player.Kennedy));
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void CassiusClayWinsGold_97_NoLeaderNoMomentumLoss(Player player)
        {
            int cardIndex = 97;

            NineteenSixtyGameEngine engine = new();
            engine.GainIssueSupport(Player.Nixon, Issue.CivilRights, 3);
            engine.GainIssueSupport(Player.Kennedy, Issue.Economy, 1);
            engine.GainMomentum(Player.Nixon, 1);
            engine.GainMomentum(Player.Kennedy, 1);

            var sut = CardManifests.TheMakingOfThePresidentGMTCards[cardIndex];

            sut.Event(engine, player);

            Assert.AreEqual(3, engine.GetIssueSupportAmount(Issue.CivilRights));
            Assert.AreEqual(0, engine.GetIssueSupportAmount(Issue.Defense));
            Assert.AreEqual(0, engine.GetIssueSupportAmount(Issue.Economy));
            Assert.AreEqual(1, engine.GetPlayerMomentum(Player.Nixon));
            Assert.AreEqual(1, engine.GetPlayerMomentum(Player.Kennedy));
        }

        #endregion
    }
}
