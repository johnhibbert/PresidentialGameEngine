using PresidentialGameEngine.ClassLibrary;
using PresidentialGameEngine.ClassLibrary.Components;
using PresidentialGameEngine.ClassLibrary.Data;
using PresidentialGameEngine.ClassLibrary.Engines;
using PresidentialGameEngine.ClassLibrary.Enums;
using PresidentialGameEngine.ClassLibrary.Manifests;
using System;

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
                default:
                    Console.WriteLine("Input not undertood.");
                    break;

            }



        }

        private static void SimplePlayground() 
        {
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
            var regionalComp = new RegionalComponent<State, Region, Player>(statesAndRegions, playerStartingLocations);


            ComponentCollection<Player, Leader, Issue, State, Region> componentCollection = new();

            componentCollection.MomentumComponent = momentumComp;
            componentCollection.IssueSupportComponent = issueSupportComp;
            componentCollection.StateSupportComponent = stateSupportComp;
            componentCollection.IssuePositioningComponent  = issuePositioningComp;
            componentCollection.PoliticalCapitalComponent = politicalCapitalComp;
            var isReady = componentCollection.IsReady();
            componentCollection.RestComponent = restComp;
            componentCollection.RegionalComponent = regionalComp;
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

            var regionalComp = new RegionalComponent<State, Region, Player>(statesAndRegions, playerStartingLocations);

            var regionOfRI = regionalComp.GetRegionByState(State.RI);
            var regionOfIL = regionalComp.GetRegionByState(State.IL);

            var westernStates = regionalComp.GetStatesWithinRegion(Region.West).ToList();


            var random = new DefaultRandomnessProvider();

            var momentumComp = new AccumulatingComponent<Player>();
            var issueSupportComp = new SupportComponent<Player, Leader, Issue>();
            var stateSupportComp = new SupportComponent<Player, Leader, State>();
            var issuePositioningComp = new PositioningComponent<Issue>();
            var politicalCapitalComp = new PoliticalCapitalComponent<Player>(random, 12);
            var restComp = new AccumulatingComponent<Player>();

            var compColl = new ComponentCollection<Player, Leader, Issue, State, Region>()
            {
                MomentumComponent = momentumComp,
                IssueSupportComponent = issueSupportComp,
                StateSupportComponent = stateSupportComp,
                IssuePositioningComponent = issuePositioningComp,
                PoliticalCapitalComponent = politicalCapitalComp,
                RegionalComponent = regionalComp,
                RestComponent = restComp,
            };

            var generic = new GenericPresidentialGameEngine<Player, Leader, Issue, State, Region>
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

            PlayerChosenChanges<Player, Issue, State> allChanges = new PlayerChosenChanges<Player, Issue, State>();

            allChanges.IssueChanges.Add(nixonGainsInDefense);
            allChanges.StateChanges.Add(kennedyGainsInFlorida);

            generic.ImplementChanges(allChanges);

            var kennedyHomeState = generic.GetPlayerState(Player.Kennedy);
            var allWesternStates = generic.GetStatesInRegion(Region.West);
            generic.MovePlayerToState(Player.Kennedy, State.CO);

            generic.GainRest(Player.Nixon, 2);
            var rest = generic.GetPlayerRest(Player.Nixon);
            generic.EmptyRest(Player.Nixon);
            

        }


        public static void DisplayAllCards()
        {
            Console.WriteLine($"Cards implemented: {NineteenSixty.GMTCards.Values.Count}");
            Console.WriteLine();
            Console.WriteLine("Press Enter to continue.");
            Console.ReadLine();

            foreach (NewCard card in NineteenSixty.GMTCards.Values)
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
