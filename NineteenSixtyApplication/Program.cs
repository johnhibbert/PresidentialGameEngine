using PresidentialGameEngine.ClassLibrary.Components;
using PresidentialGameEngine.ClassLibrary.Data;
using PresidentialGameEngine.ClassLibrary.Engines;
using PresidentialGameEngine.ClassLibrary.Enums;
using PresidentialGameEngine.ClassLibrary.Interfaces;
using PresidentialGameEngine.ClassLibrary.Manifests;
using PresidentialGameEngine.ClassLibrary.Randomness;

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





            engine.DoInitialSetup();


            var gameState = engine.GetGameState();

            //Theoretical tilt application:

            //Probably want to pass this to the Engine itself instead of the pieces
            //And save it, since the Support component won't know anything about it.
            //foreach(var kvp in NineteenSixty.StateTilts) 
            //{
            //    if(kvp.Value.StartingSupport > 0) 
            //    {
            //        engine.GainSupport(kvp.Value.Player, kvp.Key, kvp.Value.StartingSupport);
            //    }
            //}



            DisplayGameState(gameState);

        }


        private static void DisplayGameState(GameState<Player, Leader, Issue, State, Region> gameState) 
        {
            Console.WriteLine("Momentum Levels:");
            foreach (Player player in gameState.Momentum.Keys) 
            {
                Console.WriteLine($"{player} Momentum: {gameState.Momentum[player]}");
            }
            Console.WriteLine();


            Console.WriteLine("Candidate Locations:");
            foreach (Player player in gameState.PlayerLocations.Keys)
            {
                Console.WriteLine($"{player} in {gameState.PlayerLocations[player]}");
            }
            Console.WriteLine();



            Console.WriteLine("State Support Levels:");
            int counter = 0;
            foreach (State state in gameState.StateContests.Keys)
            {
                var leader = gameState.StateContests[state].Leader;
                string shortLeader = "_";
                switch (leader) 
                {
                    case Leader.Kennedy:
                        shortLeader = "K";
                        break;
                    case Leader.Nixon:
                        shortLeader = "N";
                        break;
                }

                Console.Write($"{state}:{shortLeader}{gameState.StateContests[state].Amount}  ");
                counter++;
                if(counter % 10 == 0) 
                {
                    Console.WriteLine();
                }
            }



            Console.WriteLine();
            Console.WriteLine("Press Enter to continue.");
            Console.ReadLine();
        
        
        
        }


        private static NineteenSixtyGameEngine GetGameEngine() 
        {
            //var ff = NineteenSixty.RegionByState;
            var ll = NineteenSixty.PlayerStartingPositions;

            var playerLocationComp = new PlayerLocationComponent<Player, State>(ll);

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
            var staticDataComponent = new StaticDataComponent<State, Player, Region>(NineteenSixty.StateData);

            var compColl = new ComponentCollection<Player, Leader, Issue, State, Region, Card>()
            {
                MomentumComponent = momentumComp,
                IssueSupportComponent = issueSupportComp,
                StateSupportComponent = stateSupportComp,
                IssuePositioningComponent = issuePositioningComp,
                PoliticalCapitalComponent = politicalCapitalComp,
                PlayerLocationComponent = playerLocationComp,
                RestComponent = restComp,
                EndorsementComponent = endorsement,
                MediaSupportComponent = mediaSupport,
                CardComponent = cardComponent,
                ExhaustionComponent = exhaustionComponent,
                StaticDataComponent = staticDataComponent,
            };

            var engine = new NineteenSixtyGameEngine
            (
                compColl
            );

            return engine;
        }
    }
}
