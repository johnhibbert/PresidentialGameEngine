using PresidentialGameEngine.ClassLibrary.Components;
using PresidentialGameEngine.ClassLibrary.Data;
using PresidentialGameEngine.ClassLibrary.Engines;
using PresidentialGameEngine.ClassLibrary.Enums;
using PresidentialGameEngine.ClassLibrary.Interfaces;
using PresidentialGameEngine.ClassLibrary.Manifests;
using PresidentialGameEngine.ClassLibrary.Randomness;
using System.Linq;

namespace PresidentialGameEngine.ConsoleRunner
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Presidential Game Engine.");
            Console.WriteLine("This exercises a class library that enforces the rules of board games based around presidential elections, such");
            Console.WriteLine("as 1960: The Making of the President.");

            Console.WriteLine();
            Console.WriteLine("Select an option:");
            Console.WriteLine("1: Simple Playground");
            Console.WriteLine("2: Run simple component tests");
            Console.WriteLine("3: Display all cards");
            //Console.WriteLine("4: Display state tilts");
            Console.WriteLine();


            int userSelection = GetIntFromUser();

            switch (userSelection) 
            {
                case 1:
                    SimplePlayground();
                    break;
                case 2:
                    SimpleComponentTests();
                    break;
                case 3:
                    DisplayAllCards();
                    break;
                //case 4:
                //    DisplayStateTilts();
                //    break;
                default:
                    Console.WriteLine("Input not undertood.");
                    break;

            }



        }

        private static void SimplePlayground() 
        {

            


            string threeStateSupportInMA = "MA+3";

            /*
            //How to encode actions?
            //State abbrev = state support followed by 1 digit number (with optional/mandatory sign?)

            MA+2RI+1

            Issues:  IC, ID, IE - Nope, conflicts with idaho
            CR, DF, EC?  Or maybe XC, XD, XE?  OR CI, DI, EI? CivilIssue, Defense Issue, Econ Issue?

            Regions:  Media support and Advertising
                East, Midwest, South and West for EMWS.  EM for east media support and EE for East endorsements

            would be EM, EE, MM, ME (-nope-)

            Maybe if X for endorsements instead of E?

            EM, EX, MM, MX, SE, SX, WE, WX

            So:
            State support: State abbreve:
            Issue support: first letter and I
            mediau support: region first letter and M.
            endorsement: region first letter and X.

            */

            //string currentString = threeStateSupportInMA;

            string[] issueKeys = ["CI", "DI", "EI"];
            string[] mediaKeys = ["EM", "MM", "SM", "WM"];
            string[] endorsementKeys = ["EX", "MX", "SX", "WX"];

            Dictionary<string, Issue> issueDict = new Dictionary<string, Issue>()
            {
                {"CI", Issue.CivilRights },
                {"DI", Issue.Defense },
                {"EI", Issue.Economy },
            };

            Dictionary<string, Region> mediaDict = new Dictionary<string, Region>()
            {
                {"EM", Region.East},
                {"MM", Region.Midwest },
                {"SM", Region.South },
                {"WM", Region.West },
            };

            Dictionary<string, Region> endorsementDict = new Dictionary<string, Region>()
            {
                {"EX", Region.East},
                {"MX", Region.Midwest },
                {"SX", Region.South },
                {"WX", Region.West },
            };


            Player currentPlayer = Player.Nixon;

            string currentString = "MA+3EX+1DI-1MM+1";

            PlayerChosenChanges<Player, Issue, State, Region> changes = new();

            while(string.IsNullOrWhiteSpace(currentString) == false) 
            {
                var chunk = currentString.Substring(0, 4);
                currentString = currentString.Substring(4);


                var target = chunk[..2];
                var digit = chunk.Substring(2, 2);


                var parsedDigit = int.Parse(digit);

                //Check if state
                if(Enum.TryParse<State>(target, out var result)) 
                {
                    var stateChange = new SupportChange<Player, State>(currentPlayer, result, parsedDigit);
                    changes.StateChanges.Add(stateChange);
                }
                else if (issueDict.TryGetValue(target, out Issue issueVal)) 
                {
                    var issueChange = new SupportChange<Player, Issue>(currentPlayer, issueVal, parsedDigit);
                    changes.IssueChanges.Add(issueChange);

                }
                else if (mediaDict.TryGetValue(target, out Region mediaVal))
                {
                    var mediaChange = new SupportChange<Player, Region>(currentPlayer, mediaVal, parsedDigit);
                    changes.MediaSupportChanges.Add(mediaChange);

                }
                else if (endorsementDict.TryGetValue(target, out Region endorsementVal))
                {
                    var endorsementChange = new SupportChange<Player, Region>(currentPlayer, endorsementVal, parsedDigit);
                    changes.EndorsementChanges.Add(endorsementChange);
                }
                else 
                {
                    throw new Exception();
                }


            }

           


            ///var fff = new StaticDataComponent<State, Player, Region>(NineteenSixty.StateData);


            //var holder = NineteenSixty.stateLocationData;
            int s = 0;

            //var ll = new StaticDataComponent<State, Player, Region>(holder);


            //staticStateData

            //StateData rhodeIsland = new()
            //{
            //    ElectoralVotes = 3,
            //    Region = Region.East,
            //    Tilt = Player.Kennedy,
            //    StartingSupportAmount = 2
            //};

            //Dictionary<State, StateData> fff = new Dictionary<State, StateData>();



            //DefaultRandomnessProvider rando = new DefaultRandomnessProvider(6);


            //CardComponent<Player, Card> cardComponent 
            //    = new(rando, NineteenSixty.GMTCards);

            //cardComponent.DrawCards(Player.Kennedy, 5);
            //cardComponent.DrawCards(Player.Nixon, 5);


            //var nixonHand = cardComponent.GetPlayerHand(Player.Nixon);
            //var kennedyHand = cardComponent.GetPlayerHand(Player.Kennedy);

            //cardComponent.MoveCardFromOneZoneToAnother(Player.Nixon, nixonHand.First(), CardZone.Hand, CardZone.Discard);
            //var discardPile = cardComponent.ViewCardsInZone(CardZone.Discard, Player.Nixon).ToList();




            //var ll = cardComponent.RetrieveCardFromDiscardToHand()


            int i = 0;

            /*
            var lowVoteStates = NineteenSixty.ElectoralVotes.Where(x => x.Value <= 10).Select(y => y.Key).ToList();
            var westOrMidWestStates = NineteenSixty.StatesByRegion[Region.Midwest];
            westOrMidWestStates.AddRange(NineteenSixty.StatesByRegion[Region.West]);
            var heartlandStates = lowVoteStates.Intersect(westOrMidWestStates);
            */


            //var holder = NineteenSixty.ElectoralVotes.Where(x => x.Value >= 20).Select(y=>y.Key).ToList();




            //var holder = NineteenSixty.StatesByRegion;


            //var oldDict = NineteenSixty.RegionByState;//.Select(x => x.Value);

            //Dictionary<Region, List<State>> newDict = [];

            //foreach (Region region in Enum.GetValues(typeof(Region)))
            //{
            //    newDict.Add(region, []);
            //}

            //foreach (State state in oldDict.Keys)
            //{
            //    newDict[oldDict[state]].Add(state);
            //}




            //foreach()


            Console.WriteLine(NineteenSixty.GMTCards[90].ToString());
            Console.WriteLine(NineteenSixty.GMTCards[90].ToLongString());

            //Code here is not permanent.
            //Not that any code is permanent.

            Dictionary<State, Region> statesAndRegions = [];
            statesAndRegions.Add(State.RI, Region.East);
            statesAndRegions.Add(State.MA, Region.East);
            statesAndRegions.Add(State.LA, Region.South);
            statesAndRegions.Add(State.FL, Region.South);
            statesAndRegions.Add(State.MI, Region.Midwest);
            statesAndRegions.Add(State.IL, Region.Midwest);
            statesAndRegions.Add(State.CO, Region.West);
            statesAndRegions.Add(State.CA, Region.West);

            Dictionary<Player, State> playerStartingLocations = [];
            playerStartingLocations.Add(Player.Nixon, State.CA);
            playerStartingLocations.Add(Player.Kennedy, State.MA);

            var random = new DefaultRandomnessProvider();

            var momentumComp = new AccumulatingComponent<Player>();
            var issueSupportComp = new SupportComponent<Player, Leader, Issue>();
            var stateSupportComp = new CarriableSupportComponent<Player, Leader, State>();
            var issuePositioningComp = new PositioningComponent<Issue>();
            var politicalCapitalComp = new PoliticalCapitalComponent<Player>(random, 12);
            var restComp = new AccumulatingComponent<Player>();
            var regionalComp = new PlayerLocationComponent<Player, State>(playerStartingLocations);
            var endorsementComp = new SupportComponent<Player, Leader, Region>();
            var staticDataComp = new StaticDataComponent<State, Player, Region>(NineteenSixty.StateData);
            var mediaSupportComponent = new SupportComponent<Player, Leader, Region>();
            var exhaustionComponent = new ExhaustionComponent<Player>();
            var cardComponent = new CardComponent<Player, Card>(random, NineteenSixty.GMTCards);

            ComponentCollection<Player, Leader, Issue, State, Region, Card> componentCollection = new();

            componentCollection.MomentumComponent = momentumComp;
            componentCollection.IssueSupportComponent = issueSupportComp;
            componentCollection.StateSupportComponent = stateSupportComp;
            componentCollection.IssuePositioningComponent  = issuePositioningComp;
            componentCollection.PoliticalCapitalComponent = politicalCapitalComp;
            var isReady = componentCollection.IsReady();
            componentCollection.RestComponent = restComp;
            componentCollection.PlayerLocationComponent = regionalComp;
            componentCollection.EndorsementComponent = endorsementComp;
            componentCollection.StaticDataComponent = staticDataComp;
            componentCollection.MediaSupportComponent = mediaSupportComponent;
            componentCollection.ExhaustionComponent = exhaustionComponent;
            componentCollection.CardComponent = cardComponent;

            isReady = componentCollection.IsReady();



            var newEngine = new NineteenSixtyGameEngine(componentCollection);

            //GameController<Player, Leader, Issue, State, Region, Card> controller
            //    = new(newEngine);

            //var card = NineteenSixty.GMTCards[3];

            //GameAction<Player, Leader, Issue, State, Region, Card> action = new()
            //{
            //    Player = Player.Kennedy,
            //    Card = card,
            //    changes = new PlayerChosenChanges<Player, Issue, State, Region>()
            //};

            //controller.PlayCardAsEvent(action);


            var NEWchanges = new NEWChangeBattery<Player,Issue,State,Region>();

            NEWchanges.StateChanges.Add(new NEWSupportChange<Player, State>(Player.Kennedy, State.RI, NEWChangeDirection.Gain, 1));


            newEngine.NEWImplementChanges(NEWchanges);


        }

        private static int GetIntFromUser() 
        {
            int returnValue = 0;

            while (returnValue == 0) {

                var input = Console.ReadLine();
                if(int.TryParse(input, out int parsed)) 
                {
                    returnValue = parsed;
                };
            }

            return returnValue;
        }

        public static void SimpleComponentTests() 
        {
            Dictionary<Player, State> playerStartingLocations = [];
            playerStartingLocations.Add(Player.Nixon, State.CA);
            playerStartingLocations.Add(Player.Kennedy, State.MA);

            var playerLocation = new PlayerLocationComponent<Player, State>(playerStartingLocations);

            var random = new DefaultRandomnessProvider();

            var momentumComp = new AccumulatingComponent<Player>();
            var issueSupportComp = new SupportComponent<Player, Leader, Issue>();
            var stateSupportComp = new CarriableSupportComponent<Player, Leader, State>();
            var issuePositioningComp = new PositioningComponent<Issue>();
            var politicalCapitalComp = new PoliticalCapitalComponent<Player>(random, 12);
            var restComp = new AccumulatingComponent<Player>();
            var endorsement = new SupportComponent<Player, Leader, Region>();
            var mediaSupport = new SupportComponent<Player, Leader, Region>();

            var compColl = new ComponentCollection<Player, Leader, Issue, State, Region, Card>()
            {
                MomentumComponent = momentumComp,
                IssueSupportComponent = issueSupportComp,
                StateSupportComponent = stateSupportComp,
                IssuePositioningComponent = issuePositioningComp,
                PoliticalCapitalComponent = politicalCapitalComp,
                PlayerLocationComponent = playerLocation,
                RestComponent = restComp,
                EndorsementComponent = endorsement,
                MediaSupportComponent = mediaSupport
            };

            var generic = new GenericPresidentialGameEngine<Player, Leader, Issue, State, Region, Card>
            (
                compColl
            );

            generic.GainMomentum(Player.Nixon, 2);
            generic.LoseMomentum(Player.Nixon, 1);

            generic.GainSupport(Player.Kennedy, Issue.CivilRights, 2);
            generic.LoseSupport(Player.Kennedy, Issue.CivilRights, 1);

            generic.GainSupport(Player.Nixon, State.AK, 3);
            generic.LoseSupport(Player.Nixon, State.AK, 2);

            List<Issue> newIssueOrder = [Issue.CivilRights, Issue.Defense, Issue.Economy];
            generic.SetIssueOrder(newIssueOrder);
            generic.MoveIssueUp(Issue.Economy);

            var initiative = generic.InitiativeCheck();
            var support = generic.SupportCheck(Player.Nixon, 3);
            generic.AddCubesToBag(Player.Kennedy, 2);

            SupportChange<Player, Issue> nixonGainsInDefense = new(Player.Nixon, Issue.Defense, 2);
            SupportChange<Player, State> kennedyGainsInFlorida = new(Player.Kennedy, State.FL, 1);

            //PlayerChosenChanges<Player, Issue, State> allChanges = new PlayerChosenChanges<Player, Issue, State>();

            //allChanges.IssueChanges.Add(nixonGainsInDefense);
            //allChanges.StateChanges.Add(kennedyGainsInFlorida);

            //generic.ImplementChanges(allChanges);

            var kennedyHomeState = generic.GetPlayerState(Player.Kennedy);
            generic.MovePlayerToState(Player.Kennedy, State.CO);

            generic.GainRest(Player.Nixon, 2);
            var rest = generic.GetPlayerRest(Player.Nixon);
            generic.EmptyRest(Player.Nixon);


            //generic.GainEndorsement(Player.Nixon, Region.West);
            var endorsementInTheWest = generic.GetEndorsementLeader(Region.West);
            var endorsementCount = generic.GetNumberOfEndorsements(Region.West);
            var endorsementInTheMidwest = generic.GetEndorsementLeader(Region.Midwest);


            generic.GainMediaSupport(Player.Kennedy, Region.East, 3);
            var mediaSupportInEast = generic.GetMediaSupportAmount(Region.East);


            Console.WriteLine();
        }


        //public static void DisplayStateTilts() 
        //{
        //    foreach (State state in NineteenSixty.StateTilts.Keys)
        //    {
        //        Tilt<Player> tilt = NineteenSixty.StateTilts[state];

        //        Console.Clear();
        //        Console.WriteLine($"State: {state}");
        //        Console.WriteLine($"Player: {tilt.Player}");
        //        Console.WriteLine($"Support: {tilt.StartingSupport}");

        //        Console.WriteLine();
        //        Console.WriteLine("Press Enter to continue.");
        //        Console.ReadLine();
        //    }
        //}

        public static void DisplayAllCards()
        {
            Console.WriteLine($"Cards implemented: {NineteenSixty.GMTCards.Values.Count}");
            Console.WriteLine();

            int i = 1;

            while (i <= 97)
            {
                if (NineteenSixty.GMTCards.TryGetValue(i, out Card? value))
                {
                    var card = value;
                    Console.WriteLine($"[{card.Index,2}] {card.Title}");
                }
                else 
                {
                    Console.WriteLine($"[{i,2}] - - -");
                }
                i++;
            }

            Console.WriteLine();
            Console.WriteLine("Press Enter to continue.");
            Console.ReadLine();
            Console.Clear();

            foreach (Card card in NineteenSixty.GMTCards.Values)
            {
                Console.Clear();
                Console.WriteLine($"Index: {card.Index}");
                Console.WriteLine($"Title: {card.Title}");
                Console.WriteLine($"CP: {card.CampaignPoints} / Rest: {card.RestCubes}");
                Console.WriteLine($"Issue: {card.Issue}");
                Console.Write($"Candidate: ");

                switch (card.Affiliation)
                {
                    case Affiliation.Kennedy:
                        Console.WriteLine("Donkey");
                        break;
                    case Affiliation.Nixon:
                        Console.WriteLine("Elephant");
                        break;
                    case Affiliation.Both:
                        Console.WriteLine("Both");
                        break;
                    default:
                        Console.WriteLine("None");
                        break;
                }

                Console.WriteLine($"State: {card.State}");
                Console.WriteLine();
                Console.WriteLine($"Text: {card.Text}");

                Console.WriteLine();
                Console.WriteLine("Press Enter to continue.");
                Console.ReadLine();
            }
        }
    }
}
