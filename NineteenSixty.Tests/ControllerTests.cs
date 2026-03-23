using NSubstitute;
using NineteenSixty;
using NineteenSixty.Data;
using NineteenSixty.Enums;
using NineteenSixty.Interfaces;
using NSubstitute.ReturnsExtensions;
using PresidentialGameEngine.ClassLibrary.Data;

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
            {Player.Kennedy, 1},
            {Player.Nixon, 2},
        };

        var restCubes = new Dictionary<Player, int>()
        {
            {Player.Kennedy, 20},
            {Player.Nixon, 5},
        };

        var issueContests = new Dictionary<Issue, SupportContest<Leader>>()
        {
            {Issue.CivilRights, new SupportContest<Leader>()},
            {Issue.Economy, new SupportContest<Leader>()},
            {Issue.Defense, new SupportContest<Leader>()},
        };

        issueContests[Issue.Economy].Leader = Leader.Nixon;
        issueContests[Issue.Economy].Amount = 1;

        issueContests[Issue.CivilRights].Leader = Leader.Kennedy;
        issueContests[Issue.CivilRights].Amount = 2;

        var stateContests = new Dictionary<State, SupportContest<Leader>>()
        {
            {State.CT, new SupportContest<Leader>()},
            {State.NH, new SupportContest<Leader>()},
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
            {Region.South, new SupportContest<Leader>()},
            {Region.West, new SupportContest<Leader>()},
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
            {Player.Kennedy, State.AK},
            {Player.Nixon, State.HI},
        };

        var playerStatuses = new Dictionary<Player, Status>()
        {
            {Player.Kennedy, Status.Exhausted},
            {Player.Nixon, Status.Ready},
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
    #endregion

    #region GetGameState Tests
    
    [TestMethod]
    public void GetGameState_GetsInfoFromEngine()
    {
        var mockEngine = Substitute.For<IEngine>();

        var expectedGameState = ExampleGameState;
        
        mockEngine.GetGameState().Returns(expectedGameState);

        var sut = new Controller(mockEngine);
        
        var result = sut.GetGameState();
        Assert.AreEqual(expectedGameState, result);

    }

    #endregion
}