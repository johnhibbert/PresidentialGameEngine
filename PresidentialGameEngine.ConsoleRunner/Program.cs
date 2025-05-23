using PresidentialGameEngine.ClassLibrary;
using PresidentialGameEngine.ClassLibrary.Data;
using PresidentialGameEngine.ClassLibrary.Enums;

namespace PresidentialGameEngine.ConsoleRunner
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var choices = new PlayerChosenChanges();

            SupportChange<Issue> civilRightsSupportChange = new SupportChange<Issue>(Player.Kennedy, Issue.CivilRights, 1);
            SupportChange<Issue> defenseSupportChange = new SupportChange<Issue>(Player.Kennedy, Issue.Defense, 2);
            //SupportChange<State> stateSupportChange = new SupportChange<State>(Player.Kennedy, State.AK, -2);

            choices.IssueChanges.Add(civilRightsSupportChange);
            choices.IssueChanges.Add(defenseSupportChange);
            //choices.StateChanges.Add(stateSupportChange);

            //DisplayAllCards();

            //int cardIndex = 86;
            int cardIndex = 41;

            Card card = CardManifests.TheMakingOfThePresidentGMTCards[cardIndex];

            var holder = card.AreChangesValid(choices);


            int i = 0;

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
                    case Candidate.Kennedy:
                        Console.WriteLine("Donkey");
                        break;
                    case Candidate.Nixon:
                        Console.WriteLine("Elephant");
                        break;
                    case Candidate.Both:
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
