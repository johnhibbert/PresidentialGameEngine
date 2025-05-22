using PresidentialGameEngine.ClassLibrary;
using PresidentialGameEngine.ClassLibrary.Data;
using PresidentialGameEngine.ClassLibrary.Enums;

namespace PresidentialGameEngine.ConsoleRunner
{
    internal class Program
    {
        static void Main(string[] args)
        {

            DisplayAllCards();

            //Console.WriteLine($"Cards implemented: {CardManifests.TheMakingOfThePresidentGMTCards.Values.Count}");
            //Console.WriteLine();
            //Console.WriteLine("Press Enter to continue.");
            //Console.ReadLine();

            //foreach (Card card in CardManifests.TheMakingOfThePresidentGMTCards.Values) 
            //{
            //    Console.Clear();
            //    Console.WriteLine($"Index: {card.Index}");
            //    Console.WriteLine($"Title: {card.Title}");
            //    Console.WriteLine($"CP: {card.CampaignPoints} / Rest: {card.RestCubes}");
            //    Console.WriteLine($"Issue: {card.Issue}");
            //    Console.Write($"Candidate: ");

            //    switch (card.Candidate) 
            //    {
            //        case Candidate.Kennedy:
            //            Console.WriteLine("Dem");
            //            break;
            //        case Candidate.Nixon:
            //            Console.WriteLine("Rep");
            //            break;
            //        case Candidate.Both:
            //            Console.WriteLine("Both");
            //            break;
            //        default:
            //            Console.WriteLine("None");
            //            break;
            //    }

            //    Console.WriteLine($"State: {card.State}");
            //    Console.WriteLine();
            //    Console.WriteLine($"Text: {card.Text}");

            //    Console.WriteLine();
            //    Console.WriteLine("Press Enter to continue.");
            //    Console.ReadLine();

            //}



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
