using Microsoft.VisualStudio.TestTools.UnitTesting;
using PresidentialGameEngine.ClassLibrary.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PresidentialGameEngine.ClassLibrary.Tests.TestStubsFakesAndMocks;

namespace PresidentialGameEngine.ClassLibrary.Tests
{
    [TestClass]
    public class PositioningComponentTests
    {

        #region Constructor Tests
        [TestMethod]
        public void Constructor_AllEntriesCreated()
        {
            PositioningComponent<FakeSubject> sut = new();

            Assert.IsNotNull(sut.GetSubjectOrder);
        }

        [TestMethod]
        public void Constructor_EntryForNoneRemoved()
        {
            PositioningComponent<FakeSubject> sut = new();

            Assert.IsFalse(sut.GetSubjectOrder.Contains(FakeSubject.NoSubject));
        }
        #endregion

        #region SetSubjectOrder Tests
        [TestMethod]
        public void SetSubjectOrder_ChangeReflected()
        {
            PositioningComponent<FakeSubject> sut = new();

            FakeSubject[] newOrder = [FakeSubject.SubjectFive, FakeSubject.SubjectFour, FakeSubject.SubjectThree, FakeSubject.SubjectTwo, FakeSubject.SubjectOne];

            sut.SetSubjectOrder(newOrder);

            var result = sut.GetSubjectOrder;

            CollectionAssert.AreEqual(newOrder, result.ToArray());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetSubjectOrder_ProvidedCollectionOfWrongLength_Throws()
        {
            PositioningComponent<FakeSubject> sut = new();

            FakeSubject[] newOrder = [FakeSubject.SubjectFive];

            sut.SetSubjectOrder(newOrder);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetSubjectOrder_ProvidedCollectionContainsValueUnknownEntry_Throws()
        {
            PositioningComponent<FakeSubject> sut = new();

            FakeSubject[] newOrder = [FakeSubject.NoSubject, FakeSubject.SubjectFour, FakeSubject.SubjectThree, FakeSubject.SubjectTwo, FakeSubject.SubjectOne];

            sut.SetSubjectOrder(newOrder);
        }

        #endregion

        #region MoveSubjectUp Tests
        [TestMethod]
        public void MoveSubjectUp_SubjectMovesUpAsExpected()
        {
            PositioningComponent<FakeSubject> sut = new();

            FakeSubject[] initialOrder = [FakeSubject.SubjectFive, FakeSubject.SubjectFour, FakeSubject.SubjectThree, FakeSubject.SubjectTwo, FakeSubject.SubjectOne];
            sut.SetSubjectOrder(initialOrder);

            sut.MoveSubjectUp(FakeSubject.SubjectFour);
 
            Assert.AreEqual(FakeSubject.SubjectFour, sut.GetSubjectOrder[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MoveSubjectUp_ProvidedCollectionContainsValueUnknownEntry_Throws()
        {
            PositioningComponent<FakeSubject> sut = new();

            FakeSubject[] initialOrder = [FakeSubject.SubjectFive, FakeSubject.SubjectFour, FakeSubject.SubjectThree, FakeSubject.SubjectTwo, FakeSubject.SubjectOne];
            sut.SetSubjectOrder(initialOrder);

            sut.MoveSubjectUp(FakeSubject.NoSubject);

        }
        #endregion
    }
}
