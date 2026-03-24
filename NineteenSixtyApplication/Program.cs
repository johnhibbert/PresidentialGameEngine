using NineteenSixty;
using NineteenSixty.Data;
using NineteenSixty.Enums;
using NineteenSixty.Interfaces;
using PresidentialGameEngine.ClassLibrary.Components;
using PresidentialGameEngine.ClassLibrary.Interfaces;
using PresidentialGameEngine.ClassLibrary.Randomness;

namespace NineteenSixtyApplication;

internal class Program
{
    private static IController controller;
    
    static void Main(string[] args)
    {
        
        Console.WriteLine("Welcome to the 1960 application.");
        Console.WriteLine("----");
        Console.WriteLine("Press Enter to Begin");
        Console.ReadLine();
        Console.Clear();
        
        var seed = GetSeedFromUser();
        var randomnessProvider = new DefaultRandomnessProvider(seed);

        var edition = GetGameEditionFromUser();
        
        
        controller = GetController(randomnessProvider, edition);

        controller.SetUpBoard();

        var holder = controller.GetGameState();

        DisplayGameState(holder);
        int i = 0;
    }


    //private static string Border = "123456789012345678901234567890123456789012345678901234567890123456789012";
    private static string Border = " ---------------------------------------------------------------------- ";
    private static string TTTTTT = "|                                                                      |";
    private static string Label  = "|     John F. Kennedy in MA        |    Richard M. Nixon in CA         |";
    private static string UUUUU  = "| Momentum: 2, Rest: 3, Ready  |      Richard M. Nixon             |";

    // |          John F. Kennedy         |         Richard M. Nixon          |
    //  1234567890               1234567890123456789                1234567890
    
    
    //PlayerLocations
    //PlayerStatuses
    
    static void DisplayGameState(GameState gameState)
    {
        Console.Clear();
        
        
        var momentum = gameState.Momentum;
        Console.WriteLine(Border);
        Console.WriteLine(GetLineOne(gameState));
        Console.WriteLine(GetLineTwo(gameState));
        Console.WriteLine(Border);
        // Console.Write($"|     John F. Kennedy in {gameState.PlayerLocations[Player.Kennedy]}        |");
        // Console.WriteLine($"    Richard M. Nixon in {gameState.PlayerLocations[Player.Nixon]}         |");
        
        //Console.WriteLine("12345678901234567890123456789012345678901234567890123456789012345678901234567890");
        //Console.WriteLine("123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890");
        Console.WriteLine("Momentum");
        Console.Write($"Kennedy: {momentum[Player.Kennedy]} Nixon: {momentum[Player.Nixon]}");
        
        
        //gameState.
        
    }

    private static string GetLineOne(GameState gameState)
    {
        var line = "| Kennedy: ";
        line+= gameState.PlayerStatuses[Player.Kennedy] == Status.Exhausted ? "Exhausted  " : "Ready      ";
        line += $"Location: {gameState.PlayerLocations[Player.Kennedy]} ";
        line += "| ";
        line += "Nixon: ";
        line += gameState.PlayerStatuses[Player.Nixon] == Status.Exhausted ? "Exhausted  " : "Ready      ";
        line += $"  Location: {gameState.PlayerLocations[Player.Nixon]} ";
        line += "|";
        return line;
    }
    
    private static string GetLineTwo(GameState gameState)
    {
        var line = $"| Momentum: {gameState.Momentum[Player.Kennedy],-2}";
        line += $"            Rest: {gameState.RestCubes[Player.Kennedy],-2} ";
        line += $"| Momentum: {gameState.Momentum[Player.Nixon],-2}";
         line += $"            Rest: {gameState.RestCubes[Player.Nixon],-2} ";
         line += "| ";
        return line;
    }
    
    
    
    static GameEdition GetGameEditionFromUser()
    {
        Console.WriteLine("The revised GMT edition of this game included some changes");
        Console.WriteLine("most notably 6 additional cards.");
        Console.WriteLine("This program can support either game edition.");
        Console.WriteLine("----");
        Console.WriteLine("Enter 1 to use original ZMan edition");
        Console.WriteLine("Enter 2 to use the updated GMT edition");
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
        
        Console.WriteLine("This application uses a seeded randomizer.");
        Console.WriteLine("You can choose to a default seed for consistent testing");
        Console.WriteLine("You can also select your own or get a random seed.");
        Console.WriteLine("----");
        Console.WriteLine("Enter 1 to use the default seed");
        Console.WriteLine("Enter 2 to select your seed");
        Console.WriteLine("Enter 3 to get a random seed");
        var input = GetIntegerInputFromUser(3);

        switch (input)
        {
            case 1:
                //Do nothing, we set it above
                break;
            case 2:
                Console.WriteLine("Type in a valid integer.");
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
            ([CardZone.Hand, CardZone.CampaignStrategy]);

        return new Engine(momentumComponent, issueSupportComponent, stateSupportComponent, issuePositioningComponent,
            politicalCapitalComponent, playerLocationComponent, restComponent, endorsementComponent,
            mediaSupportComponent, exhaustionComponent, cardZoneComponent);
    }
    
}

