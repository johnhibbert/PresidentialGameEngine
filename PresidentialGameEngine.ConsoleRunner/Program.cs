using PresidentialGameEngine.ClassLibrary;
using PresidentialGameEngine.ClassLibrary.Components;
using PresidentialGameEngine.ClassLibrary.Data;
using PresidentialGameEngine.ClassLibrary.Enums;

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
            Console.WriteLine("1: Run simple component tests");
            Console.WriteLine("2: Display all cards");

            int userSelection = GetIntFromUser();

            switch (userSelection) 
            {
                case 1:
                    SimpleComponentTests();
                    break;
                case 2:
                    DisplayAllCards();
                    break;
                default:
                    Console.WriteLine("Input not undertood.");
                    break;

            }



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
            var random = new DefaultRandomnessProvider();

            var momentumComp = new MomentumComponent<Player>();
            var issueSupportComp = new SupportComponent<Player, Leader, Issue>();
            var stateSupportComp = new SupportComponent<Player, Leader, State>();
            var issuePositioningComp = new PositioningComponent<Issue>();
            var politicalCapitalComp = new PoliticalCapitalComponent<Player>(random, 12);

            var generic = new GenericPresidentialGameEngine<Player, Leader, Issue, State>
                (
                    momentumComp, issueSupportComp, stateSupportComp, issuePositioningComp, politicalCapitalComp
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
            generic.AddCubes(Player.Kennedy, 2);

            SupportChange<Player, Issue> nixonGainsInDefense = new(Player.Nixon, Issue.Defense, 2);
            SupportChange<Player, State> kennedyGainsInFlorida = new(Player.Kennedy, State.FL, 1);

            PlayerChosenChanges<Player, Issue, State> allChanges = new PlayerChosenChanges<Player, Issue, State>();

            allChanges.IssueChanges.Add(nixonGainsInDefense);
            allChanges.StateChanges.Add(kennedyGainsInFlorida);

            generic.ImplementChanges(allChanges);
        }


        public static void DisplayAllCards()
        {
            Console.WriteLine($"Cards implemented: {CardManifests.TheMakingOfThePresidentGMTCards.Values.Count}");
            Console.WriteLine();
            Console.WriteLine("Press Enter to continue.");
            Console.ReadLine();

            foreach (Card card in CardManifests.TheMakingOfThePresidentGMTCards.Values)
            {
                Console.Clear();
                Console.WriteLine($"Index: {card.Index}");
                Console.WriteLine($"Title: {card.Title}");
                Console.WriteLine($"CP: {card.CampaignPoints} / Rest: {card.RestCubes}");
                Console.WriteLine($"Issue: {card.Issue}");
                Console.Write($"Candidate: ");

                switch (card.Candidate)
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
