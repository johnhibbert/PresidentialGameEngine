using PresidentialGameEngine.ClassLibrary.Enums;
using PresidentialGameEngine.ClassLibrary.Manifests;

namespace PresidentialGameEngine.ClassLibrary.Tests
{
    [TestClass]
    public class NineteenSixtySimpleCardValidation
    {
        #region Card Value Confirmations

        [TestMethod]
        [DataRow(1, 4, 0, Issue.Defense, Affiliation.Kennedy, State.NY)]
        [DataRow(2, 4, 0, Issue.Economy, Affiliation.Kennedy, State.TX)]
        [DataRow(3, 3, 1, Issue.CivilRights, Affiliation.Both, State.MO)]
        [DataRow(4, 2, 2, Issue.CivilRights, Affiliation.Nixon, State.AZ)]
        [DataRow(5, 2, 2, Issue.Defense, Affiliation.Both, State.OR)]
        [DataRow(6, 3, 1, Issue.Defense, Affiliation.Kennedy, State.LA)]
        [DataRow(7, 2, 2, Issue.Economy, Affiliation.Kennedy, State.SC)]
        [DataRow(8, 2, 2, Issue.Economy, Affiliation.Both, State.NH)]
        [DataRow(9, 3, 1, Issue.CivilRights, Affiliation.Nixon, State.MA)]
        [DataRow(10, 3, 1, Issue.CivilRights, Affiliation.Both, State.VA)]
        [DataRow(11, 3, 1, Issue.CivilRights, Affiliation.Both, State.MA)]
        [DataRow(12, 3, 1, Issue.Defense, Affiliation.Nixon, State.KY)]
        [DataRow(13, 3, 1, Issue.Economy, Affiliation.Both, State.FL)]
        [DataRow(14, 4, 0, Issue.None, Affiliation.Both, State.None)]
        [DataRow(15, 4, 0, Issue.None, Affiliation.Both, State.None)]
        [DataRow(16, 4, 0, Issue.None, Affiliation.Both, State.None)]
        [DataRow(17, 4, 0, Issue.None, Affiliation.Both, State.None)]
        [DataRow(18, 4, 0, Issue.CivilRights, Affiliation.Nixon, State.PA)]
        [DataRow(19, 4, 0, Issue.Defense, Affiliation.Both, State.NY)]
        [DataRow(20, 2, 2, Issue.Defense, Affiliation.Kennedy, State.HI)]
        [DataRow(21, 2, 2, Issue.Defense, Affiliation.Both, State.AK)]
        [DataRow(22, 4, 0, Issue.Economy, Affiliation.Both, State.TX)]
        [DataRow(23, 4, 0, Issue.CivilRights, Affiliation.Both, State.CA)]
        [DataRow(24, 3, 1, Issue.Economy, Affiliation.Kennedy, State.MI)]
        [DataRow(25, 2, 2, Issue.CivilRights, Affiliation.Nixon, State.ND)]
        [DataRow(26, 2, 2, Issue.CivilRights, Affiliation.Kennedy, State.MD)]
        [DataRow(27, 3, 1, Issue.Defense, Affiliation.Nixon, State.KY)]
        [DataRow(28, 2, 2, Issue.CivilRights, Affiliation.Nixon, State.ME)]
        [DataRow(29, 3, 1, Issue.Economy, Affiliation.Nixon, State.WI)]
        [DataRow(30, 2, 2, Issue.Economy, Affiliation.Nixon, State.MO)]
        [DataRow(31, 2, 2, Issue.CivilRights, Affiliation.Kennedy, State.DE)]
        [DataRow(32, 2, 2, Issue.CivilRights, Affiliation.Both, State.WY)]
        [DataRow(33, 4, 0, Issue.Economy, Affiliation.Nixon, State.CA)]
        [DataRow(34, 3, 1, Issue.Defense, Affiliation.Kennedy, State.NC)]
        [DataRow(35, 4, 0, Issue.CivilRights, Affiliation.Kennedy, State.PA)]
        [DataRow(36, 2, 2, Issue.Defense, Affiliation.Kennedy, State.WV)]
        [DataRow(37, 3, 1, Issue.Defense, Affiliation.Both, State.NJ)]
        [DataRow(38, 2, 2, Issue.CivilRights, Affiliation.Kennedy, State.AR)]
        [DataRow(39, 4, 0, Issue.CivilRights, Affiliation.Kennedy, State.CA)]
        [DataRow(40, 3, 1, Issue.Defense, Affiliation.Both, State.MN)]
        [DataRow(41, 3, 1, Issue.CivilRights, Affiliation.Kennedy, State.AL)]
        [DataRow(42, 4, 0, Issue.Economy, Affiliation.Nixon, State.NY)]
        [DataRow(43, 4, 0, Issue.Economy, Affiliation.Kennedy, State.NY)]
        [DataRow(44, 3, 1, Issue.CivilRights, Affiliation.Nixon, State.NE)]
        [DataRow(45, 3, 1, Issue.Economy, Affiliation.Nixon, State.MI)]
        [DataRow(46, 3, 1, Issue.CivilRights, Affiliation.Both, State.AL)]
        [DataRow(47, 3, 1, Issue.Defense, Affiliation.Both, State.MI)]
        [DataRow(48, 3, 1, Issue.CivilRights, Affiliation.Nixon, State.IA)]
        [DataRow(49, 3, 1, Issue.Economy, Affiliation.Kennedy, State.FL)]
        [DataRow(50, 3, 1, Issue.Defense, Affiliation.Nixon, State.IN)]
        [DataRow(51, 3, 1, Issue.Economy, Affiliation.Kennedy, State.GA)]
        [DataRow(52, 2, 2, Issue.CivilRights, Affiliation.Both, State.MT)]
        [DataRow(53, 2, 2, Issue.Defense, Affiliation.Both, State.NM)]
        [DataRow(54, 2, 2, Issue.Economy, Affiliation.Kennedy, State.CT)]
        [DataRow(55, 4, 0, Issue.Defense, Affiliation.Kennedy, State.PA)]
        [DataRow(56, 4, 0, Issue.Defense, Affiliation.Nixon, State.OH)]
        [DataRow(57, 2, 2, Issue.Defense, Affiliation.Kennedy, State.RI)]
        [DataRow(58, 4, 0, Issue.Defense, Affiliation.Nixon, State.IL)]
        [DataRow(59, 3, 1, Issue.CivilRights, Affiliation.Kennedy, State.TN)]
        [DataRow(60, 3, 1, Issue.Economy, Affiliation.Both, State.UT)]
        [DataRow(61, 4, 0, Issue.CivilRights, Affiliation.Both, State.OH)]
        [DataRow(62, 3, 1, Issue.Economy, Affiliation.Both, State.WI)]
        [DataRow(63, 4, 0, Issue.Economy, Affiliation.Kennedy, State.OH)]
        [DataRow(64, 4, 0, Issue.Economy, Affiliation.Both, State.OH)]
        [DataRow(65, 3, 1, Issue.Defense, Affiliation.Kennedy, State.GA)]
        [DataRow(66, 3, 1, Issue.CivilRights, Affiliation.Kennedy, State.MA)]
        [DataRow(67, 2, 2, Issue.CivilRights, Affiliation.Both, State.WA)]
        [DataRow(68, 2, 2, Issue.Defense, Affiliation.Nixon, State.CO)]
        [DataRow(69, 4, 0, Issue.Defense, Affiliation.Nixon, State.PA)]
        [DataRow(70, 4, 0, Issue.Economy, Affiliation.Nixon, State.IL)]
        [DataRow(71, 3, 1, Issue.Defense, Affiliation.Nixon, State.NJ)]
        [DataRow(72, 3, 1, Issue.Economy, Affiliation.Nixon, State.IN)]
        [DataRow(73, 3, 1, Issue.Economy, Affiliation.Nixon, State.VA)]
        [DataRow(74, 4, 0, Issue.CivilRights, Affiliation.Kennedy, State.IL)]
        [DataRow(75, 4, 0, Issue.CivilRights, Affiliation.Nixon, State.CA)]
        [DataRow(76, 4, 0, Issue.Defense, Affiliation.Kennedy, State.IL)]
        [DataRow(77, 3, 1, Issue.Economy, Affiliation.Kennedy, State.MN)]
        [DataRow(78, 3, 1, Issue.Defense, Affiliation.Both, State.TN)]
        [DataRow(79, 2, 2, Issue.Economy, Affiliation.Both, State.NV)]
        [DataRow(80, 2, 2, Issue.Economy, Affiliation.Kennedy, State.MS)]
        [DataRow(81, 3, 1, Issue.CivilRights, Affiliation.Kennedy, State.LA)]
        [DataRow(82, 2, 2, Issue.Economy, Affiliation.Both, State.ID)]
        [DataRow(83, 4, 0, Issue.Defense, Affiliation.Both, State.TX)]
        [DataRow(84, 3, 1, Issue.Economy, Affiliation.Both, State.NC)]
        [DataRow(85, 3, 1, Issue.Defense, Affiliation.Kennedy, State.NJ)]
        [DataRow(86, 3, 1, Issue.CivilRights, Affiliation.Nixon, State.IA)]
        [DataRow(87, 2, 2, Issue.Defense, Affiliation.Nixon, State.SD)]
        [DataRow(88, 2, 2, Issue.Economy, Affiliation.Nixon, State.VT)]
        [DataRow(89, 2, 2, Issue.CivilRights, Affiliation.Nixon, State.KS)]
        [DataRow(90, 4, 0, Issue.CivilRights, Affiliation.Nixon, State.TX)]
        [DataRow(91, 2, 2, Issue.Defense, Affiliation.Nixon, State.OK)]
        [DataRow(92, 3, 1, Issue.Economy, Affiliation.Nixon, State.NY)]
        [DataRow(93, 4, 0, Issue.Defense, Affiliation.Nixon, State.CA)]
        [DataRow(94, 3, 1, Issue.Economy, Affiliation.Kennedy, State.IL)]
        [DataRow(95, 4, 0, Issue.CivilRights, Affiliation.Kennedy, State.TX)]
        [DataRow(96, 2, 2, Issue.Defense, Affiliation.Both, State.PA)]
        [DataRow(97, 2, 2, Issue.CivilRights, Affiliation.Both, State.OH)]

        //This test is just here to be sure that if we change things again
        //We don't accidentally break anything simple with a typo.
        public void ConfirmBasicCardValues(int index, int campaignPoints, int rest,
            Issue issue, Affiliation affiliation, State state)
        {
            //This if check is just because the entire library hasn't been made yet
            //This will be removed once the library is finished.
            if (NineteenSixty.GMTCards.ContainsKey(index) == false)
            {
                Assert.IsTrue(true);
            }
            else
            {
                var sut = NineteenSixty.GMTCards[index];

                Assert.AreEqual(campaignPoints, sut.CampaignPoints);
                Assert.AreEqual(rest, sut.RestCubes);
                Assert.AreEqual(issue, sut.Issue);
                Assert.AreEqual(affiliation, sut.Affiliation);
                Assert.AreEqual(state, sut.State);
            }
        }
        #endregion
    }
}
