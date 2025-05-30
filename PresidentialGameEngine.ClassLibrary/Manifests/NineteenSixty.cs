using PresidentialGameEngine.ClassLibrary.Data;
using PresidentialGameEngine.ClassLibrary.Enums;

namespace PresidentialGameEngine.ClassLibrary.Manifests
{
    public class NineteenSixty
    {
        public static readonly Dictionary<Player, State> PlayerStartingPositions = new()
        {
            { Player.Kennedy, State.MA },
            { Player.Nixon, State.CA }
        };

        public static readonly Dictionary<State, Region> RegionByState = new()
        {
            { State.AK, Region.West },
            { State.AL, Region.South },
            { State.AR, Region.South },
            { State.AZ, Region.West },
            { State.CA, Region.West },
            { State.CO, Region.West },
            { State.CT, Region.East },
            { State.DE, Region.East },
            { State.FL, Region.South },
            { State.GA, Region.South },
            { State.HI, Region.West },
            { State.IA, Region.Midwest },
            { State.ID, Region.West },
            { State.IL, Region.Midwest },
            { State.IN, Region.Midwest },
            { State.KS, Region.West },
            { State.KY, Region.Midwest },
            { State.LA, Region.South },
            { State.MA, Region.East },
            { State.MD, Region.East },
            { State.ME, Region.East },
            { State.MI, Region.Midwest },
            { State.MN, Region.Midwest },
            { State.MO, Region.Midwest },
            { State.MS, Region.South },
            { State.MT, Region.West },
            { State.NC, Region.South },
            { State.ND, Region.West },
            { State.NE, Region.West },
            { State.NH, Region.East },
            { State.NJ, Region.East },
            { State.NM, Region.West },
            { State.NV, Region.West },
            { State.NY, Region.East },
            { State.OH, Region.Midwest },
            { State.OK, Region.West },
            { State.OR, Region.West },
            { State.PA, Region.East },
            { State.RI, Region.East },
            { State.SC, Region.South },
            { State.SD, Region.West },
            { State.TN, Region.South },
            { State.TX, Region.South },
            { State.UT, Region.West },
            { State.VA, Region.South },
            { State.VT, Region.East },
            { State.WA, Region.West },
            { State.WI, Region.Midwest },
            { State.WV, Region.East },
            { State.WY, Region.West }
        };

        public static readonly Dictionary<Region, List<State>> StatesByRegion = ReverseDictionary();

        public static readonly Dictionary<State, int> ElectoralVotes = new()
        {
            { State.AK, 3 },
            { State.AL, 11 },
            { State.AR, 8 },
            { State.AZ, 4 },
            { State.CA, 32 },
            { State.CO, 6 },
            { State.CT, 8 },
            { State.DE, 3 },
            { State.FL, 10 },
            { State.GA, 12 },
            { State.HI, 3 },
            { State.IA, 10 },
            { State.ID, 4 },
            { State.IL, 27 },
            { State.IN, 13 },
            { State.KS, 8 },
            { State.KY, 10 },
            { State.LA, 10 },
            { State.MA, 16 },
            { State.MD, 9 },
            { State.ME, 5 },
            { State.MI, 20 },
            { State.MN, 11 },
            { State.MO, 13 },
            { State.MS, 8 },
            { State.MT, 4 },
            { State.NC, 14 },
            { State.ND, 4 },
            { State.NE, 6 },
            { State.NH, 4 },
            { State.NJ, 16 },
            { State.NM, 4 },
            { State.NV, 3 },
            { State.NY, 45 },
            { State.OH, 25 },
            { State.OK, 8 },
            { State.OR, 6 },
            { State.PA, 32 },
            { State.RI, 4 },
            { State.SC, 8 },
            { State.SD, 4 },
            { State.TN, 11 },
            { State.TX, 24 },
            { State.UT, 4 },
            { State.VA, 12 },
            { State.VT, 3 },
            { State.WA, 9 },
            { State.WI, 12 },
            { State.WV, 8 },
            { State.WY, 3 },
        };

        private static Dictionary<Region, List<State>> ReverseDictionary() 
        {
            var oldDict = NineteenSixty.RegionByState;

            Dictionary<Region, List<State>> newDict = [];

            foreach (Region region in Enum.GetValues(typeof(Region)))
            {
                newDict.Add(region, []);
            }

            foreach (State state in oldDict.Keys)
            {
                newDict[oldDict[state]].Add(state);
            }

            return newDict;
        } 

        public static readonly Dictionary<State, Tilt<Player>> StateTilts = new()
        {
            { State.AK, new Tilt<Player>{Player = Player.Nixon, StartingSupport = 0 } },
            { State.AL, new Tilt<Player>{Player = Player.Kennedy, StartingSupport = 1 } },
            { State.AR, new Tilt<Player>{Player = Player.Kennedy, StartingSupport = 1 } },
            { State.AZ, new Tilt<Player>{Player = Player.Nixon, StartingSupport = 1 } },
            { State.CA, new Tilt<Player>{Player = Player.Nixon, StartingSupport = 0 } },
            { State.CO, new Tilt<Player>{Player = Player.Nixon, StartingSupport = 1 } },
            { State.CT, new Tilt<Player>{Player = Player.Kennedy, StartingSupport = 0 } },
            { State.DE, new Tilt<Player>{Player = Player.Kennedy, StartingSupport = 0 } },
            { State.FL, new Tilt<Player>{Player = Player.Nixon, StartingSupport = 0 } },
            { State.GA, new Tilt<Player>{Player = Player.Kennedy, StartingSupport = 2 } },
            { State.HI, new Tilt<Player>{Player = Player.Nixon, StartingSupport = 0 } },
            { State.IA, new Tilt<Player>{Player = Player.Nixon, StartingSupport = 1 } },
            { State.ID, new Tilt<Player>{Player = Player.Nixon, StartingSupport = 0 } },
            { State.IL, new Tilt<Player>{Player = Player.Kennedy, StartingSupport = 0 } },
            { State.IN, new Tilt<Player>{Player = Player.Nixon, StartingSupport = 1 } },
            { State.KS, new Tilt<Player>{Player = Player.Nixon, StartingSupport = 2 } },
            { State.KY, new Tilt<Player>{Player = Player.Nixon, StartingSupport = 0 } },
            { State.LA, new Tilt<Player>{Player = Player.Kennedy, StartingSupport = 2 } },
            { State.MA, new Tilt<Player>{Player = Player.Kennedy, StartingSupport = 2 } },
            { State.MD, new Tilt<Player>{Player = Player.Kennedy, StartingSupport = 0 } },
            { State.ME, new Tilt<Player>{Player = Player.Nixon, StartingSupport = 1 } },
            { State.MI, new Tilt<Player>{Player = Player.Kennedy, StartingSupport = 0 } },
            { State.MN, new Tilt<Player>{Player = Player.Kennedy, StartingSupport = 0 } },
            { State.MO, new Tilt<Player>{Player = Player.Kennedy, StartingSupport = 1 } },
            { State.MS, new Tilt<Player>{Player = Player.Kennedy, StartingSupport = 2 } },
            { State.MT, new Tilt<Player>{Player = Player.Nixon , StartingSupport = 0 } },
            { State.NC, new Tilt<Player>{Player = Player.Kennedy, StartingSupport = 1 } },
            { State.ND, new Tilt<Player>{Player = Player.Nixon, StartingSupport = 1 } },
            { State.NE, new Tilt<Player>{Player = Player.Nixon, StartingSupport = 2 } },
            { State.NH, new Tilt<Player>{Player = Player.Nixon, StartingSupport = 0 } },
            { State.NJ, new Tilt<Player>{Player = Player.Kennedy, StartingSupport = 0 } },
            { State.NM, new Tilt<Player>{Player = Player.Kennedy, StartingSupport = 0 } },
            { State.NV, new Tilt<Player>{Player = Player.Kennedy, StartingSupport = 0 } },
            { State.NY, new Tilt<Player>{Player = Player.Kennedy, StartingSupport = 0 } },
            { State.OH, new Tilt<Player>{Player = Player.Nixon, StartingSupport = 1 } },
            { State.OK, new Tilt<Player>{Player = Player.Nixon, StartingSupport = 1 } },
            { State.OR, new Tilt<Player>{Player = Player.Nixon, StartingSupport = 0 } },
            { State.PA, new Tilt<Player>{Player = Player.Kennedy, StartingSupport = 0 } },
            { State.RI, new Tilt<Player>{Player = Player.Kennedy, StartingSupport = 2 } },
            { State.SC, new Tilt<Player>{Player = Player.Kennedy, StartingSupport = 1 } },
            { State.SD, new Tilt<Player>{Player = Player.Nixon, StartingSupport = 1 } },
            { State.TN, new Tilt<Player>{Player = Player.Nixon, StartingSupport = 0 } },
            { State.TX, new Tilt<Player>{Player = Player.Kennedy, StartingSupport = 0 } },
            { State.UT, new Tilt<Player>{Player = Player.Nixon, StartingSupport = 1 } },
            { State.VA, new Tilt<Player>{Player = Player.Nixon, StartingSupport = 0 } },
            { State.VT, new Tilt<Player>{Player = Player.Nixon, StartingSupport = 1 } },
            { State.WA, new Tilt<Player>{Player = Player.Nixon, StartingSupport = 0 } },
            { State.WI, new Tilt<Player>{Player = Player.Nixon, StartingSupport = 0 } },
            { State.WV, new Tilt<Player>{Player = Player.Kennedy, StartingSupport = 0 } },
            { State.WY, new Tilt<Player>{Player =  Player.Nixon, StartingSupport = 1 } },
        };

        public static readonly Dictionary<int, Card> ZManCards = new()
        {

            //new Card(1, "Greater Houston Ministerial Ass’n"),
            //new Card(2, "Nixon’s Knee"),
            //new Card(3, "Gallup Poll"),
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
                    AreChangesValid = (choices) =>
                    {
                        State[] newEnglandStates = [State.RI, State.MA, State.CT, State.VT, State.NH, State.ME];

                        var fiveOrFewerPointsOfStateChanges = choices.TotalStateChanges <= 5;
                        var onlyNewEnglandStatesIncluded = choices.StateChanges.Select(s => s.Target).All(x => newEnglandStates.Contains(x));
                        var statePlayerIsOnlyKennedy = choices.StateChanges.Select(x => x.Player).All(y => y == Player.Kennedy);
                        var noValueAboveTwo = choices.HighestStateChange <= 2;
                        var AndOnlyThisTypeOfTest = choices.ContainsOnlyTheseChangeTypes([ChangeType.StateSupport]);

                        return fiveOrFewerPointsOfStateChanges && onlyNewEnglandStatesIncluded && noValueAboveTwo
                                && statePlayerIsOnlyKennedy && AndOnlyThisTypeOfTest;
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
            //new Card(36, "Henry Luce"),
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
                    AreChangesValid = (choices) =>
                    {
                        var threePointsOfIssueChanges = choices.TotalIssueChanges <= 3;
                        var noValueAboveOne = choices.HighestStateChange <= 1;
                        var issuePlayerAreAllSame = choices.IssueChanges.Select(x => x.Player).Distinct().Count() == 1;
                        var AndOnlyThisTypeOfTest = choices.ContainsOnlyTheseChangeTypes([ChangeType.IssueSupport]);

                        return threePointsOfIssueChanges && noValueAboveOne
                                && issuePlayerAreAllSame && AndOnlyThisTypeOfTest;
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
                    AreChangesValid = (choices) =>{
                        //throw new NotImplementedException();
                        var fivePointsOfIssueChanges = choices.TotalStateChanges <= 5;
                        var twoPointsForTexas = choices.StateChanges.Single(x => x.Target == State.TX).Change >= 2;
                        var statePlayerIsOnlyKennedy = choices.StateChanges.Select(x => x.Player).All(y => y == Player.Kennedy);
                        var noStateAboveTwo = choices.HighestStateChange <=2;
                        var onlySouthernStates = choices.StateChanges.Select(s => s.Target).All(x => StatesByRegion[Region.South].Contains(x));
                        var AndOnlyThisTypeOfTest = choices.ContainsOnlyTheseChangeTypes([ChangeType.StateSupport]);

                        return fivePointsOfIssueChanges && noStateAboveTwo && twoPointsForTexas
                              && onlySouthernStates && statePlayerIsOnlyKennedy && AndOnlyThisTypeOfTest;
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
                    AreChangesValid = (choices) =>
                    {
                        var threePointsOfIssueChanges = choices.TotalIssueChanges == 3;
                        var onlyOneIssueIncluded = choices.IssueChanges.Where(x => x.Change > 0).Count() == 2;
                        var issuePlayerIsOnlyKennedy = choices.IssueChanges.Select(x => x.Player).All(y => y == Player.Kennedy);
                        var AndOnlyThisTypeOfTest = choices.ContainsOnlyTheseChangeTypes([ChangeType.IssueSupport]);

                        return threePointsOfIssueChanges && onlyOneIssueIncluded
                                && issuePlayerIsOnlyKennedy && AndOnlyThisTypeOfTest;
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
                    AreChangesValid = (choices) =>{
                        //This has no player choices.
						return true;

                    },
                }
            },
            //new Card(43, "Catholic Support"),
            //new Card(44, "Puerto Rican Bishops"),
            //new Card(45, "Compact Of 5th Avenue"),
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
            //new Card(60, "World Series Ends"),
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
                    AreChangesValid = (choices) => {
                        var lowVoteStates = ElectoralVotes.Where(x => x.Value <= 10).Select(y=>y.Key).ToList();
                        var westOrMidWestStates = StatesByRegion[Region.Midwest];
                        westOrMidWestStates.AddRange(StatesByRegion[Region.West]);
                        var heartlandStates = lowVoteStates.Intersect(westOrMidWestStates);

                        var onlyHeartlandStates = choices.StateChanges.Select(s => s.Target).All(x => heartlandStates.Contains(x));
                        var sevenOrFewerPointsOfStateChanges = choices.TotalStateChanges <= 7;
                        var noValueAboveOne = choices.HighestStateChange <= 1;
                        var statePlayerIsOnlyNixon = choices.StateChanges.Select(x => x.Player).All(y => y == Player.Nixon);
                        var AndOnlyThisTypeOfTest = choices.ContainsOnlyTheseChangeTypes([ChangeType.StateSupport]);

                        return onlyHeartlandStates && sevenOrFewerPointsOfStateChanges
                            && statePlayerIsOnlyNixon && noValueAboveOne && AndOnlyThisTypeOfTest;
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
                    AreChangesValid = (choices) => {
                        List<State> southernStates = StatesByRegion[Region.South];

                        var onlySouthernStatesIncluded = choices.StateChanges.Select(s => s.Target).All(x => southernStates.Contains(x));
                        var fiveOrFewerPointsOfStateChanges = choices.TotalStateChanges <= 5;
                        var noValueAboveTwo = choices.HighestStateChange <= 2;
                        var statePlayerIsOnlyNixon = choices.StateChanges.Select(x => x.Player).All(y => y == Player.Nixon);
                        var AndOnlyThisTypeOfTest = choices.ContainsOnlyTheseChangeTypes([ChangeType.StateSupport]);

                        return onlySouthernStatesIncluded && fiveOrFewerPointsOfStateChanges
                            && statePlayerIsOnlyNixon && noValueAboveTwo && AndOnlyThisTypeOfTest;
                    },
                }
            },
            //new Card(73, "Norman Vincent Peale"),
            //new Card(74, "Eisenhower’s Silence"),
            //new Card(75, "Republican TV Spots"),
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
                    AreChangesValid = (choices) => {
                        var suburbanStates = ElectoralVotes.Where(x => x.Value >= 20).Select(y=>y.Key).ToList();

                        var onlySuburbanStates = choices.StateChanges.Select(s => s.Target).All(x => suburbanStates.Contains(x));
                        var fiveOrFewerPointsOfStateChanges = choices.TotalStateChanges <= 5;
                        var noValueAboveTwo = choices.HighestStateChange <= 2;
                        var statePlayerIsOnlyKennedy = choices.StateChanges.Select(x => x.Player).All(y => y == Player.Kennedy);
                        var AndOnlyThisTypeOfTest = choices.ContainsOnlyTheseChangeTypes([ChangeType.StateSupport]);

                        return onlySuburbanStates && fiveOrFewerPointsOfStateChanges
                            && statePlayerIsOnlyKennedy && noValueAboveTwo && AndOnlyThisTypeOfTest;
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
                    AreChangesValid = (choices) => {
						//This has no player choices.
						return true;
                    },
                }
            },
            //new Card(79, "Advance Men"),
            //new Card(80, "Herblock"),
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
                    AreChangesValid = (choices) =>
                    {
                        var threePointsOfIssueChanges = choices.TotalIssueChanges == 3;
                        var issuePlayerIsOnlyNixon = choices.IssueChanges.Select(x => x.Player).All(y => y == Player.Nixon);
                        var AndOnlyThisTypeOfTest = choices.ContainsOnlyTheseChangeTypes([ChangeType.IssueSupport]);

                        return threePointsOfIssueChanges && issuePlayerIsOnlyNixon && AndOnlyThisTypeOfTest;
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
                    AreChangesValid = (choices) =>
                    {

                        var threePointsOfStateChanges = choices.TotalStateChanges == 3;
                        var statePlayerIsOnlyNixon = choices.StateChanges.Select(x => x.Player).All(y => y == Player.Nixon);
                        var onlyOneState = choices.StateChanges.Select(x => x.Target).Count() == 1;
                        var AndOnlyThisTypeOfTest = choices.ContainsOnlyTheseChangeTypes([ChangeType.StateSupport]);

                        return threePointsOfStateChanges && statePlayerIsOnlyNixon && AndOnlyThisTypeOfTest;
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
                    AreChangesValid = (choices) => {
                        var threePointsOfStateChanges = choices.TotalStateChanges <= 3;
                        var noValueAboveOne = choices.HighestStateChange <= 1;
                        var statePlayerIsOnlyKennedy = choices.StateChanges.Select(x => x.Player).All(y => y == Player.Kennedy);
                        var AndOnlyThisTypeOfTest = choices.ContainsOnlyTheseChangeTypes([ChangeType.StateSupport]);

                        return threePointsOfStateChanges && noValueAboveOne
                                && statePlayerIsOnlyKennedy && AndOnlyThisTypeOfTest;
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
