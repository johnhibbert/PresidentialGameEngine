using NineteenSixty.Data;
using NineteenSixty.Enums;
using NineteenSixty.Tests.Fixtures;
using PresidentialGameEngine.ClassLibrary.Data;

namespace NineteenSixty.Tests.Cards;

[TestClass]
public class WorldSeriesEnds60Tests
{
    //"The player with media support cubes in the East (if any) may add a total of 5 state support in the East, no more than 2 per state."
    private const int CardIndex = 60;
    
    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void WorldSeriesEnds_60_SupportGainedIfMediaSupportInEast(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();
        engine.GainMediaSupport(player, Region.East, 1);

        SetOfChanges playerChoices = new();
        var oneSupportInRhodeIsland = new SupportChange<Player, State>(Player.Nixon, State.RI, 1);
        var oneSupportInWestVirginia = new SupportChange<Player, State>(Player.Nixon, State.WV, 1);
        var twoSupportInNewYork = new SupportChange<Player, State>(Player.Nixon, State.NY, 2);
        var oneSupportInPenn = new SupportChange<Player, State>(Player.Nixon, State.PA, 1);

        playerChoices.StateChanges.Add(oneSupportInRhodeIsland);
        playerChoices.StateChanges.Add(oneSupportInWestVirginia);
        playerChoices.StateChanges.Add(twoSupportInNewYork);
        playerChoices.StateChanges.Add(oneSupportInPenn);

        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(engine, player, playerChoices);

        Assert.AreEqual(1, engine.GetSupportAmount(State.RI));
        Assert.AreEqual(1, engine.GetSupportAmount(State.WV));
        Assert.AreEqual(2, engine.GetSupportAmount(State.NY));
        Assert.AreEqual(1, engine.GetSupportAmount(State.PA));
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void WorldSeriesEnds_60_NoChangeIfNoMediaSupportInEast(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        SetOfChanges playerChoices = new();
        var oneSupportInRhodeIsland = new SupportChange<Player, State>(Player.Nixon, State.RI, 1);
        var oneSupportInWestVirginia = new SupportChange<Player, State>(Player.Nixon, State.WV, 1);
        var twoSupportInNewYork = new SupportChange<Player, State>(Player.Nixon, State.NY, 2);
        var oneSupportInPenn = new SupportChange<Player, State>(Player.Nixon, State.PA, 1);

        playerChoices.StateChanges.Add(oneSupportInRhodeIsland);
        playerChoices.StateChanges.Add(oneSupportInWestVirginia);
        playerChoices.StateChanges.Add(twoSupportInNewYork);
        playerChoices.StateChanges.Add(oneSupportInPenn);

        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(engine, player, playerChoices);

        Assert.AreEqual(0, engine.GetSupportAmount(State.RI));
        Assert.AreEqual(0, engine.GetSupportAmount(State.WV));
        Assert.AreEqual(0, engine.GetSupportAmount(State.NY));
        Assert.AreEqual(0, engine.GetSupportAmount(State.PA));
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void WorldSeriesEnds_60_NoChangeIfMediaSupportOutsideOfEast(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();
        engine.GainMediaSupport(player, Region.West, 1);
        engine.GainMediaSupport(player, Region.Midwest, 1);
        engine.GainMediaSupport(player, Region.South, 1);

        SetOfChanges playerChoices = new();
        var oneSupportInRhodeIsland = new SupportChange<Player, State>(Player.Nixon, State.RI, 1);
        var oneSupportInWestVirginia = new SupportChange<Player, State>(Player.Nixon, State.WV, 1);
        var twoSupportInNewYork = new SupportChange<Player, State>(Player.Nixon, State.NY, 2);
        var oneSupportInPenn = new SupportChange<Player, State>(Player.Nixon, State.PA, 1);

        playerChoices.StateChanges.Add(oneSupportInRhodeIsland);
        playerChoices.StateChanges.Add(oneSupportInWestVirginia);
        playerChoices.StateChanges.Add(twoSupportInNewYork);
        playerChoices.StateChanges.Add(oneSupportInPenn);

        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(engine, player, playerChoices);

        Assert.AreEqual(0, engine.GetSupportAmount(State.RI));
        Assert.AreEqual(0, engine.GetSupportAmount(State.WV));
        Assert.AreEqual(0, engine.GetSupportAmount(State.NY));
        Assert.AreEqual(0, engine.GetSupportAmount(State.PA));
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void WorldSeriesEnds_60_ValidationFailsIfStateOutsideOfEast(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();
        engine.GainMediaSupport(player, Region.East, 1);

        SetOfChanges playerChoices = new();
        var oneSupportInRhodeIsland = new SupportChange<Player, State>(Player.Nixon, State.RI, 1);
        var oneSupportInWestVirginia = new SupportChange<Player, State>(Player.Nixon, State.WV, 1);
        var twoSupportInNewYork = new SupportChange<Player, State>(Player.Nixon, State.NY, 2);
        var invalidStateOutsideOfEast = new SupportChange<Player, State>(Player.Nixon, State.FL, 1);

        playerChoices.StateChanges.Add(oneSupportInRhodeIsland);
        playerChoices.StateChanges.Add(oneSupportInWestVirginia);
        playerChoices.StateChanges.Add(twoSupportInNewYork);
        playerChoices.StateChanges.Add(invalidStateOutsideOfEast);

        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(playerChoices);
        Assert.IsFalse(result);
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void WorldSeriesEnds_60_ValidationFailsIfMediaSupportChange(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();
        engine.GainMediaSupport(player, Region.East, 1);

        SetOfChanges playerChoices = new();
        var oneSupportInRhodeIsland = new SupportChange<Player, State>(player, State.RI, 1);
        var oneSupportInWestVirginia = new SupportChange<Player, State>(player, State.WV, 1);
        var twoSupportInNewYork = new SupportChange<Player, State>(player, State.NY, 2);
        var oneSupportInPenn = new SupportChange<Player, State>(player, State.PA, 1);
        var invalidMediaSupportChange = new SupportChange<Player, Region>(player, Region.South, 1);

        playerChoices.StateChanges.Add(oneSupportInRhodeIsland);
        playerChoices.StateChanges.Add(oneSupportInWestVirginia);
        playerChoices.StateChanges.Add(twoSupportInNewYork);
        playerChoices.StateChanges.Add(oneSupportInPenn);
        playerChoices.MediaSupportChanges.Add(invalidMediaSupportChange);

        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(playerChoices);
        Assert.IsFalse(result);
    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void WorldSeriesEnds_60_ValidationFailsIfInvalidIssueSupportChange(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();
        engine.GainMediaSupport(player, Region.East, 1);

        SetOfChanges playerChoices = new();
        var oneSupportInRhodeIsland = new SupportChange<Player, State>(player, State.RI, 1);
        var oneSupportInWestVirginia = new SupportChange<Player, State>(player, State.WV, 1);
        var twoSupportInNewYork = new SupportChange<Player, State>(player, State.NY, 2);
        var oneSupportInPenn = new SupportChange<Player, State>(player, State.PA, 1);
        var invalidIssueSupportChange = new SupportChange<Player, Issue>(player, Issue.Defense, 1);

        playerChoices.StateChanges.Add(oneSupportInRhodeIsland);
        playerChoices.StateChanges.Add(oneSupportInWestVirginia);
        playerChoices.StateChanges.Add(twoSupportInNewYork);
        playerChoices.StateChanges.Add(oneSupportInPenn);
        playerChoices.IssueChanges.Add(invalidIssueSupportChange);

        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(playerChoices);
        Assert.IsFalse(result);
    }


}