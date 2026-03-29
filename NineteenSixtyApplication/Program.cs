using NineteenSixty;
using NineteenSixty.Data;
using NineteenSixty.Enums;
using NineteenSixty.Interfaces;
using PresidentialGameEngine.ClassLibrary.Components;
using PresidentialGameEngine.ClassLibrary.Data;
using PresidentialGameEngine.ClassLibrary.Interfaces;
using PresidentialGameEngine.ClassLibrary.Randomness;
using Card = NineteenSixty.Data.Card;

namespace NineteenSixtyApplication;

internal class Program
{
    private static IController controller;

    static void Main(string[] args)
    {
        ShowIntroMessage();
        
        DoInitialSetup();
        
        DrawHands();
        DisplayHands();
        WaitForPlayerToPressEnter(BoxForm.OnlyBottom);
        ClearScreen();
        
        ConductInitiativeCheck();
        ClearScreen();
        
        ShowGameTime(BoxForm.OnlyTop);
        
        //DisplayToConsole.DisplayGameState(controller.GetGameState());
        
        /*
        var gameTime = controller.GetGameTime();
        DrawToConsole.DrawGameTime(gameTime);
*/
        
        /* THIS CODE WORKS.
        DrawToConsole.DrawGameState(controller.GetGameState());
        controller.PlayCardAsEvent(Manifest.GMTCards[5], new SetOfChanges(), Player.Kennedy);
        DrawToConsole.DrawGameState(controller.GetGameState());
        */


        var gameTime = controller.GetGameTime();
        var chosenAction = GetActionFromUser();

        switch (chosenAction)
        {
            case ActionType.PlayCardForEvent:
                PlayCardAsEvent(gameTime.ActivePlayer);
                break;
            default:
                break;
        }
        
        int i = 0;
    }


    static void WaitForPlayerToPressEnter(BoxForm boxForm)
    {
        DisplayToConsole.WaitForUserToPressEnter(boxForm);
    }
    
    static void ClearScreen()
    {
        Console.Clear();
    }
    
    static void ShowIntroMessage()
    {
        DisplayToConsole.DisplayIntroMessage();
    }
    
    static void DoInitialSetup()
    {
        var seed = GetSeedFromUser();
        var randomnessProvider = new DefaultRandomnessProvider(seed);

        var edition = GetGameEditionFromUser();

        controller = GetController(randomnessProvider, edition);

        controller.SetUpBoard();

    }

    static void ShowGameTime(BoxForm boxForm)
    {
        DisplayToConsole.DisplayGameTime(controller.GetGameTime(), boxForm);
    }
    
    static void ConductInitiativeCheck()
    {
        var turn = controller.GetGameTime().TurnNumber;
        var initiativeCheck = controller.ConductInitiativeCheck();
        var firstPlayer = GetFirstPlayerFromUser(initiativeCheck, turn);
        
        controller.SetFirstPlayerForActivityPhase(firstPlayer);
    }

    static void DrawHands()
    {
        controller.DrawCards(Player.Kennedy, 6);
        controller.DrawCards(Player.Nixon, 6);
    }


    static void DisplayHands()
    {
        DisplayToConsole.DisplayGenericMessage("PLAYER HANDS".PadLeft(40), BoxForm.OnlyTop);
        
        var kennedyCards = controller.GetCardsInHand(Player.Kennedy);
        DisplayToConsole.DisplayCardsForPlayerInParagraph(Player.Kennedy, kennedyCards, BoxForm.OnlyTop);
        
        var nixonCards = controller.GetCardsInHand(Player.Nixon);
        DisplayToConsole.DisplayCardsForPlayerInParagraph(Player.Nixon, nixonCards);
    }
    
    
    static void PlayCardAsEvent(Player player)
    {
        DisplayToConsole.DisplayGameState(controller.GetGameState());
        
        DisplayToConsole.DisplayGenericMessage($"{player} player: Select a card:", BoxForm.OnlyTop);
        
        var hand = controller.GetCardsInHand(player).ToList();
        DisplayToConsole.DisplayCardsInList(hand);
        //var holder = GetIntegerInputFromUser();

        var chosenInt = GetIntegerInputFromUser(hand.Select(x => x.Index));

        var cardToPlay = hand.Single(x => x.Index == chosenInt);
        
        controller.PlayCardAsEvent(cardToPlay, null, player);
        
        int i = 0;
        
        DisplayToConsole.DisplayGameState(controller.GetGameState());
    }
    
    
    
    
    
    
    static Player GetFirstPlayerFromUser(InitiativeCheckResult initiativeCheck, int turnNumber)
    {
        var messages = new List<string>()
        {
            $"You have the initiative for Turn #{turnNumber}.",
            "  You get to choose which player goes first.",
            "(It is usually advantageous to go second.)".PadLeft(67),
        };
        
        DisplayToConsole.DisplayAlertToPlayer(initiativeCheck.PlayerWithInitiative, messages, BoxForm.OnlyTop);
        return GetPlayerFromUser();
    }
    
    
    static GameEdition GetGameEditionFromUser()
    {
        DisplayToConsole.DisplayGameEditionMessage();
        var input = GetIntegerInputFromUser(2);

        switch (input)
        {
            case 1:
                return GameEdition.FirstEditionByZMan;
            default:
                return GameEdition.SecondEditionByGmt;
        }
    }
    

    static int GetSeedFromUser()
    {
        var returnValue = 1960;
        
        DisplayToConsole.DisplayRandomizerSeedMessage();
        
        var input = GetIntegerInputFromUser(3);

        switch (input)
        {
            case 1:
                //Do nothing, we set it above
                break;
            case 2:
                DisplayToConsole.DisplaySeedValueRequestMessage();
                returnValue = GetIntegerInputFromUser(int.MaxValue);
                break;
            case 3:
                var rnd =  new Random();
                returnValue = rnd.Next();
                break;
        }
        
        return returnValue;
    }
    
    
    static Player GetPlayerFromUser()
    {
        DisplayToConsole.DisplayRequestForPlayer();
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
    
    
    static ActionType GetActionFromUser()
    {
        DisplayToConsole.DisplayRequestForAction();
        var intFromUser = GetIntegerInputFromUser(1);

        return (ActionType)intFromUser;
    }


    
    
    
    
    
    
    private static IController GetController(IRandomnessProvider randomnessProvider, GameEdition edition)
    {
        return new Controller(GetEngine(randomnessProvider, edition), edition);
    }

    private static IEngine GetEngine(IRandomnessProvider randomnessProvider, GameEdition edition)
    {
        var momentumComponent = new AccumulatingComponent<Player>();
        var issueSupportComponent = new SupportComponent<Player, Leader, Issue>();
        var stateSupportComponent = new CarriableSupportComponent<Player, Leader, State>();
        var issuePositioningComponent = new PositioningComponent<Issue>();
        var politicalCapitalComponent = new BlindBagComponent<Player>(Manifest.StartingPoliticalCapital[edition], randomnessProvider);
        var playerLocationComponent = new PlayerLocationComponent<Player, State>(Manifest.PlayerStartingPositions);
        var restComponent = new AccumulatingComponent<Player>();
        var endorsementComponent = new SupportComponent<Player, Leader, Region>();
        var mediaSupportComponent = new SupportComponent<Player, Leader, Region>();
        var exhaustionComponent = new PlayerStatusComponent<Player, Status>();
        var cardZoneComponent = new CardZoneComponent<CardZone, Player, Card>
            ([CardZone.Hand, CardZone.CampaignStrategy], randomnessProvider);

        return new Engine(momentumComponent, issueSupportComponent, stateSupportComponent, issuePositioningComponent,
            politicalCapitalComponent, playerLocationComponent, restComponent, endorsementComponent,
            mediaSupportComponent, exhaustionComponent, cardZoneComponent);
    }
    
}

public enum ActionType
{
    PlayCardForEvent = 1,
}
