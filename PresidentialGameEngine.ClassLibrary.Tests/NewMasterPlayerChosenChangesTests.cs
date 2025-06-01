using PresidentialGameEngine.ClassLibrary.Data;
using static PresidentialGameEngine.ClassLibrary.Tests.TestStubsFakesAndMocks;

namespace PresidentialGameEngine.ClassLibrary.Tests
{
    [TestClass]
    public class NewMasterPlayerChosenChangesTests
    {
        [TestMethod]
        public void ContainsExactlyOneTypeOfChange_MultipleOfOneTypeReturnTrue()
        {
            var sut = new NewMasterPlayerChosenChanges<FakePlayer, FakeIssue, FakeState, FakeRegion>();

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
            var sut = new NewMasterPlayerChosenChanges<FakePlayer, FakeIssue, FakeState, FakeRegion>();

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

    }
}
