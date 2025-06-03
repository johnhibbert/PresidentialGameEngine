using PresidentialGameEngine.ClassLibrary.Components;
using PresidentialGameEngine.ClassLibrary.Data;
using PresidentialGameEngine.ClassLibrary.Engines;
using PresidentialGameEngine.ClassLibrary.Enums;
using PresidentialGameEngine.ClassLibrary.Interfaces;
using PresidentialGameEngine.ClassLibrary.Manifests;
using PresidentialGameEngine.ClassLibrary.Randomness;
using System.ComponentModel.DataAnnotations;

namespace NineteenSixtyApplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Console.WriteLine("This is the 1960 application.");
            Console.WriteLine("Use this to play the game");
            Console.WriteLine("----");
            Console.WriteLine("Press Enter to Begin");
            Console.ReadLine();
            Console.Clear();

            var engine = GetGameEngine(12);





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



            //DisplayGameState(gameState);

            int turn = 1;



            while (turn < 5)
            {
                engine.DrawCards(Player.Nixon, 6);
                engine.DrawCards(Player.Kennedy, 6);

                int phase = 1;

                Console.WriteLine($"---START OF TURN {turn}---");

                var firstPlayer = GetFirstPlayerForTurn(engine);

                var currentPlayer = firstPlayer;

                while (phase <= 5)
                {
                    Console.WriteLine($"Turn {turn}, Phase {phase}: {currentPlayer}");

                    var hand = engine.GetPlayerHand(currentPlayer);

                    DisplayGameState(engine.GetGameState(), false);

                    ShowCards(hand);

                    var card = GetCardFromPlayer(hand);

                    if(card.RequiresPlayerInput == false) 
                    {
                        card.Event(engine, currentPlayer, null);
                        //DisplayGameState(engine.GetGameState(), false);
                        engine.MoveCardFromHandToRemovedPile(currentPlayer, card);
                    }
                    else
                    {
                        //Placeholder, just discarding it.
                        engine.DiscardCardFromHand(currentPlayer, card);
                    }
                    //if(currentPlayer.ToOpponent().ToAffiliation() == card.Affiliation) 
                    //{
                    //    Console.WriteLine("You sure, bro?");
                    //}



                    if (currentPlayer != firstPlayer)
                    {
                        phase++;
                    }

                    currentPlayer = currentPlayer.ToOpponent();

                    Console.WriteLine();
                }

                //Placeholder, just discarding it.
                var nixonHand = engine.GetPlayerHand(Player.Nixon);
                engine.DiscardCardFromHand(Player.Nixon, nixonHand.First());

                var kennedyHand = engine.GetPlayerHand(Player.Kennedy);
                engine.DiscardCardFromHand(Player.Kennedy, kennedyHand.First());

                turn++;
        }


            //var playerWithInitiative = engine.InitiativeCheck();

            //Console.WriteLine($"It is {playerWithInitiative}'s turn.");



            ////int turnTracker = 1;

            //var hand = engine.GetPlayerHand(playerWithInitiative);

            //hand.First().Event(engine, playerWithInitiative,new PlayerChosenChanges<Player, Issue, State>());



            Console.WriteLine("Game Over?");


        }



        //private void GetActionTypeFromPlayer() 
        //{
            
        //}


        private static void ShowCards(IEnumerable<Card> cards) 
        {
            foreach(Card card in cards.OrderBy(x => x.Index)) 
            {
                Console.WriteLine($"[{card.Index,2}] {card.Title}");
            }
        }

        private static Card GetCardFromPlayer(IEnumerable<Card> cards) 
        {
            Console.WriteLine("Select a card with its index.");

            var index = GetIntegerInputFromUser(cards.Select(x => x.Index));

            return cards.Single(x => x.Index == index);

        }

        private static Player GetFirstPlayerForTurn(NineteenSixtyGameEngine engine) 
        {

            var playerWithInitiative = engine.InitiativeCheck();

            Console.WriteLine($"{playerWithInitiative}: You have the initiative.");
            Console.WriteLine($"You can choose which player takes the first action.");

            return GetPlayerFromUser();



        }



        static Player GetPlayerFromUser() 
        {
            Console.WriteLine("Type 1 for Kennedy, 2 for Nixon.");
            var intFromUser = GetIntegerInputFromUser(2);

            return intFromUser switch
            {
                1 => Player.Kennedy,
                _ => Player.Nixon,
            };
        }


        static int GetIntegerInputFromUser(int maxValue)
        {
            return GetIntegerInputFromUser(Enumerable.Range(1, maxValue));
        }

        static int GetIntegerInputFromUser(IEnumerable<int> validInts)
        {
            int returnValue = 0;

            bool inputUnderstood = false;

            while (inputUnderstood == false)
            {
                Console.Write(">  ");
                var input = Console.ReadLine();

                if (int.TryParse(input, out int value) && validInts.Contains(value))
                {
                    inputUnderstood = true;
                    returnValue = value;
                }
                else
                {
                    Console.WriteLine("Input not understood.");
                }
            }

            return returnValue;
        }


        private static void DisplayGameState(GameState<Player, Leader, Issue, State, Region> gameState, bool pressEnterToContinue)
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
                if (counter % 10 == 0)
                {
                    Console.WriteLine();
                }




            }


            if (pressEnterToContinue)
            {

                Console.WriteLine();
                Console.WriteLine("Press Enter to continue.");
                Console.ReadLine();

            }
        
        }


        private static NineteenSixtyGameEngine GetGameEngine(int seed = 0) 
        {
            //var ff = NineteenSixty.RegionByState;
            var ll = NineteenSixty.PlayerStartingPositions;

            var playerLocationComp = new PlayerLocationComponent<Player, State>(ll);

            var random = new DefaultRandomnessProvider(seed);


            var momentumComp = new AccumulatingComponent<Player>();
            var issueSupportComp = new SupportComponent<Player, Leader, Issue>();
            var stateSupportComp = new CarriableSupportComponent<Player, Leader, State>();
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
