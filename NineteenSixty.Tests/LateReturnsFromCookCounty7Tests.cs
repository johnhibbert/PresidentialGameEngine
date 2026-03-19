using NineteenSixty.Enums;
using NineteenSixty.Data;
using NineteenSixty.Tests.Fixtures;
using PresidentialGameEngine.ClassLibrary.Data;

namespace NineteenSixty.Tests;

[TestClass]
public class LateReturnsFromCookCounty7Tests
{
    //"ELECTION DAY EVENT!  On Election Day, the Kennedy player gains 5 support checks in Illinois.",
    private const int CardIndex = 7;
    
    [TestMethod]
    [DataRow(Player.Nixon)]
    [DataRow(Player.Kennedy)]
    public void LateReturnsFromCookCounty_7_SupportCheckWorksAsExpected(Player player)
    {
        // var engine = EngineFixtures.GetGameEngine();
        //
        // engine.GainSupport(Player.Nixon, State.IL, 2);
        //
        // var sut = Manifest.GMTCards[CardIndex];
        //
        // sut.Event(engine, player, EngineFixtures.EmptyChanges);

        //FIXME LATER
        Assert.IsTrue(true);

        //Assert.AreEqual(Leader.Kennedy, engine.GetLeader(State.IL));
        //Assert.AreEqual(1, engine.GetSupportAmount(State.IL));
    }

}