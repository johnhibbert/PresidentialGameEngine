using PresidentialGameEngine.ClassLibrary.Components;
using PresidentialGameEngine.ClassLibrary.Data;
using PresidentialGameEngine.ClassLibrary.Engines;
using PresidentialGameEngine.ClassLibrary.Enums;
using PresidentialGameEngine.ClassLibrary.Manifests;
using System.Numerics;
using static PresidentialGameEngine.ClassLibrary.Tests.TestStubsFakesAndMocks;

namespace PresidentialGameEngine.ClassLibrary.Tests
{
    [TestClass]
    public class NineteenSixtyCardSpecificTests
    {
        //"If it isn't tested, it's broken." Eliotte Rusty Harold
        //https://youtu.be/fr1E9aVnBxw?si=uW41ZLChgJ5EEYnl&t=138

        public static PlayerChosenChanges<Player, Issue, State> EmptyChanges { get { return GetEmptyChanges(); } }

        private static PlayerChosenChanges<Player, Issue, State> GetEmptyChanges() 
        {
            return new();
        }

        public static PlayerChosenChanges<Player, Issue, State> InvalidChanges { get { return GetInvalidChanges(); } }

        private static PlayerChosenChanges<Player, Issue, State> GetInvalidChanges()
        {
            //We're sending these changes that are bound to be invalid
            //but only to the methods we expect to not use them.
            //This is to get the compiler to stop complaining about sending null

            PlayerChosenChanges<Player, Issue, State> ImplausiblyLargeAndContradictoryChanges = new();

            var hugeKennedySupportInHawaii = new SupportChange<Player, State>(Player.Kennedy, State.HI, int.MaxValue);
            var nixonIsThereTooSupportInHawaii = new SupportChange<Player, State>(Player.Kennedy, State.HI, int.MaxValue);
            var changeInNoneIssue = new SupportChange<Player, Issue>(Player.Kennedy, Issue.None, int.MinValue);

            ImplausiblyLargeAndContradictoryChanges.StateChanges.Add(hugeKennedySupportInHawaii);
            ImplausiblyLargeAndContradictoryChanges.StateChanges.Add(nixonIsThereTooSupportInHawaii);
            ImplausiblyLargeAndContradictoryChanges.IssueChanges.Add(changeInNoneIssue);

            return ImplausiblyLargeAndContradictoryChanges;
        }

        private static NineteenSixtyGameEngine GetGameEngine() 
        {
            SeededRandomnessProviderForTesting seed = new();
            ComponentCollection<Player, Leader, Issue, State, Region, Card> compColl = new()
            {
                MomentumComponent = new AccumulatingComponent<Player>(),
                IssueSupportComponent = new SupportComponent<Player, Leader, Issue>(),
                StateSupportComponent = new SupportComponent<Player, Leader, State>(),
                IssuePositioningComponent = new PositioningComponent<Issue>(),
                PoliticalCapitalComponent = new PoliticalCapitalComponent<Player>(seed, 12),
                RegionalComponent = new RegionalComponent<State, Region, Player>(NineteenSixty.RegionByState, NineteenSixty.PlayerStartingPositions),
                RestComponent = new AccumulatingComponent<Player>(),
                EndorsementComponent = new SupportComponent<Player, Leader, Region>(),
                MediaSupportComponent = new SupportComponent<Player, Leader, Region>(),
                ExhaustionComponent = new ExhaustionComponent<Player>(),
                CardComponent = new CardComponent<Player, Card>(seed, NineteenSixty.GMTCards)
            };

            return new NineteenSixtyGameEngine(compColl);
        }



        #region #5 - Volunteers
        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void Volunteers_5_PlayerMomentumIsIncreasedByOne(Player player)
        {
            int cardIndex = 5;
            var engine = GetGameEngine();

            var playerStartingMomentum = engine.GetPlayerMomentum(player);

            var sut = NineteenSixty.GMTCards[cardIndex];

            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(engine.GetPlayerMomentum(player), playerStartingMomentum + 1);
        }

        [TestMethod]
        public void Volunteers_5_ValidationAlwaysTrue()
        {
            int cardIndex = 5;
            var sut = NineteenSixty.GMTCards[cardIndex];

            var result = sut.AreChangesValid(InvalidChanges);

            Assert.IsTrue(result);
        }
        #endregion

        #region #6 - New England
        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void NewEngland_6_SupportAddedToStates(Player player)
        {
            int cardIndex = 6;
            var engine = GetGameEngine();

            PlayerChosenChanges<Player, Issue, State> playerChoices = new();
            var oneSupportInRhodeIsland = new SupportChange<Player,State>(Player.Kennedy, State.RI, 1);
            var oneSupportInMaine = new SupportChange<Player, State>(Player.Kennedy, State.ME, 1);
            var twoSupportInNewHampshire = new SupportChange<Player, State>(Player.Kennedy, State.NH, 2);
            var oneSupportInVermont = new SupportChange<Player,State>(Player.Kennedy, State.VT, 1);

            playerChoices.StateChanges.Add(oneSupportInRhodeIsland);
            playerChoices.StateChanges.Add(oneSupportInMaine);
            playerChoices.StateChanges.Add(twoSupportInNewHampshire);
            playerChoices.StateChanges.Add(oneSupportInVermont);

            var sut = NineteenSixty.GMTCards[cardIndex];

            sut.Event(engine, player, playerChoices);

            Assert.AreEqual(1, engine.GetSupportAmount(State.RI));
            Assert.AreEqual(1, engine.GetSupportAmount(State.ME));
            Assert.AreEqual(2, engine.GetSupportAmount(State.NH));
            Assert.AreEqual(1, engine.GetSupportAmount(State.VT));
        }

        [TestMethod]
        public void NewEngland_6_FailsValidationIfNixonGains()
        {
            int cardIndex = 6;
            var engine = GetGameEngine();

            PlayerChosenChanges<Player, Issue, State> playerChoices = new();
            var oneSupportInRhodeIsland = new SupportChange<Player, State>(Player.Kennedy, State.RI, 1);
            var oneSupportInMaine = new SupportChange<Player, State>(Player.Nixon, State.ME, 1);
            var twoSupportInNewHampshire = new SupportChange<Player, State>(Player.Kennedy, State.NH, 2);
            var oneSupportInVermont = new SupportChange<Player, State>(Player.Kennedy, State.VT, 1);

            playerChoices.StateChanges.Add(oneSupportInRhodeIsland);
            playerChoices.StateChanges.Add(oneSupportInMaine);
            playerChoices.StateChanges.Add(twoSupportInNewHampshire);
            playerChoices.StateChanges.Add(oneSupportInVermont);

            var sut = NineteenSixty.GMTCards[cardIndex];
            var result = sut.AreChangesValid(playerChoices);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void NewEngland_6_FailsValidationIfIssueGains()
        {
            int cardIndex = 6;
            var engine = GetGameEngine();

            PlayerChosenChanges<Player, Issue, State> playerChoices = new();
            var oneSupportInRhodeIsland = new SupportChange<Player, State>(Player.Kennedy, State.RI, 1);
            var oneSupportInMaine = new SupportChange<Player, State>(Player.Kennedy, State.ME, 1);
            var twoSupportInNewHampshire = new SupportChange<Player, State>(Player.Kennedy, State.NH, 2);
            var oneSupportInVermont = new SupportChange<Player, State>(Player.Kennedy, State.VT, 1);
            var issueSupport = new SupportChange<Player, Issue>(Player.Kennedy, Issue.Defense, 1);

            playerChoices.StateChanges.Add(oneSupportInRhodeIsland);
            playerChoices.StateChanges.Add(oneSupportInMaine);
            playerChoices.StateChanges.Add(twoSupportInNewHampshire);
            playerChoices.StateChanges.Add(oneSupportInVermont);
            playerChoices.IssueChanges.Add(issueSupport);

            var sut = NineteenSixty.GMTCards[cardIndex];
            var result = sut.AreChangesValid(playerChoices);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void NewEngland_6_FailsValidationIfGreaterThanTwo()
        {
            int cardIndex = 6;
            var engine = GetGameEngine();

            PlayerChosenChanges<Player, Issue, State> playerChoices = new();
            var oneSupportInRhodeIsland = new SupportChange<Player, State>(Player.Kennedy, State.RI, 1);
            var oneSupportInMaine = new SupportChange<Player, State>(Player.Kennedy, State.ME, 1);
            var threeSupportInNewHampshire = new SupportChange<Player, State>(Player.Kennedy, State.NH, 3); ;

            playerChoices.StateChanges.Add(oneSupportInRhodeIsland);
            playerChoices.StateChanges.Add(oneSupportInMaine);
            playerChoices.StateChanges.Add(threeSupportInNewHampshire);

            var sut = NineteenSixty.GMTCards[cardIndex];
            var result = sut.AreChangesValid(playerChoices);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void NewEngland_6_FailsValidationIfExcludedState()
        {
            int cardIndex = 6;
            var engine = GetGameEngine();

            PlayerChosenChanges<Player, Issue, State> playerChoices = new();
            var oneSupportInRhodeIsland = new SupportChange<Player, State>(Player.Kennedy, State.RI, 1);
            var twoSupportInMaine = new SupportChange<Player, State>(Player.Kennedy, State.ME, 2);
            var twoSupportInAlaska = new SupportChange<Player, State>(Player.Kennedy, State.AK, 2); ;

            playerChoices.StateChanges.Add(oneSupportInRhodeIsland);
            playerChoices.StateChanges.Add(twoSupportInMaine);
            playerChoices.StateChanges.Add(twoSupportInAlaska);

            var sut = NineteenSixty.GMTCards[cardIndex];
            var result = sut.AreChangesValid(playerChoices);
            Assert.IsFalse(result);
        }

        #endregion

        #region 7 - Late Returns From Cook County
        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void LateReturnsFromCookCounty_7_SupportCheckWorksAsExpected(Player player)
        {
            int cardIndex = 7;
            var engine = GetGameEngine();

            engine.GainSupport(Player.Nixon, State.IL, 2);

            var sut = NineteenSixty.GMTCards[cardIndex];

            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(Leader.Kennedy, engine.GetLeader(State.IL));
            Assert.AreEqual(1, engine.GetSupportAmount(State.IL));
        }
        #endregion

        #region #8 - Soviet Economic Growth
        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void SovietEconomicGrowth_8_EconomyGoesUpOneSpace(Player player)
        {
            int cardIndex = 8;
            var engine = GetGameEngine();

            engine.SetIssueOrder([Issue.CivilRights, Issue.Defense, Issue.Economy]);

            var sut = NineteenSixty.GMTCards[cardIndex];
            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(Issue.Economy, engine.GetIssueOrder[1]);
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void SovietEconomicGrowth_8_EconomyAtTopRemainsAtTop(Player player)
        {
            int cardIndex = 8;
            var engine = GetGameEngine();

            engine.SetIssueOrder([Issue.Economy, Issue.Defense, Issue.CivilRights]);

            var sut = NineteenSixty.GMTCards[cardIndex];
            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(Issue.Economy, engine.GetIssueOrder[0]);
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void SovietEconomicGrowth_8_StateSupportGained(Player player)
        {
            int cardIndex = 8;
            var engine = GetGameEngine();

            engine.SetIssueOrder([Issue.Economy, Issue.Defense, Issue.CivilRights]);
            engine.GainSupport(player, Issue.Economy, 1);

            var sut = NineteenSixty.GMTCards[cardIndex];
            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(1, engine.GetSupportAmount(State.NY));
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void SovietEconomicGrowth_8_NoStateSupportGainedIfNoLeader(Player player)
        {
            int cardIndex = 8;
            var engine = GetGameEngine();

            engine.SetIssueOrder([Issue.Economy, Issue.Defense, Issue.CivilRights]);

            var sut = NineteenSixty.GMTCards[cardIndex];
            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(0, engine.GetSupportAmount(State.NY));
        }

        [TestMethod]
        public void SovietEconomicGrowth_8_ValidationAlwaysTrue()
        {
            int cardIndex = 8;
            var sut = NineteenSixty.GMTCards[cardIndex];

            var result = sut.AreChangesValid(InvalidChanges);

            Assert.IsTrue(result);
        }
        #endregion

        #region #22 - Gaffe

        [TestMethod]
        [DataRow(Player.Nixon, State.MI)]
        [DataRow(Player.Kennedy, State.AZ)]
        public void Gaffe_22_SupportLost(Player player, State state)
        {
            int cardIndex = 22;
            var engine = GetGameEngine();

            var opponent = player.ToOpponent();

            engine.MovePlayerToState(opponent, state);
            engine.GainSupport(opponent, state, 3);

            var sut = NineteenSixty.GMTCards[cardIndex];
            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(0, engine.GetSupportAmount(state));
            Assert.AreEqual(Leader.None, engine.GetLeader(state));
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void Gaffe_22_MomentumLost(Player player)
        {
            int cardIndex = 22;
            var engine = GetGameEngine();

            var opponent = player.ToOpponent();
            engine.GainMomentum(opponent, 2);

            var sut = NineteenSixty.GMTCards[cardIndex];
            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(1, engine.GetPlayerMomentum(opponent));
        }

        [TestMethod]
        [DataRow(Player.Nixon, State.MO)]
        [DataRow(Player.Kennedy, State.HI)]
        public void Gaffe_22_SupportAndMomentumDoNotGoNegative(Player player, State state)
        {
            int cardIndex = 22;
            var engine = GetGameEngine();

            var opponent = player.ToOpponent();

            engine.MovePlayerToState(opponent, state);
            engine.GainSupport(opponent, state, 1);

            var sut = NineteenSixty.GMTCards[cardIndex];
            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(0, engine.GetSupportAmount(state));
            Assert.AreEqual(Leader.None, engine.GetLeader(state));
        }

        [TestMethod]
        public void Gaffe_22_ValidationAlwaysTrue()
        {
            int cardIndex = 22;
            var sut = NineteenSixty.GMTCards[cardIndex];

            var result = sut.AreChangesValid(InvalidChanges);

            Assert.IsTrue(result);
        }

        #endregion

        #region #23 - Martin Luther King Arrested
        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void MartinLutherKingArrested_23_CivilRightsGoesUpOneSpace(Player player)
        {
            int cardIndex = 23;
            var engine = GetGameEngine();

            engine.SetIssueOrder([Issue.Economy, Issue.Defense, Issue.CivilRights]);

            var sut = NineteenSixty.GMTCards[cardIndex];
            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(Issue.CivilRights, engine.GetIssueOrder[1]);
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void MartinLutherKingArrested_23_EconomyAtTopRemainsAtTop(Player player)
        {
            int cardIndex = 23;
            var engine = GetGameEngine();

            engine.SetIssueOrder([Issue.CivilRights, Issue.Defense, Issue.Economy]);

            var sut = NineteenSixty.GMTCards[cardIndex];
            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(Issue.CivilRights, engine.GetIssueOrder[0]);
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void MartinLutherKingArrested_23_IssueSupportGained(Player player)
        {
            int cardIndex = 23;
            var engine = GetGameEngine();

            engine.SetIssueOrder([Issue.Economy, Issue.Defense, Issue.CivilRights]);
            engine.GainSupport(player, Issue.CivilRights, 1);

            var sut = NineteenSixty.GMTCards[cardIndex];
            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(4, engine.GetSupportAmount(Issue.CivilRights));
            Assert.AreEqual(player.ToLeader(), engine.GetLeader(Issue.CivilRights));
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void MartinLutherKingArrested_23_StateSupportDecaysOpponent(Player player)
        {
            int cardIndex = 23;
            var engine = GetGameEngine();

            engine.SetIssueOrder([Issue.Economy, Issue.Defense, Issue.CivilRights]);
            engine.GainSupport(player.ToOpponent(), Issue.CivilRights, 2);

            var sut = NineteenSixty.GMTCards[cardIndex];
            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(1, engine.GetSupportAmount(Issue.CivilRights));
            Assert.AreEqual(player.ToLeader(), engine.GetLeader(Issue.CivilRights));
        }

        [TestMethod]
        public void MartinLutherKingArrested_23_ValidationAlwaysTrue()
        {
            int cardIndex = 23;
            var sut = NineteenSixty.GMTCards[cardIndex];

            var result = sut.AreChangesValid(InvalidChanges);

            Assert.IsTrue(result);
        }
        #endregion

        #region #25 - 1960 Civil Rights Act
        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void CivilRightsAct_25_CivilRightsGoesUpOneSpace(Player player)
        {
            int cardIndex = 25;
            var engine = GetGameEngine();

            engine.SetIssueOrder([Issue.Economy, Issue.Defense, Issue.CivilRights]);

            var sut = NineteenSixty.GMTCards[cardIndex];
            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(Issue.CivilRights, engine.GetIssueOrder[1]);
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void CivilRightsAct_25_EconomyAtTopRemainsAtTop(Player player)
        {
            int cardIndex = 25;
            var engine = GetGameEngine();

            engine.SetIssueOrder([Issue.CivilRights, Issue.Defense, Issue.Economy]);

            var sut = NineteenSixty.GMTCards[cardIndex];
            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(Issue.CivilRights, engine.GetIssueOrder[0]);
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void CivilRightsAct_25_IssueSupportGained(Player player)
        {
            int cardIndex = 25;
            var engine = GetGameEngine();

            engine.SetIssueOrder([Issue.Economy, Issue.Defense, Issue.CivilRights]);
            engine.GainSupport(Player.Nixon, Issue.CivilRights, 1);

            var sut = NineteenSixty.GMTCards[cardIndex];
            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(2, engine.GetSupportAmount(Issue.CivilRights));
            Assert.AreEqual(Leader.Nixon, engine.GetLeader(Issue.CivilRights));
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void CivilRightsAct_25_StateSupportDecaysOpponent(Player player)
        {
            int cardIndex = 25;
            var engine = GetGameEngine();

            engine.SetIssueOrder([Issue.Economy, Issue.Defense, Issue.CivilRights]);
            engine.GainSupport(Player.Kennedy, Issue.CivilRights, 2);

            var sut = NineteenSixty.GMTCards[cardIndex];
            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(1, engine.GetSupportAmount(Issue.CivilRights));
            Assert.AreEqual(Leader.Kennedy, engine.GetLeader(Issue.CivilRights));
        }

        [TestMethod]
        public void CivilRightsAct_25_ValidationAlwaysTrue()
        {
            int cardIndex = 25;
            var sut = NineteenSixty.GMTCards[cardIndex];

            var result = sut.AreChangesValid(InvalidChanges);

            Assert.IsTrue(result);
        }
        #endregion

        #region #29 - The Great Seal Bug

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void TheGreatSealBug_29_NixonGainsDefenseSupport(Player player)
        {
            int cardIndex = 29;
            var engine = GetGameEngine();

            var sut = NineteenSixty.GMTCards[cardIndex];
            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(1, engine.GetSupportAmount(Issue.Defense));
            
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void TheGreatSealBug_29_HenryCabotLodgeRecoveredFromDiscard(Player player)
        {
            int cardIndex = 29;
            var engine = GetGameEngine();

            var cardToRetrieve = NineteenSixty.GMTCards[42];

            engine.MoveCardFromOneZoneToAnother(Player.Nixon, NineteenSixty.GMTCards[42], CardZone.Deck, CardZone.Discard);

            var sut = NineteenSixty.GMTCards[cardIndex];
            

            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(cardToRetrieve, engine.GetPlayerHand(Player.Nixon).First());
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void TheGreatSealBug_29_HenryCabotLodgeNotRecoveredFromRemovedPile(Player player)
        {
            int cardIndex = 29;
            var engine = GetGameEngine();

            var cardToRetrieve = NineteenSixty.GMTCards[42];

            engine.MoveCardFromOneZoneToAnother(Player.Nixon, NineteenSixty.GMTCards[42], CardZone.Deck, CardZone.Removed);

            var sut = NineteenSixty.GMTCards[cardIndex];

            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(0, engine.GetPlayerHand(Player.Nixon).Count());
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void TheGreatSealBug_29_HenryCabotLodgeNotRecoveredFromStrategyPile(Player player)
        {
            int cardIndex = 29;
            var engine = GetGameEngine();

            var cardToRetrieve = NineteenSixty.GMTCards[42];

            engine.MoveCardFromOneZoneToAnother(player, NineteenSixty.GMTCards[42], CardZone.Deck, CardZone.CampaignStrategy);

            var sut = NineteenSixty.GMTCards[cardIndex];

            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(0, engine.GetPlayerHand(Player.Nixon).Count());
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void TheGreatSealBug_29_HenryCabotLodgeNotRecoveredFromOpponentHand(Player player)
        {
            int cardIndex = 29;
            var engine = GetGameEngine();

            var cardToRetrieve = NineteenSixty.GMTCards[42];

            engine.MoveCardFromOneZoneToAnother(Player.Kennedy, NineteenSixty.GMTCards[42], CardZone.Deck, CardZone.Hand);

            var sut = NineteenSixty.GMTCards[cardIndex];

            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(0, engine.GetPlayerHand(Player.Nixon).Count());
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void TheGreatSealBug_29_HenryCabotLodgeNotRecoveredFromDeck(Player player)
        {
            int cardIndex = 29;
            var engine = GetGameEngine();

            var cardToRetrieve = NineteenSixty.GMTCards[42];
            var sut = NineteenSixty.GMTCards[cardIndex];

            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(0, engine.GetPlayerHand(Player.Nixon).Count());
        }

        [TestMethod]
        public void TheGreatSealBug_29_ValidationAlwaysTrue()
        {
            int cardIndex = 29;
            var sut = NineteenSixty.GMTCards[cardIndex];

            var result = sut.AreChangesValid(InvalidChanges);

            Assert.IsTrue(result);
        }

        #endregion

        #region #37 - Lunch Counter Sit-Ins
        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void LunchCounterSitIns_37_CivilRightsMovesUp(Player player)
        {
            int cardIndex = 37;
            var engine = GetGameEngine();

            engine.GainSupport(player, Issue.CivilRights, 1);
            engine.SetIssueOrder([Issue.Economy, Issue.Defense, Issue.CivilRights]);

            PlayerChosenChanges<Player, Issue, State> playerChoices = new();
            var oneSupportInHawaii = new SupportChange<Player, State>(player, State.HI, 1);
            var oneSupportInFlorida = new SupportChange<Player, State>(player, State.FL, 1);
            var oneSupportInVermont = new SupportChange<Player, State>(player, State.VT, 1);

            playerChoices.StateChanges.Add(oneSupportInHawaii);
            playerChoices.StateChanges.Add(oneSupportInFlorida);
            playerChoices.StateChanges.Add(oneSupportInVermont);

            var sut = NineteenSixty.GMTCards[cardIndex];

            sut.Event(engine, player, playerChoices);

            Assert.AreEqual(Issue.CivilRights, engine.GetIssueOrder[1]);

        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void LunchCounterSitIns_37_SupportAddedToStates(Player player)
        {
            int cardIndex = 37;
            var engine = GetGameEngine();

            engine.GainSupport(player, Issue.CivilRights, 1);

            PlayerChosenChanges<Player, Issue, State> playerChoices = new();
            var oneSupportInHawaii = new SupportChange<Player, State>(player, State.HI, 1);
            var oneSupportInFlorida = new SupportChange<Player, State>(player, State.FL, 1);
            var oneSupportInVermont = new SupportChange<Player, State>(player, State.VT, 1);

            playerChoices.StateChanges.Add(oneSupportInHawaii);
            playerChoices.StateChanges.Add(oneSupportInFlorida);
            playerChoices.StateChanges.Add(oneSupportInVermont);

            var sut = NineteenSixty.GMTCards[cardIndex];

            sut.Event(engine, player, playerChoices);

            Assert.AreEqual(1, engine.GetSupportAmount(State.HI));
            Assert.AreEqual(1, engine.GetSupportAmount(State.FL));
            Assert.AreEqual(1, engine.GetSupportAmount(State.VT));
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void LunchCounterSitIns_37_FailsValidationIfNonLeaderGains(Player player)
        {
            int cardIndex = 37;
            var engine = GetGameEngine();

            engine.GainSupport(player, Issue.CivilRights, 1);

            PlayerChosenChanges<Player, Issue, State> playerChoices = new();
            var oneSupportInHawaii = new SupportChange<Player, State>(player, State.HI, 1);
            var oneSupportInFlorida = new SupportChange<Player, State>(player.ToOpponent(), State.FL, 1);
            var oneSupportInVermont = new SupportChange<Player, State>(player, State.VT, 1);

            playerChoices.StateChanges.Add(oneSupportInHawaii);
            playerChoices.StateChanges.Add(oneSupportInFlorida);
            playerChoices.StateChanges.Add(oneSupportInVermont);

            var sut = NineteenSixty.GMTCards[cardIndex];
            var result = sut.AreChangesValid(playerChoices);
            Assert.IsFalse(result);
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void LunchCounterSitIns_37_FailsValidationIfIssueGains(Player player)
        {
            int cardIndex = 37;
            var engine = GetGameEngine();

            engine.GainSupport(player, Issue.CivilRights, 1);

            PlayerChosenChanges<Player, Issue, State> playerChoices = new();
            var oneSupportInHawaii = new SupportChange<Player, State>(player, State.HI, 1);
            var oneSupportInFlorida = new SupportChange<Player, State>(player, State.FL, 1);
            var oneSupportInVermont = new SupportChange<Player, State>(player, State.VT, 1);
            playerChoices.StateChanges.Add(oneSupportInHawaii);
            playerChoices.StateChanges.Add(oneSupportInFlorida);
            playerChoices.StateChanges.Add(oneSupportInVermont);

            var issueSupport = new SupportChange<Player, Issue>(player, Issue.Defense, 1);
            playerChoices.IssueChanges.Add(issueSupport);

            var sut = NineteenSixty.GMTCards[cardIndex];
            var result = sut.AreChangesValid(playerChoices);
            Assert.IsFalse(result);
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void LunchCounterSitIns_37_FailsValidationIfGreaterThanOne(Player player)
        {
            int cardIndex = 37;
            var engine = GetGameEngine();

            PlayerChosenChanges<Player, Issue, State> playerChoices = new();
            var oneSupportInHawaii = new SupportChange<Player, State>(player, State.HI, 1);
            var twoSupportInFlorida = new SupportChange<Player, State>(player, State.FL, 2);
            playerChoices.StateChanges.Add(oneSupportInHawaii);
            playerChoices.StateChanges.Add(twoSupportInFlorida);

            var sut = NineteenSixty.GMTCards[cardIndex];
            var result = sut.AreChangesValid(playerChoices);
            Assert.IsFalse(result);
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void LunchCounterSitIns_37_FailsValidationIfSumGreaterThanThree(Player player)
        {
            int cardIndex = 37;
            var engine = GetGameEngine();

            PlayerChosenChanges<Player, Issue, State> playerChoices = new();
            var oneSupportInHawaii = new SupportChange<Player, State>(player, State.HI, 1);
            var oneSupportInFlorida = new SupportChange<Player, State>(player, State.FL, 1);
            var oneSupportInVermont = new SupportChange<Player, State>(player, State.VT, 1);
            var oneSupportInMissouri = new SupportChange<Player, State>(player, State.MO, 1);
            playerChoices.StateChanges.Add(oneSupportInHawaii);
            playerChoices.StateChanges.Add(oneSupportInFlorida);
            playerChoices.StateChanges.Add(oneSupportInVermont);
            playerChoices.StateChanges.Add(oneSupportInMissouri);

            var sut = NineteenSixty.GMTCards[cardIndex];
            var result = sut.AreChangesValid(playerChoices);
            Assert.IsFalse(result);
        }
        #endregion

        #region #39 - Lyndon Johnson

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void LyndonJohnson_39_SupportAddedToStates(Player player)
        {
            int cardIndex = 39;
            var engine = GetGameEngine();

            PlayerChosenChanges<Player, Issue, State> playerChoices = new();
            var twoSupportInTexas = new SupportChange<Player, State>(Player.Kennedy, State.TX, 2);
            var oneSupportInFlorida = new SupportChange<Player, State>(Player.Kennedy, State.FL, 1);
            var oneSupportInAlabama = new SupportChange<Player, State>(Player.Kennedy, State.AL, 1);
            var oneSupportInTennessee = new SupportChange<Player, State>(Player.Kennedy, State.TN, 1);

            playerChoices.StateChanges.Add(twoSupportInTexas);
            playerChoices.StateChanges.Add(oneSupportInFlorida);
            playerChoices.StateChanges.Add(oneSupportInAlabama);
            playerChoices.StateChanges.Add(oneSupportInTennessee);

            var sut = NineteenSixty.GMTCards[cardIndex];

            sut.Event(engine, player, playerChoices);

            Assert.AreEqual(2, engine.GetSupportAmount(State.TX));
            Assert.AreEqual(1, engine.GetSupportAmount(State.FL));
            Assert.AreEqual(1, engine.GetSupportAmount(State.AL));
            Assert.AreEqual(1, engine.GetSupportAmount(State.TN));
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void LyndonJohnson_39_PlayerCardUnexhausted(Player player)
        {
            int cardIndex = 39;
            var engine = GetGameEngine();

            PlayerChosenChanges<Player, Issue, State> playerChoices = new();
            var twoSupportInTexas = new SupportChange<Player, State>(Player.Kennedy, State.TX, 2);
            var oneSupportInFlorida = new SupportChange<Player, State>(Player.Kennedy, State.FL, 1);
            var oneSupportInAlabama = new SupportChange<Player, State>(Player.Kennedy, State.AL, 1);
            var oneSupportInTennessee = new SupportChange<Player, State>(Player.Kennedy, State.TN, 1);

            playerChoices.StateChanges.Add(twoSupportInTexas);
            playerChoices.StateChanges.Add(oneSupportInFlorida);
            playerChoices.StateChanges.Add(oneSupportInAlabama);
            playerChoices.StateChanges.Add(oneSupportInTennessee);

            engine.ExhaustPlayer(Player.Kennedy);

            var sut = NineteenSixty.GMTCards[cardIndex];

            sut.Event(engine, player, playerChoices);

            Assert.IsTrue(engine.IsPlayerReady(Player.Kennedy));

        }

        [TestMethod]
        public void LyndonJohnson_39_FailsValidationIfNixonGains()
        {
            int cardIndex = 39;
            var engine = GetGameEngine();

            PlayerChosenChanges<Player, Issue, State> playerChoices = new();
            var twoSupportInTexas = new SupportChange<Player, State>(Player.Kennedy, State.TX, 2);
            var oneSupportInFlorida = new SupportChange<Player, State>(Player.Nixon, State.FL, 1);
            var oneSupportInAlabama = new SupportChange<Player, State>(Player.Kennedy, State.AL, 1);
            var oneSupportInTennessee = new SupportChange<Player, State>(Player.Kennedy, State.TN, 1);

            playerChoices.StateChanges.Add(twoSupportInTexas);
            playerChoices.StateChanges.Add(oneSupportInFlorida);
            playerChoices.StateChanges.Add(oneSupportInAlabama);
            playerChoices.StateChanges.Add(oneSupportInTennessee);

            var sut = NineteenSixty.GMTCards[cardIndex];
            var result = sut.AreChangesValid(playerChoices);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void LyndonJohnson_39_FailsValidationIfIssueGains()
        {
            int cardIndex = 39;
            var engine = GetGameEngine();

            PlayerChosenChanges<Player, Issue, State> playerChoices = new();

            var twoSupportInTexas = new SupportChange<Player, State>(Player.Kennedy, State.TX, 2);
            var oneSupportInFlorida = new SupportChange<Player, State>(Player.Nixon, State.FL, 1);
            var oneSupportInAlabama = new SupportChange<Player, State>(Player.Kennedy, State.AL, 1);
            var oneSupportInTennessee = new SupportChange<Player, State>(Player.Kennedy, State.TN, 1);
            var issueSupport = new SupportChange<Player, Issue>(Player.Kennedy, Issue.Defense, 1);

            playerChoices.StateChanges.Add(twoSupportInTexas);
            playerChoices.StateChanges.Add(oneSupportInFlorida);
            playerChoices.StateChanges.Add(oneSupportInAlabama);
            playerChoices.StateChanges.Add(oneSupportInTennessee);
            playerChoices.IssueChanges.Add(issueSupport);

            var sut = NineteenSixty.GMTCards[cardIndex];
            var result = sut.AreChangesValid(playerChoices);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void LyndonJohnson_39_FailsValidationIfGreaterThanOne()
        {
            int cardIndex = 39;
            var engine = GetGameEngine();

            PlayerChosenChanges<Player, Issue, State> playerChoices = new();
            var twoSupportInTexas = new SupportChange<Player, State>(Player.Kennedy, State.TX, 2);
            var threeSupportInFlorida = new SupportChange<Player, State>(Player.Kennedy, State.FL, 3);

            playerChoices.StateChanges.Add(twoSupportInTexas);
            playerChoices.StateChanges.Add(threeSupportInFlorida);

            var sut = NineteenSixty.GMTCards[cardIndex];
            var result = sut.AreChangesValid(playerChoices);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void LyndonJohnson_39_FailsValidationIfStateNotInCorrectRegion()
        {
            int cardIndex = 39;
            var engine = GetGameEngine();

            PlayerChosenChanges<Player, Issue, State> playerChoices = new();
            var twoSupportInTexas = new SupportChange<Player, State>(Player.Kennedy, State.TX, 2);
            var oneSupportInFlorida = new SupportChange<Player, State>(Player.Kennedy, State.FL, 1);
            var oneSupportInAlabama = new SupportChange<Player, State>(Player.Kennedy, State.AL, 1);
            var oneSupportInOhio = new SupportChange<Player, State>(Player.Kennedy, State.OH, 1);

            playerChoices.StateChanges.Add(twoSupportInTexas);
            playerChoices.StateChanges.Add(oneSupportInFlorida);
            playerChoices.StateChanges.Add(oneSupportInAlabama);
            playerChoices.StateChanges.Add(oneSupportInOhio);

            var sut = NineteenSixty.GMTCards[cardIndex];
            var result = sut.AreChangesValid(playerChoices);
            Assert.IsFalse(result);
        }

        #endregion

        #region #41 - Pierre Salinger

        [TestMethod]
        [DataRow(Issue.CivilRights)]
        [DataRow(Issue.Defense)]
        [DataRow(Issue.Economy)]
        public void PierreSalinger_41_SupportChangesReflectedInIssue(Issue issue)
        {
            int cardIndex = 41;
            var engine = GetGameEngine();

            engine.GainSupport(Player.Kennedy, issue, 1);

            PlayerChosenChanges<Player, Issue, State> playerChoices = new();
            var threeSupportInOneIssue = new SupportChange<Player, Issue>(Player.Kennedy, issue, 3);
            playerChoices.IssueChanges.Add(threeSupportInOneIssue);

            var sut = NineteenSixty.GMTCards[cardIndex];

            sut.Event(engine, Player.Kennedy, playerChoices);

            Assert.AreEqual(4, engine.GetSupportAmount(issue));
        }

        [TestMethod]
        [DataRow(Issue.CivilRights)]
        [DataRow(Issue.Defense)]
        [DataRow(Issue.Economy)]
        public void PierreSalinger_41_SupportChangesDeductedFromOpponent(Issue issue)
        {
            int cardIndex = 41;
            var engine = GetGameEngine();
            
            engine.GainSupport(Player.Nixon, issue, 2);

            PlayerChosenChanges<Player, Issue, State> playerChoices = new();
            var threeSupportInOneIssue = new SupportChange<Player, Issue>(Player.Kennedy, issue, 3);
            playerChoices.IssueChanges.Add(threeSupportInOneIssue);

            var sut = NineteenSixty.GMTCards[cardIndex];

            sut.Event(engine, Player.Kennedy, playerChoices);

            Assert.AreEqual(1, engine.GetSupportAmount(issue));
        }

        [TestMethod]
        [DataRow(Issue.CivilRights)]
        [DataRow(Issue.Defense)]
        [DataRow(Issue.Economy)]
        public void PierreSalinger_41_FailsValidationIfNixonGainsSupport(Issue issue)
        {
            int cardIndex = 41;

            PlayerChosenChanges<Player, Issue, State> playerChoices = new();
            var threeSupportInOneIssue = new SupportChange<Player, Issue>(Player.Nixon, issue, 3);
            playerChoices.IssueChanges.Add(threeSupportInOneIssue);

            var sut = NineteenSixty.GMTCards[cardIndex];
            var result = sut.AreChangesValid(playerChoices);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void PierreSalinger_41_FailsValidationIfSupportGainedInMoreThanOneIssue()
        {
            int cardIndex = 41;

            PlayerChosenChanges<Player, Issue, State> playerChoices = new();
            var oneSupportInCivilRights = new SupportChange<Player, Issue>(Player.Kennedy, Issue.CivilRights, 1);
            var oneSupportInDefense = new SupportChange<Player, Issue>(Player.Kennedy, Issue.Defense, 1);
            var oneSupportInEconomy = new SupportChange<Player, Issue>(Player.Kennedy, Issue.Economy, 1);
            playerChoices.IssueChanges.Add(oneSupportInCivilRights);
            playerChoices.IssueChanges.Add(oneSupportInDefense);
            playerChoices.IssueChanges.Add(oneSupportInEconomy);

            var sut = NineteenSixty.GMTCards[cardIndex];
            var result = sut.AreChangesValid(playerChoices);
            Assert.IsFalse(result);
        }

        [TestMethod]
        [DataRow(Issue.CivilRights)]
        [DataRow(Issue.Defense)]
        [DataRow(Issue.Economy)]
        public void PierreSalinger_41_FailsValidationIfStateSupportGained(Issue issue)
        {
            int cardIndex = 41;

            PlayerChosenChanges<Player, Issue, State> playerChoices = new();
            var threeSupportInOneIssue = new SupportChange<Player, Issue>(Player.Kennedy, issue, 3);
            var oneSupportInNewYork = new SupportChange<Player, State>(Player.Kennedy, State.NY, 1);
            playerChoices.IssueChanges.Add(threeSupportInOneIssue);
            playerChoices.StateChanges.Add(oneSupportInNewYork);

            var sut = NineteenSixty.GMTCards[cardIndex];
            var result = sut.AreChangesValid(playerChoices);
            Assert.IsFalse(result);
        }

        #endregion

        #region #42 Henry Cabot Lodge
        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void HenryCabotLodge_42_StateSupportGained(Player player)
        {
            int cardIndex = 42;
            var engine = GetGameEngine();
            engine.GainSupport(Player.Kennedy, State.MA, 1);
            engine.GainSupport(Player.Kennedy, Issue.Defense, 1);

            var sut = NineteenSixty.GMTCards[cardIndex];

            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(Leader.Nixon, engine.GetLeader(State.MA));
            Assert.AreEqual(1, engine.GetSupportAmount(State.MA));
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void HenryCabotLodge_42_IssueSupportGained(Player player)
        {
            int cardIndex = 42;
            var engine = GetGameEngine();
            engine.GainSupport(Player.Kennedy, State.MA, 1);
            engine.GainSupport(Player.Kennedy, Issue.Defense, 1);

            var sut = NineteenSixty.GMTCards[cardIndex];

            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(Leader.Nixon, engine.GetLeader(Issue.Defense));
            Assert.AreEqual(1, engine.GetSupportAmount(Issue.Defense));
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void HenryCabotLodge_42_NixonIsUnexhausted(Player player)
        {
            int cardIndex = 42;
            var engine = GetGameEngine();
            engine.ExhaustPlayer(Player.Nixon);

            var sut = NineteenSixty.GMTCards[cardIndex];

            sut.Event(engine, player, EmptyChanges);

            Assert.IsTrue(engine.IsPlayerReady(Player.Nixon));
        }
        #endregion

        #region #48 - Rising Food Prices
        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void RisingFoodPrices_48_EconomyGoesUpOneSpace(Player player)
        {
            int cardIndex = 48;
            var engine = GetGameEngine();

            engine.SetIssueOrder([Issue.CivilRights, Issue.Defense, Issue.Economy]);

            var sut = NineteenSixty.GMTCards[cardIndex];

            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(Issue.Economy, engine.GetIssueOrder[1]);
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void RisingFoodPrices_48_EconomyAtTopRemainsAtTop(Player player)
        {
            int cardIndex = 48;
            var engine = GetGameEngine();

            engine.SetIssueOrder([Issue.Economy, Issue.Defense, Issue.CivilRights]);

            var sut = NineteenSixty.GMTCards[cardIndex];

            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(Issue.Economy, engine.GetIssueOrder[0]);
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void RisingFoodPrices_48_IssueSupportGained(Player player)
        {
            int cardIndex = 48;
            var engine = GetGameEngine();

            engine.SetIssueOrder([Issue.Economy, Issue.Defense, Issue.CivilRights]);

            var sut = NineteenSixty.GMTCards[cardIndex];

            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(Leader.Nixon, engine.GetLeader(Issue.Economy));
            Assert.AreEqual(2, engine.GetSupportAmount(Issue.Economy));
        }

        [TestMethod]
        public void RisingFoodPrices_48_ValidationAlwaysTrue()
        {
            int cardIndex = 48;
            var sut = NineteenSixty.GMTCards[cardIndex];

            var result = sut.AreChangesValid(InvalidChanges);

            Assert.IsTrue(result);
        }
        #endregion

        #region #51 - Missile Gap
        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void MissileGap_51_ExpectedSupportGained(Player player)
        {
            int cardIndex = 51;
            var engine = GetGameEngine();

            var sut = NineteenSixty.GMTCards[cardIndex];

            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(Leader.Kennedy, engine.GetLeader(Issue.Defense));
            Assert.AreEqual(3, engine.GetSupportAmount(Issue.Defense));
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void MissileGap_51_SupportFromOpponentRemoved(Player player)
        {
            int cardIndex = 51;
            var engine = GetGameEngine();


            engine.GainSupport(Player.Nixon, Issue.Defense, 2);
            var sut = NineteenSixty.GMTCards[cardIndex];

            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(Leader.Kennedy, engine.GetLeader(Issue.Defense));
            Assert.AreEqual(1, engine.GetSupportAmount(Issue.Defense));
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void MissileGap_51_SupportChangedToZero(Player player)
        {
            int cardIndex = 51;
            var engine = GetGameEngine();

            engine.GainSupport(Player.Nixon, Issue.Defense, 3);

            var sut = NineteenSixty.GMTCards[cardIndex];
            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(Leader.None, engine.GetLeader(Issue.Defense));
            Assert.AreEqual(0, engine.GetSupportAmount(Issue.Defense));
        }

        [TestMethod]
        public void MissileGap_51_ValidationAlwaysTrue()
        {
            int cardIndex = 51;
            var sut = NineteenSixty.GMTCards[cardIndex];

            var result = sut.AreChangesValid(InvalidChanges);

            Assert.IsTrue(result);
        }
        #endregion

        #region #51 - Hurricane Donna

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void HurricaneDonna_52_PlayerMoved(Player player)
        {
            int cardIndex = 52;
            var engine = GetGameEngine();

            var sut = NineteenSixty.GMTCards[cardIndex];
            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(State.FL, engine.GetPlayerState(player));
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void HurricaneDonna_52_SupportGained(Player player)
        {
            int cardIndex = 52;
            var engine = GetGameEngine();

            var sut = NineteenSixty.GMTCards[cardIndex];
            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(1, engine.GetSupportAmount(State.FL));
        }


        [TestMethod]
        public void HurricaneDonna_52_ValidationAlwaysTrue()
        {
            int cardIndex = 52;
            var sut = NineteenSixty.GMTCards[cardIndex];

            var result = sut.AreChangesValid(InvalidChanges);

            Assert.IsTrue(result);
        }

        #endregion

        #region #61 - Fatigue Sets In
        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void FatigueSetsIn_61_OpponentExhausted(Player player)
        {
            int cardIndex = 61;
            var engine = GetGameEngine();

            var sut = NineteenSixty.GMTCards[cardIndex];
            sut.Event(engine, player, EmptyChanges);

            Assert.IsFalse(engine.IsPlayerReady(player.ToOpponent()));
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void FatigueSetsIn_61_OpponentAlreadyExhaustedStillWorks(Player player)
        {
            int cardIndex = 61;
            var engine = GetGameEngine();
            engine.ExhaustPlayer(player.ToOpponent());

            var sut = NineteenSixty.GMTCards[cardIndex];
            sut.Event(engine, player, EmptyChanges);

            Assert.IsFalse(engine.IsPlayerReady(player.ToOpponent()));
        }
        #endregion

        #region #62 - The Trial of Gary Powers
        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void TheTrialOfGaryPowers_62_DefenseMovesUpTwoSpaces(Player player)
        {
            int cardIndex = 62;
            var engine = GetGameEngine();

            engine.SetIssueOrder([Issue.CivilRights, Issue.Economy, Issue.Defense]);

            var sut = NineteenSixty.GMTCards[cardIndex];
            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(Issue.Defense, engine.GetIssueOrder[0]);
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void TheTrialOfGaryPowers_62_DefenseAtTopUnchanged(Player player)
        {
            int cardIndex = 62;
            var engine = GetGameEngine();

            engine.SetIssueOrder([Issue.Defense, Issue.Economy, Issue.CivilRights]);

            var sut = NineteenSixty.GMTCards[cardIndex];
            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(Issue.Defense, engine.GetIssueOrder[0]);
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void TheTrialOfGaryPowers_62_LeaderAwardedMomentum(Player player)
        {
            int cardIndex = 62;
            var engine = GetGameEngine();

            engine.GainSupport(player, Issue.Defense, 1);

            var sut = NineteenSixty.GMTCards[cardIndex];
            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(1, engine.GetPlayerMomentum(player));
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void TheTrialOfGaryPowers_62_NoLeaderNoMomentumChange(Player player)
        {
            int cardIndex = 62;
            var engine = GetGameEngine();

            var sut = NineteenSixty.GMTCards[cardIndex];
            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(0, engine.GetPlayerMomentum(Player.Kennedy));
            Assert.AreEqual(0, engine.GetPlayerMomentum(Player.Nixon));
        }

        [TestMethod]
        public void TheTrialOfGaryPowers_62_ValidationAlwaysTrue()
        {
            int cardIndex = 62;
            var sut = NineteenSixty.GMTCards[cardIndex];

            var result = sut.AreChangesValid(InvalidChanges);

            Assert.IsTrue(result);
        }
        #endregion

        #region #63 - “Give Me A Week”
        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void GiveMeAWeek_63_ExpectedSupportAndMomentumLost(Player player)
        {
            int cardIndex = 63;
            var engine = GetGameEngine();

            engine.GainMomentum(Player.Nixon, 5);
            engine.GainSupport(Player.Nixon, Issue.Defense, 4);
            engine.GainSupport(Player.Nixon, Issue.Economy, 3);
            engine.GainSupport(Player.Nixon, Issue.CivilRights, 2);

            var sut = NineteenSixty.GMTCards[cardIndex];
            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(3, engine.GetPlayerMomentum(Player.Nixon));
            Assert.AreEqual(3, engine.GetSupportAmount(Issue.Defense));
            Assert.AreEqual(2, engine.GetSupportAmount(Issue.Economy));
            Assert.AreEqual(1, engine.GetSupportAmount(Issue.CivilRights));
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void GiveMeAWeek_63_SupportAndMomentumDoNotGoNegative(Player player)
        {
            int cardIndex = 63;
            var engine = GetGameEngine();

            engine.GainMomentum(Player.Nixon, 1);
            engine.GainSupport(Player.Nixon, Issue.Defense, 0);
            engine.GainSupport(Player.Nixon, Issue.Economy, 0);
            engine.GainSupport(Player.Nixon, Issue.CivilRights, 0);

            var sut = NineteenSixty.GMTCards[cardIndex];
            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(0, engine.GetPlayerMomentum(Player.Nixon));
            Assert.AreEqual(0, engine.GetSupportAmount(Issue.Defense));
            Assert.AreEqual(0, engine.GetSupportAmount(Issue.Economy));
            Assert.AreEqual(0, engine.GetSupportAmount(Issue.CivilRights));
        }

        [TestMethod]
        public void TheTrialOfGaryPowers_63_ValidationAlwaysTrue()
        {
            int cardIndex = 63;
            var sut = NineteenSixty.GMTCards[cardIndex];

            var result = sut.AreChangesValid(InvalidChanges);

            Assert.IsTrue(result);
        }
        #endregion

        #region #64 - Stump Speech
        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void StumpSpeech_64_LowerMomentumIsGained(Player player)
        {
            int cardIndex = 64;
            var engine = GetGameEngine();

            engine.GainMomentum(player.ToOpponent(), 5);

            var sut = NineteenSixty.GMTCards[cardIndex];
            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(engine.GetPlayerMomentum(player), engine.GetPlayerMomentum(player.ToOpponent()));
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void StumpSpeech_64_HigherMomentumNoChange(Player player)
        {
            int cardIndex = 64;
            var engine = GetGameEngine();

            engine.GainMomentum(player, 5);

            var sut = NineteenSixty.GMTCards[cardIndex];
            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(5, engine.GetPlayerMomentum(player));
            Assert.AreEqual(0, engine.GetPlayerMomentum(player.ToOpponent()));
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void StumpSpeech_64_TiedMomentumNoChange(Player player)
        {
            int cardIndex = 64;
            var engine = GetGameEngine();

            var sut = NineteenSixty.GMTCards[cardIndex];
            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(engine.GetPlayerMomentum(player), engine.GetPlayerMomentum(player.ToOpponent()));
        }

        [TestMethod]
        public void StumpSpeech_64_ValidationAlwaysTrue()
        {
            int cardIndex = 64;
            var sut = NineteenSixty.GMTCards[cardIndex];

            var result = sut.AreChangesValid(InvalidChanges);

            Assert.IsTrue(result);
        }
        #endregion

        #region #68 - "Peace Without Surrender"
        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void PeaceWithoutSurrender_68_DefenseGoesUpOneSpace(Player player)
        {
            int cardIndex = 68;
            var engine = GetGameEngine();

            engine.SetIssueOrder([Issue.CivilRights, Issue.Economy, Issue.Defense]);

            var sut = NineteenSixty.GMTCards[cardIndex];

            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(Issue.Defense, engine.GetIssueOrder[1]);
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void PeaceWithoutSurrender_68_EconomyAtTopRemainsAtTop(Player player)
        {
            int cardIndex = 68;
            var engine = GetGameEngine();

            engine.SetIssueOrder([Issue.Defense, Issue.Economy, Issue.CivilRights]);

            var sut = NineteenSixty.GMTCards[cardIndex];

            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(Issue.Defense, engine.GetIssueOrder[0]);
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void PeaceWithoutSurrender_68_IssueSupportGained(Player player)
        {
            int cardIndex = 68;
            var engine = GetGameEngine();

            engine.SetIssueOrder([Issue.Defense, Issue.Economy, Issue.CivilRights]);

            var sut = NineteenSixty.GMTCards[cardIndex];

            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(Leader.Nixon, engine.GetLeader(Issue.Defense));
            Assert.AreEqual(1, engine.GetSupportAmount(Issue.Defense));
        }

        [TestMethod]
        public void PeaceWithoutSurrender_68_ValidationAlwaysTrue()
        {
            int cardIndex = 68;
            var sut = NineteenSixty.GMTCards[cardIndex];

            var result = sut.AreChangesValid(InvalidChanges);

            Assert.IsTrue(result);
        }
        #endregion

        #region #70 - The Old Nixon
        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void TheOldNixon_70_ExpectedAmountOfMomentumLost(Player player)
        {
            int cardIndex = 70;
            var engine = GetGameEngine();

            engine.GainMomentum(Player.Nixon, 5);
            engine.GainMomentum(Player.Kennedy, 5);

            var nixonStartingMomentum = engine.GetPlayerMomentum(Player.Kennedy);
            var kennedyStartingMomentum = engine.GetPlayerMomentum(Player.Nixon);

            var sut = NineteenSixty.GMTCards[cardIndex];

            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(engine.GetPlayerMomentum(Player.Nixon), nixonStartingMomentum - 1);
            Assert.AreEqual(engine.GetPlayerMomentum(Player.Kennedy), kennedyStartingMomentum - 3);
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void TheOldNixon_70_MomentumDoesNotGoNegative(Player player)
        {
            int cardIndex = 70;
            var engine = GetGameEngine();

            engine.GainMomentum(Player.Nixon, 0);
            engine.GainMomentum(Player.Kennedy, 2);

            var sut = NineteenSixty.GMTCards[cardIndex];

            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(engine.GetPlayerMomentum(Player.Nixon), 0);
            Assert.AreEqual(engine.GetPlayerMomentum(Player.Kennedy), 0);
        }

        [TestMethod]
        public void TheOldNixon_70_ValidationAlwaysTrue()
        {
            int cardIndex = 70;
            var sut = NineteenSixty.GMTCards[cardIndex];

            var result = sut.AreChangesValid(InvalidChanges);

            Assert.IsTrue(result);
        }

        #endregion

        #region #71 - Heartland of America
        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void HeartlandOfAmerica_71_SupportAddedToStates(Player player)
        {
            int cardIndex = 71;
            var engine = GetGameEngine();

            PlayerChosenChanges<Player, Issue, State> playerChoices = new();
            var oneSupportInWyoming = new SupportChange<Player, State>(Player.Nixon, State.WY, 1);
            var oneSupportInIdaho = new SupportChange<Player, State>(Player.Nixon, State.ID, 1);
            var oneSupportInNorthDakota = new SupportChange<Player, State>(Player.Nixon, State.ND, 1);
            var oneSupportInIowa = new SupportChange<Player, State>(Player.Nixon, State.IA, 1);
            var oneSupportInKentucky = new SupportChange<Player, State>(Player.Nixon, State.KY, 1);
            var oneSupportInOklahoma = new SupportChange<Player, State>(Player.Nixon, State.OK, 1);
            var oneSupportInNebraska = new SupportChange<Player, State>(Player.Nixon, State.NE, 1);

            playerChoices.StateChanges.Add(oneSupportInWyoming);
            playerChoices.StateChanges.Add(oneSupportInIdaho);
            playerChoices.StateChanges.Add(oneSupportInNorthDakota);
            playerChoices.StateChanges.Add(oneSupportInIowa);
            playerChoices.StateChanges.Add(oneSupportInKentucky);
            playerChoices.StateChanges.Add(oneSupportInOklahoma);
            playerChoices.StateChanges.Add(oneSupportInNebraska);

            var sut = NineteenSixty.GMTCards[cardIndex];

            sut.Event(engine, player, playerChoices);

            Assert.AreEqual(1, engine.GetSupportAmount(State.WY));
            Assert.AreEqual(1, engine.GetSupportAmount(State.ID));
            Assert.AreEqual(1, engine.GetSupportAmount(State.ND));
            Assert.AreEqual(1, engine.GetSupportAmount(State.IA));
            Assert.AreEqual(1, engine.GetSupportAmount(State.KY));
            Assert.AreEqual(1, engine.GetSupportAmount(State.OK));
            Assert.AreEqual(1, engine.GetSupportAmount(State.NE));
        }

        [TestMethod]
        public void HeartlandOfAmerica_71_FailsValidationIfKennedyGains()
        {
            int cardIndex = 71;
            var engine = GetGameEngine();

            PlayerChosenChanges<Player, Issue, State> playerChoices = new();
            var oneSupportInWyoming = new SupportChange<Player, State>(Player.Kennedy, State.WY, 1);
            var oneSupportInIdaho = new SupportChange<Player, State>(Player.Nixon, State.ID, 1);
            var oneSupportInNorthDakota = new SupportChange<Player, State>(Player.Nixon, State.ND, 1);
            var oneSupportInIowa = new SupportChange<Player, State>(Player.Nixon, State.IA, 1);
            var oneSupportInKentucky = new SupportChange<Player, State>(Player.Nixon, State.KY, 1);
            var oneSupportInOklahoma = new SupportChange<Player, State>(Player.Nixon, State.OK, 1);
            var oneSupportInNebraska = new SupportChange<Player, State>(Player.Nixon, State.NE, 1);

            playerChoices.StateChanges.Add(oneSupportInWyoming);
            playerChoices.StateChanges.Add(oneSupportInIdaho);
            playerChoices.StateChanges.Add(oneSupportInNorthDakota);
            playerChoices.StateChanges.Add(oneSupportInIowa);
            playerChoices.StateChanges.Add(oneSupportInKentucky);
            playerChoices.StateChanges.Add(oneSupportInOklahoma);
            playerChoices.StateChanges.Add(oneSupportInNebraska);

            var sut = NineteenSixty.GMTCards[cardIndex];
            var result = sut.AreChangesValid(playerChoices);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void HeartlandOfAmerica_71_FailsValidationIfIssueGains()
        {
            int cardIndex = 71;
            var engine = GetGameEngine();

            PlayerChosenChanges<Player, Issue, State> playerChoices = new();

            var oneSupportInWyoming = new SupportChange<Player, State>(Player.Nixon, State.WY, 1);
            var oneSupportInIdaho = new SupportChange<Player, State>(Player.Nixon, State.ID, 1);
            var oneSupportInNorthDakota = new SupportChange<Player, State>(Player.Nixon, State.ND, 1);
            var oneSupportInIowa = new SupportChange<Player, State>(Player.Nixon, State.IA, 1);
            var oneSupportInKentucky = new SupportChange<Player, State>(Player.Nixon, State.KY, 1);
            var oneSupportInOklahoma = new SupportChange<Player, State>(Player.Nixon, State.OK, 1);
            var oneSupportInNebraska = new SupportChange<Player, State>(Player.Nixon, State.NE, 1);;
            var issueSupport = new SupportChange<Player, Issue>(Player.Kennedy, Issue.Defense, 1);

            playerChoices.StateChanges.Add(oneSupportInWyoming);
            playerChoices.StateChanges.Add(oneSupportInIdaho);
            playerChoices.StateChanges.Add(oneSupportInNorthDakota);
            playerChoices.StateChanges.Add(oneSupportInIowa);
            playerChoices.StateChanges.Add(oneSupportInKentucky);
            playerChoices.StateChanges.Add(oneSupportInOklahoma);
            playerChoices.StateChanges.Add(oneSupportInNebraska);
            playerChoices.IssueChanges.Add(issueSupport);

            var sut = NineteenSixty.GMTCards[cardIndex];
            var result = sut.AreChangesValid(playerChoices);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void HeartlandOfAmerica_71_FailsValidationIfGreaterThanOne()
        {
            int cardIndex = 71;
            var engine = GetGameEngine();

            PlayerChosenChanges<Player, Issue, State> playerChoices = new();
            var oneSupportInWyoming = new SupportChange<Player, State>(Player.Nixon, State.WY, 1);
            var oneSupportInIdaho = new SupportChange<Player, State>(Player.Nixon, State.ID, 1);
            var oneSupportInNorthDakota = new SupportChange<Player, State>(Player.Nixon, State.ND, 1);
            var oneSupportInIowa = new SupportChange<Player, State>(Player.Nixon, State.IA, 1);
            var oneSupportInKentucky = new SupportChange<Player, State>(Player.Nixon, State.KY, 1);
            var twoSupportInOklahoma = new SupportChange<Player, State>(Player.Nixon, State.OK, 2);
            var oneSupportInNebraska = new SupportChange<Player, State>(Player.Nixon, State.NE, 1);

            playerChoices.StateChanges.Add(oneSupportInWyoming);
            playerChoices.StateChanges.Add(oneSupportInIdaho);
            playerChoices.StateChanges.Add(oneSupportInNorthDakota);
            playerChoices.StateChanges.Add(oneSupportInIowa);
            playerChoices.StateChanges.Add(oneSupportInKentucky);
            playerChoices.StateChanges.Add(twoSupportInOklahoma);

            var sut = NineteenSixty.GMTCards[cardIndex];
            var result = sut.AreChangesValid(playerChoices);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void HeartlandOfAmerica_71_FailsValidationIfStateNotInCorrectRegion()
        {
            int cardIndex = 71;
            var engine = GetGameEngine();

            PlayerChosenChanges<Player, Issue, State> playerChoices = new();
            var oneSupportInWyoming = new SupportChange<Player, State>(Player.Nixon, State.WY, 1);
            var oneSupportInIdaho = new SupportChange<Player, State>(Player.Nixon, State.ID, 1);
            var oneSupportInNorthDakota = new SupportChange<Player, State>(Player.Nixon, State.ND, 1);
            var oneSupportInIowa = new SupportChange<Player, State>(Player.Nixon, State.IA, 1);
            var oneSupportInKentucky = new SupportChange<Player, State>(Player.Nixon, State.KY, 1);
            var oneSupportInOklahoma = new SupportChange<Player, State>(Player.Nixon, State.OK, 1);
            var oneSupportInFlorida = new SupportChange<Player, State>(Player.Nixon, State.FL, 1);

            playerChoices.StateChanges.Add(oneSupportInWyoming);
            playerChoices.StateChanges.Add(oneSupportInIdaho);
            playerChoices.StateChanges.Add(oneSupportInNorthDakota);
            playerChoices.StateChanges.Add(oneSupportInIowa);
            playerChoices.StateChanges.Add(oneSupportInKentucky);
            playerChoices.StateChanges.Add(oneSupportInOklahoma);
            playerChoices.StateChanges.Add(oneSupportInFlorida);

            var sut = NineteenSixty.GMTCards[cardIndex];
            var result = sut.AreChangesValid(playerChoices);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void HeartlandOfAmerica_71_FailsValidationIfStateHasTooManyVotes()
        {
            int cardIndex = 71;
            var engine = GetGameEngine();

            PlayerChosenChanges<Player, Issue, State> playerChoices = new();
            var oneSupportInWyoming = new SupportChange<Player, State>(Player.Nixon, State.WY, 1);
            var oneSupportInIdaho = new SupportChange<Player, State>(Player.Nixon, State.ID, 1);
            var oneSupportInNorthDakota = new SupportChange<Player, State>(Player.Nixon, State.ND, 1);
            var oneSupportInIowa = new SupportChange<Player, State>(Player.Nixon, State.IA, 1);
            var oneSupportInKentucky = new SupportChange<Player, State>(Player.Nixon, State.KY, 1);
            var oneSupportInOklahoma = new SupportChange<Player, State>(Player.Nixon, State.OK, 1);
            var oneSupportInCalifornia = new SupportChange<Player, State>(Player.Nixon, State.CA, 1);

            playerChoices.StateChanges.Add(oneSupportInWyoming);
            playerChoices.StateChanges.Add(oneSupportInIdaho);
            playerChoices.StateChanges.Add(oneSupportInNorthDakota);
            playerChoices.StateChanges.Add(oneSupportInIowa);
            playerChoices.StateChanges.Add(oneSupportInKentucky);
            playerChoices.StateChanges.Add(oneSupportInOklahoma);
            playerChoices.StateChanges.Add(oneSupportInCalifornia);

            var sut = NineteenSixty.GMTCards[cardIndex];
            var result = sut.AreChangesValid(playerChoices);
            Assert.IsFalse(result);
        }

        #endregion

        #region 72 - Southern Revolt

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void SouthernRevolt_72_SupportAddedToStates(Player player)
        {
            int cardIndex = 72;
            var engine = GetGameEngine();
            engine.GainSupport(Player.Kennedy, Issue.CivilRights, 2);

            PlayerChosenChanges<Player, Issue, State> playerChoices = new();
            var oneSupportInFlorida = new SupportChange<Player, State>(Player.Nixon, State.FL, 1);
            var oneSupportInVirginia = new SupportChange<Player, State>(Player.Nixon, State.VA, 1);
            var twoSupportInLouisiana = new SupportChange<Player, State>(Player.Nixon, State.LA, 2);
            var oneSupportInTexas = new SupportChange<Player, State>(Player.Nixon, State.TX, 1);

            playerChoices.StateChanges.Add(oneSupportInFlorida);
            playerChoices.StateChanges.Add(oneSupportInVirginia);
            playerChoices.StateChanges.Add(twoSupportInLouisiana);
            playerChoices.StateChanges.Add(oneSupportInTexas);

            var sut = NineteenSixty.GMTCards[cardIndex];

            sut.Event(engine, player, playerChoices);

            Assert.AreEqual(1, engine.GetSupportAmount(State.FL));
            Assert.AreEqual(1, engine.GetSupportAmount(State.VA));
            Assert.AreEqual(2, engine.GetSupportAmount(State.LA));
            Assert.AreEqual(1, engine.GetSupportAmount(State.TX));
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void SouthernRevolt_72_NoChangeIfNixonLeadsInCivilRights(Player player)
        {
            int cardIndex = 72;
            var engine = GetGameEngine();
            engine.GainSupport(Player.Nixon, Issue.CivilRights, 2);

            PlayerChosenChanges<Player, Issue, State> playerChoices = new();
            var oneSupportInFlorida = new SupportChange<Player, State>(Player.Nixon, State.FL, 1);
            var oneSupportInVirginia = new SupportChange<Player, State>(Player.Nixon, State.VA, 1);
            var twoSupportInLouisiana = new SupportChange<Player, State>(Player.Nixon, State.LA, 2);
            var oneSupportInTexas = new SupportChange<Player, State>(Player.Nixon, State.TX, 1);

            playerChoices.StateChanges.Add(oneSupportInFlorida);
            playerChoices.StateChanges.Add(oneSupportInVirginia);
            playerChoices.StateChanges.Add(twoSupportInLouisiana);
            playerChoices.StateChanges.Add(oneSupportInTexas);

            var sut = NineteenSixty.GMTCards[cardIndex];

            sut.Event(engine, player, playerChoices);

            Assert.AreEqual(0, engine.GetSupportAmount(State.FL));
            Assert.AreEqual(0, engine.GetSupportAmount(State.VA));
            Assert.AreEqual(0, engine.GetSupportAmount(State.LA));
            Assert.AreEqual(0, engine.GetSupportAmount(State.TX));
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void SouthernRevolt_72_NoChangeIfNoLeaderInCivilRights(Player player)
        {
            int cardIndex = 72;
            var engine = GetGameEngine();

            PlayerChosenChanges<Player, Issue, State> playerChoices = new();
            var oneSupportInFlorida = new SupportChange<Player, State>(Player.Nixon, State.FL, 1);
            var oneSupportInVirginia = new SupportChange<Player, State>(Player.Nixon, State.VA, 1);
            var twoSupportInLouisiana = new SupportChange<Player, State>(Player.Nixon, State.LA, 2);
            var oneSupportInTexas = new SupportChange<Player, State>(Player.Nixon, State.TX, 1);

            playerChoices.StateChanges.Add(oneSupportInFlorida);
            playerChoices.StateChanges.Add(oneSupportInVirginia);
            playerChoices.StateChanges.Add(twoSupportInLouisiana);
            playerChoices.StateChanges.Add(oneSupportInTexas);

            var sut = NineteenSixty.GMTCards[cardIndex];

            sut.Event(engine, player, playerChoices);

            Assert.AreEqual(0, engine.GetSupportAmount(State.FL));
            Assert.AreEqual(0, engine.GetSupportAmount(State.VA));
            Assert.AreEqual(0, engine.GetSupportAmount(State.LA));
            Assert.AreEqual(0, engine.GetSupportAmount(State.TX));
        }

        [TestMethod]
        public void SouthernRevolt_72_PassedValidationEvenIfNixonLeadsCivilRights()
        {
            int cardIndex = 72;
            var engine = GetGameEngine();
            engine.GainSupport(Player.Nixon, Issue.CivilRights, 2);

            PlayerChosenChanges<Player, Issue, State> playerChoices = new();
            var oneSupportInFlorida = new SupportChange<Player, State>(Player.Nixon, State.FL, 1);
            var oneSupportInVirginia = new SupportChange<Player, State>(Player.Nixon, State.VA, 1);
            var twoSupportInLouisiana = new SupportChange<Player, State>(Player.Nixon, State.LA, 2);
            var oneSupportInTexas = new SupportChange<Player, State>(Player.Nixon, State.TX, 1);

            playerChoices.StateChanges.Add(oneSupportInFlorida);
            playerChoices.StateChanges.Add(oneSupportInVirginia);
            playerChoices.StateChanges.Add(twoSupportInLouisiana);
            playerChoices.StateChanges.Add(oneSupportInTexas);

            var sut = NineteenSixty.GMTCards[cardIndex];
            var result = sut.AreChangesValid(playerChoices);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void SouthernRevolt_72_PassedValidationEvenIfNoLeaderInCivilRights()
        {
            int cardIndex = 72;
            var engine = GetGameEngine();
  
            PlayerChosenChanges<Player, Issue, State> playerChoices = new();
            var oneSupportInFlorida = new SupportChange<Player, State>(Player.Nixon, State.FL, 1);
            var oneSupportInVirginia = new SupportChange<Player, State>(Player.Nixon, State.VA, 1);
            var twoSupportInLouisiana = new SupportChange<Player, State>(Player.Nixon, State.LA, 2);
            var oneSupportInTexas = new SupportChange<Player, State>(Player.Nixon, State.TX, 1);

            playerChoices.StateChanges.Add(oneSupportInFlorida);
            playerChoices.StateChanges.Add(oneSupportInVirginia);
            playerChoices.StateChanges.Add(twoSupportInLouisiana);
            playerChoices.StateChanges.Add(oneSupportInTexas);

            var sut = NineteenSixty.GMTCards[cardIndex];
            var result = sut.AreChangesValid(playerChoices);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void SouthernRevolt_72_FailsValidationIfKennedyGains()
        {
            int cardIndex = 72;
            var engine = GetGameEngine();
            engine.GainSupport(Player.Kennedy, Issue.CivilRights, 2);

            PlayerChosenChanges<Player, Issue, State> playerChoices = new();
            var oneSupportInFlorida = new SupportChange<Player, State>(Player.Nixon, State.FL, 1);
            var oneSupportInVirginia = new SupportChange<Player, State>(Player.Kennedy, State.VA, 1);
            var twoSupportInLouisiana = new SupportChange<Player, State>(Player.Nixon, State.LA, 2);
            var oneSupportInTexas = new SupportChange<Player, State>(Player.Nixon, State.TX, 1);

            playerChoices.StateChanges.Add(oneSupportInFlorida);
            playerChoices.StateChanges.Add(oneSupportInVirginia);
            playerChoices.StateChanges.Add(twoSupportInLouisiana);
            playerChoices.StateChanges.Add(oneSupportInTexas);

            var sut = NineteenSixty.GMTCards[cardIndex];
            var result = sut.AreChangesValid(playerChoices);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void SouthernRevolt_72_FailsValidationIfIssueGains()
        {
            int cardIndex = 72;
            var engine = GetGameEngine();
            engine.GainSupport(Player.Kennedy, Issue.CivilRights, 2);

            PlayerChosenChanges<Player, Issue, State> playerChoices = new();
            var oneSupportInFlorida = new SupportChange<Player, State>(Player.Nixon, State.FL, 1);
            var oneSupportInVirginia = new SupportChange<Player, State>(Player.Nixon, State.VA, 1);
            var twoSupportInLouisiana = new SupportChange<Player, State>(Player.Nixon, State.LA, 2);
            var oneSupportInTexas = new SupportChange<Player, State>(Player.Nixon, State.TX, 1);
            var issueSupport = new SupportChange<Player, Issue>(Player.Nixon, Issue.Defense, 1);

            playerChoices.StateChanges.Add(oneSupportInFlorida);
            playerChoices.StateChanges.Add(oneSupportInVirginia);
            playerChoices.StateChanges.Add(twoSupportInLouisiana);
            playerChoices.StateChanges.Add(oneSupportInTexas);
            playerChoices.IssueChanges.Add(issueSupport);

            var sut = NineteenSixty.GMTCards[cardIndex];
            var result = sut.AreChangesValid(playerChoices);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void SouthernRevolt_72_FailsValidationIfGreaterThanTwo()
        {
            int cardIndex = 72;
            var engine = GetGameEngine();
            engine.GainSupport(Player.Kennedy, Issue.CivilRights, 2);

            PlayerChosenChanges<Player, Issue, State> playerChoices = new();
            var oneSupportInFlorida = new SupportChange<Player, State>(Player.Nixon, State.FL, 1);
            var oneSupportInVirginia = new SupportChange<Player, State>(Player.Nixon, State.VA, 1);
            var threeSupportInLouisiana = new SupportChange<Player, State>(Player.Nixon, State.LA, 3);

            playerChoices.StateChanges.Add(oneSupportInFlorida);
            playerChoices.StateChanges.Add(oneSupportInVirginia);
            playerChoices.StateChanges.Add(threeSupportInLouisiana);

            var sut = NineteenSixty.GMTCards[cardIndex];
            var result = sut.AreChangesValid(playerChoices);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void SouthernRevolt_72_FailsValidationIfExcludedState()
        {
            int cardIndex = 72;
            var engine = GetGameEngine();
            engine.GainSupport(Player.Kennedy, Issue.CivilRights, 2);

            PlayerChosenChanges<Player, Issue, State> playerChoices = new();
            var oneSupportInFlorida = new SupportChange<Player, State>(Player.Nixon, State.FL, 1);
            var oneSupportInVirginia = new SupportChange<Player, State>(Player.Nixon, State.VA, 1);
            var twoSupportInLouisiana = new SupportChange<Player, State>(Player.Nixon, State.LA, 2);
            var oneSupportInRhodeIsland = new SupportChange<Player, State>(Player.Kennedy, State.RI, 1);

            playerChoices.StateChanges.Add(oneSupportInFlorida);
            playerChoices.StateChanges.Add(oneSupportInVirginia);
            playerChoices.StateChanges.Add(twoSupportInLouisiana);
            playerChoices.StateChanges.Add(oneSupportInRhodeIsland);

            var sut = NineteenSixty.GMTCards[cardIndex];
            var result = sut.AreChangesValid(playerChoices);
            Assert.IsFalse(result);
        }

        #endregion

        #region #77 - Suburban Voters
        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void SuburbanVoters_77_SupportAddedToStates(Player player)
        {
            int cardIndex = 77;
            var engine = GetGameEngine();

            PlayerChosenChanges<Player, Issue, State> playerChoices = new();
            var oneSupportInMichigan = new SupportChange<Player, State>(Player.Kennedy, State.MI, 1);
            var oneSupportInPenn = new SupportChange<Player, State>(Player.Kennedy, State.PA, 1);
            var twoSupportInCali = new SupportChange<Player, State>(Player.Kennedy, State.CA, 2);
            var oneSupportInNewYork = new SupportChange<Player, State>(Player.Kennedy, State.NY, 1);

            playerChoices.StateChanges.Add(oneSupportInMichigan);
            playerChoices.StateChanges.Add(oneSupportInPenn);
            playerChoices.StateChanges.Add(twoSupportInCali);
            playerChoices.StateChanges.Add(oneSupportInNewYork);

            var sut = NineteenSixty.GMTCards[cardIndex];

            sut.Event(engine, player, playerChoices);

            Assert.AreEqual(1, engine.GetSupportAmount(State.MI));
            Assert.AreEqual(1, engine.GetSupportAmount(State.PA));
            Assert.AreEqual(2, engine.GetSupportAmount(State.CA));
            Assert.AreEqual(1, engine.GetSupportAmount(State.NY));
        }

        [TestMethod]
        public void SuburbanVoters_77_FailsValidationIfNixonGains()
        {
            int cardIndex = 77;
            var engine = GetGameEngine();

            PlayerChosenChanges<Player, Issue, State> playerChoices = new();
            var oneSupportInMichigan = new SupportChange<Player, State>(Player.Kennedy, State.MI, 1);
            var oneSupportInPenn = new SupportChange<Player, State>(Player.Nixon, State.PA, 1);
            var twoSupportInCali = new SupportChange<Player, State>(Player.Kennedy, State.CA, 2);
            var oneSupportInNewYork = new SupportChange<Player, State>(Player.Kennedy, State.NY, 1);

            playerChoices.StateChanges.Add(oneSupportInMichigan);
            playerChoices.StateChanges.Add(oneSupportInPenn);
            playerChoices.StateChanges.Add(twoSupportInCali);
            playerChoices.StateChanges.Add(oneSupportInNewYork);

            var sut = NineteenSixty.GMTCards[cardIndex];
            var result = sut.AreChangesValid(playerChoices);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void SuburbanVoters_77_FailsValidationIfIssueGains()
        {
            int cardIndex = 77;
            var engine = GetGameEngine();

            PlayerChosenChanges<Player, Issue, State> playerChoices = new();
            var oneSupportInMichigan = new SupportChange<Player, State>(Player.Kennedy, State.MI, 1);
            var oneSupportInPenn = new SupportChange<Player, State>(Player.Kennedy, State.PA, 1);
            var twoSupportInCali = new SupportChange<Player, State>(Player.Kennedy, State.CA, 2);
            var oneSupportInNewYork = new SupportChange<Player, State>(Player.Kennedy, State.NY, 1);
            var issueSupport = new SupportChange<Player, Issue>(Player.Kennedy, Issue.Defense, 1);

            playerChoices.StateChanges.Add(oneSupportInMichigan);
            playerChoices.StateChanges.Add(oneSupportInPenn);
            playerChoices.StateChanges.Add(twoSupportInCali);
            playerChoices.StateChanges.Add(oneSupportInNewYork);
            playerChoices.IssueChanges.Add(issueSupport);

            var sut = NineteenSixty.GMTCards[cardIndex];
            var result = sut.AreChangesValid(playerChoices);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void SuburbanVoters_77_FailsValidationIfGreaterThanTwo()
        {
            int cardIndex = 77;
            var engine = GetGameEngine();

            PlayerChosenChanges<Player, Issue, State> playerChoices = new();
            var oneSupportInMichigan = new SupportChange<Player, State>(Player.Kennedy, State.MI, 1);
            var oneSupportInPenn = new SupportChange<Player, State>(Player.Nixon, State.PA, 1);
            var threeSupportInCali = new SupportChange<Player, State>(Player.Kennedy, State.CA, 2);

            playerChoices.StateChanges.Add(oneSupportInMichigan);
            playerChoices.StateChanges.Add(oneSupportInPenn);
            playerChoices.StateChanges.Add(threeSupportInCali);

            var sut = NineteenSixty.GMTCards[cardIndex];
            var result = sut.AreChangesValid(playerChoices);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void SuburbanVoters_77_FailsValidationIfExcludedState()
        {
            int cardIndex = 77;
            var engine = GetGameEngine();

            PlayerChosenChanges<Player, Issue, State> playerChoices = new();
            var oneSupportInMichigan = new SupportChange<Player, State>(Player.Kennedy, State.MI, 1);
            var oneSupportInPenn = new SupportChange<Player, State>(Player.Kennedy, State.PA, 1);
            var twoSupportInCali = new SupportChange<Player, State>(Player.Kennedy, State.CA, 2);
            var oneSupportInKansas = new SupportChange<Player, State>(Player.Kennedy, State.KS, 1);

            playerChoices.StateChanges.Add(oneSupportInMichigan);
            playerChoices.StateChanges.Add(oneSupportInPenn);
            playerChoices.StateChanges.Add(twoSupportInCali);
            playerChoices.StateChanges.Add(oneSupportInKansas);

            var sut = NineteenSixty.GMTCards[cardIndex];
            var result = sut.AreChangesValid(playerChoices);
            Assert.IsFalse(result);
        }

        #endregion

        #region #78 - Stock Market In Decline

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void StockMarketInDecline_78_EconomyGoesUpTwoSpace(Player player)
        {
            int cardIndex = 78;
            var engine = GetGameEngine();

            engine.SetIssueOrder([Issue.CivilRights, Issue.Defense, Issue.Economy]);

            var sut = NineteenSixty.GMTCards[cardIndex];
            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(Issue.Economy, engine.GetIssueOrder[0]);
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void StockMarketInDecline_78_EconomyAtTopRemainsAtTop(Player player)
        {
            int cardIndex = 78;
            var engine = GetGameEngine();

            engine.SetIssueOrder([Issue.Economy, Issue.Defense, Issue.CivilRights]);

            var sut = NineteenSixty.GMTCards[cardIndex];
            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(Issue.Economy, engine.GetIssueOrder[0]);
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void StockMarketInDecline_78_StateSupportGained(Player player)
        {
            int cardIndex = 78;
            var engine = GetGameEngine();

            engine.SetIssueOrder([Issue.Economy, Issue.Defense, Issue.CivilRights]);
            engine.GainSupport(player, Issue.Economy, 1);

            var sut = NineteenSixty.GMTCards[cardIndex];
            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(2, engine.GetSupportAmount(State.NY));
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void StockMarketInDecline_78_NoStateSupportGainedIfNoLeader(Player player)
        {
            int cardIndex = 78;
            var engine = GetGameEngine();

            engine.SetIssueOrder([Issue.Economy, Issue.Defense, Issue.CivilRights]);

            var sut = NineteenSixty.GMTCards[cardIndex];
            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(0, engine.GetSupportAmount(State.NY));
        }

        [TestMethod]
        public void StockMarketInDecline_78_ValidationAlwaysTrue()
        {
            int cardIndex = 78;
            var sut = NineteenSixty.GMTCards[cardIndex];

            var result = sut.AreChangesValid(InvalidChanges);

            Assert.IsTrue(result);
        }
        #endregion

        #region #82 - Fidel Castro

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void FidelCastro_82_LeaderGainsStateSupportAndMomentum(Player player)
        {
            int cardIndex = 82;
            var engine = GetGameEngine();

            engine.GainSupport(player, Issue.Defense, 1);

            var sut = NineteenSixty.GMTCards[cardIndex];
            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(1, engine.GetSupportAmount(State.FL));
            Assert.AreEqual(1, engine.GetPlayerMomentum(player));
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void FidelCastro_82_TieAwardsNothing(Player player)
        {
            int cardIndex = 82;
            var engine = GetGameEngine();

            var sut = NineteenSixty.GMTCards[cardIndex];
            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(0, engine.GetSupportAmount(State.FL));
            Assert.AreEqual(0, engine.GetPlayerMomentum(player));
        }

        [TestMethod]
        public void FidelCastro_82_ValidationAlwaysTrue()
        {
            int cardIndex = 82;
            var sut = NineteenSixty.GMTCards[cardIndex];

            var result = sut.AreChangesValid(InvalidChanges);

            Assert.IsTrue(result);
        }
        #endregion

        #region #86 - Herb Klein

        [TestMethod]
        [DataRow(Issue.CivilRights)]
        [DataRow(Issue.Defense)]
        [DataRow(Issue.Economy)]
        public void HerbKlein_86_SupportChangesReflectedInSingleIssue(Issue issue)
        {
            int cardIndex = 86;
            var engine = GetGameEngine();

            engine.GainSupport(Player.Nixon, issue, 1);

            PlayerChosenChanges<Player, Issue, State> playerChoices = new();
            var threeSupportInOneIssue = new SupportChange<Player, Issue>(Player.Nixon, issue, 3);
            playerChoices.IssueChanges.Add(threeSupportInOneIssue);

            var sut = NineteenSixty.GMTCards[cardIndex];

            sut.Event(engine, Player.Kennedy, playerChoices);

            Assert.AreEqual(4, engine.GetSupportAmount(issue));
        }

        [TestMethod]
        public void HerbKlein_86_SupportChangesReflectedAcrossMultipleIssues()
        {
            int cardIndex = 86;
            var engine = GetGameEngine();

            PlayerChosenChanges<Player, Issue, State> playerChoices = new();
            var oneSupportInCivilRights = new SupportChange<Player, Issue>(Player.Nixon, Issue.CivilRights, 1);
            var oneSupportInDefense = new SupportChange<Player, Issue>(Player.Nixon, Issue.Defense, 1);
            var oneSupportInEconomy = new SupportChange<Player, Issue>(Player.Nixon, Issue.Economy, 1);
            playerChoices.IssueChanges.Add(oneSupportInCivilRights);
            playerChoices.IssueChanges.Add(oneSupportInDefense);
            playerChoices.IssueChanges.Add(oneSupportInEconomy);

            var sut = NineteenSixty.GMTCards[cardIndex];

            sut.Event(engine, Player.Kennedy, playerChoices);

            Assert.AreEqual(1, engine.GetSupportAmount(Issue.CivilRights));
            Assert.AreEqual(1, engine.GetSupportAmount(Issue.Defense));
            Assert.AreEqual(1, engine.GetSupportAmount(Issue.Economy));
        }

        [TestMethod]
        public void HerbKlein_86_FailsValidationIfKennedyGainsSupport()
        {
            int cardIndex = 86;
            var engine = GetGameEngine();

            PlayerChosenChanges<Player, Issue, State> playerChoices = new();
            var oneSupportInCivilRights = new SupportChange<Player, Issue>(Player.Nixon, Issue.CivilRights, 1);
            var oneSupportInDefense = new SupportChange<Player, Issue>(Player.Kennedy, Issue.Defense, 1);
            var oneSupportInEconomy = new SupportChange<Player, Issue>(Player.Nixon, Issue.Economy, 1);
            playerChoices.IssueChanges.Add(oneSupportInCivilRights);
            playerChoices.IssueChanges.Add(oneSupportInDefense);
            playerChoices.IssueChanges.Add(oneSupportInEconomy);

            var sut = NineteenSixty.GMTCards[cardIndex];

            var result = sut.AreChangesValid(playerChoices);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void HerbKlein_86_FailsValidationIfStateSupportGained()
        {
            int cardIndex = 86;
            var engine = GetGameEngine();

            PlayerChosenChanges<Player, Issue, State> playerChoices = new();
            var oneSupportInCivilRights = new SupportChange<Player, Issue>(Player.Nixon, Issue.CivilRights, 1);
            var oneSupportInDefense = new SupportChange<Player, Issue>(Player.Nixon, Issue.Defense, 1);
            var oneSupportInEconomy = new SupportChange<Player, Issue>(Player.Nixon, Issue.Economy, 1);
            var oneSupportInNewYork = new SupportChange<Player, State>(Player.Nixon, State.NY, 1);
            playerChoices.IssueChanges.Add(oneSupportInCivilRights);
            playerChoices.IssueChanges.Add(oneSupportInDefense);
            playerChoices.IssueChanges.Add(oneSupportInEconomy);
            playerChoices.StateChanges.Add(oneSupportInNewYork);

            var sut = NineteenSixty.GMTCards[cardIndex];
            var result = sut.AreChangesValid(playerChoices);

            Assert.IsFalse(result);
        }

        #endregion

        #region #89 - The New Nixon
        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void TheNewNixon_89_NixonMomentumIsIncreasedByOne(Player player)
        {
            int cardIndex = 89;
            var engine = GetGameEngine();

            var nixonStartingMomentum = engine.GetPlayerMomentum(Player.Nixon);
            var kennedyStartingMomentum = engine.GetPlayerMomentum(Player.Kennedy);

            var sut = NineteenSixty.GMTCards[cardIndex];

            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(engine.GetPlayerMomentum(Player.Nixon), nixonStartingMomentum + 1);
            Assert.AreEqual(engine.GetPlayerMomentum(Player.Kennedy), kennedyStartingMomentum);
        }

        [TestMethod]
        public void TheNewNixon_89_ValidationAlwaysTrue()
        {
            int cardIndex = 89;
            var sut = NineteenSixty.GMTCards[cardIndex];

            var result = sut.AreChangesValid(InvalidChanges);

            Assert.IsTrue(result);
        }
        #endregion

        #region #90 - Recount
        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void Recount_90_SupportCheckWorksAsExpected(Player player)
        {
            int cardIndex = 90;
            var engine = GetGameEngine();

            engine.GainSupport(Player.Kennedy, State.TX, 1);

            PlayerChosenChanges<Player, Issue, State> playerChoices = new();
            var threeSupportInTexas = new SupportChange<Player, State>(Player.Nixon, State.TX, 3);
            playerChoices.StateChanges.Add(threeSupportInTexas);

            var sut = NineteenSixty.GMTCards[cardIndex];

            sut.Event(engine, player, playerChoices);

            Assert.AreEqual(Leader.None, engine.GetLeader(State.TX));
            Assert.AreEqual(0, engine.GetSupportAmount(State.TX));
        }

        [TestMethod]
        public void Recount_90_FailsValidationIfNixonGains()
        {
            int cardIndex = 90;

            var engine = GetGameEngine();

            PlayerChosenChanges<Player, Issue, State> playerChoices = new();
            var threeSupportInKentucky = new SupportChange<Player, State>(Player.Kennedy, State.KY, 3);

            playerChoices.StateChanges.Add(threeSupportInKentucky);

            var sut = NineteenSixty.GMTCards[cardIndex];
            var result = sut.AreChangesValid(playerChoices);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Recount_90_FailsValidationIfIssueGains()
        {
            int cardIndex = 90;
            var engine = GetGameEngine();

            PlayerChosenChanges<Player, Issue, State> playerChoices = new();
            var threeSupportInKentucky = new SupportChange<Player, State>(Player.Kennedy, State.KY, 3);
            var issueSupport = new SupportChange<Player, Issue>(Player.Kennedy, Issue.Defense, 1);

            playerChoices.StateChanges.Add(threeSupportInKentucky);
            playerChoices.IssueChanges.Add(issueSupport);

            var sut = NineteenSixty.GMTCards[cardIndex];
            var result = sut.AreChangesValid(playerChoices);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Recount_90_FailsValidationIfGreaterThanThree()
        {
            int cardIndex = 90;
            var engine = GetGameEngine();

            PlayerChosenChanges<Player, Issue, State> playerChoices = new();
            var threeSupportInKentucky = new SupportChange<Player, State>(Player.Kennedy, State.KY, 4);

            playerChoices.StateChanges.Add(threeSupportInKentucky);

            var sut = NineteenSixty.GMTCards[cardIndex];
            var result = sut.AreChangesValid(playerChoices);
            Assert.IsFalse(result);
        }

        #endregion

        #region #93 - Experience Counts
        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void ExperienceCounts_93_NixonMomentumGainedAndKennedyIssueSupportLost(Player player)
        {
            int cardIndex = 93;
            var engine = GetGameEngine();

            engine.GainMomentum(Player.Nixon, 2);

            engine.GainSupport(Player.Kennedy, Issue.CivilRights, 3);
            engine.GainSupport(Player.Kennedy, Issue.Defense, 2);
            engine.GainSupport(Player.Kennedy, Issue.Economy, 1);

            var sut = NineteenSixty.GMTCards[cardIndex];
            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(engine.GetPlayerMomentum(Player.Nixon), 3);
            Assert.AreEqual(2, engine.GetSupportAmount(Issue.CivilRights));
            Assert.AreEqual(1, engine.GetSupportAmount(Issue.Defense));
            Assert.AreEqual(0, engine.GetSupportAmount(Issue.Economy));
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void ExperienceCounts_93_NixonAndNeutralSupportNotLost(Player player)
        {
            int cardIndex = 93;
            var engine = GetGameEngine();

            engine.GainSupport(Player.Kennedy, Issue.CivilRights, 3);
            engine.GainSupport(Player.Nixon, Issue.Defense, 2);

            var sut = NineteenSixty.GMTCards[cardIndex];
            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(2, engine.GetSupportAmount(Issue.CivilRights));
            Assert.AreEqual(2, engine.GetSupportAmount(Issue.Defense));
            Assert.AreEqual(0, engine.GetSupportAmount(Issue.Economy));
        }

        [TestMethod]
        public void ExperienceCounts_93_ValidationAlwaysTrue()
        {
            int cardIndex = 93;
            var sut = NineteenSixty.GMTCards[cardIndex];

            var result = sut.AreChangesValid(InvalidChanges);

            Assert.IsTrue(result);
        }
        #endregion

        #region #95 - A Time For Greatness
        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void ATimeForGreatness_95_NixonLosesOnIssues(Player player)
        {
            int cardIndex = 95;
            var engine = GetGameEngine();

            engine.GainSupport(Player.Nixon, Issue.CivilRights, 2);
            engine.GainSupport(Player.Nixon, Issue.Defense, 3);
            engine.GainSupport(Player.Nixon, Issue.Economy, 4);

            PlayerChosenChanges<Player, Issue, State> playerChoices = new();
            var oneSupportInNewYork = new SupportChange<Player, State>(Player.Kennedy, State.NY, 1);
            var oneSupportInColorado = new SupportChange<Player, State>(Player.Kennedy, State.CO, 1);
            var oneSupportInWestVirginia = new SupportChange<Player, State>(Player.Kennedy, State.WV, 1);

            playerChoices.StateChanges.Add(oneSupportInNewYork);
            playerChoices.StateChanges.Add(oneSupportInColorado);
            playerChoices.StateChanges.Add(oneSupportInWestVirginia);

            var sut = NineteenSixty.GMTCards[cardIndex];

            sut.Event(engine, player, playerChoices);
            Assert.AreEqual(Leader.Nixon, engine.GetLeader(Issue.CivilRights));
            Assert.AreEqual(Leader.Nixon, engine.GetLeader(Issue.CivilRights));
            Assert.AreEqual(Leader.Nixon, engine.GetLeader(Issue.CivilRights));

            Assert.AreEqual(1, engine.GetSupportAmount(Issue.CivilRights));
            Assert.AreEqual(2, engine.GetSupportAmount(Issue.Defense));
            Assert.AreEqual(3, engine.GetSupportAmount(Issue.Economy));
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void ATimeForGreatness_95_SupportAddedToStates(Player player)
        {
            int cardIndex = 95;
            var engine = GetGameEngine();

            engine.GainSupport(Player.Nixon, Issue.CivilRights, 2);
            engine.GainSupport(Player.Nixon, Issue.Defense, 3);
            engine.GainSupport(Player.Nixon, Issue.Economy, 4);

            PlayerChosenChanges<Player, Issue, State> playerChoices = new();
            var oneSupportInNewYork = new SupportChange<Player, State>(Player.Kennedy, State.NY, 1);
            var oneSupportInColorado = new SupportChange<Player, State>(Player.Kennedy, State.CO, 1);
            var oneSupportInWestVirginia = new SupportChange<Player, State>(Player.Kennedy, State.WV, 1);

            playerChoices.StateChanges.Add(oneSupportInNewYork);
            playerChoices.StateChanges.Add(oneSupportInColorado);
            playerChoices.StateChanges.Add(oneSupportInWestVirginia);

            var sut = NineteenSixty.GMTCards[cardIndex];

            sut.Event(engine, player, playerChoices);

            Assert.AreEqual(Leader.Kennedy, engine.GetLeader(State.NY));
            Assert.AreEqual(Leader.Kennedy, engine.GetLeader(State.CO));
            Assert.AreEqual(Leader.Kennedy, engine.GetLeader(State.WV));

            Assert.AreEqual(1, engine.GetSupportAmount(State.NY));
            Assert.AreEqual(1, engine.GetSupportAmount(State.CO));
            Assert.AreEqual(1, engine.GetSupportAmount(State.WV));
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void ATimeForGreatness_95_FailsValidationIfNixonGains(Player player)
        {
            int cardIndex = 95;
            var engine = GetGameEngine();

            PlayerChosenChanges<Player, Issue, State> playerChoices = new();
            var oneSupportInNewYork = new SupportChange<Player, State>(Player.Nixon, State.NY, 1);
            var oneSupportInColorado = new SupportChange<Player, State>(Player.Kennedy, State.CO, 1);
            var oneSupportInWestVirginia = new SupportChange<Player, State>(Player.Kennedy, State.WV, 1);

            playerChoices.StateChanges.Add(oneSupportInNewYork);
            playerChoices.StateChanges.Add(oneSupportInColorado);
            playerChoices.StateChanges.Add(oneSupportInWestVirginia);

            var sut = NineteenSixty.GMTCards[cardIndex];
            var result = sut.AreChangesValid(playerChoices);
            Assert.IsFalse(result);
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void ATimeForGreatness_95_FailsValidationIfIssueGains(Player player)
        {
            int cardIndex = 95;
            var engine = GetGameEngine();

            engine.GainSupport(player, Issue.CivilRights, 1);

            PlayerChosenChanges<Player, Issue, State> playerChoices = new();
            var oneSupportInNewYork = new SupportChange<Player, State>(Player.Kennedy, State.NY, 1);
            var oneSupportInColorado = new SupportChange<Player, State>(Player.Kennedy, State.CO, 1);
            var oneSupportInWestVirginia = new SupportChange<Player, State>(Player.Kennedy, State.WV, 1);

            playerChoices.StateChanges.Add(oneSupportInNewYork);
            playerChoices.StateChanges.Add(oneSupportInColorado);
            playerChoices.StateChanges.Add(oneSupportInWestVirginia);

            var issueSupport = new SupportChange<Player, Issue>(player, Issue.Defense, 1);
            playerChoices.IssueChanges.Add(issueSupport);

            var sut = NineteenSixty.GMTCards[cardIndex];
            var result = sut.AreChangesValid(playerChoices);
            Assert.IsFalse(result);
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void ATimeForGreatness_95_FailsValidationIfGreaterThanOne(Player player)
        {
            int cardIndex = 95;
            var engine = GetGameEngine();

            PlayerChosenChanges<Player, Issue, State> playerChoices = new();
            var oneSupportInNewYork = new SupportChange<Player, State>(Player.Kennedy, State.NY, 1);
            var twoSupportInColorado = new SupportChange<Player, State>(Player.Kennedy, State.CO, 2);

            playerChoices.StateChanges.Add(oneSupportInNewYork);
            playerChoices.StateChanges.Add(twoSupportInColorado);

            var sut = NineteenSixty.GMTCards[cardIndex];
            var result = sut.AreChangesValid(playerChoices);
            Assert.IsFalse(result);
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void ATimeForGreatness_95_FailsValidationIfSumGreaterThanThree(Player player)
        {
            int cardIndex = 95;
            var engine = GetGameEngine();

            PlayerChosenChanges<Player, Issue, State> playerChoices = new();
            var oneSupportInHawaii = new SupportChange<Player, State>(Player.Kennedy, State.HI, 1);
            var oneSupportInFlorida = new SupportChange<Player, State>(Player.Kennedy, State.FL, 1);
            var oneSupportInVermont = new SupportChange<Player, State>(Player.Kennedy, State.VT, 1);
            var oneSupportInMissouri = new SupportChange<Player, State>(Player.Kennedy, State.MO, 1);
            playerChoices.StateChanges.Add(oneSupportInHawaii);
            playerChoices.StateChanges.Add(oneSupportInFlorida);
            playerChoices.StateChanges.Add(oneSupportInVermont);
            playerChoices.StateChanges.Add(oneSupportInMissouri);

            var sut = NineteenSixty.GMTCards[cardIndex];
            var result = sut.AreChangesValid(playerChoices);
            Assert.IsFalse(result);
        }
        #endregion

        #region #96 - Medal Count
        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void MedalCount_96_SharedLeaderLosesMomentumAsWell(Player player)
        {
            int cardIndex = 96;
            var engine = GetGameEngine();

            engine.GainSupport(player, Issue.CivilRights, 3);
            engine.GainSupport(player, Issue.Defense, 2);
            engine.GainSupport(player, Issue.Economy, 1);
            engine.GainMomentum(player, 1);

            var sut = NineteenSixty.GMTCards[cardIndex];

            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(0, engine.GetPlayerMomentum(player));
            Assert.AreEqual(2, engine.GetSupportAmount(Issue.CivilRights));
            Assert.AreEqual(2, engine.GetSupportAmount(Issue.Defense));
            Assert.AreEqual(0, engine.GetSupportAmount(Issue.Economy));
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void MedalCount_96_SplitLeaderNoMomentumLoss(Player player)
        {
            int cardIndex = 96;
            var engine = GetGameEngine();

            engine.GainSupport(Player.Nixon, Issue.CivilRights, 3);
            engine.GainSupport(Player.Nixon, Issue.Defense, 2);
            engine.GainSupport(Player.Kennedy, Issue.Economy, 1);
            engine.GainMomentum(Player.Nixon, 1);
            engine.GainMomentum(Player.Kennedy, 1);

            var sut = NineteenSixty.GMTCards[cardIndex];

            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(2, engine.GetSupportAmount(Issue.CivilRights));
            Assert.AreEqual(2, engine.GetSupportAmount(Issue.Defense));
            Assert.AreEqual(0, engine.GetSupportAmount(Issue.Economy));
            Assert.AreEqual(1, engine.GetPlayerMomentum(Player.Nixon));
            Assert.AreEqual(1, engine.GetPlayerMomentum(Player.Kennedy));
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void MedalCount_96_NoLeaderInBothNoMomentumLoss(Player player)
        {
            int cardIndex = 96;
            var engine = GetGameEngine();

            engine.GainSupport(Player.Nixon, Issue.CivilRights, 3);
            engine.GainSupport(Player.Nixon, Issue.Defense, 2);
            engine.GainSupport(Player.Kennedy, Issue.Economy, 1);
            engine.GainMomentum(Player.Nixon, 1);
            engine.GainMomentum(Player.Kennedy, 1);

            var sut = NineteenSixty.GMTCards[cardIndex];

            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(2, engine.GetSupportAmount(Issue.CivilRights));
            Assert.AreEqual(2, engine.GetSupportAmount(Issue.Defense));
            Assert.AreEqual(0, engine.GetSupportAmount(Issue.Economy));
            Assert.AreEqual(1, engine.GetPlayerMomentum(Player.Nixon));
            Assert.AreEqual(1, engine.GetPlayerMomentum(Player.Kennedy));
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void MedalCount_96_NoLeaderInOneNoMomentumLoss(Player player)
        {
            int cardIndex = 96;
            var engine = GetGameEngine();

            engine.GainSupport(Player.Nixon, Issue.Defense, 2);
            engine.GainSupport(Player.Kennedy, Issue.Economy, 1);
            engine.GainMomentum(Player.Nixon, 1);
            engine.GainMomentum(Player.Kennedy, 1);

            var sut = NineteenSixty.GMTCards[cardIndex];

            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(0, engine.GetSupportAmount(Issue.CivilRights));
            Assert.AreEqual(2, engine.GetSupportAmount(Issue.Defense));
            Assert.AreEqual(0, engine.GetSupportAmount(Issue.Economy));
            Assert.AreEqual(1, engine.GetPlayerMomentum(Player.Nixon));
            Assert.AreEqual(1, engine.GetPlayerMomentum(Player.Kennedy));
        }

        [TestMethod]
        public void MedalCount_96_ValidationAlwaysTrue()
        {
            int cardIndex = 96;
            var sut = NineteenSixty.GMTCards[cardIndex];

            var result = sut.AreChangesValid(InvalidChanges);

            Assert.IsTrue(result);
        }

        #endregion

        #region #97 - Cassius Clay Wins Gold
        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void CassiusClayWinsGold_97_SharedLeaderLosesMomentumAsWell(Player player)
        {
            int cardIndex = 97;
            var engine = GetGameEngine();

            engine.GainSupport(player, Issue.CivilRights, 3);
            engine.GainSupport(player, Issue.Defense, 2);
            engine.GainSupport(player, Issue.Economy, 1);
            engine.GainMomentum(player, 1);

            var sut = NineteenSixty.GMTCards[cardIndex];

            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(0, engine.GetPlayerMomentum(player));
            Assert.AreEqual(3, engine.GetSupportAmount(Issue.CivilRights));
            Assert.AreEqual(1, engine.GetSupportAmount(Issue.Defense));
            Assert.AreEqual(0, engine.GetSupportAmount(Issue.Economy));
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void CassiusClayWinsGold_97_SplitLeaderNoMomentumLoss(Player player)
        {
            int cardIndex = 97;
            var engine = GetGameEngine();

            engine.GainSupport(Player.Nixon, Issue.CivilRights, 3);
            engine.GainSupport(Player.Nixon, Issue.Defense, 2);
            engine.GainSupport(Player.Kennedy, Issue.Economy, 1);
            engine.GainMomentum(Player.Nixon, 1);
            engine.GainMomentum(Player.Kennedy, 1);

            var sut = NineteenSixty.GMTCards[cardIndex];

            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(3, engine.GetSupportAmount(Issue.CivilRights));
            Assert.AreEqual(1, engine.GetSupportAmount(Issue.Defense));
            Assert.AreEqual(0, engine.GetSupportAmount(Issue.Economy));
            Assert.AreEqual(1, engine.GetPlayerMomentum(Player.Nixon));
            Assert.AreEqual(1, engine.GetPlayerMomentum(Player.Kennedy));
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void CassiusClayWinsGold_97_NoLeaderInOneNoMomentumLoss(Player player)
        {
            int cardIndex = 97;
            var engine = GetGameEngine();

            engine.GainSupport(Player.Nixon, Issue.CivilRights, 3);
            engine.GainSupport(Player.Kennedy, Issue.Economy, 1);
            engine.GainMomentum(Player.Nixon, 1);
            engine.GainMomentum(Player.Kennedy, 1);

            var sut = NineteenSixty.GMTCards[cardIndex];

            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(3, engine.GetSupportAmount(Issue.CivilRights));
            Assert.AreEqual(0, engine.GetSupportAmount(Issue.Defense));
            Assert.AreEqual(0, engine.GetSupportAmount(Issue.Economy));
            Assert.AreEqual(1, engine.GetPlayerMomentum(Player.Nixon));
            Assert.AreEqual(1, engine.GetPlayerMomentum(Player.Kennedy));
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void CassiusClayWinsGold_97_NoLeaderInBothNoMomentumLoss(Player player)
        {
            int cardIndex = 97;
            var engine = GetGameEngine();

            engine.GainSupport(Player.Nixon, Issue.CivilRights, 3);
            engine.GainSupport(Player.Kennedy, Issue.Economy, 1);
            engine.GainSupport(Player.Nixon, Issue.Defense, 2);
            engine.GainMomentum(Player.Nixon, 1);
            engine.GainMomentum(Player.Kennedy, 1);

            var sut = NineteenSixty.GMTCards[cardIndex];

            sut.Event(engine, player, EmptyChanges);

            Assert.AreEqual(3, engine.GetSupportAmount(Issue.CivilRights));
            Assert.AreEqual(1, engine.GetSupportAmount(Issue.Defense));
            Assert.AreEqual(0, engine.GetSupportAmount(Issue.Economy));
            Assert.AreEqual(1, engine.GetPlayerMomentum(Player.Nixon));
            Assert.AreEqual(1, engine.GetPlayerMomentum(Player.Kennedy));
        }

        [TestMethod]
        public void CassiusClayWinsGold_97_ValidationAlwaysTrue()
        {
            int cardIndex = 97;
            var sut = NineteenSixty.GMTCards[cardIndex];

            var result = sut.AreChangesValid(InvalidChanges);

            Assert.IsTrue(result);
        }

        #endregion
    }
}
