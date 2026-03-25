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

public static class DrawToConsole
{
    private static string Border = " ---------------------------------------------------------------------- ";
    
    public static void DrawGameState(GameState gameState)
    {
        Console.Clear();
        Console.WriteLine(Border);
        Console.WriteLine(DrawPlayerStatusAndLocation(gameState));
        Console.WriteLine(DrawMomentumAndRest(gameState));
        Console.WriteLine(Border);
        Console.WriteLine(DrawIssueInfo(gameState));
        Console.WriteLine(Border);
        Console.WriteLine(DrawMediaSupportAndEndorsements(gameState));
        Console.WriteLine(Border);
        Console.WriteLine(DrawStateContests(gameState));
        Console.WriteLine(Border);
    }

    private static string DrawPlayerStatusAndLocation(GameState gameState)
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

    private static string DrawMomentumAndRest(GameState gameState)
    {
        var line = $"| Momentum: {gameState.Momentum[Player.Kennedy],-2}";
        line += $"            Rest: {gameState.RestCubes[Player.Kennedy],-2} ";
        line += $"| Momentum: {gameState.Momentum[Player.Nixon],-2}";
         line += $"            Rest: {gameState.RestCubes[Player.Nixon],-2} ";
         line += "| ";
        return line;
    }

    private static string DrawIssueInfo(GameState gameState)
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

    private static string DrawMediaSupportAndEndorsements(GameState gameState)
    {
        var endorsements = gameState.Endorsements;
        var mediaSupport = gameState.MediaSupportLevels;
        
        var mediaString = $"| Media Support | Kennedy: {DrawRegionalContests(mediaSupport,  Leader.Kennedy)}";
        mediaString += $" | Nixon: {DrawRegionalContests(mediaSupport,  Leader.Nixon)}";
        mediaString = mediaString.PadRight(70) + "|";
        
        var endorsementString = $"| Endorsements  | Kennedy: {DrawRegionalContests(endorsements,  Leader.Kennedy)}";
        endorsementString += $"| Nixon: {DrawRegionalContests(endorsements,  Leader.Nixon)}";
        endorsementString = endorsementString.PadRight(70) + "|"; 
        
        return mediaString + Environment.NewLine + endorsementString;
    }

    private static string DrawRegionalContests(IDictionary<Region, SupportContest<Leader>> contests, Leader leader)
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

    private static string DrawStateContests(GameState gameState)
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