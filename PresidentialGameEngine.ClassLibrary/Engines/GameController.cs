using PresidentialGameEngine.ClassLibrary.Data;
using PresidentialGameEngine.ClassLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresidentialGameEngine.ClassLibrary.Engines
{
    public class GameController<TPlayer, TLeader, TIssue, TState, TRegion, TCard>
        where TPlayer : Enum
        where TLeader : Enum
        where TIssue : Enum
        where TState : Enum
        where TRegion : Enum
        where TCard : ICard
    {

        readonly GenericPresidentialGameEngine<TPlayer, TLeader, TIssue, TState, TRegion, TCard> Engine;

        public GameController(GenericPresidentialGameEngine<TPlayer, TLeader, TIssue, TState, TRegion, TCard> engine)
        {
            Engine = engine;
        }

        public void PlayCardAsEvent(GameAction<TPlayer, TLeader, TIssue, TState, TRegion, TCard> gameAction) 
        {
            throw new NotImplementedException();

            //This is why we need an interface and not just a generic class
            //var ff = gameAction.Card.Index;
        }

        public void PlayCandidateCard(GameAction<TPlayer, TLeader, TIssue, TState, TRegion, TCard> gameAction)
        {
            throw new NotImplementedException();
        }

        public void PlayCardForCampaignPoints(GameAction<TPlayer, TLeader, TIssue, TState, TRegion, TCard> gameAction)
        {
            throw new NotImplementedException();
        }

        //return a result?
        //encapsulated in a game action class?
    }

    public class GameAction<TPlayer, TLeader, TIssue, TState, TRegion, TCard>
        where TPlayer : Enum
        where TLeader : Enum
        where TIssue : Enum
        where TState : Enum
        where TRegion : Enum
        where TCard : ICard
    {
        public TPlayer Player;
        public TCard Card;
        public PlayerChosenChanges<TPlayer, TIssue, TState, TRegion> changes;

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
