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


            //NewPlayerChosenChanges
            //NewSupportChange

            SupportChange<Player, Issue> nixonGainsInDefense = new(Player.Nixon, Issue.Defense, 2);
            SupportChange<Player, State> kennedyGainsInFlorida = new(Player.Kennedy, State.FL, 1);

            PlayerChosenChanges<Player, Issue, State> allChanges = new PlayerChosenChanges<Player, Issue, State>();

            allChanges.IssueChanges.Add(nixonGainsInDefense);
            allChanges.StateChanges.Add(kennedyGainsInFlorida);

            generic.ImplementChanges(allChanges);

            //var choices = new PlayerChosenChanges();

            //SupportChange<Issue> civilRightsSupportChange = new SupportChange<Issue>(Player.Kennedy, Issue.CivilRights, 1);
            //SupportChange<Issue> defenseSupportChange = new SupportChange<Issue>(Player.Kennedy, Issue.Defense, 2);
            ////SupportChange<State> stateSupportChange = new SupportChange<State>(Player.Kennedy, State.AK, -2);

            //choices.IssueChanges.Add(civilRightsSupportChange);
            //choices.IssueChanges.Add(defenseSupportChange);
            //choices.StateChanges.Add(stateSupportChange);

            DisplayAllCards();

            //int cardIndex = 86;
            //int cardIndex = 41;

            //Card card = CardManifests.TheMakingOfThePresidentGMTCards[cardIndex];

            //var holder = card.AreChangesValid(choices);


            //int i = 0;

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
