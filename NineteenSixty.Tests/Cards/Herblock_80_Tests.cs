// ReSharper disable InconsistentNaming
using NineteenSixty.Data;
using NineteenSixty.Enums;
using NineteenSixty.Tests.Fixtures;
using PresidentialGameEngine.ClassLibrary.Data;

namespace NineteenSixty.Tests.Cards;

[TestClass]
public class Herblock_80_Tests
{
    //"The Kennedy player may remove 2 Nixon media support cubes from the board."
    private const int CardIndex = 80;
    
    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void Herblock_80_NixonLosesMediaSupport(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        engine.GainMediaSupport(Player.Nixon, Region.South, 2);
        engine.GainMediaSupport(Player.Nixon, Region.West, 1);

        SetOfChanges playerChoices = new();
        var oneMediaSupportLostInSouth = new SupportChange<Player, Region>(Player.Nixon, Region.South, -1);
        var oneMediaSupportLostInWest = new SupportChange<Player, Region>(Player.Nixon, Region.West, -1);

        playerChoices.MediaSupportChanges.Add(oneMediaSupportLostInSouth);
        playerChoices.MediaSupportChanges.Add(oneMediaSupportLostInWest);

        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(engine, player, playerChoices);

        Assert.AreEqual(1, engine.GetGameState().MediaSupportLevels[Region.South].Amount);
        Assert.AreEqual(0, engine.GetGameState().MediaSupportLevels[Region.West].Amount);
    }

    [TestMethod]
    public void Herblock_80_ValidationFailsIfMediaSupportLossInExcessOfTwo()
    {
        SetOfChanges playerChoices = new();
        var invalidMediaSupportInExcessOfTwo = new SupportChange<Player, Region>(Player.Nixon, Region.West, -3);

        playerChoices.MediaSupportChanges.Add(invalidMediaSupportInExcessOfTwo);

        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(playerChoices);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void Herblock_80_ValidationFailsIfKennedyLosesMediaSupport()
    {
        SetOfChanges playerChoices = new();
        var oneMediaSupportLostInSouth = new SupportChange<Player, Region>(Player.Nixon, Region.South, -1);
        var invalidMediaSupportLossForKennedy = new SupportChange<Player, Region>(Player.Kennedy, Region.West, -1);

        playerChoices.MediaSupportChanges.Add(oneMediaSupportLostInSouth);
        playerChoices.MediaSupportChanges.Add(invalidMediaSupportLossForKennedy);

        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(playerChoices);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void Herblock_80_ValidationFailsIfMediaSupportGained()
    {
        SetOfChanges playerChoices = new();
        var invalidMediaSupportGainForKennedy = new SupportChange<Player, Region>(Player.Kennedy, Region.West, 1);

        playerChoices.MediaSupportChanges.Add(invalidMediaSupportGainForKennedy);

        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(playerChoices);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void Herblock_80_ValidationFailsIfStateSupportGained()
    {
        SetOfChanges playerChoices = new();
        var invalidStateSupportGain = new SupportChange<Player, State>(Player.Kennedy, State.GA, 1);

        playerChoices.StateChanges.Add(invalidStateSupportGain);

        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(playerChoices);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void Herblock_80_ValidationFailsIfIssueSupportGained()
    {
        SetOfChanges playerChoices = new();
        var invalidIssueSupportGain = new SupportChange<Player, Issue>(Player.Kennedy, Issue.CivilRights, 1);

        playerChoices.IssueChanges.Add(invalidIssueSupportGain);

        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(playerChoices);
        Assert.IsFalse(result);
    }

}