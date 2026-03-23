using NineteenSixty.Data;
using NineteenSixty.Enums;
using PresidentialGameEngine.ClassLibrary.Data;
using ChangeType = NineteenSixty.Enums.ChangeType;

namespace NineteenSixty.Tests;

[TestClass]
public class SetOfChangesTests
{
    #region ContainsOnlyExactlyTheseChangeTypes Tests
    
    [TestMethod]
    [DataRow(ChangeType.IssueSupport, true)]
    [DataRow(ChangeType.StateSupport, false)]
    [DataRow(ChangeType.Endorsement, false)]
    [DataRow(ChangeType.MediaSupport, false)]
    [DataRow(ChangeType.NewIssueOrder, false)]
    public void ContainsOnlyExactlyTheseChangeTypes_IssueChanges(ChangeType changeType, bool expectedResult)
    {
        var sut = new SetOfChanges();
        var issueChange = new SupportChange<Player, Issue>(Player.Kennedy, Issue.CivilRights, 1);
        sut.IssueChanges.Add(issueChange);

        Assert.AreEqual(expectedResult, sut.ContainsOnlyExactlyTheseChangeTypes([changeType]));
    }
    
    [TestMethod]
    [DataRow(ChangeType.IssueSupport, false)]
    [DataRow(ChangeType.StateSupport, true)]
    [DataRow(ChangeType.Endorsement, false)]
    [DataRow(ChangeType.MediaSupport, false)]
    [DataRow(ChangeType.NewIssueOrder, false)]
    public void ContainsOnlyExactlyTheseChangeTypes_StateChanges(ChangeType changeType, bool expectedResult)
    {
        var sut = new SetOfChanges();
        var stateChange = new SupportChange<Player, State>(Player.Kennedy, State.AR, 1);
        sut.StateChanges.Add(stateChange);

        Assert.AreEqual(expectedResult, sut.ContainsOnlyExactlyTheseChangeTypes([changeType]));
    }
    
    [TestMethod]
    [DataRow(ChangeType.IssueSupport, false)]
    [DataRow(ChangeType.StateSupport, false)]
    [DataRow(ChangeType.Endorsement, true)]
    [DataRow(ChangeType.MediaSupport, false)]
    [DataRow(ChangeType.NewIssueOrder, false)]
    public void ContainsOnlyExactlyTheseChangeTypes_EndorsementChanges(ChangeType changeType, bool expectedResult)
    {
        var sut = new SetOfChanges();
        var endorsementChange = new SupportChange<Player, Region>(Player.Kennedy, Region.South, 1);
        sut.EndorsementChanges.Add(endorsementChange);

        Assert.AreEqual(expectedResult, sut.ContainsOnlyExactlyTheseChangeTypes([changeType]));
    }
    
    [TestMethod]
    [DataRow(ChangeType.IssueSupport, false)]
    [DataRow(ChangeType.StateSupport, false)]
    [DataRow(ChangeType.Endorsement, false)]
    [DataRow(ChangeType.MediaSupport, true)]
    [DataRow(ChangeType.NewIssueOrder, false)]
    public void ContainsOnlyExactlyTheseChangeTypes_MediaSupportChanges(ChangeType changeType, bool expectedResult)
    {
        var sut = new SetOfChanges();
        var mediaSupport = new SupportChange<Player, Region>(Player.Kennedy, Region.West, 1);
        sut.MediaSupportChanges.Add(mediaSupport);

        Assert.AreEqual(expectedResult, sut.ContainsOnlyExactlyTheseChangeTypes([changeType]));
    }
    
    [TestMethod]
    [DataRow(ChangeType.IssueSupport, false)]
    [DataRow(ChangeType.StateSupport, false)]
    [DataRow(ChangeType.Endorsement, false)]
    [DataRow(ChangeType.MediaSupport, false)]
    [DataRow(ChangeType.NewIssueOrder, true)]
    public void ContainsOnlyExactlyTheseChangeTypes_NewIssueOrderChanges(ChangeType changeType, bool expectedResult)
    {
        var sut = new SetOfChanges();
        var issueOrderChange = new List<Issue>() { Issue.Economy, Issue.CivilRights, Issue.Defense };
        sut.NewIssuesOrder = issueOrderChange; 

        Assert.AreEqual(expectedResult, sut.ContainsOnlyExactlyTheseChangeTypes([changeType]));
    }
    
    [TestMethod]
    public void ContainsOnlyExactlyTheseChangeTypes_MultipleChecks()
    {
        var sut = new SetOfChanges();
        var issueChange = new SupportChange<Player, Issue>(Player.Kennedy, Issue.CivilRights, 1);
        var stateChange = new SupportChange<Player, State>(Player.Kennedy, State.AR, 1);
        sut.IssueChanges.Add(issueChange);
        sut.StateChanges.Add(stateChange);

        Assert.AreEqual(true, sut.ContainsOnlyExactlyTheseChangeTypes([ChangeType.IssueSupport, ChangeType.StateSupport]));
    }
    
    #endregion
    
    #region ContainsNoLosses Tests
    
    [TestMethod]
    [DataRow(1, true)]
    [DataRow(0, true)]
    [DataRow(-1, false)]
    public void ContainsNoLosses_IssueChanges(int change, bool expectedResult)
    {
        var sut = new SetOfChanges();
        var issueChange = new SupportChange<Player, Issue>(Player.Kennedy, Issue.CivilRights, change);
        sut.IssueChanges.Add(issueChange);

        Assert.AreEqual(expectedResult, sut.ContainsNoLosses());
    }
    
    [TestMethod]
    [DataRow(1, true)]
    [DataRow(0, true)]
    [DataRow(-1, false)]
    public void ContainsNoLosses_StateChanges(int change, bool expectedResult)
    {
        var sut = new SetOfChanges();
        var stateChange = new SupportChange<Player, State>(Player.Kennedy, State.AR, change);
        sut.StateChanges.Add(stateChange);

        Assert.AreEqual(expectedResult, sut.ContainsNoLosses());
    }
    
    [TestMethod]
    [DataRow(1, true)]
    [DataRow(0, true)]
    [DataRow(-1, false)]
    public void ContainsNoLosses_EndorsementChanges(int change, bool expectedResult)
    {
        var sut = new SetOfChanges();
        var endorsementChange = new SupportChange<Player, Region>(Player.Kennedy, Region.South, change);
        sut.EndorsementChanges.Add(endorsementChange);

        Assert.AreEqual(expectedResult, sut.ContainsNoLosses());
    }
    
    [TestMethod]
    [DataRow(1, true)]
    [DataRow(0, true)]
    [DataRow(-1, false)]
    public void ContainsNoLosses_MediaSupportChanges(int change, bool expectedResult)
    {
        var sut = new SetOfChanges();
        var mediaSupport = new SupportChange<Player, Region>(Player.Kennedy, Region.West, change);
        sut.MediaSupportChanges.Add(mediaSupport);

        Assert.AreEqual(expectedResult, sut.ContainsNoLosses());
    }
    
    #endregion
    
    #region ContainsNoGains Tests
    
    [TestMethod]
    [DataRow(1, false)]
    [DataRow(0, true)]
    [DataRow(-1, true)]
    public void ContainsNoGains_IssueChanges(int change, bool expectedResult)
    {
        var sut = new SetOfChanges();
        var issueChange = new SupportChange<Player, Issue>(Player.Kennedy, Issue.CivilRights, change);
        sut.IssueChanges.Add(issueChange);

        Assert.AreEqual(expectedResult, sut.ContainsNoGains());
    }
    
    [TestMethod]
    [DataRow(1, false)]
    [DataRow(0, true)]
    [DataRow(-1, true)]
    public void ContainsNoGains_StateChanges(int change, bool expectedResult)
    {
        var sut = new SetOfChanges();
        var stateChange = new SupportChange<Player, State>(Player.Kennedy, State.AR, change);
        sut.StateChanges.Add(stateChange);

        Assert.AreEqual(expectedResult, sut.ContainsNoGains());
    }
    
    [TestMethod]
    [DataRow(1, false)]
    [DataRow(0, true)]
    [DataRow(-1, true)]
    public void ContainsNoGains_EndorsementChanges(int change, bool expectedResult)
    {
        var sut = new SetOfChanges();
        var endorsementChange = new SupportChange<Player, Region>(Player.Kennedy, Region.South, change);
        sut.EndorsementChanges.Add(endorsementChange);

        Assert.AreEqual(expectedResult, sut.ContainsNoGains());
    }
    
    [TestMethod]
    [DataRow(1, false)]
    [DataRow(0, true)]
    [DataRow(-1, true)]
    public void ContainsNoGains_MediaSupportChanges(int change, bool expectedResult)
    {
        var sut = new SetOfChanges();
        var mediaSupport = new SupportChange<Player, Region>(Player.Kennedy, Region.West, change);
        sut.MediaSupportChanges.Add(mediaSupport);

        Assert.AreEqual(expectedResult, sut.ContainsNoGains());
    }
    
    #endregion
    
}