// ReSharper disable InconsistentNaming
using NineteenSixty.Data;
using NineteenSixty.Enums;
using NineteenSixty.Tests.Fixtures;
using PresidentialGameEngine.ClassLibrary.Data;

namespace NineteenSixty.Tests.Cards;

[TestClass]
public class HenryLuce_36_Tests
{
    //"The Kennedy player may place 1 endorsement marker in any region."
    private const int CardIndex = 36;
    
    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void HenryLuce_36_KennedyEndorsementGained(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        SetOfChanges playerChoices = new();
        var oneRegionalSupportInWest = new SupportChange<Player, Region>(Player.Kennedy, Region.West, 1);

        playerChoices.EndorsementChanges.Add(oneRegionalSupportInWest);

        var plan = new ActionPlan{Engine = engine, Changes = playerChoices};
        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(plan, player);

        Assert.AreEqual(Leader.Kennedy, engine.GetEndorsementLeader(Region.West));

    }

    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void HenryLuce_36_FailsValidationMoreThanOneEndorsementGained(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        SetOfChanges playerChoices = new();
        var oneRegionalSupportInWest = new SupportChange<Player, Region>(Player.Kennedy, Region.West, 1);
        var oneRegionalSupportTooMany = new SupportChange<Player, Region>(Player.Kennedy, Region.East, 1);

        playerChoices.EndorsementChanges.Add(oneRegionalSupportInWest);
        playerChoices.EndorsementChanges.Add(oneRegionalSupportTooMany);

        var plan = new ActionPlan{Engine = null, Changes = playerChoices};
        var sut = Manifest.GMTCards[CardIndex]; 
        var result = sut.AreChangesValid(plan);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void HenryLuce_36_FailsValidationIfNixonGainsEndorsement()
    {
        SetOfChanges playerChoices = new();
        var nixonEndorsement = new SupportChange<Player, Region>(Player.Nixon, Region.West, 1);
        playerChoices.EndorsementChanges.Add(nixonEndorsement);
        
        var plan = new ActionPlan{Engine = null, Changes = playerChoices};
        var sut = Manifest.GMTCards[CardIndex]; 
        var result = sut.AreChangesValid(plan);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void HenryLuce_36_FailsValidationIfIssueSupportGained()
    {
        SetOfChanges playerChoices = new();
        var oneRegionalSupportInWest = new SupportChange<Player, Region>(Player.Kennedy, Region.West, 1);
        var anInvalidIssueSupportGain = new SupportChange<Player, Issue>(Player.Nixon, Issue.CivilRights, 1);
        
        playerChoices.EndorsementChanges.Add(oneRegionalSupportInWest);
        playerChoices.IssueChanges.Add(anInvalidIssueSupportGain);
        
        var plan = new ActionPlan{Engine = null, Changes = playerChoices};
        var sut = Manifest.GMTCards[CardIndex]; 
        var result = sut.AreChangesValid(plan);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void HenryLuce_36_FailsValidationIfStateSupportGained()
    {
        SetOfChanges playerChoices = new();
        var oneRegionalSupportInWest = new SupportChange<Player, Region>(Player.Kennedy, Region.West, 1);
        var anInvalidSateSupportGain = new SupportChange<Player, State>(Player.Nixon, State.MA, 1);
        
        playerChoices.EndorsementChanges.Add(oneRegionalSupportInWest);
        playerChoices.StateChanges.Add(anInvalidSateSupportGain);
        
        var plan = new ActionPlan{Engine = null, Changes = playerChoices};
        var sut = Manifest.GMTCards[CardIndex]; 
        var result = sut.AreChangesValid(plan);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void HenryLuce_36_FailsValidationIfAnyNegativeValues()
    {
        SetOfChanges playerChoices = new();
        var invalidEndorsementLoss = new SupportChange<Player, Region>(Player.Kennedy, Region.East, -1);
        playerChoices.EndorsementChanges.Add(invalidEndorsementLoss);

        var plan = new ActionPlan{Engine = null, Changes = playerChoices};
        var sut = Manifest.GMTCards[CardIndex]; 
        var result = sut.AreChangesValid(plan);
        Assert.IsFalse(result);
    }
    
}