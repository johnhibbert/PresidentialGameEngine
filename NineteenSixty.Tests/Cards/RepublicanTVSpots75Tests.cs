using NineteenSixty.Data;
using NineteenSixty.Enums;
using NineteenSixty.Tests.Fixtures;
using PresidentialGameEngine.ClassLibrary.Data;

namespace NineteenSixty.Tests.Cards;

[TestClass]
public class RepublicanTvSpots75Tests
{
    //"Immediately move the Nixon candidate token to New York, but do not pay the normal travel costs for doing so.  The Nixon player may place 3 media support cubes."
    private const int CardIndex = 75;
    
    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void RepublicanTVSpots_75_NixonMovedToNewYork(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        engine.MovePlayerToState(Player.Nixon, State.AK);
        
        SetOfChanges playerChoices = new();
        var mediaSupportGained = new SupportChange<Player, Region>(Player.Nixon, Region.West, 3);
        playerChoices.MediaSupportChanges.Add(mediaSupportGained);
        
        var sut = Manifest.GMTCards[CardIndex];

        sut.Event(engine, player, playerChoices);

        Assert.AreEqual(State.NY, engine.GetPlayerState(Player.Nixon));
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void RepublicanTVSpots_75_NixonGainsThreeMediaSupport(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();
        engine.GainSupport(Player.Kennedy, Issue.CivilRights, 2);

        SetOfChanges playerChoices = new();
        var mediaSupportGained = new SupportChange<Player, Region>(Player.Nixon, Region.West, 3);
        playerChoices.MediaSupportChanges.Add(mediaSupportGained);

        var sut = Manifest.GMTCards[CardIndex];

        sut.Event(engine, player, playerChoices);

        Assert.AreEqual(3, engine.GetGameState().MediaSupportLevels[Region.West].Amount);
    }
    
    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void RepublicanTVSpots_75_WorksEvenIfNixonWasAlreadyInNewYork(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();
        engine.MovePlayerToState(Player.Nixon, State.NY);
        
        engine.GainSupport(Player.Kennedy, Issue.CivilRights, 2);
     
        SetOfChanges playerChoices = new();
        var mediaSupportGained = new SupportChange<Player, Region>(Player.Nixon, Region.West, 3);
        playerChoices.MediaSupportChanges.Add(mediaSupportGained);

        var sut = Manifest.GMTCards[CardIndex];

        sut.Event(engine, player, playerChoices);
        
        Assert.AreEqual(State.NY, engine.GetPlayerState(Player.Nixon));
        Assert.AreEqual(3, engine.GetGameState().MediaSupportLevels[Region.West].Amount);
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void RepublicanTVSpots_75_ValidationGreaterThanThreeMediaSupportFails(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();
        engine.GainSupport(Player.Kennedy, Issue.CivilRights, 2);

        SetOfChanges playerChoices = new();
        var invalidMediaSupportGainedGreaterThanThree = new SupportChange<Player, Region>(Player.Nixon, Region.West, 4);

        playerChoices.MediaSupportChanges.Add(invalidMediaSupportGainedGreaterThanThree);

        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(playerChoices);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void RepublicanTVSpots_75_ValidationFailsIfStateSupportChanged()
    {
        var engine = EngineFixtures.GetGameEngine();
        engine.GainSupport(Player.Kennedy, Issue.CivilRights, 2);

        SetOfChanges playerChoices = new();
        var invalidStateSupport = new SupportChange<Player, State>(Player.Nixon, State.CT, 1);

        playerChoices.StateChanges.Add(invalidStateSupport);

        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(playerChoices);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void RepublicanTVSpots_75_ValidationFailsIfIssueSupportChanged()
    {
        var engine = EngineFixtures.GetGameEngine();
        engine.GainSupport(Player.Kennedy, Issue.CivilRights, 2);

        SetOfChanges playerChoices = new();
        var invalidIssueSupport = new SupportChange<Player, Issue>(Player.Nixon, Issue.Defense, 1);

        playerChoices.IssueChanges.Add(invalidIssueSupport);

        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(playerChoices);
        Assert.IsFalse(result);
    }

}