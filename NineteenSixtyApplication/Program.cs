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

        var initialGameState = controller.GetGameState();
        DisplayGameState(initialGameState);
        
        //DisplayGameState(GetFakeGameState());
        
        
        int i = 0;
    }


    //private static string Border = "123456789012345678901234567890123456789012345678901234567890123456789012";
    private static string Border = " ---------------------------------------------------------------------- ";
    private static string TTTTTT = "|                                                                      |";
    private static string Label = "|     John F. Kennedy in MA        |    Richard M. Nixon in CA         |";
    private static string UUUUU = "| Momentum: 2, Rest: 3, Ready  |      Richard M. Nixon             |";

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
        Console.WriteLine(GetLinesThreeAneFour(gameState));
        Console.WriteLine(Border);
        Console.WriteLine(GetLineFive(gameState));
        Console.WriteLine(Border);
        Console.WriteLine(GetStateContestLines(gameState));
        Console.WriteLine(Border);
    }

    private static GameState GetFakeGameState()
    {
        return new GameState
        {

            IssueOrder = new List<Issue>()
            {
                Issue.CivilRights,
                Issue.Defense,
                Issue.Economy
            },
            IssueContests = new Dictionary<Issue, SupportContest<Leader>>()
            {
                {
                    Issue.Defense, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy,
                        Amount = 2
                    }
                },
                {
                    Issue.CivilRights, new SupportContest<Leader>()
                    {
                        Leader = Leader.Nixon,
                        Amount = 9
                    }
                },
                {
                Issue.Economy, new SupportContest<Leader>()
                {
                    Leader = Leader.None,
                    Amount = 0
                }
            },
            },
            Momentum = new Dictionary<Player, int>()
            {
                {Player.Kennedy, 6},
                {Player.Nixon, 3},
            },
            RestCubes = new Dictionary<Player, int>()
            {
                {Player.Kennedy, 41},
                {Player.Nixon, 10},
            },
            Endorsements = new Dictionary<Region, SupportContest<Leader>>()
            {
                {
                    Region.East, new SupportContest<Leader>()
                    {
                        Amount = 2, Leader = Leader.Kennedy
                    }
                },
                {
                    Region.Midwest, new SupportContest<Leader>()
                    {
                        Amount = 7, Leader = Leader.Kennedy
                    }
                },
                {
                    Region.South, new SupportContest<Leader>()
                    {
                        Amount = 2, Leader = Leader.Nixon
                    }
                },
                {
                    Region.West, new SupportContest<Leader>()
                    {
                        Amount = 0, Leader = Leader.None
                    }
                }
            },
            MediaSupportLevels = new Dictionary<Region, SupportContest<Leader>>()
            {
                {
                    Region.East, new SupportContest<Leader>()
                    {
                        Amount = 0, Leader = Leader.None
                    }
                },
                {
                    Region.Midwest, new SupportContest<Leader>()
                    {
                        Amount = 3, Leader = Leader.Nixon
                    }
                },
                {
                    Region.South, new SupportContest<Leader>()
                    {
                        Amount = 5, Leader = Leader.Nixon
                    }
                },
                {
                    Region.West, new SupportContest<Leader>()
                    {
                        Amount = 1, Leader = Leader.Kennedy
                    }
                }
            },
            PlayerLocations = new Dictionary<Player, State>()
            {
                { Player.Kennedy, State.RI },
                { Player.Nixon, State.OR },
            },
            PlayerStatuses = new Dictionary<Player, Status>()
            {
                { Player.Kennedy, Status.Exhausted },
                { Player.Nixon,  Status.Exhausted },
            },
            StateContests = new Dictionary<State, SupportContest<Leader>>()
            {
                { State.AK, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy, Amount = 1
                    }
                },
                { State.AL, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy, Amount = 1
                    }
                },
                { State.AR, new SupportContest<Leader>()
                    {
                        Leader = Leader.None, Amount = 0
                    }
                },
                { State.AZ, new SupportContest<Leader>()
                    {
                        Leader = Leader.Nixon, Amount = 3
                    }
                },
                { State.CA, new SupportContest<Leader>()
                    {
                        Leader = Leader.None, Amount = 0
                    }
                },
                { State.CO, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy, Amount = 3
                    }
                },
                { State.CT, new SupportContest<Leader>()
                    {
                        Leader = Leader.Nixon, Amount = 4
                    }
                },
                { State.DE, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy, Amount = 1
                    }
                },
                { State.FL, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy, Amount = 1
                    }
                },
                { State.GA, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy, Amount = 1
                    }
                },
                { State.HI, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy, Amount = 1
                    }
                },
                { State.IA, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy, Amount = 1
                    }
                },
                { State.ID, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy, Amount = 1
                    }
                },
                { State.IL, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy, Amount = 1
                    }
                },
                { State.IN, new SupportContest<Leader>()
                    {
                        Leader = Leader.None, Amount = 0
                    }
                },
                { State.KS, new SupportContest<Leader>()
                    {
                        Leader = Leader.Nixon, Amount = 3
                    }
                },
                { State.KY, new SupportContest<Leader>()
                    {
                        Leader = Leader.None, Amount = 0
                    }
                },
                { State.LA, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy, Amount = 3
                    }
                },
                { State.MA, new SupportContest<Leader>()
                    {
                        Leader = Leader.Nixon, Amount = 4
                    }
                },
                { State.MD, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy, Amount = 1
                    }
                },
                { State.ME, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy, Amount = 1
                    }
                },
                { State.MI, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy, Amount = 1
                    }
                },
                { State.MN, new SupportContest<Leader>()
                    {
                        Leader = Leader.None, Amount = 0
                    }
                },
                { State.MO, new SupportContest<Leader>()
                    {
                        Leader = Leader.Nixon, Amount = 3
                    }
                },
                { State.MS, new SupportContest<Leader>()
                    {
                        Leader = Leader.None, Amount = 0
                    }
                },
                { State.MT, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy, Amount = 3
                    }
                },
                { State.NC, new SupportContest<Leader>()
                    {
                        Leader = Leader.Nixon, Amount = 4
                    }
                },
                { State.ND, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy, Amount = 1
                    }
                },
                { State.NE, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy, Amount = 1
                    }
                },
                { State.NH, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy, Amount = 1
                    }
                },
                { State.NJ, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy, Amount = 1
                    }
                },
                { State.NM, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy, Amount = 1
                    }
                },
                { State.NV, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy, Amount = 1
                    }
                },
                { State.NY, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy, Amount = 1
                    }
                },
                { State.OH, new SupportContest<Leader>()
                    {
                        Leader = Leader.None, Amount = 0
                    }
                },
                { State.OK, new SupportContest<Leader>()
                    {
                        Leader = Leader.Nixon, Amount = 3
                    }
                },
                { State.OR, new SupportContest<Leader>()
                    {
                        Leader = Leader.None, Amount = 0
                    }
                },
                { State.PA, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy, Amount = 3
                    }
                },
                { State.RI, new SupportContest<Leader>()
                    {
                        Leader = Leader.Nixon, Amount = 4
                    }
                },
                { State.SC, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy, Amount = 1
                    }
                },
                { State.SD, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy, Amount = 1
                    }
                },
                { State.TN, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy, Amount = 1
                    }
                },
                { State.TX, new SupportContest<Leader>()
                    {
                        Leader = Leader.None, Amount = 0
                    }
                },
                { State.UT, new SupportContest<Leader>()
                    {
                        Leader = Leader.Nixon, Amount = 3
                    }
                },
                { State.VA, new SupportContest<Leader>()
                    {
                        Leader = Leader.None, Amount = 0
                    }
                },
                { State.VT, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy, Amount = 3
                    }
                },
                { State.WA, new SupportContest<Leader>()
                    {
                        Leader = Leader.Nixon, Amount = 4
                    }
                },
                { State.WI, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy, Amount = 1
                    }
                },
                { State.WV, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy, Amount = 1
                    }
                },
                { State.WY, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy, Amount = 1
                    }
                },
            },
        };


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

    private static string GetLinesThreeAneFour(GameState gameState)
    {
        var defense = "Defense      "; //13
        var civilRights = "Civil Rights "; //13
        var economy = "Economy      "; //13

        Dictionary<Issue, string> dict = new Dictionary<Issue, string>()
        {
            { Issue.Defense, defense },
            { Issue.CivilRights, civilRights },
            { Issue.Economy, economy }
        };
        
        var issueOrder = gameState.IssueOrder;
        var contests = gameState.IssueContests;
        
        var line = $"| Issues | 1st: ";
        line += dict[issueOrder[0]];
        line += "| 2nd: ";
        line += dict[issueOrder[1]];
        line += "| 3rd: ";
        line += dict[issueOrder[2]];
        line += " |";
        line += Environment.NewLine;
        line += $"|        ";
        line += $"| {contests[issueOrder[0]].Leader}: {contests[issueOrder[0]].Amount}".PadRight(20);
        line += $"| {contests[issueOrder[1]].Leader}: {contests[issueOrder[1]].Amount}".PadRight(20);
        line += $"| {contests[issueOrder[2]].Leader}: {contests[issueOrder[2]].Amount}".PadRight(20);
        line += " |";
        return line;
    }

    private static string GetLineFive(GameState gameState)
    {
        var endorsements = gameState.Endorsements;
        var mediaSupport = gameState.MediaSupportLevels;
        
        var mediaString = $"| Media Support | Kennedy: {GetLineForRegionalContests(mediaSupport,  Leader.Kennedy)}";
        mediaString += $" | Nixon: {GetLineForRegionalContests(mediaSupport,  Leader.Nixon)}";
        mediaString = mediaString.PadRight(70) + "|";
        
        var endorsementString = $"| Endorsements  | Kennedy: {GetLineForRegionalContests(endorsements,  Leader.Kennedy)}";
        endorsementString += $"| Nixon: {GetLineForRegionalContests(endorsements,  Leader.Nixon)}";
        endorsementString = endorsementString.PadRight(70) + "|"; 
        
        return mediaString + Environment.NewLine + endorsementString;
    }

    static string GetLineForRegionalContests(IDictionary<Region, SupportContest<Leader>> contests, Leader leader)
    {
        var returnValue = string.Empty;
        
        var leads =
            contests.Where(x => x.Value.Leader == leader).ToArray();
        
        if (leads.Length != 0)
        {
            foreach (var kvp in leads)
            {
                returnValue += $"{kvp.Key.ToString()[..1]}={kvp.Value.Amount} ";
                if (kvp.Key == Region.Midwest)
                {
                    returnValue = returnValue.Replace("M=", "MW=");
                }
            }
        }
        else
        {
            returnValue += "None ";
        }

        return returnValue;
    }

    static string GetStateContestLines(GameState gameState)
    {
        string returnValue =  string.Empty;
        returnValue += "| States: ";

        string div = "-";
        string empty = "X0";
        
        int index = 1;
        var contests = gameState.StateContests;
        
        var alphabetizedStates = contests.Keys.OrderBy(x => x);
        
        foreach(var state in alphabetizedStates)
        {
            var stateContest = contests[state];
            if (stateContest.Leader != Leader.None)
            {
                returnValue += $"{state}{div}{stateContest.Leader.ToString()[..1]}{stateContest.Amount} ";
            }
            else
            {
                returnValue += $"{state}{div}{empty} ";
            }

            if (index == 50)
            {
                returnValue += "|";
            }
            else if (index % 10 == 0)
            {
                returnValue += "|" + Environment.NewLine + "|         ";
            }
            
            index++;
        }
        
        
        return returnValue;
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

