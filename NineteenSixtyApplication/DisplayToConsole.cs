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


public static class DisplayToConsole
{
    private static string CornerBorder = " --------------------------------------------------------------------- ";
    private static string Border = "-----------------------------------------------------------------------";
    private static string FullInternalBorder = "|---------------------------------------------------------------------|";
    private static string ShortInternalBorder = "---------------------------------------------------------------------";

    private static List<string> allBorders = new List<string>()
    {
        CornerBorder,
        Border,
        FullInternalBorder,
        ShortInternalBorder
    };
    
    //Maybe pass an enum for topbottom etc?
    private static void DisplayLinesInBox(IEnumerable<string> lines, bool drawBottomLine = true)
    {
        Console.WriteLine(CornerBorder);
        foreach (var line in lines)
        {
            if (allBorders.Contains(line))
            {
                Console.WriteLine(line);
            }
            else
            {
                Console.WriteLine($"| {line,-67} |");
            }
        }
        
        if (drawBottomLine)
        {
            Console.WriteLine(CornerBorder);
        }
    }
    
    
    public static void DisplayIntroMessage()
    {
        Console.WriteLine("Welcome to the 1960 application.");
        Console.WriteLine("----");
        Console.WriteLine("Press Enter to Begin");
        Console.ReadLine();
        Console.Clear();
    }

    public static void DisplayCardsForPlayer(Player player, IEnumerable<Card> cards, bool drawBottomLine = true)
    {
        List<string> lines = [];

        var line = $"{player} cards: ";
        foreach (var card in cards)
        {
            var asString = card.ToString();
            if ($"{line} {asString}".Length < 68)
            {
                line += $" {asString}";
            }
            else
            {
                lines.Add(line);
                line = $" {asString}";
            }
            
        }
        lines.Add(line);

        DisplayLinesInBox(lines, drawBottomLine);
    }

    public static void DisplayGameTime(GameTime gameTime, bool drawBottomLine = true)
    {
        string timeMessage = $"Turn {gameTime.TurnNumber}, {gameTime.CurrentPhase} Phase ";
        if (gameTime.CurrentPhase == Phase.Activity)
        {
            timeMessage += $"#{gameTime.ActivityPhaseNumber} | ";
            timeMessage += $"Active Player: {gameTime.ActivePlayer} ";
            timeMessage += gameTime.ActivePlayer == gameTime.FirstPlayer ? "(Top of Inning)" : "(Bottom of Inning)";
        }

        DisplayLinesInBox([timeMessage], drawBottomLine);
    }
    
    public static void DisplayGameState(GameState gameState)
    {
        var lines = new List<string>();
        
        lines.Add(GetLinesForPlayerStatusAndLocation(gameState));
        lines.Add(GetLinesForMomentumRestAndCardCount(gameState));
        lines.Add(FullInternalBorder);
        lines.Add(GetLinesForIssueInfo(gameState));
        lines.Add(FullInternalBorder);
        lines.AddRange(GetLinesForMediaSupportAndEndorsements(gameState));
        lines.Add(FullInternalBorder);
        lines.AddRange(GetLinesForStateContests(gameState));
        
        DisplayLinesInBox(lines);

    }
    
    private static string GetLinesForPlayerStatusAndLocation(GameState gameState)
    {
        var line = "Kennedy: ";
        line+= gameState.PlayerStatuses[Player.Kennedy] == Status.Exhausted ? "Exhausted  " : "Ready      ";
        line += $"Location: {gameState.PlayerLocations[Player.Kennedy]} ";
        line += "| ";
        line += "Nixon: ";
        line += gameState.PlayerStatuses[Player.Nixon] == Status.Exhausted ? "Exhausted  " : "Ready      ";
        line += $"  Location: {gameState.PlayerLocations[Player.Nixon]}";
        return line;
    }

    private static string GetLinesForMomentumRestAndCardCount(GameState gameState)
    {
        var line = $"Momentum: {gameState.Momentum[Player.Kennedy],-2}";
        line += $" Rest: {gameState.RestCubes[Player.Kennedy],-2} ";
        line += $" Cards: {gameState.NumberOfCardsInPlayerHands[Player.Kennedy],-2}";
        line += $"| Momentum: {gameState.Momentum[Player.Nixon],-2}";
        line += $" Rest: {gameState.RestCubes[Player.Nixon],-2} ";
        line += $" Cards: {gameState.NumberOfCardsInPlayerHands[Player.Nixon],-2}";
        return line;
    }

    private static string GetLinesForIssueInfo(GameState gameState)
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
        
        var line = $"Issues | 1st: ";
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
        return line;
    }

    private static IEnumerable<string> GetLinesForMediaSupportAndEndorsements(GameState gameState)
    {
        var endorsements = gameState.Endorsements;
        var mediaSupport = gameState.MediaSupportLevels;
        
        var mediaString = $"Media Support | Kennedy: {GetLinesForRegionalContests(mediaSupport,  Leader.Kennedy)}";
        mediaString += $"Nixon: {GetLinesForRegionalContests(mediaSupport,  Leader.Nixon)}";
        
        var endorsementString = $"Endorsements  | Kennedy: {GetLinesForRegionalContests(endorsements,  Leader.Kennedy)}";
        endorsementString += $"Nixon: {GetLinesForRegionalContests(endorsements,  Leader.Nixon)}";
        
        return [mediaString, endorsementString];
    }

    private static string GetLinesForRegionalContests(IDictionary<Region, SupportContest<Leader>> contests, Leader leader)
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

    private static List<string> GetLinesForStateContests(GameState gameState)
    {
        List<string> returnValue = new List<string>();
        
        string currentString =  string.Empty;
        currentString += "States: ";

        string div = "-";
        string empty = "X0 ";
        
        int index = 1;
        var contests = gameState.StateContests;
        
        var alphabetizedStates = contests.Keys.OrderBy(x => x);
        
        foreach(var state in alphabetizedStates)
        {
            var stateContest = contests[state];
            if (stateContest.Leader != Leader.None)
            {
                currentString += $"{state}{div}{stateContest.Leader.ToString()[..1]}{stateContest.Amount} ";
            }
            else
            {
                currentString += $"{state}{div}{empty}";
            }

            if (index % 10 == 0)
            {
                returnValue.Add(currentString.TrimEnd());
                currentString = "        ";
            }
            
            index++;
        }
        
        return returnValue;
    }
}