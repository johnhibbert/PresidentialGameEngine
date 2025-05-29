using PresidentialGameEngine.ClassLibrary.Components;
using PresidentialGameEngine.ClassLibrary.Data;
using PresidentialGameEngine.ClassLibrary.Engines;
using PresidentialGameEngine.ClassLibrary.Enums;
using PresidentialGameEngine.ClassLibrary.Manifests;
using static PresidentialGameEngine.ClassLibrary.Tests.TestStubsFakesAndMocks;

namespace PresidentialGameEngine.ClassLibrary.Tests
{
    [TestClass]
    public class NineteenSixtyCardSpecificTests
    {
        //"If it isn't tested, it's broken." Eliotte Rusty Harold
        //https://youtu.be/fr1E9aVnBxw?si=uW41ZLChgJ5EEYnl&t=138

        private static PlayerChosenChanges<Player, Issue, State> GetEmptyChanges() 
        {
            return new();
        }

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

        private NineteenSixtyGameEngine GetGameEngine() 
        {
            SeededRandomnessProviderForTesting seed = new();
            ComponentCollection<Player, Leader, Issue, State, Region> compColl = new()
            {
                MomentumComponent = new AccumulatingComponent<Player>(),
                IssueSupportComponent = new SupportComponent<Player, Leader, Issue>(),
                StateSupportComponent = new SupportComponent<Player, Leader, State>(),
                IssuePositioningComponent = new PositioningComponent<Issue>(),
                PoliticalCapitalComponent = new PoliticalCapitalComponent<Player>(seed, 12),
                RegionalComponent = new RegionalComponent<State, Region, Player>(NineteenSixty.RegionByState, NineteenSixty.PlayerStartingPositions),
                RestComponent = new AccumulatingComponent<Player>(),
                EndorsementComponent = new SupportComponent<Player, Leader, Region>(),
                MediaSupportComponent = new SupportComponent<Player, Leader, Region>()
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

            sut.Event(engine, player, GetEmptyChanges());

            Assert.AreEqual(engine.GetPlayerMomentum(player), playerStartingMomentum + 1);
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void Volunteers_5_ValidationAlwaysTrue(Player player)
        {
            int cardIndex = 5;
            var sut = NineteenSixty.GMTCards[cardIndex];

            var result = sut.AreChangesValid(GetInvalidChanges());

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
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void NewEngland_6_FailsValidationIfNixonGains(Player player)
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
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void NewEngland_6_FailsValidationIfIssueGains(Player player)
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
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void NewEngland_6_FailsValidationIfGreaterThanTwo(Player player)
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
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void NewEngland_6_FailsValidationIfExcludedState(Player player)
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

            sut.Event(engine, player, GetEmptyChanges());

            Assert.AreEqual(Leader.Kennedy, engine.GetLeader(State.IL));
            Assert.AreEqual(2, engine.GetSupportAmount(State.IL));
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
            sut.Event(engine, player, GetEmptyChanges());

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
            sut.Event(engine, player, GetEmptyChanges());

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
            sut.Event(engine, player, GetEmptyChanges());

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
            sut.Event(engine, player, GetEmptyChanges());

            Assert.AreEqual(0, engine.GetSupportAmount(State.NY));
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void SovietEconomicGrowth_8_ValidationAlwaysTrue(Player player)
        {
            int cardIndex = 8;
            var sut = NineteenSixty.GMTCards[cardIndex];

            var result = sut.AreChangesValid(GetInvalidChanges());

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
            sut.Event(engine, player, GetEmptyChanges());

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
            sut.Event(engine, player, GetEmptyChanges());

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
            sut.Event(engine, player, GetEmptyChanges());

            Assert.AreEqual(0, engine.GetSupportAmount(state));
            Assert.AreEqual(Leader.None, engine.GetLeader(state));
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void Gaffe_22_ValidationAlwaysTrue(Player player)
        {
            int cardIndex = 22;
            var sut = NineteenSixty.GMTCards[cardIndex];

            var result = sut.AreChangesValid(GetInvalidChanges());

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
            sut.Event(engine, player, GetEmptyChanges());

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
            sut.Event(engine, player, GetEmptyChanges());

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
            sut.Event(engine, player, GetEmptyChanges());

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
            sut.Event(engine, player, GetEmptyChanges());

            Assert.AreEqual(1, engine.GetSupportAmount(Issue.CivilRights));
            Assert.AreEqual(player.ToLeader(), engine.GetLeader(Issue.CivilRights));
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void MartinLutherKingArrested_23_ValidationAlwaysTrue(Leader player)
        {
            int cardIndex = 23;
            var sut = NineteenSixty.GMTCards[cardIndex];

            var result = sut.AreChangesValid(GetInvalidChanges());

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
            sut.Event(engine, player, GetEmptyChanges());

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
            sut.Event(engine, player, GetEmptyChanges());

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
            sut.Event(engine, player, GetEmptyChanges());

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
            sut.Event(engine, player, GetEmptyChanges());

            Assert.AreEqual(1, engine.GetSupportAmount(Issue.CivilRights));
            Assert.AreEqual(Leader.Kennedy, engine.GetLeader(Issue.CivilRights));
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void CivilRightsAct_25_ValidationAlwaysTrue(Player player)
        {
            int cardIndex = 25;
            var sut = NineteenSixty.GMTCards[cardIndex];

            var result = sut.AreChangesValid(GetInvalidChanges());

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
        [DataRow(Issue.CivilRights)]
        [DataRow(Issue.Defense)]
        [DataRow(Issue.Economy)]
        public void PierreSalinger_41_FailsValidationIfSupportGainedInMoreThanOneIssue(Issue issue)
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

            sut.Event(engine, player, GetEmptyChanges());

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

            sut.Event(engine, player, GetEmptyChanges());

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

            sut.Event(engine, player, GetEmptyChanges());

            Assert.AreEqual(Leader.Nixon, engine.GetLeader(Issue.Economy));
            Assert.AreEqual(2, engine.GetSupportAmount(Issue.Economy));
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void RisingFoodPrices_48_ValidationAlwaysTrue(Player player)
        {
            int cardIndex = 48;
            var sut = NineteenSixty.GMTCards[cardIndex];

            var result = sut.AreChangesValid(GetInvalidChanges());

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

            sut.Event(engine, player, GetEmptyChanges());

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

            sut.Event(engine, player, GetEmptyChanges());

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
            sut.Event(engine, player, GetEmptyChanges());

            Assert.AreEqual(Leader.None, engine.GetLeader(Issue.Defense));
            Assert.AreEqual(0, engine.GetSupportAmount(Issue.Defense));
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void MissileGap_51_ValidationAlwaysTrue(Player player)
        {
            int cardIndex = 51;
            var sut = NineteenSixty.GMTCards[cardIndex];

            var result = sut.AreChangesValid(GetInvalidChanges());

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
            sut.Event(engine, player, GetEmptyChanges());

            Assert.AreEqual(State.FL, engine.GetPlayerState(player));
        }

        [TestMethod]
        [DataRow(Player.Nixon, State.MI)]
        [DataRow(Player.Kennedy, State.AZ)]
        public void HurricaneDonna_52_SupportGained(Player player, State state)
        {
            int cardIndex = 52;
            var engine = GetGameEngine();

            var sut = NineteenSixty.GMTCards[cardIndex];
            sut.Event(engine, player, GetEmptyChanges());

            Assert.AreEqual(1, engine.GetSupportAmount(State.FL));
        }


        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void HurricaneDonna_52_ValidationAlwaysTrue(Player player)
        {
            int cardIndex = 52;
            var sut = NineteenSixty.GMTCards[cardIndex];

            var result = sut.AreChangesValid(GetInvalidChanges());

            Assert.IsTrue(result);
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
            sut.Event(engine, player, GetEmptyChanges());

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
            sut.Event(engine, player, GetEmptyChanges());

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
            sut.Event(engine, player, GetEmptyChanges());

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
            sut.Event(engine, player, GetEmptyChanges());

            Assert.AreEqual(0, engine.GetPlayerMomentum(Player.Kennedy));
            Assert.AreEqual(0, engine.GetPlayerMomentum(Player.Nixon));
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void TheTrialOfGaryPowers_62_ValidationAlwaysTrue(Player player)
        {
            int cardIndex = 62;
            var sut = NineteenSixty.GMTCards[cardIndex];

            var result = sut.AreChangesValid(GetInvalidChanges());

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
            sut.Event(engine, player, GetEmptyChanges());

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
            sut.Event(engine, player, GetEmptyChanges());

            Assert.AreEqual(0, engine.GetPlayerMomentum(Player.Nixon));
            Assert.AreEqual(0, engine.GetSupportAmount(Issue.Defense));
            Assert.AreEqual(0, engine.GetSupportAmount(Issue.Economy));
            Assert.AreEqual(0, engine.GetSupportAmount(Issue.CivilRights));
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void TheTrialOfGaryPowers_63_ValidationAlwaysTrue(Player player)
        {
            int cardIndex = 63;
            var sut = NineteenSixty.GMTCards[cardIndex];

            var result = sut.AreChangesValid(GetInvalidChanges());

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
            sut.Event(engine, player, GetEmptyChanges());

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
            sut.Event(engine, player, GetEmptyChanges());

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
            sut.Event(engine, player, GetEmptyChanges());

            Assert.AreEqual(engine.GetPlayerMomentum(player), engine.GetPlayerMomentum(player.ToOpponent()));
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void StumpSpeech_64_ValidationAlwaysTrue(Player player)
        {
            int cardIndex = 64;
            var sut = NineteenSixty.GMTCards[cardIndex];

            var result = sut.AreChangesValid(GetInvalidChanges());

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

            sut.Event(engine, player, GetEmptyChanges());

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

            sut.Event(engine, player, GetEmptyChanges());

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

            sut.Event(engine, player, GetEmptyChanges());

            Assert.AreEqual(Leader.Nixon, engine.GetLeader(Issue.Defense));
            Assert.AreEqual(1, engine.GetSupportAmount(Issue.Defense));
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void PeaceWithoutSurrender_68_ValidationAlwaysTrue(Player player)
        {
            int cardIndex = 68;
            var sut = NineteenSixty.GMTCards[cardIndex];

            var result = sut.AreChangesValid(GetInvalidChanges());

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

            sut.Event(engine, player, GetEmptyChanges());

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

            sut.Event(engine, player, GetEmptyChanges());

            Assert.AreEqual(engine.GetPlayerMomentum(Player.Nixon), 0);
            Assert.AreEqual(engine.GetPlayerMomentum(Player.Kennedy), 0);
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void TheOldNixon_70_ValidationAlwaysTrue(Player player)
        {
            int cardIndex = 70;
            var sut = NineteenSixty.GMTCards[cardIndex];

            var result = sut.AreChangesValid(GetInvalidChanges());

            Assert.IsTrue(result);
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
            sut.Event(engine, player, GetEmptyChanges());

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
            sut.Event(engine, player, GetEmptyChanges());

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
            sut.Event(engine, player, GetEmptyChanges());

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
            sut.Event(engine, player, GetEmptyChanges());

            Assert.AreEqual(0, engine.GetSupportAmount(State.NY));
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void StockMarketInDecline_78_ValidationAlwaysTrue(Player player)
        {
            int cardIndex = 78;
            var sut = NineteenSixty.GMTCards[cardIndex];

            var result = sut.AreChangesValid(GetInvalidChanges());

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
            sut.Event(engine, player, GetEmptyChanges());

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
            sut.Event(engine, player, GetEmptyChanges());

            Assert.AreEqual(0, engine.GetSupportAmount(State.FL));
            Assert.AreEqual(0, engine.GetPlayerMomentum(player));
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void FidelCastro_82_ValidationAlwaysTrue(Player player)
        {
            int cardIndex = 82;
            var sut = NineteenSixty.GMTCards[cardIndex];

            var result = sut.AreChangesValid(GetInvalidChanges());

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

            sut.Event(engine, player, GetEmptyChanges());

            Assert.AreEqual(engine.GetPlayerMomentum(Player.Nixon), nixonStartingMomentum + 1);
            Assert.AreEqual(engine.GetPlayerMomentum(Player.Kennedy), kennedyStartingMomentum);
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void TheNewNixon_89_ValidationAlwaysTrue(Player player)
        {
            int cardIndex = 89;
            var sut = NineteenSixty.GMTCards[cardIndex];

            var result = sut.AreChangesValid(GetInvalidChanges());

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
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void Recount_90_FailsValidationIfNixonGains(Player player)
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
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void Recount_90_FailsValidationIfIssueGains(Player player)
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
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void Recount_90_FailsValidationIfGreaterThanThree(Player player)
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
            sut.Event(engine, player, GetEmptyChanges());

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
            sut.Event(engine, player, GetEmptyChanges());

            Assert.AreEqual(2, engine.GetSupportAmount(Issue.CivilRights));
            Assert.AreEqual(2, engine.GetSupportAmount(Issue.Defense));
            Assert.AreEqual(0, engine.GetSupportAmount(Issue.Economy));
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void ExperienceCounts_93_ValidationAlwaysTrue(Player player)
        {
            int cardIndex = 93;
            var sut = NineteenSixty.GMTCards[cardIndex];

            var result = sut.AreChangesValid(GetInvalidChanges());

            Assert.IsTrue(result);
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

            sut.Event(engine, player, GetEmptyChanges());

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

            sut.Event(engine, player, GetEmptyChanges());

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

            sut.Event(engine, player, GetEmptyChanges());

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

            sut.Event(engine, player, GetEmptyChanges());

            Assert.AreEqual(0, engine.GetSupportAmount(Issue.CivilRights));
            Assert.AreEqual(2, engine.GetSupportAmount(Issue.Defense));
            Assert.AreEqual(0, engine.GetSupportAmount(Issue.Economy));
            Assert.AreEqual(1, engine.GetPlayerMomentum(Player.Nixon));
            Assert.AreEqual(1, engine.GetPlayerMomentum(Player.Kennedy));
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void MedalCount_96_ValidationAlwaysTrue(Player player)
        {
            int cardIndex = 96;
            var sut = NineteenSixty.GMTCards[cardIndex];

            var result = sut.AreChangesValid(GetInvalidChanges());

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

            sut.Event(engine, player, GetEmptyChanges());

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

            sut.Event(engine, player, GetEmptyChanges());

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

            sut.Event(engine, player, GetEmptyChanges());

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

            sut.Event(engine, player, GetEmptyChanges());

            Assert.AreEqual(3, engine.GetSupportAmount(Issue.CivilRights));
            Assert.AreEqual(1, engine.GetSupportAmount(Issue.Defense));
            Assert.AreEqual(0, engine.GetSupportAmount(Issue.Economy));
            Assert.AreEqual(1, engine.GetPlayerMomentum(Player.Nixon));
            Assert.AreEqual(1, engine.GetPlayerMomentum(Player.Kennedy));
        }

        [TestMethod]
        [DataRow(Player.Nixon)]
        [DataRow(Player.Kennedy)]
        public void CassiusClayWinsGold_97_ValidationAlwaysTrue(Leader player)
        {
            int cardIndex = 97;
            var sut = NineteenSixty.GMTCards[cardIndex];

            var result = sut.AreChangesValid(GetInvalidChanges());

            Assert.IsTrue(result);
        }

        #endregion
    }
}
