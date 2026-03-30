using System.Data;
using NineteenSixty;
using NineteenSixty.Data;
using NineteenSixty.Enums;
using NineteenSixty.Exceptions;
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
        
        var gameTime = controller.GetGameTime();

        while (gameTime.CurrentPhase == Phase.Activity)
        {

            ShowGameTime(BoxForm.OnlyTop);
            gameTime = controller.GetGameTime();
            var chosenAction = GetActionFromUser();

            switch (chosenAction)
            {
                case ActionType.PlayCardForEvent:
                    PlayCardAsEvent(gameTime.ActivePlayer);
                    break;
                default:
                    break;
            }

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
        //DisplayToConsole.DisplayGameState(controller.GetGameState());
        
        DisplayToConsole.DisplayGenericMessage($"{player} player: Select a card:", BoxForm.OnlyTop);
        
        var hand = controller.GetCardsInHand(player).ToList();
        DisplayToConsole.DisplayCardsInList(hand);
        var chosenInt = GetIntegerInputFromUser(hand.Select(x => x.Index));
        var cardToPlay = hand.Single(x => x.Index == chosenInt);

        bool changesAccepted = false;

        while (!changesAccepted)
        {
            SetOfChanges changes = new SetOfChanges();
            if (cardToPlay.RequiresPlayerInput)
            {
                changes = GetDesiredSetOfChangesFromUser(cardToPlay);
            }

            try
            {
                controller.PlayCardAsEvent(cardToPlay, changes, player);
                changesAccepted = true;
            }
            catch (InvalidPlayerChoices e)
            {
                Console.WriteLine(e.Message);
            }
        }

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

    static SetOfChanges GetDesiredSetOfChangesFromUser(Card card)
    {
        var returnValue = new SetOfChanges();

        var instructions = new List<string>();
        instructions.Add("To input the player chosen changes, type in a 5 character string.");
        instructions.Add("Characters 1 and 2: the state abbreviation");
        instructions.Add("Character 3: the sign of the change (+ for gains or - for losses)");
        instructions.Add("Character 4: the amount of the change (0-9)");
        instructions.Add("Character 5: the candidate affected (K/N)");
        instructions.Add("");
        instructions.Add("An example would be CA+2K is 2 support for Kennedy in California.");
        instructions.Add("");
        instructions.Add("For non-state changes, replace the state with the following:");
        instructions.Add("For issues: I and then C, D or E for the issue. (IC+1K)");
        instructions.Add("For endorsements: E and then E, M, S or W for the region. (ES+1N)");
        instructions.Add("For media support: M and then E, M, S or W for the region. (MM-1N)");
        instructions.Add("");
        instructions.Add("To reorder issues with Gallup Poll, use IO instead of the state");
        instructions.Add("Then C, D and E for the new order. (IODEC)");
        instructions.Add("");
        instructions.Add("Do not add any changes that are not chosen by the player.");
        instructions.Add("These will be done automatically.");
        instructions.Add("");
        instructions.Add("Type 'Q' to quit.");


        DisplayToConsole.DisplayLinesInBox(instructions);

        
        DisplayToConsole.DisplayLongLineWithWordWrapInBox(card.Text, BoxForm.OnlyBottom);
        

        var issueLookup = new Dictionary<string, Issue>()
        {
            { "IC", Issue.CivilRights },
            { "ID", Issue.Defense },
            { "IE", Issue.Economy },
        };

        var endorsementLookup = new Dictionary<string, Region>()
        {
            { "EE", Region.East },
            { "EM", Region.Midwest },
            { "ES", Region.South },
            { "EW", Region.West },
        };

        var mediaSupportLookup = new Dictionary<string, Region>()
        {
            { "ME", Region.East },
            { "MM", Region.Midwest },
            { "MS", Region.South },
            { "MW", Region.West },
        };
        
        bool continueGettingInput = true;

        while (continueGettingInput)
        {
            try
            {
                var input = Console.ReadLine();
                input = input.ToUpper();

                if (input == "Q")
                {
                    continueGettingInput = false;
                    continue;
                }
                else if (input.Length != 5)
                {
                    throw new InvalidOperationException();
                }

                var chunkOne = input[..2];
                
                //Special handling for Gallup Poll.
                if(chunkOne == "IO")
                {
                    var newIssueOrder = new List<Issue>();

                    //'Casting' it into the other dictionary
                    var firstString = $"I{input[2]}";
                    var secondString = $"I{input[3]}";
                    var thirdString = $"I{input[4]}";

                    if (issueLookup.TryGetValue(firstString, out var firstIssue) &&
                        issueLookup.TryGetValue(secondString, out var secondIssue) &&
                        issueLookup.TryGetValue(thirdString, out var thirdIssue))
                    {
                        newIssueOrder.Add(firstIssue);
                        newIssueOrder.Add(secondIssue);
                        newIssueOrder.Add(thirdIssue);
                        returnValue.NewIssuesOrder =  newIssueOrder;
                        Console.WriteLine("Input accepted.");
                        continue;
                    }

                    throw new InvalidOperationException();
                }
                
                var chunkTwo = input.Substring(2, 2);
                var chunkThree = input.Substring(4, 1);
                
                var affectedPlayer = Player.Kennedy;
                switch (chunkThree)
                {
                    case "K":
                        affectedPlayer = Player.Kennedy;
                        break;
                    case "N":
                        affectedPlayer = Player.Nixon;
                        break;
                    default:
                        throw new InvalidOperationException();
                }

                if (!int.TryParse(chunkTwo, out int intFromUser))
                {
                    throw new InvalidOperationException();
                }

                if (Enum.TryParse(chunkOne, out State chunkOneAsState))
                {
                    var change = new SupportChange<Player, State>(affectedPlayer, chunkOneAsState, intFromUser);
                    returnValue.StateChanges.Add(change);
                }
                else if (issueLookup.TryGetValue(chunkOne, out var issue))
                {
                    var change = new SupportChange<Player, Issue>(affectedPlayer, issue, intFromUser);
                    returnValue.IssueChanges.Add(change);
                }
                else if (endorsementLookup.TryGetValue(chunkOne, out var endorsementRegion))
                {
                    var change = new SupportChange<Player, Region>(affectedPlayer, endorsementRegion, intFromUser);
                    returnValue.EndorsementChanges.Add(change);
                }
                else if (mediaSupportLookup.TryGetValue(chunkOne, out var mediaRegion))
                {
                    var change = new SupportChange<Player, Region>(affectedPlayer, mediaRegion, intFromUser);
                    returnValue.MediaSupportChanges.Add(change);
                }
                else  throw new InvalidOperationException();

                Console.WriteLine("Input accepted.");
            }
            catch (Exception e)
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
