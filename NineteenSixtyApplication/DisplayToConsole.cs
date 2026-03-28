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
    private static string Border = " --------------------------------------------------------------------- ";

    public static void DisplayIntroMessage()
    {
        Console.WriteLine("Welcome to the 1960 application.");
        Console.WriteLine("----");
        Console.WriteLine("Press Enter to Begin");
        Console.ReadLine();
        Console.Clear();
    }
    
    public static void DisplayPlayerHand(Player player, IEnumerable<Card> cards, bool drawBottomLine = true)
    {
        Console.WriteLine(Border);
        
        List<string> lines = [];

        var line = $"| {player} drew: ";
        foreach (var card in cards)
        {
            var asString = card.ToString();
            if ($"{line} {asString}".Length < 70)
            {
                line += $" {asString}";
            }
            else
            {
                line = line.PadRight(69) + "|";
                lines.Add(line);
                line = $"| {asString}";
            }
            
        }
        lines.Add(line.PadRight(69) + "|");

        foreach (var s in lines)
        {
            Console.WriteLine(s);
        }
        
        if (drawBottomLine)
        {
            Console.WriteLine(Border);
        }
    }
    
    public static void DisplayGameTime(GameTime gameTime, bool drawBottomLine = true)
    {
        Console.WriteLine(Border);
        
        string timeMessage = "| ";
        timeMessage += $"Turn {gameTime.TurnNumber}, {gameTime.CurrentPhase} Phase ";
        if (gameTime.CurrentPhase == Phase.Activity)
        {
            timeMessage += $"#{gameTime.ActivityPhaseNumber} | ";
            timeMessage += $"Active Player: {gameTime.ActivePlayer} ";
            timeMessage += gameTime.ActivePlayer == gameTime.FirstPlayer ? "(Top of Inning)" : "(Bottom of Inning)";
        }
        
        timeMessage = timeMessage.PadRight(70) + "|";

        Console.WriteLine(timeMessage);
        if (drawBottomLine)
        {
            Console.WriteLine(Border);
        }
        
    }
    
    public static void DisplayGameState(GameState gameState)
    {
        Console.WriteLine(Border);
        Console.WriteLine(DisplayPlayerStatusAndLocation(gameState));
        Console.WriteLine(DisplayMomentumRestAndCardCount(gameState));
        Console.WriteLine(Border);
        Console.WriteLine(DisplayIssueInfo(gameState));
        Console.WriteLine(Border);
        Console.WriteLine(DisplayMediaSupportAndEndorsements(gameState));
        Console.WriteLine(Border);
        Console.WriteLine(DisplayStateContests(gameState));
        Console.WriteLine(Border);
    }

    
    
    private static string DisplayPlayerStatusAndLocation(GameState gameState)
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

    private static string DisplayMomentumRestAndCardCount(GameState gameState)
    {
        var line = $"| Momentum: {gameState.Momentum[Player.Kennedy],-2}";
        line += $" Rest: {gameState.RestCubes[Player.Kennedy],-2} ";
        line += $" Cards: {gameState.NumberOfCardsInPlayerHands[Player.Kennedy],-2} ";
        line += $"| Momentum: {gameState.Momentum[Player.Nixon],-2}";
        line += $" Rest: {gameState.RestCubes[Player.Nixon],-2} ";
        line += $" Cards: {gameState.NumberOfCardsInPlayerHands[Player.Nixon],-2} ";
        line += "| ";
        return line;
    }

    private static string DisplayIssueInfo(GameState gameState)
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

    private static string DisplayMediaSupportAndEndorsements(GameState gameState)
    {
        var endorsements = gameState.Endorsements;
        var mediaSupport = gameState.MediaSupportLevels;
        
        var mediaString = $"| Media Support | Kennedy: {DisplayRegionalContests(mediaSupport,  Leader.Kennedy)}";
        mediaString += $" | Nixon: {DisplayRegionalContests(mediaSupport,  Leader.Nixon)}";
        mediaString = mediaString.PadRight(70) + "|";
        
        var endorsementString = $"| Endorsements  | Kennedy: {DisplayRegionalContests(endorsements,  Leader.Kennedy)}";
        endorsementString += $"| Nixon: {DisplayRegionalContests(endorsements,  Leader.Nixon)}";
        endorsementString = endorsementString.PadRight(70) + "|"; 
        
        return mediaString + Environment.NewLine + endorsementString;
    }

    private static string DisplayRegionalContests(IDictionary<Region, SupportContest<Leader>> contests, Leader leader)
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

    private static string DisplayStateContests(GameState gameState)
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



}