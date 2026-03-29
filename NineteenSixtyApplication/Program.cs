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

        //ShowGameTime();

        DisplayHands();
        
        WaitForPlayerToPressEnter();
        
        
        ConductInitiativeCheck();
        
        ClearScreen();
        
        DisplayToConsole.DisplayGameState(controller.GetGameState());
        
        /*
        var gameTime = controller.GetGameTime();
        DrawToConsole.DrawGameTime(gameTime);
*/
        
        /* THIS CODE WORKS.
        DrawToConsole.DrawGameState(controller.GetGameState());
        controller.PlayCardAsEvent(Manifest.GMTCards[5], new SetOfChanges(), Player.Kennedy);
        DrawToConsole.DrawGameState(controller.GetGameState());
        */
        
        var chosenAction = GetActionFromPlayer();

        switch (chosenAction)
        {
            case ActionType.PlayCardForEvent:
                
                break;
            default:
                break;
        }
        
        int i = 0;
    }


    static void WaitForPlayerToPressEnter()
    {
        DisplayToConsole.WaitForUserToPressEnter();
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

    static void ShowGameTime()
    {
        DisplayToConsole.DisplayGameTime(controller.GetGameTime(), false);
    }
    
    static void ConductInitiativeCheck()
    {
        var initiativeCheck = controller.ConductInitiativeCheck();
        var firstPlayer = GetFirstPlayerFromUser(initiativeCheck);
        
        controller.SetFirstPlayerForActivityPhase(firstPlayer);
    }

    static void DrawHands()
    {
        controller.DrawCards(Player.Kennedy, 6);
        controller.DrawCards(Player.Nixon, 6);
    }


    static void DisplayHands()
    {
        DisplayToConsole.DisplayGenericMessage("PLAYER HANDS".PadLeft(40), false);
        
        var kennedyCards = controller.GetCardsInHand(Player.Kennedy);
        DisplayToConsole.DisplayCardsForPlayer(Player.Kennedy, kennedyCards, false);
        
        var nixonCards = controller.GetCardsInHand(Player.Nixon);
        DisplayToConsole.DisplayCardsForPlayer(Player.Nixon, nixonCards);
    }
    
    
    static Player GetFirstPlayerFromUser(InitiativeCheckResult initiativeCheck)
    {
        var messages = new List<string>()
        {
            "You have the initiative.",
            "  You get to choose which player goes first.",
            "(It is usually advantageous to go second.)".PadLeft(67),
        };
        
        DisplayToConsole.DisplayAlertToPlayer(initiativeCheck.PlayerWithInitiative, messages, false);
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
    
    
    static ActionType GetActionFromPlayer() 
    {
        Console.WriteLine("Choose an action:");
        Console.WriteLine("1: Play card for event");
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
