using PresidentialGameEngine.ClassLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresidentialGameEngine.ClassLibrary.Engines
{
    public class GameController<PlayersEnum, LeadersEnum, IssuesEnum, StatesEnum, RegionsEnum, CardClass>
        where PlayersEnum : Enum
        where LeadersEnum : Enum
        where IssuesEnum : Enum
        where StatesEnum : Enum
        where RegionsEnum : Enum
        where CardClass : class
    {

        readonly GenericPresidentialGameEngine<PlayersEnum, LeadersEnum, IssuesEnum, StatesEnum, RegionsEnum, CardClass> Engine;

        public GameController(GenericPresidentialGameEngine<PlayersEnum, LeadersEnum, IssuesEnum, StatesEnum, RegionsEnum, CardClass> engine)
        {
            Engine = engine;
        }

        public void PlayCardAsEvent(GameAction<PlayersEnum, LeadersEnum, IssuesEnum, StatesEnum, RegionsEnum, CardClass> gameAction) 
        {
            throw new NotImplementedException();

            //This is why we need an interface and not just a generic class
            //gameAction.Card.
        }

        public void PlayCandidateCard(GameAction<PlayersEnum, LeadersEnum, IssuesEnum, StatesEnum, RegionsEnum, CardClass> gameAction)
        {
            throw new NotImplementedException();
        }

        public void PlayCardForCampaignPoints(GameAction<PlayersEnum, LeadersEnum, IssuesEnum, StatesEnum, RegionsEnum, CardClass> gameAction)
        {
            throw new NotImplementedException();
        }

        //return a result?
        //encapsulated in a game action class?
    }

    public class GameAction<PlayersEnum, LeadersEnum, IssuesEnum, StatesEnum, RegionsEnum, CardClass>
        where PlayersEnum : Enum
        where LeadersEnum : Enum
        where IssuesEnum : Enum
        where StatesEnum : Enum
        where RegionsEnum : Enum
        where CardClass : class
    {
        public PlayersEnum Player;
        public CardClass Card;
        public PlayerChosenChanges<PlayersEnum, IssuesEnum, StatesEnum, RegionsEnum> changes;

        //Should the game action include the action type?
        //Or just do it by method?
        //Might depend on the return type.
    }

    //Record?
    public class GameActionResult() 
    {
        bool IsSuccessful;
    }
}
