// ReSharper disable InconsistentNaming
using NineteenSixty.Data;
using NineteenSixty.Enums;
using NineteenSixty.Tests.Fixtures;
using PresidentialGameEngine.ClassLibrary.Data;

namespace NineteenSixty.Tests.Cards;

[TestClass]
public class GreaterHoustonMinisterialAssociation_1_Tests
{
    //"PREVENTION EVENT!  Immediately move the Kennedy candidate token to Texas, without paying the normal travel costs.  The Kennedy player gains 1 momentum marker and may add a total of 5 state support anywhere, no more than 1 per state.  This event prevents the Baptist Ministers, Norman Vincent Peale, and Puerto Rican Bishops events."
    private const int CardIndex = 1;
    
    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void GreaterHoustonMinisterialAssociation_1_KennedyMovedToTexas(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(engine, player, EngineFixtures.EmptyChanges);

        Assert.AreEqual(State.TX, engine.GetPlayerState(Player.Kennedy));
    }
    
    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void HurricaneDonna_52_KennedyGainsOneMomentum(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();
        
        engine.GainMomentum(Player.Kennedy, 1);

        var sut = Manifest.GMTCards[CardIndex];
        sut.Event(engine, player, EngineFixtures.EmptyChanges);

        Assert.AreEqual(2, engine.GetPlayerMomentum(Player.Kennedy));
    }
    
    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void GreaterHoustonMinisterialAssociation_1_SupportAddedToStates(Player player)
    {
        var engine = EngineFixtures.GetGameEngine();

        SetOfChanges playerChoices = new();
        var oneSupportInWyoming = new SupportChange<Player, State>(Player.Nixon, State.WY, 1);
        var oneSupportInWashington = new SupportChange<Player, State>(Player.Nixon, State.WA, 1);
        var oneSupportInNorthDakota = new SupportChange<Player, State>(Player.Nixon, State.ND, 1);
        var oneSupportInDelaware = new SupportChange<Player, State>(Player.Nixon, State.DE, 1);
        var oneSupportInKentucky = new SupportChange<Player, State>(Player.Nixon, State.KY, 1);
        var oneSupportInRhodeIsland = new SupportChange<Player, State>(Player.Nixon, State.RI, 1);
        var oneSupportInAlaska = new SupportChange<Player, State>(Player.Nixon, State.AK, 1);

        playerChoices.StateChanges.Add(oneSupportInWyoming);
        playerChoices.StateChanges.Add(oneSupportInWashington);
        playerChoices.StateChanges.Add(oneSupportInNorthDakota);
        playerChoices.StateChanges.Add(oneSupportInDelaware);
        playerChoices.StateChanges.Add(oneSupportInKentucky);
        playerChoices.StateChanges.Add(oneSupportInRhodeIsland);
        playerChoices.StateChanges.Add(oneSupportInAlaska);

        var sut = Manifest.GMTCards[CardIndex];

        sut.Event(engine, player, playerChoices);

        Assert.AreEqual(1, engine.GetSupportAmount(State.WY));
        Assert.AreEqual(1, engine.GetSupportAmount(State.WA));
        Assert.AreEqual(1, engine.GetSupportAmount(State.ND));
        Assert.AreEqual(1, engine.GetSupportAmount(State.DE));
        Assert.AreEqual(1, engine.GetSupportAmount(State.KY));
        Assert.AreEqual(1, engine.GetSupportAmount(State.RI));
        Assert.AreEqual(1, engine.GetSupportAmount(State.AK));
    }

    [TestMethod]
    public void GreaterHoustonMinisterialAssociation_1_FailsValidationIfNixonGains()
    {
        SetOfChanges playerChoices = new();
        var oneSupportInWyoming = new SupportChange<Player, State>(Player.Kennedy, State.WY, 1);
        var oneSupportInWashington = new SupportChange<Player, State>(Player.Kennedy, State.WA, 1);
        var oneSupportInNorthDakota = new SupportChange<Player, State>(Player.Kennedy, State.ND, 1);
        var oneSupportInDelaware = new SupportChange<Player, State>(Player.Kennedy, State.DE, 1);
        var oneSupportInKentucky = new SupportChange<Player, State>(Player.Kennedy, State.KY, 1);
        var oneSupportInRhodeIsland = new SupportChange<Player, State>(Player.Kennedy, State.RI, 1);
        var invalidSupportGainForNixon = new SupportChange<Player, State>(Player.Nixon, State.MA, 1);

        playerChoices.StateChanges.Add(oneSupportInWyoming);
        playerChoices.StateChanges.Add(oneSupportInWashington);
        playerChoices.StateChanges.Add(oneSupportInNorthDakota);
        playerChoices.StateChanges.Add(oneSupportInDelaware);
        playerChoices.StateChanges.Add(oneSupportInKentucky);
        playerChoices.StateChanges.Add(oneSupportInRhodeIsland);
        playerChoices.StateChanges.Add(invalidSupportGainForNixon);

        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(playerChoices);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void GreaterHoustonMinisterialAssociation_1_FailsValidationIfIssueGains()
    {
        SetOfChanges playerChoices = new();
        var oneSupportInWyoming = new SupportChange<Player, State>(Player.Kennedy, State.WY, 1);
        var oneSupportInWashington = new SupportChange<Player, State>(Player.Kennedy, State.WA, 1);
        var oneSupportInNorthDakota = new SupportChange<Player, State>(Player.Kennedy, State.ND, 1);
        var oneSupportInDelaware = new SupportChange<Player, State>(Player.Kennedy, State.DE, 1);
        var oneSupportInKentucky = new SupportChange<Player, State>(Player.Kennedy, State.KY, 1);
        var oneSupportInRhodeIsland = new SupportChange<Player, State>(Player.Kennedy, State.RI, 1);
        var invalidIssueSupport = new SupportChange<Player, Issue>(Player.Kennedy, Issue.CivilRights, 1);

        playerChoices.StateChanges.Add(oneSupportInWyoming);
        playerChoices.StateChanges.Add(oneSupportInWashington);
        playerChoices.StateChanges.Add(oneSupportInNorthDakota);
        playerChoices.StateChanges.Add(oneSupportInDelaware);
        playerChoices.StateChanges.Add(oneSupportInKentucky);
        playerChoices.StateChanges.Add(oneSupportInRhodeIsland);
        playerChoices.IssueChanges.Add(invalidIssueSupport);

        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(playerChoices);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void GreaterHoustonMinisterialAssociation_1_FailsValidationIfGreaterThanOne()
    {
        SetOfChanges playerChoices = new();
        var oneSupportInWyoming = new SupportChange<Player, State>(Player.Nixon, State.WY, 1);
        var oneSupportInWashington = new SupportChange<Player, State>(Player.Nixon, State.WA, 1);
        var oneSupportInDelaware = new SupportChange<Player, State>(Player.Nixon, State.DE, 1);
        var oneSupportInKentucky = new SupportChange<Player, State>(Player.Nixon, State.KY, 1);
        var oneSupportInRhodeIsland = new SupportChange<Player, State>(Player.Nixon, State.RI, 1);
        var invalidStateSupportGreaterThanOne = new SupportChange<Player, State>(Player.Nixon, State.OK, 2);

        playerChoices.StateChanges.Add(oneSupportInWyoming);
        playerChoices.StateChanges.Add(oneSupportInWashington);
        playerChoices.StateChanges.Add(oneSupportInDelaware);
        playerChoices.StateChanges.Add(oneSupportInKentucky);
        playerChoices.StateChanges.Add(oneSupportInRhodeIsland);
        playerChoices.StateChanges.Add(invalidStateSupportGreaterThanOne);

        var sut = Manifest.GMTCards[CardIndex];
        var result = sut.AreChangesValid(playerChoices);
        Assert.IsFalse(result);
    }
    
    
}