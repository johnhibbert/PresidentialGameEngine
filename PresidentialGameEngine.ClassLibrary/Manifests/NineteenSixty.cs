using PresidentialGameEngine.ClassLibrary.Components;
using PresidentialGameEngine.ClassLibrary.Data;
using PresidentialGameEngine.ClassLibrary.Enums;
using PresidentialGameEngine.ClassLibrary.Interfaces;

namespace PresidentialGameEngine.ClassLibrary.Manifests
{
    public class NineteenSixty
    {
        public static readonly Dictionary<State, ILocationData<State, Player, Region>> StateData = new()
        {
            {State.AK, new StateData(State.AK, Region.West, 3, Player.Nixon, 0)},
            {State.AL, new StateData(State.AL, Region.South, 3, Player.Nixon, 0)},
            {State.AR, new StateData(State.AR, Region.South, 11, Player.Kennedy, 1)},
            {State.AZ, new StateData(State.AZ, Region.West, 8, Player.Kennedy, 1)},
            {State.CA, new StateData(State.CA, Region.West, 4, Player.Nixon, 1)},
            {State.CO, new StateData(State.CO, Region.West, 32, Player.Nixon, 0)},
            {State.CT, new StateData(State.CT, Region.East, 6, Player.Nixon, 1)},
            {State.DE, new StateData(State.DE, Region.East, 8, Player.Kennedy, 0)},
            {State.FL, new StateData(State.FL, Region.South, 3, Player.Kennedy, 0)},
            {State.GA, new StateData(State.GA, Region.South, 10, Player.Nixon, 0)},
            {State.HI, new StateData(State.HI, Region.West, 12, Player.Kennedy, 2)},
            {State.IA, new StateData(State.IA, Region.Midwest, 3, Player.Nixon, 0)},
            {State.ID, new StateData(State.ID, Region.West, 10, Player.Nixon, 1)},
            {State.IL, new StateData(State.IL, Region.Midwest, 4, Player.Nixon, 0)},
            {State.IN, new StateData(State.IN, Region.Midwest, 27, Player.Kennedy, 0)},
            {State.KS, new StateData(State.KS, Region.West, 13, Player.Nixon, 1)},
            {State.KY, new StateData(State.KY, Region.Midwest, 8, Player.Nixon, 2)},
            {State.LA, new StateData(State.LA, Region.South, 10, Player.Nixon, 0)},
            {State.MA, new StateData(State.MA, Region.East, 10, Player.Kennedy, 2)},
            {State.MD, new StateData(State.MD, Region.East, 16, Player.Kennedy, 2)},
            {State.ME, new StateData(State.ME, Region.East, 9, Player.Kennedy, 0)},
            {State.MI, new StateData(State.MI, Region.Midwest, 5, Player.Nixon, 1)},
            {State.MN, new StateData(State.MN, Region.Midwest, 20, Player.Kennedy, 0)},
            {State.MO, new StateData(State.MO, Region.Midwest, 11, Player.Kennedy, 0)},
            {State.MS, new StateData(State.MS, Region.South, 13, Player.Kennedy, 1)},
            {State.MT, new StateData(State.MT, Region.West, 8, Player.Kennedy, 2)},
            {State.NC, new StateData(State.NC, Region.South, 4, Player.Nixon , 0)},
            {State.ND, new StateData(State.ND, Region.West, 14, Player.Kennedy, 1)},
            {State.NE, new StateData(State.NE, Region.West, 4, Player.Nixon, 1)},
            {State.NH, new StateData(State.NH, Region.East, 6, Player.Nixon, 2)},
            {State.NJ, new StateData(State.NJ, Region.East, 4, Player.Nixon, 0)},
            {State.NM, new StateData(State.NM, Region.West, 16, Player.Kennedy, 0)},
            {State.NV, new StateData(State.NV, Region.West, 4, Player.Kennedy, 0)},
            {State.NY, new StateData(State.NY, Region.East, 3, Player.Kennedy, 0)},
            {State.OH, new StateData(State.OH, Region.Midwest, 45, Player.Kennedy, 0)},
            {State.OK, new StateData(State.OK, Region.West, 25, Player.Nixon, 1)},
            {State.OR, new StateData(State.OR, Region.West, 8, Player.Nixon, 1)},
            {State.PA, new StateData(State.PA, Region.East, 6, Player.Nixon, 0)},
            {State.RI, new StateData(State.RI, Region.East, 32, Player.Kennedy, 0)},
            {State.SC, new StateData(State.SC, Region.South, 4, Player.Kennedy, 2)},
            {State.SD, new StateData(State.SD, Region.West, 8, Player.Kennedy, 1)},
            {State.TN, new StateData(State.TN, Region.South, 4, Player.Nixon, 1)},
            {State.TX, new StateData(State.TX, Region.South, 11, Player.Nixon, 0)},
            {State.UT, new StateData(State.UT, Region.West, 24, Player.Kennedy, 0)},
            {State.VA, new StateData(State.VA, Region.South, 4, Player.Nixon, 1)},
            {State.VT, new StateData(State.VT, Region.East, 12, Player.Nixon, 0)},
            {State.WA, new StateData(State.WA, Region.West, 3, Player.Nixon, 1)},
            {State.WI, new StateData(State.WI, Region.Midwest, 9, Player.Nixon, 0)},
            {State.WV, new StateData(State.WV, Region.East, 12, Player.Nixon, 0)},
            {State.WY, new StateData(State.WY, Region.West, 8, Player.Kennedy, 0)},
        };

        public static readonly Dictionary<Player, State> PlayerStartingPositions = new()
        {
            { Player.Kennedy, State.MA },
            { Player.Nixon, State.CA }
        };

        public static readonly Dictionary<int, Card> ZManCards = new()
        {

            //new Card(1, "Greater Houston Ministerial Ass’n"),
            //new Card(2, "Nixon’s Knee"),
            {3, new Card()
                {
                    Index = 3,
                    Title = "Gallup Poll",
                    Text =  "Player may alter the order of the issues on the Issue Track as desired.",
                    CampaignPoints = 3,
                    EventType = EventType.None,
                    Issue = Issue.CivilRights,
                    Affiliation = Affiliation.Both,
                    State = State.MO,
                    Event = (engine, player, choices) =>
                    {
                        engine.ImplementChanges(choices);
                    },
                    RequiresPlayerInput = true,
                    AreChangesValid = (choices) =>
                    {
                        var issueListCorrectLength = choices.NewIssuesOrder.Count == Enum.GetNames(typeof(Issue)).Length;
                        var AndOnlyOneTypeOfTest = choices.ContainsExactlyOneTypeOfChange();

                        return issueListCorrectLength && AndOnlyOneTypeOfTest;
                    },
                }
            },
            //new Card(4, "Citizens for Nixon-Lodge"),
            {5, new Card()
                {
                    Index = 5,
                    Title = "Volunteers",
                    Text =  "Player gains 1 momentum marker.",
                    CampaignPoints = 2,
                    EventType = EventType.None,
                    Issue = Issue.Defense,
                    Affiliation = Affiliation.Both,
                    State = State.OR,
                    Event = (engine, player, choices) =>
                    {
                        engine.GainMomentum(player, 1);
                    },
                    RequiresPlayerInput = false,
                    AreChangesValid = (choices) =>
                    {
						//This has no player choices.
						return true;
                    },
                }
            },
            {6, new Card()
                {
                    Index = 6,
                    Title = "New England",
                    Text = "The Kennedy player may add a total of 5 state support in Connecticut, Massachusetts, Maine, New York, Rhode Island, and Vermont, no more than 2 per state.",
                    CampaignPoints = 3,
                    EventType = EventType.None,
                    Issue = Issue.Defense,
                    Affiliation = Affiliation.Kennedy,
                    State = State.LA,
                    Event = (engine, player, choices) => {
                        engine.ImplementChanges(choices);
                    },
                    RequiresPlayerInput = true,
                    AreChangesValid = (choices) =>
                    {
                        State[] newEnglandStates = [State.RI, State.MA, State.CT, State.VT, State.NH, State.ME];

                        var fiveOrFewerPointsOfStateChanges = choices.TotalStateChanges <= 5;
                        var onlyNewEnglandStatesIncluded = choices.StateChanges.Select(s => s.Target).All(x => newEnglandStates.Contains(x));
                        var statePlayerIsOnlyKennedy = choices.StateChanges.Select(x => x.Player).All(y => y == Player.Kennedy);
                        var noValueAboveTwo = choices.HighestStateChange <= 2;
                        var AndOnlyOneTypeOfTest = choices.ContainsExactlyOneTypeOfChange();

                        return fiveOrFewerPointsOfStateChanges && onlyNewEnglandStatesIncluded && noValueAboveTwo
                                && statePlayerIsOnlyKennedy && AndOnlyOneTypeOfTest;
                    },
                }
            },
            {7, new Card()
                {
                    Index = 7,
                    Title = "Late Returns From Cook County",
                    Text = "ELECTION DAY EVENT!  On Election Day, the Kennedy player gains 5 support checks in Illinois.",
                    CampaignPoints = 2,
                    EventType = EventType.None,
                    Issue = Issue.Economy,
                    Affiliation = Affiliation.Kennedy,
                    State = State.SC,
                    Event = (engine, player, choices) => {
                        //engine.GainMomentum(player, 1);
                        var supportCheckResult = engine.SupportCheck(Player.Kennedy, 5);
                        engine.GainSupport(Player.Kennedy, State.IL, supportCheckResult.Successes);
                    },
                    RequiresPlayerInput = false,
                    AreChangesValid = (choices) =>
                    {
						//This has no player choices.
						return true;
                    },
                }
            },
            {8, new Card()
                {
                    Index = 8,
                    Title = "Soviet Economic Growth",
                    Text = "Economy moves up one space on the Issue Track.  The leader in Economy gains 1 state support in New York.",
                    CampaignPoints = 2,
                    EventType = EventType.None,
                    Issue = Issue.Economy,
                    Affiliation = Affiliation.Both,
                    State = State.NH,
                    Event = (engine, player, choices) => {
                        engine.MoveIssueUp(Issue.Economy);
                        var econLeader = engine .GetLeader(Issue.Economy);
                        if(econLeader != Leader.None)
                        {
                            engine.GainSupport(econLeader.ToPlayer(), State.NY, 1);
                        }
                    },
                    RequiresPlayerInput = false,
                    AreChangesValid = (choices) => {
						//This has no player choices.
						return true;
                    },
                }
            },
            //new Card(9, "Baptist Ministers"),
            //new Card(10, "Swing State"),
            //new Card(11, "Sputnik V"),
            //new Card(12, "Southern Strategy"),
            //new Card(13, "Nikita Kruschev"),
            //new Card(14, "Gathering Momentum In The South!"),
            //new Card(15, "Gathering Momentum In The East!"),
            //new Card(16, "Gathering Momentum In The Midwest!"),
            //new Card(17, "Gathering Momentum In The West!"),
            //new Card(18, "Dwight Eisenhower"),
            //new Card(19, "Old South"),
            //new Card(20, "Nixon Egged In Michigan"),
            //new Card(21, "Fifty Stars"),
            {22, new Card()
                {
                    Index = 22,
                    Title = "Gaffe",
                    Text = "Opponent loses 1 momentum marker and 3 state support in the state currently occupied by their candidate token.",
                    CampaignPoints = 4,
                    EventType = EventType.None,
                    Issue = Issue.Economy,
                    Affiliation = Affiliation.Both,
                    State = State.TX,
                    Event = (engine, player, choices) => {
                        var opponent = player.ToOpponent();
                        var opponentLocation = engine.GetPlayerState(opponent);
                        engine.LoseMomentum(opponent, 1);
                        engine.LoseSupport(opponent, opponentLocation, 3);
                    },
                    RequiresPlayerInput = false,
                    AreChangesValid = (choices) => {
						//This has no player choices.
						return true;
                    },
                }
            },
            {23, new Card()
                {
                    Index = 23,
                    Title = "Martin Luther King Arrested",
                    Text = "Civil Rights moves up one space on the Issue Track.  Player gains 3 issue support in Civil Rights.",
                    CampaignPoints = 4,
                    EventType = EventType.None,
                    Issue = Issue.CivilRights,
                    Affiliation = Affiliation.Both,
                    State = State.CA,
                    Event = (engine, player, choices) => {
                        engine.MoveIssueUp(Issue.CivilRights);
                        engine.GainSupport(player, Issue.CivilRights, 3);
                    },
                    RequiresPlayerInput = false,
                    AreChangesValid = (choices) => {
						//This has no player choices.
						return true;
                    },
                }
            },
            //new Card(24, "East Harlem Pledge"),
            {25, new Card()
                {
                    Index = 25,
                    Title = "1960 Civil Rights Act",
                    Text = "Civil Rights moves up one space on the Issue Track and Nixon gains 1 issue support in Civil Rights.",
                    CampaignPoints = 2,
                    EventType = EventType.None,
                    Issue = Issue.CivilRights,
                    Affiliation = Affiliation.Nixon,
                    State = State.ND,
                    Event = (engine, player, choices) => {
                        engine.MoveIssueUp(Issue.CivilRights);
                        engine.GainSupport(Player.Nixon, Issue.CivilRights, 1);
                    },
                    RequiresPlayerInput = false,
                    AreChangesValid = (choices) => {
						//This has no player choices.
						return true;
                    },
                }
            },
            //new Card(26, "Ken-Air"),
            //new Card(27, "Nelson Rockefeller"),
            //new Card(28, "Harry F. Byrd"),
            {29, new Card()
                {
                    Index = 29,
                    Title = "The Great Seal Bug",
                    Text = "Nixon gains 1 issue support in Defense and may retrieve the Henry Cabot Lodge card from the discard pile if it is there.",
                    CampaignPoints = 3,
                    EventType = EventType.None,
                    Issue = Issue.Economy,
                    Affiliation = Affiliation.Nixon,
                    State = State.WI,
                    Event = (engine, player, choices) => {
                        engine.GainSupport(Player.Nixon, Issue.Defense, 1);
                        //Hmm odd. It doesn't like doing it this way.
                        //Or at least the compiler warns against it.
                        engine.RetrieveCardFromDiscardPile(Player.Nixon, ZManCards[42], true);
                        //Engine get card?

                    },
                    RequiresPlayerInput = false,
                    AreChangesValid = (choices) =>
                    {
 						//This has no player choices.
						return true;
                    },
                }
            },
            //new Card(30, "Johnson Jeered In Dallas"),
            //new Card(31, "Profiles In Courage"),
            //new Card(32, "Early Returns From Connecticut"),
            //new Card(33, "Unpledged Electors"),
            //new Card(34, "“Lazy Shave”"),
            //new Card(35, "Harvard Brain Trust"),
            {36, new Card()
                {
                    Index = 36,
                    Title = "Henry Luce",
                    Text = "The Kennedy player may place 1 endorsement marker in any region.",
                    CampaignPoints = 2,
                    EventType = EventType.None,
                    Issue = Issue.Defense,
                    Affiliation = Affiliation.Kennedy,
                    State = State.WV,
                    Event = (engine, player, choices) => {
                        engine.ImplementChanges(choices);
                    },
                    RequiresPlayerInput = true,
                    AreChangesValid = (choices) =>
                    {
                        var noValueAboveOne = choices.TotalEndorsementChanges <= 1;
                        var playerIsOnlyKennedy = choices.EndorsementChanges.Select(x => x.Player).All(y => y == Player.Kennedy);
                        var AndOnlyOneTypeOfTest = choices.ContainsExactlyOneTypeOfChange();

                        return noValueAboveOne && playerIsOnlyKennedy && AndOnlyOneTypeOfTest;
                    },
                }
            },
            {37, new Card()
                {
                    Index = 37,
                    Title = "Lunch Counter Sit-Ins",
                    Text = "Civil Rights moves up one space on the Issue Track.  The leader in Civil Rights may add a total of 3 state support anywhere, no more than 1 per state.",
                    CampaignPoints = 3,
                    EventType = EventType.None,
                    Issue = Issue.Defense,
                    Affiliation = Affiliation.Both,
                    State = State.NJ,
                    Event = (engine, player, choices) => {

                        engine.MoveIssueUp(Issue.CivilRights);
                        var leader = engine.GetLeader(Issue.CivilRights);
                        if(leader != Leader.None)
                        {
                            engine.ImplementChanges(choices);
                        }
                    },
                    RequiresPlayerInput = true,
                    AreChangesValid = (choices) =>
                    {
                        var threePointsOfIssueChanges = choices.TotalIssueChanges <= 3;
                        var noValueAboveOne = choices.HighestStateChange <= 1;
                        var issuePlayerAreAllSame = choices.IssueChanges.Select(x => x.Player).Distinct().Count() == 1;
                        var AndOnlyOneTypeOfTest = choices.ContainsExactlyOneTypeOfChange();

                        return threePointsOfIssueChanges && noValueAboveOne
                                && issuePlayerAreAllSame && AndOnlyOneTypeOfTest;
                    },
                }
            },
            //new Card(38, "“High Hopes”"),
            {39, new Card()
                {
                    Index = 39,
                    Title = "Lyndon Johnson",
                    Text = "The Kennedy player may add 2 state support in Texas and a total of 3 additional state support anywhere in the South, no more than 2 per state.  If the Kennedy candidate card is currently flipped to its Exhausted side, the Kennedy player may reclaim it face up.",
                    CampaignPoints = 4,
                    EventType = EventType.None,
                    Issue = Issue.CivilRights,
                    Affiliation = Affiliation.Kennedy,
                    State = State.CA,
                    Event = (engine, player, choices) => {
                        //TODO: It's unclear if you could use this to double-dip on texas.
                        //Also, should the 2 points for texas be baked in?  Right now it's assumed to be added.
                        engine.ImplementChanges(choices);
                        engine.UnexhaustPlayer(Player.Kennedy);
                    },
                    RequiresPlayerInput = true,
                    AreChangesValid = (choices) =>{
                        //throw new NotImplementedException();
                        var fivePointsOfIssueChanges = choices.TotalStateChanges <= 5;
                        var twoPointsForTexas = choices.StateChanges.Single(x => x.Target == State.TX).Change >= 2;
                        var statePlayerIsOnlyKennedy = choices.StateChanges.Select(x => x.Player).All(y => y == Player.Kennedy);
                        var noStateAboveTwo = choices.HighestStateChange <=2;

                        var southernStates = StateData.Where(y => y.Value.Region == Region.South).Select(z => z.Key);
                        var onlySouthernStates = choices.StateChanges.Select(s => s.Target).All(x => southernStates.Contains(x));

                        //var onlySouthernStates = choices.StateChanges.Select(s => s.Target).All(x => StatesByRegion[Region.South].Contains(x));
                        var AndOnlyOneTypeOfTest = choices.ContainsExactlyOneTypeOfChange();

                        return fivePointsOfIssueChanges && noStateAboveTwo && twoPointsForTexas
                              && onlySouthernStates && statePlayerIsOnlyKennedy && AndOnlyOneTypeOfTest;
                    },
                }
            },
            //new Card(40, "Northern Blacks"),
            {41, new Card()
                {
                    Index = 41,
                    Title = "Pierre Salinger",
                    Text = "The Kennedy player may add 3 issue support in any one issue.",
                    CampaignPoints = 3,
                    EventType = EventType.None,
                    Issue = Issue.CivilRights,
                    Affiliation = Affiliation.Kennedy,
                    State = State.AL,
                    Event = (engine, player, choices) => {

                        engine.ImplementChanges(choices);
                    },
                    RequiresPlayerInput = true,
                    AreChangesValid = (choices) =>
                    {
                        var threePointsOfIssueChanges = choices.TotalIssueChanges == 3;
                        var onlyOneIssueIncluded = choices.IssueChanges.Where(x => x.Change > 0).Count() == 2;
                        var issuePlayerIsOnlyKennedy = choices.IssueChanges.Select(x => x.Player).All(y => y == Player.Kennedy);
                        var AndOnlyOneTypeOfTest = choices.ContainsExactlyOneTypeOfChange();

                        return threePointsOfIssueChanges && onlyOneIssueIncluded
                                && issuePlayerIsOnlyKennedy && AndOnlyOneTypeOfTest;
                    },
                }
            },
            //new Card(42, "Henry Cabot Lodge"),
            {42, new Card()
                {
                    Index = 42,
                    Title = "Henry Cabot Lodge",
                    Text = "Nixon gains 2 state support in Massachusetts and 2 issue support in Defense.  If the Nixon candidate card is currently flipped to its Exhausted side, the Nixon player may reclaim it face-up.\r\n",
                    CampaignPoints = 4,
                    EventType = EventType.None,
                    Issue = Issue.Economy,
                    Affiliation = Affiliation.Nixon,
                    State = State.NY,
                    Event = (engine, player, choices) => {
                        engine.GainSupport(Player.Nixon, Issue.Defense, 2);
                        engine.GainSupport(Player.Nixon, State.MA, 2);
                        engine.UnexhaustPlayer(Player.Nixon);
                    },
                    RequiresPlayerInput = false,
                    AreChangesValid = (choices) =>{
                        //This has no player choices.
						return true;

                    },
                }
            },
            //new Card(43, "Catholic Support"),
            //new Card(44, "Puerto Rican Bishops"),
            //new Card(45, "Compact Of 5th Avenue"),
            {45, new Card()
                {
                    Index = 45,
                    Title = "Compact Of 5th Avenue",
                    Text = "Immediately move the Nixon candidate token to New York without paying the normal travel costs.  Nixon gains 1 issue support in Civil Rights, 2 state support in New York, and 1 media support cube in the East.",
                    CampaignPoints = 3,
                    EventType = EventType.None,
                    Issue = Issue.Economy,
                    Affiliation = Affiliation.Nixon,
                    State = State.MI,
                    Event = (engine, player, choices) => {
                        engine.MovePlayerToState(Player.Nixon, State.NY);
                        engine.GainSupport(Player.Nixon, State.NY, 2);
                        engine.GainSupport(Player.Nixon, Issue.CivilRights, 1);
                        engine.GainMediaSupportWithoutSupportCheck(Player.Nixon, Region.East, 1);
                    },
                    RequiresPlayerInput = false,
                    AreChangesValid = (choices) => {
						//This has no player choices.
                        return true;
                    },
                }
            },
            //new Card(46, "Prime-Time Television"),
            //new Card(47, "The Cold War"),
            {48, new Card()
                {
                    Index = 48,
                    Title = "Rising Food Prices",
                    Text = "Economy moves up one space on the Issue Track and Nixon gains 2 issue support in Economy.",
                    CampaignPoints = 3,
                    EventType = EventType.None,
                    Issue = Issue.CivilRights,
                    Affiliation = Affiliation.Nixon,
                    State = State.IA,
                    Event = (engine, player, choices) => {
                        engine.MoveIssueUp(Issue.Economy);
                        engine.GainSupport(Player.Nixon, Issue.Economy, 2);
                    },
                    RequiresPlayerInput = false,
                    AreChangesValid = (choices) => {
						//This has no player choices.
						return true;
                    },
                }
            },
            //new Card(49, "Eleanor Roosevelt’s Speaking Tour"),
            //new Card(50, "Industrial Midwest"),
            {51, new Card()
                {
                    Index = 51,
                    Title = "Missile Gap",
                    Text = "Kennedy gains 3 issue support in Defense.",
                    CampaignPoints = 3,
                    EventType = EventType.None,
                    Issue = Issue.Economy,
                    Affiliation = Affiliation.Kennedy,
                    State = State.GA,
                    Event = (engine, player, choices) => {
                        engine.GainSupport(Player.Kennedy, Issue.Defense, 3);
                    },
                    RequiresPlayerInput = false,
                    AreChangesValid = (choices) => {
						//This has no player choices.
						return true;
                    },
                }
            },
            {52, new Card()
                {
                    Index = 52,
                    Title = "Hurricane Donna",
                    Text = "Move player's candidate token to Florida.  Player gains 1 momentum marker and 1 state support in Florida.",
                    CampaignPoints = 2,
                    EventType = EventType.None,
                    Issue = Issue.CivilRights,
                    Affiliation = Affiliation.Both,
                    State = State.MT,
                    Event = (engine, player, choices) => {
                        engine.MovePlayerToState(player, State.FL);
                        engine.GainSupport(player, State.FL, 1);
                    },
                    RequiresPlayerInput = false,
                    AreChangesValid = (choices) => {
						//This has no player choices.
						return true;
                    },
                }
            },
            //new Card(53, "Campaign Headquarters"),
            //new Card(54, "Bobby Kennedy"),
            //new Card(55, "Hostile Press Corps"),
            //new Card(56, "Opposition Research"),
            //new Card(57, "“A New Frontier”"),
            //new Card(58, "Tricky Dick"),
            //new Card(59, "Mid-Atlantic"),
            {60, new Card()
                {
                    Index = 60,
                    Title = "World Series Ends",
                    Text = "The player with media support cubes in the East (if any) may add a total of 5 state support in the East, no more than 2 per state.",
                    CampaignPoints = 3,
                    EventType = EventType.None,
                    Issue = Issue.Economy,
                    Affiliation = Affiliation.Both,
                    State = State.UT,
                    Event = (engine, player, choices) => {
                        if(engine.GetMediaSupportLeader(Region.East) != Leader.None)
                        {
                            engine.ImplementChanges(choices);
                        }
                    },
                    RequiresPlayerInput = true,
                    AreChangesValid = (choices) => {
                        var easternStates = StateData.Where(y => y.Value.Region == Region.East).Select(z => z.Key);

                        var onlyEasternStatesIncluded = choices.StateChanges.Select(s => s.Target).All(x => easternStates.Contains(x));
                        var fiveOrFewerPointsOfStateChanges = choices.TotalStateChanges <= 5;
                        var noValueAboveTwo = choices.HighestStateChange <= 2;
                        var statePlayerIsOnlyNixon = choices.StateChanges.Select(x => x.Player).All(y => y == Player.Nixon);
                        var AndOnlyOneTypeOfTest = choices.ContainsExactlyOneTypeOfChange();

                        return onlyEasternStatesIncluded && fiveOrFewerPointsOfStateChanges
                            && statePlayerIsOnlyNixon && noValueAboveTwo && AndOnlyOneTypeOfTest;
                    },
                }
            },
            {61, new Card()
                {
                    Index = 61,
                    Title = "Fatigue Sets In",
                    Text = "If opponent's candidate card is currently available for play, flip it over to its Exhausted side.",
                    CampaignPoints = 4,
                    EventType = EventType.None,
                    Issue = Issue.CivilRights,
                    Affiliation = Affiliation.Both,
                    State = State.OH,
                    Event = (engine, player, choices) => {
                        engine.ExhaustPlayer(player.ToOpponent());
                    },
                    RequiresPlayerInput = false,
                    AreChangesValid = (choices) => {
						//This has no player choices.
						return true;
                    },
                }
            },
            {62, new Card()
                {
                    Index = 62,
                    Title = "Trial of Gary Powers",
                    Text = "Defense moves up two spaces on the Issue Track.  The leader in Defense gains 1 momentum marker.",
                    CampaignPoints = 3,
                    EventType = EventType.None,
                    Issue = Issue.Economy,
                    Affiliation = Affiliation.Both,
                    State = State.WI,
                    Event = (engine, player, choices) => {
                        engine.MoveIssueUp(Issue.Defense);
                        engine.MoveIssueUp(Issue.Defense);
                        var leader = engine.GetLeader(Issue.Defense);
                        if(leader != Leader.None)
                        {
                            engine.GainMomentum(leader.ToPlayer(), 1);
                        }
                    },
                    RequiresPlayerInput = false,
                    AreChangesValid = (choices) => {
						//This has no player choices.
						return true;
                    },
                }
            },

            {63, new Card()
                {
                    Index = 63,
                    Title = "“Give Me A Week”",
                    Text = "The Nixon player loses 2 momentum markers and must subtract 1 issue support in each issue.",
                    CampaignPoints = 4,
                    EventType = EventType.None,
                    Issue = Issue.Economy,
                    Affiliation = Affiliation.Kennedy,
                    State = State.OH,
                    Event = (engine, player, choices) => {
                        engine.LoseMomentum(Player.Nixon, 2);
                        engine.LoseSupport(Player.Nixon, Issue.Defense, 1);
                        engine.LoseSupport(Player.Nixon, Issue.Economy, 1);
                        engine.LoseSupport(Player.Nixon, Issue.CivilRights, 1);
                    },
                    RequiresPlayerInput = false,
                    AreChangesValid = (choices) => {
						//This has no player choices.
						return true;
                    },
                }
            },
            {64, new Card()
                {
                    Index = 64,
                    Title = "Stump Speech",
                    Text = "If opponent has more momentum markers, player gains enough to have the same number.",
                    CampaignPoints = 4,
                    EventType = EventType.None,
                    Issue = Issue.Economy,
                    Affiliation = Affiliation.Both,
                    State = State.OH,
                    Event = (engine, player, choices) => {
                        var playerMomentum = engine.GetPlayerMomentum(player);
                        var opponentMomentum = engine.GetPlayerMomentum(player.ToOpponent());

                        if(opponentMomentum > playerMomentum)
                        {
                            engine.GainMomentum(player, opponentMomentum - playerMomentum);
                        }
                    },
                    RequiresPlayerInput = false,
                    AreChangesValid = (choices) => {
						//This has no player choices.
						return true;
                    },
                }
            },
            //new Card(65, "Joe Kennedy"),
            //new Card(66, "Adlai Stevenson"),
            //new Card(67, "Voter Registration Drive"),
            {68, new Card()
                {
                    Index = 68,
                    Title = "“Peace Without Surrender”",
                    Text = "Defense moves up one space on the Issue Track and Nixon gains 1 issue support in Defense.",
                    CampaignPoints = 2,
                    EventType = EventType.None,
                    Issue = Issue.Defense,
                    Affiliation = Affiliation.Nixon,
                    State = State.CO,
                    Event = (engine, player, choices) => {
                        engine.MoveIssueUp(Issue.Defense);
                        engine.GainSupport(Player.Nixon, Issue.Defense, 1);
                    },
                    RequiresPlayerInput = false,
                    AreChangesValid = (choices) => {
						//This has no player choices.
						return true;
                    },
                }
            },
            //new Card(69, "Congressional Summer Session"),
            {70, new Card()
                {
                    Index = 70,
                    Title = "The Old Nixon",
                    Text = "The Nixon player loses 1 momentum marker.  The Kennedy player loses 3 momentum markers.",
                    CampaignPoints = 4,
                    EventType = EventType.None,
                    Issue = Issue.Economy,
                    Affiliation = Affiliation.Nixon,
                    State = State.IL,
                    Event = (engine, player, choices) => {
                        engine.LoseMomentum(Player.Nixon, 1);
                        engine.LoseMomentum(Player.Kennedy, 3);
                    },
                    RequiresPlayerInput = false,
                    AreChangesValid = (choices) => {
						//This has no player choices.
						return true;
                    },
                }
            },

            {71, new Card()
                {
                    Index = 71,
                    Title = "Heartland of America",
                    Text = "The Kennedy player may add a total of 5 state support in states having 20 or more electoral votes, no more than 2 per state.",
                    CampaignPoints = 3,
                    EventType = EventType.None,
                    Issue = Issue.Defense,
                    Affiliation = Affiliation.Nixon,
                    State = State.NJ,
                    Event = (engine, player, choices) => {
                        engine.ImplementChanges(choices);
                    },
                    RequiresPlayerInput = true,
                    AreChangesValid = (choices) => {

                        var heartlandStates = StateData.Where(x => x.Value.ElectoralVotes <=10)
                        .Where(y => y.Value.Region == Region.Midwest || y.Value.Region == Region.West).Select(z => z.Key);
                        
                        //var lowVoteStates = ElectoralVotes.Where(x => x.Value <= 10).Select(y=>y.Key).ToList();


                        //var westOrMidWestStates = StatesByRegion[Region.Midwest];
                        //westOrMidWestStates.AddRange(StatesByRegion[Region.West]);
                        //var heartlandStates = lowVoteStates.Intersect(westOrMidWestStates);

                        var onlyHeartlandStates = choices.StateChanges.Select(s => s.Target).All(x => heartlandStates.Contains(x));
                        var sevenOrFewerPointsOfStateChanges = choices.TotalStateChanges <= 7;
                        var noValueAboveOne = choices.HighestStateChange <= 1;
                        var statePlayerIsOnlyNixon = choices.StateChanges.Select(x => x.Player).All(y => y == Player.Nixon);
                        var AndOnlyOneTypeOfTest = choices.ContainsExactlyOneTypeOfChange();

                        return onlyHeartlandStates && sevenOrFewerPointsOfStateChanges
                            && statePlayerIsOnlyNixon && noValueAboveOne && AndOnlyOneTypeOfTest;
                    },
                
                }
            },
            {72, new Card()
                {
                    Index = 72,
                    Title = "Southern Revolt",
                    Text = "If Kennedy is leading in Civil Rights, the Nixon player may add a total of 5 state support in the South, no more than 2 per state.",
                    CampaignPoints = 3,
                    EventType = EventType.None,
                    Issue = Issue.Economy,
                    Affiliation = Affiliation.Nixon,
                    State = State.IN,
                    Event = (engine, player, choices) => {
                        var civilRightsleader = engine.GetLeader(Issue.CivilRights);

                        if(civilRightsleader == Leader.Kennedy)
                        {
                            engine.ImplementChanges(choices);
                        }
                    },
                    RequiresPlayerInput = true,
                    AreChangesValid = (choices) => {
                        var southernStates = StateData.Where(y => y.Value.Region == Region.South).Select(z => z.Key);

                        var onlySouthernStatesIncluded = choices.StateChanges.Select(s => s.Target).All(x => southernStates.Contains(x));
                        var fiveOrFewerPointsOfStateChanges = choices.TotalStateChanges <= 5;
                        var noValueAboveTwo = choices.HighestStateChange <= 2;
                        var statePlayerIsOnlyNixon = choices.StateChanges.Select(x => x.Player).All(y => y == Player.Nixon);
                        var AndOnlyOneTypeOfTest = choices.ContainsExactlyOneTypeOfChange();

                        return onlySouthernStatesIncluded && fiveOrFewerPointsOfStateChanges
                            && statePlayerIsOnlyNixon && noValueAboveTwo && AndOnlyOneTypeOfTest;
                    },
                }
            },
            //new Card(73, "Norman Vincent Peale"),
            //new Card(74, "Eisenhower’s Silence"),
            {75, new Card()
                {
                    Index = 75,
                    Title = "Republican TV Spots",
                    Text = "Immediately move the Nixon candidate token to New York, but do no pay the normal travel costs for doing so.  The Nixon player may place 3 media support cubes.",
                    CampaignPoints = 4,
                    EventType = EventType.None,
                    Issue = Issue.CivilRights,
                    Affiliation = Affiliation.Nixon,
                    State = State.CA,
                    Event = (engine, player, choices) => {
                        engine.MovePlayerToState(Player.Nixon, State.NY);
                        engine.ImplementChanges(choices);
                    },
                    RequiresPlayerInput = true,
                    AreChangesValid = (choices) => {
                        var threeOrFewerMediaSupportChanges = choices.TotalMediaChanges <= 3;
                        var AndOnlyOneTypeOfTest = choices.ContainsExactlyOneTypeOfChange();

                        return threeOrFewerMediaSupportChanges && AndOnlyOneTypeOfTest;
                    },
                }
            },
            //new Card(76, "Nixon’s Pledge"),
            //new Card(77, "Suburban Voters"),
            {77, new Card()
                {
                    Index = 77,
                    Title = "Suburban Voters",
                    Text = "The Kennedy player may add a total of 5 state support in states having 20 or more electoral votes, no more than 2 per state.",
                    CampaignPoints = 3,
                    EventType = EventType.None,
                    Issue = Issue.Economy,
                    Affiliation = Affiliation.Kennedy,
                    State = State.MN,
                    Event = (engine, player, choices) => {
                        engine.ImplementChanges(choices);
                    },
                    RequiresPlayerInput = true,
                    AreChangesValid = (choices) => {

                        var suburbanStates = StateData.Where(x => x.Value.ElectoralVotes >=20).Select(y => y.Key);

                        var onlySuburbanStates = choices.StateChanges.Select(s => s.Target).All(x => suburbanStates.Contains(x));
                        var fiveOrFewerPointsOfStateChanges = choices.TotalStateChanges <= 5;
                        var noValueAboveTwo = choices.HighestStateChange <= 2;
                        var statePlayerIsOnlyKennedy = choices.StateChanges.Select(x => x.Player).All(y => y == Player.Kennedy);
                        var AndOnlyOneTypeOfTest = choices.ContainsExactlyOneTypeOfChange();

                        return onlySuburbanStates && fiveOrFewerPointsOfStateChanges
                            && statePlayerIsOnlyKennedy && noValueAboveTwo && AndOnlyOneTypeOfTest;
                    },
                }
            },
            {78, new Card()
                {
                    Index = 78,
                    Title = "Stock Market In Decline",
                    Text = "Economy moves up two spaces on the Issue Track. The leader in Economy gains 2 state support in New York.",
                    CampaignPoints = 3,
                    EventType = EventType.None,
                    Issue = Issue.Defense,
                    Affiliation = Affiliation.Both,
                    State = State.TN,
                    Event = (engine, player, choices) => {
                        engine.MoveIssueUp(Issue.Economy);
                        engine.MoveIssueUp(Issue.Economy);

                        var econLeader = engine.GetLeader(Issue.Economy);
                        if(econLeader != Leader.None)
                        {
                            engine.GainSupport(econLeader.ToPlayer(), State.NY, 2);
                        }
                    },
                    RequiresPlayerInput = false,
                    AreChangesValid = (choices) => {
						//This has no player choices.
						return true;
                    },
                }
            },
            //new Card(79, "Advance Men"),
            {80, new Card()
                {
                    Index = 80,
                    Title = "Herblock",
                    Text = "The Kennedy player may remove 2 Nixon media support cubes from the board.",
                    CampaignPoints = 2,
                    EventType = EventType.None,
                    Issue = Issue.Economy,
                    Affiliation = Affiliation.Kennedy,
                    State = State.MS,
                    Event = (engine, player, choices) => {
                        engine.ImplementChanges(choices);
                    },
                    RequiresPlayerInput = true,
                    AreChangesValid = (choices) => {
                        var upToNegativeTwoPointsOfLostMediaSupport = choices.TotalMediaChanges >= -2 
                        && choices.TotalMediaChanges >= 0;
                        var affectedPlayerIsNixon = choices.MediaSupportChanges.Select(x => x.Player).All(y => y == Player.Nixon);
                        var AndOnlyOneTypeOfTest = choices.ContainsExactlyOneTypeOfChange();

                        return upToNegativeTwoPointsOfLostMediaSupport && affectedPlayerIsNixon && AndOnlyOneTypeOfTest;
                    },
                }
            },
            //new Card(81, "Kennedy’s Peace Corps"),
            {82, new Card()
                {                    
                    Index = 82,
                    Title = "Fidel Castro",
                    Text = "The leader in Defense gains 1 momentum marker and 1 state support in Florida.",
                    CampaignPoints = 2,
                    EventType = EventType.None,
                    Issue = Issue.Economy,
                    Affiliation = Affiliation.Both,
                    State = State.ID,
                    Event = (engine, player, choices) => {
                        var defenseLeader = engine.GetLeader(Issue.Defense);

                        if(defenseLeader != Leader.None)
                        {
                            engine.GainMomentum(defenseLeader.ToPlayer(), 1);
                            engine.GainSupport(defenseLeader.ToPlayer(), State.FL, 1);
                        }
                    },
                    RequiresPlayerInput = false,
                    AreChangesValid = (choices) => {
						//This has no player choices.
						return true;
                    },
                }
            },
            //new Card(83, "Whistlestop"),
            //new Card(84, "Quemoy and Matsu"),
            //new Card(85, "Jackie Kennedy"),
            {86,  new Card()
                {
                    Index = 86,
                    Title = "Herb Klein",
                    Text = "The Nixon player may add a total of 3 issue support in any issues.",
                    CampaignPoints = 3,
                    EventType = EventType.None,
                    Issue = Issue.CivilRights,
                    Affiliation = Affiliation.Nixon,
                    State = State.IA,
                    Event = (engine, player, choices) => {

                        engine.ImplementChanges(choices);
                    },
                    RequiresPlayerInput = true,
                    AreChangesValid = (choices) =>
                    {
                        var threePointsOfIssueChanges = choices.TotalIssueChanges == 3;
                        var issuePlayerIsOnlyNixon = choices.IssueChanges.Select(x => x.Player).All(y => y == Player.Nixon);
                        var AndOnlyOneTypeOfTest = choices.ContainsExactlyOneTypeOfChange();

                        return threePointsOfIssueChanges && issuePlayerIsOnlyNixon && AndOnlyOneTypeOfTest;
                    },
                }
            },
            //new Card(87, "Stevenson Loyalists"),
            //new Card(88, "Stature Gap"),
            {89, new Card()
                {
                    Index = 89,
                    Title = "The New Nixon",
                    Text = "The Nixon player gains 1 momentum marker.",
                    CampaignPoints = 2,
                    EventType = EventType.None,
                    Issue = Issue.CivilRights,
                    Affiliation = Affiliation.Nixon,
                    State = State.KS,
                    Event = (engine, player, choices) => {
                        engine.GainMomentum(Player.Nixon, 1);
                    },
                    RequiresPlayerInput = false,
                    AreChangesValid = (choices) => {
						//This has no player choices.
						return true;
                    },
                }
            },
            {90, new Card()
                {
                    Index = 90,
                    Title = "Recount",
                    Text = "ELECTION DAY EVENT!  On Election Day, the Nixon player gains 3 support checks in any one state.",
                    CampaignPoints = 4,
                    EventType = EventType.None,
                    Issue = Issue.CivilRights,
                    Affiliation = Affiliation.Nixon,
                    State = State.TX,
                    Event = (engine, player, choices) => {
                        //FIXME
                        //throw new NotImplementedException();
                        
                        var supportCheckResult = engine.SupportCheck(Player.Nixon, 3);
                        choices.StateChanges.First().Change = supportCheckResult.Successes;
                        engine.ImplementChanges(choices);


                    },
                    RequiresPlayerInput = true,
                    AreChangesValid = (choices) =>
                    {

                        var threePointsOfStateChanges = choices.TotalStateChanges == 3;
                        var statePlayerIsOnlyNixon = choices.StateChanges.Select(x => x.Player).All(y => y == Player.Nixon);
                        var onlyOneState = choices.StateChanges.Select(x => x.Target).Count() == 1;
                        var AndOnlyOneTypeOfTest = choices.ContainsExactlyOneTypeOfChange();

                        return threePointsOfStateChanges && statePlayerIsOnlyNixon && AndOnlyOneTypeOfTest;
                    },
                }
            },
            //new Card(91, "Political Capital"),

            };

        private static readonly Dictionary<int, Card> GMT_OnlyCards = new()
        {
            //new Card(92, "Give ‘Em Hell Harry"),
            {93, new Card()
                {
                    Index = 93,
                    Title = "Experience Counts",
                    Text = "Kennedy loses 1 issue support in each issue.  The Nixon player gains one momentum marker.",
                    CampaignPoints = 4,
                    EventType = EventType.None,
                    Issue = Issue.Defense,
                    Affiliation = Affiliation.Nixon,
                    State = State.CA,
                    Event = (engine, player, choices) => {
                        engine.LoseSupport(Player.Kennedy, Issue.Defense, 1);
                        engine.LoseSupport(Player.Kennedy, Issue.CivilRights, 1);
                        engine.LoseSupport(Player.Kennedy, Issue.Economy, 1);
                        engine.GainMomentum(Player.Nixon, 1);
                    },
                    RequiresPlayerInput = false,
                    AreChangesValid = (choices) => {
						//This has no player choices.
						return true;
                    },
                }
            },
            //new Card(94, "A Low Blow"),
            //new Card(95, "A Time For Greatness"),
            {95, new Card()
                {
                    Index = 95,
                    Title = "A Time For Greatness",
                    Text = "Nixon loses 1 issue support on each issue.  The Kennedy player may add 3 state support anywhere, no more than 1 per state.",
                    CampaignPoints = 4,
                    EventType = EventType.None,
                    Issue = Issue.CivilRights,
                    Affiliation = Affiliation.Kennedy,
                    State = State.TX,
                    Event = (engine, player, choices) => {
                        engine.LoseSupport(Player.Nixon, Issue.Defense, 1);
                        engine.LoseSupport(Player.Nixon, Issue.CivilRights, 1);
                        engine.LoseSupport(Player.Nixon, Issue.Economy, 1);
                        engine.ImplementChanges(choices);
                    },
                    RequiresPlayerInput = true,
                    AreChangesValid = (choices) => {
                        var threePointsOfStateChanges = choices.TotalStateChanges <= 3;
                        var noValueAboveOne = choices.HighestStateChange <= 1;
                        var statePlayerIsOnlyKennedy = choices.StateChanges.Select(x => x.Player).All(y => y == Player.Kennedy);
                        var AndOnlyOneTypeOfTest = choices.ContainsExactlyOneTypeOfChange();

                        return threePointsOfStateChanges && noValueAboveOne
                                && statePlayerIsOnlyKennedy && AndOnlyOneTypeOfTest;
                    },
                }
            },
            {96, new Card()
                {
                    Index = 96,
                    Title = "Medal Count",
                    Text = "The leaders in Civil Rights and Economy lose 1 issue support in those issues.  If the same player leads both, they also lose 1 momentum marker.",
                    CampaignPoints = 2,
                    EventType = EventType.None,
                    Issue = Issue.Defense,
                    Affiliation = Affiliation.Both,
                    State = State.PA,
                    Event = (engine, player, choices) => {

                        var civilRightsLeader = engine.GetLeader(Issue.CivilRights);
                        var econLeader = engine.GetLeader(Issue.Economy);

                        if(civilRightsLeader == econLeader && civilRightsLeader != Leader.None)
                        {
                            engine.LoseMomentum(civilRightsLeader.ToPlayer(), 1);
                        }
                        if(civilRightsLeader != Leader.None)
                        {
                        engine.LoseSupport(civilRightsLeader.ToPlayer(), Issue.CivilRights, 1);
                        }
                        if(econLeader != Leader.None)
                        {
                        engine.LoseSupport(econLeader.ToPlayer(), Issue.Economy, 1);
                        }
                    },
                    RequiresPlayerInput = false,
                    AreChangesValid = (choices) => {
						//This has no player choices.
						return true;
                    },
                }
            },
            {97, new Card()
                {
                    Index = 97,
                    Title = "Cassius Clay Wins Gold",
                    Text = "The leaders in Defense and Economy lose 1 issue support in those issues.  If the same player leads both, they also lose 1 momentum marker.",
                    CampaignPoints = 2,
                    EventType = EventType.None,
                    Issue = Issue.CivilRights,
                    Affiliation = Affiliation.Both,
                    State = State.OH,
                    Event = (engine, player, choices) => {

                        var defenseLeader = engine.GetLeader(Issue.Defense);
                        var econLeader = engine.GetLeader(Issue.Economy);

                        if(defenseLeader == econLeader && defenseLeader != Leader.None)
                        {
                            engine.LoseMomentum(defenseLeader.ToPlayer(), 1);
                        }
                        if(defenseLeader != Leader.None)
                        {
                            engine.LoseSupport(defenseLeader.ToPlayer(), Issue.Defense, 1);
                        }
                        if(econLeader != Leader.None)
                        {
                            engine.LoseSupport(econLeader.ToPlayer(), Issue.Economy, 1);
                        }
                    },
                    RequiresPlayerInput = false,
                    AreChangesValid = (choices) => {
						//This has no player choices.
						return true;
                    },
                }
            },
        };

        public static readonly Dictionary<int, Card> GMTCards =
            ZManCards.Concat(GMT_OnlyCards).ToDictionary(x => x.Key, x => x.Value);
    }
}
