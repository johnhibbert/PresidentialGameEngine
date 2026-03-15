using PresidentialGameEngine.ClassLibrary.Data;
using static PresidentialGameEngine.ClassLibrary.Tests.TestStubsFakesAndMocks;

namespace PresidentialGameEngine.ClassLibrary.Tests
{
    [TestClass]
    public class PlayerChosenChangesTests
    {
        [TestMethod]
        public void ContainsExactlyOneTypeOfChange_MultipleOfOneTypeReturnTrue()
        {
            var sut = new PlayerChosenChanges<FakePlayer, FakeIssue, FakeState, FakeRegion>();

            SupportChange<FakePlayer, FakeIssue> issueChangeOne = new(FakePlayer.PlayerOne, FakeIssue.KetchupOnHotDogs, 1);
            SupportChange<FakePlayer, FakeIssue> issueChangeTwo = new(FakePlayer.PlayerTwo, FakeIssue.PepsiOrCoke, 2);
            SupportChange<FakePlayer, FakeIssue> issueChangeThree = new(FakePlayer.PlayerThree, FakeIssue.KetchupOnHotDogs, 1);

            sut.IssueChanges.Add(issueChangeOne);
            sut.IssueChanges.Add(issueChangeTwo);
            sut.IssueChanges.Add(issueChangeThree);

            var result = sut.ContainsExactlyOneTypeOfChange();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ContainsExactlyOneTypeOfChange_MultipleTypesReturnFalse()
        {
            var sut = new PlayerChosenChanges<FakePlayer, FakeIssue, FakeState, FakeRegion>();

            SupportChange<FakePlayer, FakeIssue> issueChange = new(FakePlayer.PlayerOne, FakeIssue.KetchupOnHotDogs, 1);
            SupportChange<FakePlayer, FakeState> stateChange = new(FakePlayer.PlayerOne, FakeState.Being, 2);
            SupportChange<FakePlayer, FakeRegion> mediaChange = new(FakePlayer.PlayerOne, FakeRegion.North, 1);
            SupportChange<FakePlayer, FakeRegion> endorsementChange = new(FakePlayer.PlayerOne, FakeRegion.SouthEast, 1);

            sut.IssueChanges.Add(issueChange);
            sut.StateChanges.Add(stateChange);
            sut.MediaSupportChanges.Add(mediaChange);
            sut.EndorsementChanges.Add(endorsementChange);

            var result = sut.ContainsExactlyOneTypeOfChange();

            Assert.IsFalse(result);
        }


        [TestMethod]
        public void TotalStateChanges_ReturnsZeroIfUnassigned()
        {
            var sut = new PlayerChosenChanges<FakePlayer, FakeIssue, FakeState, FakeRegion>();

            SupportChange<FakePlayer, FakeIssue> issueChange = new(FakePlayer.PlayerOne, FakeIssue.KetchupOnHotDogs, 1);
            SupportChange<FakePlayer, FakeRegion> mediaChange = new(FakePlayer.PlayerOne, FakeRegion.North, 1);
            SupportChange<FakePlayer, FakeRegion> endorsementChange = new(FakePlayer.PlayerOne, FakeRegion.SouthEast, 1);

            sut.IssueChanges.Add(issueChange);
            sut.MediaSupportChanges.Add(mediaChange);
            sut.EndorsementChanges.Add(endorsementChange);

            Assert.AreEqual(0, sut.TotalStateChanges);
        }
        [TestMethod]
        public void HighestStateChange_ReturnsZeroIfUnassigned()
        {
            var sut = new PlayerChosenChanges<FakePlayer, FakeIssue, FakeState, FakeRegion>();

            SupportChange<FakePlayer, FakeIssue> issueChange = new(FakePlayer.PlayerOne, FakeIssue.KetchupOnHotDogs, 1);
            SupportChange<FakePlayer, FakeRegion> mediaChange = new(FakePlayer.PlayerOne, FakeRegion.North, 1);
            SupportChange<FakePlayer, FakeRegion> endorsementChange = new(FakePlayer.PlayerOne, FakeRegion.SouthEast, 1);

            sut.IssueChanges.Add(issueChange);
            sut.MediaSupportChanges.Add(mediaChange);
            sut.EndorsementChanges.Add(endorsementChange);

            Assert.AreEqual(0, sut.HighestStateChange);
        }


        [TestMethod]
        public void TotalIssueChanges_ReturnsZeroIfUnassigned()
        {
            var sut = new PlayerChosenChanges<FakePlayer, FakeIssue, FakeState, FakeRegion>();

            SupportChange<FakePlayer, FakeState> stateChange = new(FakePlayer.PlayerOne, FakeState.Being, 2);
            SupportChange<FakePlayer, FakeRegion> mediaChange = new(FakePlayer.PlayerOne, FakeRegion.North, 1);
            SupportChange<FakePlayer, FakeRegion> endorsementChange = new(FakePlayer.PlayerOne, FakeRegion.SouthEast, 1);

            sut.StateChanges.Add(stateChange);
            sut.MediaSupportChanges.Add(mediaChange);
            sut.EndorsementChanges.Add(endorsementChange);

            Assert.AreEqual(0, sut.TotalIssueChanges);
        }
        [TestMethod]
        public void HighestIssueChange_ReturnsZeroIfUnassigned()
        {
            var sut = new PlayerChosenChanges<FakePlayer, FakeIssue, FakeState, FakeRegion>();

            SupportChange<FakePlayer, FakeState> stateChange = new(FakePlayer.PlayerOne, FakeState.Being, 2);
            SupportChange<FakePlayer, FakeRegion> mediaChange = new(FakePlayer.PlayerOne, FakeRegion.North, 1);
            SupportChange<FakePlayer, FakeRegion> endorsementChange = new(FakePlayer.PlayerOne, FakeRegion.SouthEast, 1);

            sut.StateChanges.Add(stateChange);
            sut.MediaSupportChanges.Add(mediaChange);
            sut.EndorsementChanges.Add(endorsementChange);

            Assert.AreEqual(0, sut.HighestIssueChange);
        }


        [TestMethod]
        public void TotalMediaChanges_ReturnsZeroIfUnassigned()
        {
            var sut = new PlayerChosenChanges<FakePlayer, FakeIssue, FakeState, FakeRegion>();

            SupportChange<FakePlayer, FakeIssue> issueChange = new(FakePlayer.PlayerOne, FakeIssue.KetchupOnHotDogs, 1);
            SupportChange<FakePlayer, FakeState> stateChange = new(FakePlayer.PlayerOne, FakeState.Being, 2);
            SupportChange<FakePlayer, FakeRegion> endorsementChange = new(FakePlayer.PlayerOne, FakeRegion.SouthEast, 1);

            sut.IssueChanges.Add(issueChange);
            sut.StateChanges.Add(stateChange);
            sut.EndorsementChanges.Add(endorsementChange);

            Assert.AreEqual(0, sut.TotalMediaChanges);
        }
        [TestMethod]
        public void HighestMediaChange_ReturnsZeroIfUnassigned()
        {
            var sut = new PlayerChosenChanges<FakePlayer, FakeIssue, FakeState, FakeRegion>();

            SupportChange<FakePlayer, FakeIssue> issueChange = new(FakePlayer.PlayerOne, FakeIssue.KetchupOnHotDogs, 1);
            SupportChange<FakePlayer, FakeState> stateChange = new(FakePlayer.PlayerOne, FakeState.Being, 2);
            SupportChange<FakePlayer, FakeRegion> endorsementChange = new(FakePlayer.PlayerOne, FakeRegion.SouthEast, 1);

            sut.IssueChanges.Add(issueChange);
            sut.StateChanges.Add(stateChange);
            sut.EndorsementChanges.Add(endorsementChange);

            Assert.AreEqual(0, sut.HighestMediaChange);
        }


        [TestMethod]
        public void TotalEndorsementChanges_ReturnsZeroIfUnassigned()
        {
            var sut = new PlayerChosenChanges<FakePlayer, FakeIssue, FakeState, FakeRegion>();

            SupportChange<FakePlayer, FakeIssue> issueChange = new(FakePlayer.PlayerOne, FakeIssue.KetchupOnHotDogs, 1);
            SupportChange<FakePlayer, FakeState> stateChange = new(FakePlayer.PlayerOne, FakeState.Being, 2);
            SupportChange<FakePlayer, FakeRegion> mediaChange = new(FakePlayer.PlayerOne, FakeRegion.North, 1);

            sut.IssueChanges.Add(issueChange);
            sut.StateChanges.Add(stateChange);
            sut.MediaSupportChanges.Add(mediaChange);

            Assert.AreEqual(0, sut.TotalEndorsementChanges);
        }
        [TestMethod]
        public void HighestEndorsementChange_ReturnsZeroIfUnassigned()
        {
            var sut = new PlayerChosenChanges<FakePlayer, FakeIssue, FakeState, FakeRegion>();

            SupportChange<FakePlayer, FakeIssue> issueChange = new(FakePlayer.PlayerOne, FakeIssue.KetchupOnHotDogs, 1);
            SupportChange<FakePlayer, FakeState> stateChange = new(FakePlayer.PlayerOne, FakeState.Being, 2);
            SupportChange<FakePlayer, FakeRegion> mediaChange = new(FakePlayer.PlayerOne, FakeRegion.North, 1);
           

            sut.IssueChanges.Add(issueChange);
            sut.StateChanges.Add(stateChange);
            sut.MediaSupportChanges.Add(mediaChange);

            Assert.AreEqual(0, sut.HighestEndorsementChange);
        }

        //We could theoretically go over every combination with a data-driven unit test like DynamicData
        //https://learn.microsoft.com/en-us/visualstudio/test/how-to-create-a-data-driven-unit-test?view=visualstudio
        //But I think that is actually overcomplicating it.
        
        [TestMethod]
        [DataRow(ChangeType.IssueSupport, true)]
        [DataRow(ChangeType.StateSupport, false)]
        [DataRow(ChangeType.MediaSupport, false)]
        [DataRow(ChangeType.Endorsement, false)]
        [DataRow(ChangeType.NewIssueOrder, false)]
        public void ContainsOnlyExactlyTheseChangeTypes_OnlyExpectingIssueSupport(ChangeType changeType, bool expectedResult)
        {
            var sut = new PlayerChosenChanges<FakePlayer, FakeIssue, FakeState, FakeRegion>();
            SupportChange<FakePlayer, FakeIssue> issueChange = new(FakePlayer.PlayerOne, FakeIssue.KetchupOnHotDogs, 1);
            sut.IssueChanges.Add(issueChange);

            var result = sut.ContainsOnlyExactlyTheseChangeTypes([changeType]);
            Assert.AreEqual(expectedResult, result);
        }
        
        [TestMethod]
        [DataRow(ChangeType.IssueSupport, false)]
        [DataRow(ChangeType.StateSupport, true)]
        [DataRow(ChangeType.MediaSupport, false)]
        [DataRow(ChangeType.Endorsement, false)]
        [DataRow(ChangeType.NewIssueOrder, false)]
        public void ContainsOnlyExactlyTheseChangeTypes_OnlyExpectingStateSupport(ChangeType changeType, bool expectedResult)
        {
            var sut = new PlayerChosenChanges<FakePlayer, FakeIssue, FakeState, FakeRegion>();
            SupportChange<FakePlayer, FakeState> stateChange = new(FakePlayer.PlayerOne, FakeState.Denial, 1);
            sut.StateChanges.Add(stateChange);

            var result = sut.ContainsOnlyExactlyTheseChangeTypes([changeType]);
            Assert.AreEqual(expectedResult, result);
        }
        
        [TestMethod]
        [DataRow(ChangeType.IssueSupport, false)]
        [DataRow(ChangeType.StateSupport, false)]
        [DataRow(ChangeType.MediaSupport, true)]
        [DataRow(ChangeType.Endorsement, false)]
        [DataRow(ChangeType.NewIssueOrder, false)]
        public void ContainsOnlyExactlyTheseChangeTypes_OnlyExpectingMediaSupport(ChangeType changeType, bool expectedResult)
        {
            var sut = new PlayerChosenChanges<FakePlayer, FakeIssue, FakeState, FakeRegion>();
            SupportChange<FakePlayer, FakeRegion> mediaChanges = new(FakePlayer.PlayerOne, FakeRegion.North, 1);
            sut.MediaSupportChanges.Add(mediaChanges);

            var result = sut.ContainsOnlyExactlyTheseChangeTypes([changeType]);
            Assert.AreEqual(expectedResult, result);
        }
        
        [TestMethod]
        [DataRow(ChangeType.IssueSupport, false)]
        [DataRow(ChangeType.StateSupport, false)]
        [DataRow(ChangeType.MediaSupport, false)]
        [DataRow(ChangeType.Endorsement, true)]
        [DataRow(ChangeType.NewIssueOrder, false)]
        public void ContainsOnlyExactlyTheseChangeTypes_OnlyExpectingEndorsement(ChangeType changeType, bool expectedResult)
        {
            var sut = new PlayerChosenChanges<FakePlayer, FakeIssue, FakeState, FakeRegion>();
            SupportChange<FakePlayer, FakeRegion> endorsementChange = new(FakePlayer.PlayerOne, FakeRegion.North, 1);
            sut.EndorsementChanges.Add(endorsementChange);

            var result = sut.ContainsOnlyExactlyTheseChangeTypes([changeType]);
            Assert.AreEqual(expectedResult, result);
        }
        
        [TestMethod]
        [DataRow(ChangeType.IssueSupport, false)]
        [DataRow(ChangeType.StateSupport, false)]
        [DataRow(ChangeType.MediaSupport, false)]
        [DataRow(ChangeType.Endorsement, false)]
        [DataRow(ChangeType.NewIssueOrder, true)]
        public void ContainsOnlyExactlyTheseChangeTypes_OnlyExpectingIssueOrderChange(ChangeType changeType, bool expectedResult)
        {
            var sut = new PlayerChosenChanges<FakePlayer, FakeIssue, FakeState, FakeRegion>();
            List<FakeIssue> newIssueOrder = [FakeIssue.PepsiOrCoke, FakeIssue.KetchupOnHotDogs];
            sut.NewIssuesOrder = newIssueOrder;

            var result = sut.ContainsOnlyExactlyTheseChangeTypes([changeType]);
            Assert.AreEqual(expectedResult, result);
        }
        
    }
}
