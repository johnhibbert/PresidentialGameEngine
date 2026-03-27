using NSubstitute;
using NineteenSixty;
using NineteenSixty.Data;
using NineteenSixty.Enums;
using NineteenSixty.Exceptions;
using NineteenSixty.Interfaces;
using NineteenSixty.Tests.Fixtures;
using NSubstitute.ReturnsExtensions;
using PresidentialGameEngine.ClassLibrary.Data;
using Card = NineteenSixty.Data.Card;

namespace NineteenSixty.Tests;

[TestClass]
public class ControllerTests
{

    #region Fixtures

    private static GameState ExampleGameState => GetExampleGameState();


    private static GameState GetExampleGameState()
    {
        var momentum = new Dictionary<Player, int>()
        {
            { Player.Kennedy, 1 },
            { Player.Nixon, 2 },
        };

        var restCubes = new Dictionary<Player, int>()
        {
            { Player.Kennedy, 20 },
            { Player.Nixon, 5 },
        };

        var issueContests = new Dictionary<Issue, SupportContest<Leader>>()
        {
            { Issue.CivilRights, new SupportContest<Leader>() },
            { Issue.Economy, new SupportContest<Leader>() },
            { Issue.Defense, new SupportContest<Leader>() },
        };

        issueContests[Issue.Economy].Leader = Leader.Nixon;
        issueContests[Issue.Economy].Amount = 1;

        issueContests[Issue.CivilRights].Leader = Leader.Kennedy;
        issueContests[Issue.CivilRights].Amount = 2;

        var stateContests = new Dictionary<State, SupportContest<Leader>>()
        {
            { State.CT, new SupportContest<Leader>() },
            { State.NH, new SupportContest<Leader>() },
        };

        stateContests[State.CT].Leader = Leader.Nixon;
        stateContests[State.CT].Amount = 1;

        stateContests[State.NH].Leader = Leader.Kennedy;
        stateContests[State.NH].Amount = 3;

        var issueOrder = new List<Issue>()
        {
            Issue.Defense, Issue.CivilRights, Issue.Economy
        };

        var endorsements = new Dictionary<Region, SupportContest<Leader>>()
        {
            { Region.South, new SupportContest<Leader>() },
            { Region.West, new SupportContest<Leader>() },
        };

        endorsements[Region.South].Leader = Leader.Nixon;
        endorsements[Region.South].Amount = 4;
        endorsements[Region.West].Leader = Leader.Kennedy;
        endorsements[Region.West].Amount = 1;

        var mediaSupportLevels = new Dictionary<Region, SupportContest<Leader>>()
        {
            { Region.East, new SupportContest<Leader>() },
            { Region.Midwest, new SupportContest<Leader>() },
        };

        mediaSupportLevels[Region.East].Leader = Leader.Kennedy;
        mediaSupportLevels[Region.East].Amount = 2;
        mediaSupportLevels[Region.Midwest].Leader = Leader.Nixon;
        mediaSupportLevels[Region.Midwest].Amount = 3;

        var playerLocations = new Dictionary<Player, State>()
        {
            { Player.Kennedy, State.AK },
            { Player.Nixon, State.HI },
        };

        var playerStatuses = new Dictionary<Player, Status>()
        {
            { Player.Kennedy, Status.Exhausted },
            { Player.Nixon, Status.Ready },
        };

        var expectedGameState = new GameState()
        {
            Momentum = momentum,
            RestCubes = restCubes,
            IssueContests = issueContests,
            StateContests = stateContests,
            IssueOrder = issueOrder,
            Endorsements = endorsements,
            MediaSupportLevels = mediaSupportLevels,
            PlayerLocations = playerLocations,
            PlayerStatuses = playerStatuses,
        };

        return expectedGameState;
    }

    private static Card ExampleCard => GetExampleCard();

    private static Card GetExampleCard()
    {
        var exampleCard = new Card()
        {
            Index = 1,
            Title = "Example Card",
            Text = "Example Card Tests.",
            CampaignPoints = 2,
            EventType = EventType.None,
            Issue = Issue.Defense,
            Affiliation = Affiliation.Both,
            State = State.OR,
            Event = (plan, player) =>
            {
                var engine = plan.Engine;
                var choices = plan.Changes;

                engine.ImplementChanges(choices);
            },
            RequiresPlayerInput = false,
            AreChangesValid = (plan) =>
            {
                var engine = plan.Engine;
                var choices = plan.Changes;

                State[] validStates = [State.KY];
                var onlyValidStates = choices.StateChanges.Select(s => s.Target).All(x => validStates.Contains(x));
            
                return onlyValidStates;
            },
        };
        
        return  exampleCard;
    }
    
    #endregion

    #region GetGameState Tests

    [TestMethod]
    [DataRow(GameEdition.FirstEditionByZMan)]
    [DataRow(GameEdition.SecondEditionByGmt)]
    public void GetGameState_GetsInfoFromEngine(GameEdition edition)
    {
        var mockEngine = Substitute.For<IEngine>();

        var expectedGameState = ExampleGameState;

        mockEngine.GetGameState().Returns(expectedGameState);

        var sut = new Controller(mockEngine, edition);

        var result = sut.GetGameState();
        Assert.AreEqual(expectedGameState, result);

    }

    #endregion

    #region SetUpBoard Tests

    [TestMethod]
    [DataRow(State.AK, 0)]
    [DataRow(State.AL, 1)]
    [DataRow(State.AR, 1)]
    [DataRow(State.AZ, 1)]
    [DataRow(State.CA, 0)]
    [DataRow(State.CO, 1)]
    [DataRow(State.CT, 0)]
    [DataRow(State.DE, 0)]
    [DataRow(State.FL, 0)]
    [DataRow(State.GA, 2)]
    [DataRow(State.HI, 0)]
    [DataRow(State.IA, 1)]
    [DataRow(State.ID, 0)]
    [DataRow(State.IL, 0)]
    [DataRow(State.IN, 1)]
    [DataRow(State.KS, 2)]
    [DataRow(State.KY, 0)]
    [DataRow(State.LA, 2)]
    [DataRow(State.MA, 2)]
    [DataRow(State.MD, 0)]
    [DataRow(State.ME, 1)]
    [DataRow(State.MI, 0)]
    [DataRow(State.MN, 0)]
    [DataRow(State.MO, 1)]
    [DataRow(State.MS, 2)]
    [DataRow(State.MT, 0)]
    [DataRow(State.NC, 1)]
    [DataRow(State.ND, 1)]
    [DataRow(State.NE, 2)]
    [DataRow(State.NH, 0)]
    [DataRow(State.NJ, 0)]
    [DataRow(State.NM, 0)]
    [DataRow(State.NV, 0)]
    [DataRow(State.NY, 0)]
    [DataRow(State.OH, 1)]
    [DataRow(State.OK, 1)]
    [DataRow(State.OR, 0)]
    [DataRow(State.PA, 0)]
    [DataRow(State.RI, 2)]
    [DataRow(State.SC, 1)]
    [DataRow(State.SD, 1)]
    [DataRow(State.TN, 0)]
    [DataRow(State.TX, 0)]
    [DataRow(State.UT, 1)]
    [DataRow(State.VA, 0)]
    [DataRow(State.VT, 1)]
    [DataRow(State.WA, 0)]
    [DataRow(State.WI, 0)]
    [DataRow(State.WV, 0)]
    [DataRow(State.WY, 1)]

    public void SetUpBoard_DefaultStartingStateSupport(State state, int expectedAmount)
    {
        var sut = new Controller(EngineFixtures.GetGameEngine(), GameEdition.SecondEditionByGmt);
        sut.SetUpBoard();
        var result = sut.GetGameState().StateContests;

        Assert.AreEqual(expectedAmount, result[state].Amount);
    }

    [TestMethod]
    [DataRow(State.AK, Leader.None)]
    [DataRow(State.AL, Leader.Kennedy)]
    [DataRow(State.AR, Leader.Kennedy)]
    [DataRow(State.AZ, Leader.Nixon)]
    [DataRow(State.CA, Leader.None)]
    [DataRow(State.CO, Leader.Nixon)]
    [DataRow(State.CT, Leader.None)]
    [DataRow(State.DE, Leader.None)]
    [DataRow(State.FL, Leader.None)]
    [DataRow(State.GA, Leader.Kennedy)]
    [DataRow(State.HI, Leader.None)]
    [DataRow(State.IA, Leader.Nixon)]
    [DataRow(State.ID, Leader.None)]
    [DataRow(State.IL, Leader.None)]
    [DataRow(State.IN, Leader.Nixon)]
    [DataRow(State.KS, Leader.Nixon)]
    [DataRow(State.KY, Leader.None)]
    [DataRow(State.LA, Leader.Kennedy)]
    [DataRow(State.MA, Leader.Kennedy)]
    [DataRow(State.MD, Leader.None)]
    [DataRow(State.ME, Leader.Nixon)]
    [DataRow(State.MI, Leader.None)]
    [DataRow(State.MN, Leader.None)]
    [DataRow(State.MO, Leader.Kennedy)]
    [DataRow(State.MS, Leader.Kennedy)]
    [DataRow(State.MT, Leader.None)]
    [DataRow(State.NC, Leader.Kennedy)]
    [DataRow(State.ND, Leader.Nixon)]
    [DataRow(State.NE, Leader.Nixon)]
    [DataRow(State.NH, Leader.None)]
    [DataRow(State.NJ, Leader.None)]
    [DataRow(State.NM, Leader.None)]
    [DataRow(State.NV, Leader.None)]
    [DataRow(State.NY, Leader.None)]
    [DataRow(State.OH, Leader.Nixon)]
    [DataRow(State.OK, Leader.Nixon)]
    [DataRow(State.OR, Leader.None)]
    [DataRow(State.PA, Leader.None)]
    [DataRow(State.RI, Leader.Kennedy)]
    [DataRow(State.SC, Leader.Kennedy)]
    [DataRow(State.SD, Leader.Nixon)]
    [DataRow(State.TN, Leader.None)]
    [DataRow(State.TX, Leader.None)]
    [DataRow(State.UT, Leader.Nixon)]
    [DataRow(State.VA, Leader.None)]
    [DataRow(State.VT, Leader.Nixon)]
    [DataRow(State.WA, Leader.None)]
    [DataRow(State.WI, Leader.None)]
    [DataRow(State.WV, Leader.None)]
    [DataRow(State.WY, Leader.Nixon)]
    public void SetUpBoard_DefaultLeader(State state, Leader expectedLeader)
    {
        var sut = new Controller(EngineFixtures.GetGameEngine(), GameEdition.SecondEditionByGmt);
        sut.SetUpBoard();
        var result = sut.GetGameState().StateContests;

        Assert.AreEqual(expectedLeader, result[state].Leader);
    }

    [TestMethod]
    [DataRow(GameEdition.FirstEditionByZMan)]
    [DataRow(GameEdition.SecondEditionByGmt)]
    public void SetUpBoard_IssueOrderSet(GameEdition edition)
    {
        var expectedOrder = new List<Issue>() { Issue.Defense, Issue.Economy, Issue.CivilRights };
        
        var sut = new Controller(EngineFixtures.GetGameEngine(), edition);
        sut.SetUpBoard();
        var result = sut.GetGameState().IssueOrder;

        Assert.AreEqual(result[0], expectedOrder[0]);
        Assert.AreEqual(result[1], expectedOrder[1]);
        Assert.AreEqual(result[2], expectedOrder[2]);
    }
    
    [TestMethod]
    [DataRow(GameEdition.FirstEditionByZMan)]
    [DataRow(GameEdition.SecondEditionByGmt)]
    public void SetUpBoard_IssueContestsAllEmpty(GameEdition edition)
    {
        var sut = new Controller(EngineFixtures.GetGameEngine(), edition);
        sut.SetUpBoard();
        var result = sut.GetGameState().IssueContests;

        Assert.AreEqual(0, result[Issue.CivilRights].Amount);
        Assert.AreEqual(0, result[Issue.Defense].Amount);
        Assert.AreEqual(0, result[Issue.Economy].Amount);
    }
    
    [TestMethod]
    public void SetUpBoard_FirstEditionCardsShuffledIntoDeck()
    {
        var engine = EngineFixtures.GetGameEngine();

        //Making this variable instead of concrete because the number of cards implemented will change
        var numberOfImplementedCards = Manifest.ZManCards.Count;
        
        var sut = new Controller(engine, GameEdition.FirstEditionByZMan);
        sut.SetUpBoard();

        var cardsInDeck = engine.GetCardsInZone(CardZone.Deck, Player.Kennedy);

        Assert.AreEqual(numberOfImplementedCards, cardsInDeck.Count());
    }
    
    [TestMethod]
    public void SetUpBoard_SecondEditionCardsShuffledIntoDeck()
    {
        var engine = EngineFixtures.GetGameEngine();

        //Making this variable instead of concrete because the number of cards implemented will change
        var numberOfImplementedCards = Manifest.GMTCards.Count;
        
        var sut = new Controller(engine, GameEdition.SecondEditionByGmt);
        sut.SetUpBoard();

        var cardsInDeck = engine.GetCardsInZone(CardZone.Deck, Player.Kennedy);

        Assert.AreEqual(numberOfImplementedCards, cardsInDeck.Count());
    }
    
    [TestMethod]
    [ExpectedException(typeof(ActionNotAllowed))]
    public void SetUpBoard_CannotSetupBoardAfterBoardAlreadySetUp()
    {
        var sut = new Controller(EngineFixtures.GetGameEngine(), GameEdition.SecondEditionByGmt);
        sut.SetUpBoard();
        sut.SetUpBoard();
    }
    
    #endregion

    #region ConductInitiativeCheck

    [TestMethod]
    public void ConductInitiativeCheck_PlayerGainsInitiative()
    {
        var sut = new Controller(EngineFixtures.GetGameEngine(), GameEdition.SecondEditionByGmt);
        sut.SetUpBoard();
        var result = sut.ConductInitiativeCheck();

        Assert.AreEqual(Player.Nixon, result.PlayerWithInitiative);
    }

    [TestMethod]
    public void ConductInitiativeCheck_DiceDrawn()
    {
        var sut = new Controller(EngineFixtures.GetGameEngine(), GameEdition.SecondEditionByGmt);
        sut.SetUpBoard();
        var result = sut.ConductInitiativeCheck();

        Assert.AreEqual(2, result.CubesDrawn[Player.Nixon]);
    }
    
    
    #endregion
    
    #region PlayCardAsEvent
    
    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void PlayCardAsEvent_ValidChangesAreImplemented(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();
        
        var playerChoices = new SetOfChanges();
        var oneSupportInKentucky = new SupportChange<Player, State>(player, State.KY, 1);
        playerChoices.StateChanges.Add(oneSupportInKentucky);

        var plan = new ActionPlan()
        {
            Engine = engine,
            Changes = playerChoices,
        };
        
        var sut = new Controller(engine, GameEdition.SecondEditionByGmt);

        sut.PlayCardAsEvent(ExampleCard, plan, player);

        var result = sut.GetGameState().StateContests[State.KY];
        
        Assert.AreEqual(1, result.Amount);
        Assert.AreEqual(player.ToLeader(), result.Leader);
    }
    
    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    [ExpectedException(typeof(InvalidPlayerChoices))]
    public void PlayCardAsEvent_InvalidChangesThrowException(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();
        
        var playerChoices = new SetOfChanges();
        var invalidSupportInTennessee = new SupportChange<Player, State>(player, State.TN, 1);
        playerChoices.StateChanges.Add(invalidSupportInTennessee);

        var plan = new ActionPlan()
        {
            Engine = engine,
            Changes = playerChoices,
        };
        
        var sut = new Controller(engine, GameEdition.SecondEditionByGmt);

        sut.PlayCardAsEvent(ExampleCard, plan, player);
    }
    
    #endregion
    
    #region SetFirstPlayerForTurn Tests
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    [TestMethod]
    public void SetFirstPlayerForTurn_FirstPlayerSet(Player player)
    {
        var sut = new Controller(EngineFixtures.GetGameEngine(), GameEdition.SecondEditionByGmt);
        sut.SetUpBoard();
        sut.ConductInitiativeCheck();
        sut.SetFirstPlayerForActivityPhase(player);
        var result = sut.GetGameTime();

        Assert.AreEqual(player, result.FirstPlayer);
    }
    
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    [TestMethod]
    public void SetFirstPlayerForTurn_CurrentPlayerSet(Player player)
    {

        var sut = new Controller(EngineFixtures.GetGameEngine(), GameEdition.SecondEditionByGmt);
        sut.SetUpBoard();
        sut.ConductInitiativeCheck();
        sut.SetFirstPlayerForActivityPhase(player);
        var result = sut.GetGameTime();
        
        Assert.AreEqual(player, result.ActivePlayer);
    }
    
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    [TestMethod]
    public void SetFirstPlayerForTurn_ActivityPhaseNumberUpdatedToOne(Player player)
    {

        var sut = new Controller(EngineFixtures.GetGameEngine(), GameEdition.SecondEditionByGmt);
        sut.SetUpBoard();
        sut.ConductInitiativeCheck();
        sut.SetFirstPlayerForActivityPhase(player);
        var result = sut.GetGameTime();
        
        Assert.AreEqual(1, result.ActivityPhaseNumber);
    }
    
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    [TestMethod]
    public void SetFirstPlayerForTurn_TurnNumberUpdatedToOne(Player player)
    {

        var sut = new Controller(EngineFixtures.GetGameEngine(), GameEdition.SecondEditionByGmt);
        sut.SetUpBoard();
        sut.ConductInitiativeCheck();
        sut.SetFirstPlayerForActivityPhase(player);
        var result = sut.GetGameTime();
        
        Assert.AreEqual(1, result.TurnNumber);
    }
    
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    [TestMethod]
    public void SetFirstPlayerForTurn_CurrentPhaseIsActivity(Player player)
    {

        var sut = new Controller(EngineFixtures.GetGameEngine(), GameEdition.SecondEditionByGmt);
        sut.SetUpBoard();
        sut.ConductInitiativeCheck();
        sut.SetFirstPlayerForActivityPhase(player);
        var result = sut.GetGameTime();
        
        Assert.AreEqual(Phase.Activity, result.CurrentPhase);
    }
    
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    [TestMethod]
    [ExpectedException(typeof(ActionNotAllowed))]
    public void SetFirstPlayerForTurn_NotAllowedInSetupPhase(Player player)
    {
        var sut = new Controller(EngineFixtures.GetGameEngine(), GameEdition.SecondEditionByGmt);
        sut.SetFirstPlayerForActivityPhase(player);
    }
    
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    [TestMethod]
    [ExpectedException(typeof(ActionNotAllowed))]
    public void SetFirstPlayerForTurn_NotAllowedInActivityPhase(Player player)
    {
        var sut = new Controller(EngineFixtures.GetGameEngine(), GameEdition.SecondEditionByGmt);
        sut.SetFirstPlayerForActivityPhase(player);
        sut.SetFirstPlayerForActivityPhase(player);
    }
    
    #endregion

}