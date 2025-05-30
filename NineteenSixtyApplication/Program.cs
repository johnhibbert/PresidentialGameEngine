using PresidentialGameEngine.ClassLibrary.Components;
using PresidentialGameEngine.ClassLibrary.Data;
using PresidentialGameEngine.ClassLibrary.Engines;
using PresidentialGameEngine.ClassLibrary.Enums;
using PresidentialGameEngine.ClassLibrary.Manifests;
using PresidentialGameEngine.ClassLibrary.Randomness;
using System;


namespace NineteenSixtyApplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Console.WriteLine("This is the 1960 application.");
            Console.WriteLine("Use this to play the game");

            var engine = GetGameEngine();

            var gameState = engine.GetGameState();


            DisplayGameState(gameState);

        }


        private static void DisplayGameState(GameState<Player, Leader, Issue, State, Region> gameState) 
        {
            foreach(Player player in gameState.Momentum.Keys) 
            {
                Console.WriteLine($"{player} Momentum: {gameState.Momentum[player]}");
            }
            Console.WriteLine();
            foreach (Player player in gameState.PlayerLocations.Keys)
            {
                Console.WriteLine($"{player} in {gameState.PlayerLocations[player]}");
            }
            Console.WriteLine("Press Enter to continue.");
            Console.ReadLine();
        }


        private static NineteenSixtyGameEngine GetGameEngine() 
        {
            var ff = NineteenSixty.RegionByState;
            var ll = NineteenSixty.PlayerStartingPositions;

            var regionalComp = new RegionalComponent<State, Region, Player>(ff, ll);

            var random = new DefaultRandomnessProvider();

            var momentumComp = new AccumulatingComponent<Player>();
            var issueSupportComp = new SupportComponent<Player, Leader, Issue>();
            var stateSupportComp = new SupportComponent<Player, Leader, State>();
            var issuePositioningComp = new PositioningComponent<Issue>();
            var politicalCapitalComp = new PoliticalCapitalComponent<Player>(random, 12);
            var restComp = new AccumulatingComponent<Player>();
            var endorsement = new SupportComponent<Player, Leader, Region>();
            var mediaSupport = new SupportComponent<Player, Leader, Region>();
            var exhaustionComponent = new ExhaustionComponent<Player>();
            var cardComponent = new CardComponent<Player, Card>(random, NineteenSixty.GMTCards);

            var compColl = new ComponentCollection<Player, Leader, Issue, State, Region, Card>()
            {
                MomentumComponent = momentumComp,
                IssueSupportComponent = issueSupportComp,
                StateSupportComponent = stateSupportComp,
                IssuePositioningComponent = issuePositioningComp,
                PoliticalCapitalComponent = politicalCapitalComp,
                RegionalComponent = regionalComp,
                RestComponent = restComp,
                EndorsementComponent = endorsement,
                MediaSupportComponent = mediaSupport,
                CardComponent = cardComponent,
                ExhaustionComponent = exhaustionComponent
            };

            var engine = new NineteenSixtyGameEngine
            (
                compColl
            );

            return engine;
        }
    }
}
