using PresidentialGameEngine.ClassLibrary.Components;
using PresidentialGameEngine.ClassLibrary.Data;
using PresidentialGameEngine.ClassLibrary.Engines;
using PresidentialGameEngine.ClassLibrary.Enums;
using PresidentialGameEngine.ClassLibrary.Interfaces;
using PresidentialGameEngine.ClassLibrary.Manifests;
using PresidentialGameEngine.ClassLibrary.Randomness;

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


            var fff = new StaticDataComponent<State, Player, Region>(NineteenSixty.StateData);


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
            var stateSupportComp = new SupportComponent<Player, Leader, State>();
            var issuePositioningComp = new PositioningComponent<Issue>();
            var politicalCapitalComp = new PoliticalCapitalComponent<Player>(random, 12);
            var restComp = new AccumulatingComponent<Player>();
            var regionalComp = new PlayerLocationComponent<Player, State>(playerStartingLocations);
            var endorsementComp = new SupportComponent<Player, Leader, Region>();

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
            isReady = componentCollection.IsReady();


            


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
            var stateSupportComp = new SupportComponent<Player, Leader, State>();
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


            generic.GainEndorsement(Player.Nixon, Region.West);
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
            Console.WriteLine("Press Enter to continue.");
            Console.ReadLine();

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
