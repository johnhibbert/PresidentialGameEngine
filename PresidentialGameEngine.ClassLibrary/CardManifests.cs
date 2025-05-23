using PresidentialGameEngine.ClassLibrary.Data;
using PresidentialGameEngine.ClassLibrary.Enums;

namespace PresidentialGameEngine.ClassLibrary
{
    public class CardManifests
    {

        public static readonly Dictionary<int, Card> TheMakingOfThePresidentZManCards = new()
        {




            //new Card(1, "Greater Houston Ministerial Ass’n"),
            //new Card(2, "Nixon’s Knee"),
            //new Card(3, "Gallup Poll"),
            //new Card(4, "Citizens for Nixon-Lodge"),
            {5, new Card(5, "Volunteers", "Player gains 1 momentum marker.", 2, Issue.Defense, Candidate.Both, State.OR)
                {
                    Event = (engine,player,choices) => {
                        engine.GainMomentum(player, 1);
                    }
                }
            },
            //new Card(6, "New England"),
            //new Card(7, "Late Returns From Cook County"),
            {8, new Card(8, "Soviet Economic Growth", "Economy moves up one space on the Issue Track.  The leader in Economy gains 1 state support in New York.", 2, Issue.Economy, Candidate.Both, State.NH)
                {
                    Event = (engine,player,choices) => {
                        engine.MoveIssueUp(Issue.Economy);
                        var econLeader = engine.GetIssueLeader(Issue.Economy);
                        if(econLeader != Player.None)
                        {
                            engine.GainStateSupport(econLeader, State.NY, 1);
                        }
                    }
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
            //new Card(22, "Gaffe"),
            {23, new Card(23, "Martin Luther King Arrested", "Civil Rights moves up one space on the Issue Track.  Player gains 3 issue support in Civil Rights.\r\n", 4, Issue.CivilRights, Candidate.Both, State.CA)
                {
                    Event = (engine,player,choices) => {
                        engine.MoveIssueUp(Issue.CivilRights);
                        engine.GainIssueSupport(player, Issue.CivilRights, 3);
                    }
                }
            },
            //new Card(24, "East Harlem Pledge"),
            {25, new Card(25, "1960 Civil Rights Act", "Civil Rights moves up one space on the Issue Track and Nixon gains 1 issue support in Civil Rights.", 2, Issue.CivilRights, Candidate.Nixon, State.ND)
                {
                    Event = (engine,player,choices) => {
                        engine.MoveIssueUp(Issue.CivilRights);
                        engine.GainIssueSupport(Player.Nixon, Issue.CivilRights, 1);
                    }
                }
            },
            //new Card(26, "Ken-Air"),
            //new Card(27, "Nelson Rockefeller"),
            //new Card(28, "Harry F. Byrd"),
            //new Card(29, "The Great Seal Bug"),
            //new Card(30, "Johnson Jeered In Dallas"),
            //new Card(31, "Profiles In Courage"),
            //new Card(32, "Early Returns From Connecticut"),
            //new Card(33, "Unpledged Electors"),
            //new Card(34, "“Lazy Shave”"),
            //new Card(35, "Harvard Brain Trust"),
            //new Card(36, "Henry Luce"),
            //new Card(37, "Lunch Counter Sit-Ins"),
            //new Card(38, "“High Hopes”"),
            //new Card(39, "Lyndon Johnson"),
            //new Card(40, "Northern Blacks"),
            {41, new Card(41, "Pierre Salinger", "The Kennedy player may add 3 issue support in any one issue.", 3, Issue.CivilRights, Candidate.Kennedy, State.AL)
                {
                    Event = (engine, player,choices) => {

                        engine.ImplementChanges(choices);
                        //throw new NotImplementedException();
                    }
                }
            },
            //new Card(42, "Henry Cabot Lodge"),
            //new Card(43, "Catholic Support"),
            //new Card(44, "Puerto Rican Bishops"),
            //new Card(45, "Compact Of 5th Avenue"),
            //new Card(46, "Prime-Time Television"),
            //new Card(47, "The Cold War"),
            {48, new Card(48, "Rising Food Prices", "Economy moves up one space on the Issue Track and Nixon gains 2 issue support in Economy.", 3, Issue.CivilRights, Candidate.Nixon, State.IA)
                {
                    Event = (engine, player,choices) => {
                        engine.MoveIssueUp(Issue.Economy);
                        engine.GainIssueSupport(Player.Nixon, Issue.Economy, 2);
                    }
                }
            },
            //new Card(49, "Eleanor Roosevelt’s Speaking Tour"),
            //new Card(50, "Industrial Midwest"),
            {51, new Card(51, "Missile Gap", "Kennedy gains 3 issue support in Defense.", 3, Issue.Economy, Candidate.Kennedy, State.GA)
                {
                    Event = (engine, player,choices) => {
                        engine.GainIssueSupport(Player.Kennedy, Issue.Defense, 3);
                    }
                }
            },
            //new Card(52, "Hurricane Donna"),
            //new Card(53, "Campaign Headquarters"),
            //new Card(54, "Bobby Kennedy"),
            //new Card(55, "Hostile Press Corps"),
            //new Card(56, "Opposition Research"),
            //new Card(57, "“A New Frontier”"),
            //new Card(58, "Tricky Dick"),
            //new Card(59, "Mid-Atlantic"),
            //new Card(60, "World Series Ends"),
            //new Card(61, "Fatigue Sets In"),
            {62, new Card(62, "Trial of Gary Powers", "Defense moves up two spaces on the Issue Track.  The leader in Defense gains 1 momentum marker.", 3, Issue.Economy, Candidate.Both, State.WI)
                {
                    Event = (engine, player,choices) => {
                        engine.MoveIssueUp(Issue.Defense);
                        engine.MoveIssueUp(Issue.Defense);
                        var leader = engine.GetIssueLeader(Issue.Defense);
                        if(leader != Player.None)
                        {
                            engine.GainMomentum(leader, 1);
                        }
                    }
                }
            },
            {63, new Card(63, "“Give Me A Week”", "The Nixon player loses 2 momentum markers and must subtract 1 issue support in each issue.", 4, Issue.Economy, Candidate.Kennedy, State.OH)
                {
                    Event = (engine, player,choices) => {
                        engine.LoseMomentum(Player.Nixon, 2);
                        engine.LoseIssueSupport(Player.Nixon, Issue.Defense, 1);
                        engine.LoseIssueSupport(Player.Nixon, Issue.Economy, 1);
                        engine.LoseIssueSupport(Player.Nixon, Issue.CivilRights, 1);
                    }
                }
            },
            {64, new Card(64, "Stump Speech", "If opponent has more momentum markers, player gains enough to have the same number.", 4, Issue.Economy, Candidate.Both, State.OH)
                {
                    Event = (engine, player,choices) => {
                        var playerMomentum = engine.GetPlayerMomentum(player);
                        var opponentMomentum = engine.GetPlayerMomentum(player.ToOpponent());

                        if(opponentMomentum > playerMomentum)
                        {
                            engine.GainMomentum(player, opponentMomentum - playerMomentum);
                        }
                    }
                }
            },
            //new Card(65, "Joe Kennedy"),
            //new Card(66, "Adlai Stevenson"),
            //new Card(67, "Voter Registration Drive"),
            {68, new Card(68, "“Peace Without Surrender”", "Defense moves up one space on the Issue Track and Nixon gains 1 issue support in Defense.", 2, Issue.Defense, Candidate.Nixon, State.CO)
                {
                    Event = (engine, player,choices) => {
                        engine.MoveIssueUp(Issue.Defense);
                        engine.GainIssueSupport(Player.Nixon, Issue.Defense, 1);
                    }
                }
            },
            //new Card(69, "Congressional Summer Session"),
            {70, new Card(70, "The Old Nixon", "The Nixon player loses 1 momentum marker.  The Kennedy player loses 3 momentum markers.", 4, Issue.Economy, Candidate.Nixon, State.IL)
                {
                    Event = (engine, player,choices) => {
                        engine.LoseMomentum(Player.Nixon, 1);
                        engine.LoseMomentum(Player.Kennedy, 3);
                    }
                }
            },

            //new Card(71, "Heartland of America"),
            //new Card(72, "Southern Revolt"),
            //new Card(73, "Norman Vincent Peale"),
            //new Card(74, "Eisenhower’s Silence"),
            //new Card(75, "Republican TV Spots"),
            //new Card(76, "Nixon’s Pledge"),
            //new Card(77, "Suburban Voters"),
            {78, new Card(78, "Stock Market In Decline", "Economy moves up two spaces on the Issue Track. The leader in Economy gains 2 state support in New York.", 3, Issue.Defense, Candidate.Both, State.TN)
                {
                    Event = (engine, player,choices) => {
                        engine.MoveIssueUp(Issue.Economy);
                        engine.MoveIssueUp(Issue.Economy);

                        var econLeader = engine.GetIssueLeader(Issue.Economy);
                        if(econLeader != Player.None)
                        {
                            engine.GainStateSupport(econLeader, State.NY, 2);
                        }
                    }
                }
            },
            //new Card(79, "Advance Men"),
            //new Card(80, "Herblock"),
            //new Card(81, "Kennedy’s Peace Corps"),
            {82, new Card(82, "Fidel Castro", "The leader in Defense gains 1 momentum marker and 1 state support in Florida.", 2, Issue.Economy, Candidate.Both, State.ID)
                {
                    Event = (engine, player,choices) => {
                        var defenseLeader = engine.GetIssueLeader(Issue.Defense);

                        if(defenseLeader != Player.None)
                        {
                            engine.GainMomentum(defenseLeader, 1);
                            engine.GainStateSupport(defenseLeader, State.FL, 1);
                        }
                    }
                }
            },
            //new Card(83, "Whistlestop"),
            //new Card(84, "Quemoy and Matsu"),
            //new Card(85, "Jackie Kennedy"),
            //new Card(86, "Herb Klein"),
            //new Card(87, "Stevenson Loyalists"),
            //new Card(88, "Stature Gap"),
            {89, new Card(89, "The New Nixon", "The Nixon player gains 1 momentum marker.", 2, Issue.CivilRights, Candidate.Nixon, State.KS)
                {
                    Event = (engine, player,choices) => {
                        engine.GainMomentum(Player.Nixon, 1);
                    }
                }
            },
            //new Card(90, "Recount"),
            //new Card(91, "Political Capital"),
            
            };

        private static readonly Dictionary<int, Card> GMT_OnlyCards = new()
        {
            //new Card(92, "Give ‘Em Hell Harry"),
            //new Card(93, "Experience Counts"),
            {93, new Card(93, "Experience Counts", "Kennedy loses 1 issue support in each issue.  The Nixon player gains one momentum marker.", 4, Issue.Defense, Candidate.Nixon, State.CA)
                {
                    Event = (engine, player,choices) => {
                        engine.LoseIssueSupport(Player.Kennedy, Issue.Defense, 1);
                        engine.LoseIssueSupport(Player.Kennedy, Issue.CivilRights, 1);
                        engine.LoseIssueSupport(Player.Kennedy, Issue.Economy, 1);
                        engine.GainMomentum(Player.Nixon, 1);
                    }
                }
            },
            //new Card(94, "A Low Blow"),
            //new Card(95, "A Time For Greatness?"),
            {96, new Card(96, "Medal Count", "The leaders in Civil Rights and Economy lose 1 issue support in those issues.  If the same player leads both, they also lose 1 momentum marker.", 2, Issue.Defense, Candidate.Both, State.PA)
                {
                    Event = (engine, player,choices) => {

                        var civilRightsLeader = engine.GetIssueLeader(Issue.CivilRights);
                        var econLeader = engine.GetIssueLeader(Issue.Economy);

                        if(civilRightsLeader == econLeader && civilRightsLeader != Player.None)
                        {
                            engine.LoseMomentum(civilRightsLeader, 1);
                        }

                        engine.LoseIssueSupport(civilRightsLeader, Issue.CivilRights, 1);
                        engine.LoseIssueSupport(econLeader, Issue.Economy, 1);
                    }
                }
            },
            {97, new Card(97, "Cassius Clay Wins Gold", "The leaders in Defense and Economy lose 1 issue support in those issues.  If the same player leads both, they also lose 1 momentum marker.", 2, Issue.CivilRights, Candidate.Both, State.OH)
                {
                    Event = (engine, player,choices) => {

                        var defenseLeader = engine.GetIssueLeader(Issue.Defense);
                        var econLeader = engine.GetIssueLeader(Issue.Economy);

                        if(defenseLeader == econLeader && defenseLeader != Player.None)
                        {
                            engine.LoseMomentum(defenseLeader, 1);
                        }

                        engine.LoseIssueSupport(defenseLeader, Issue.Defense, 1);
                        engine.LoseIssueSupport(econLeader, Issue.Economy, 1);
                    }
                }
            },
        };

        public static readonly Dictionary<int, Card> TheMakingOfThePresidentGMTCards =
            TheMakingOfThePresidentZManCards.Concat(GMT_OnlyCards).ToDictionary(x => x.Key, x => x.Value);
    }
}
