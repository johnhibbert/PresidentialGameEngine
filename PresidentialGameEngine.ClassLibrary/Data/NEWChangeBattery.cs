using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresidentialGameEngine.ClassLibrary.Data
{
    public class NEWChangeBattery<PlayersEnum, IssuesEnum, StatesEnum, RegionsEnum>
     where PlayersEnum : Enum
     where IssuesEnum : Enum
     where StatesEnum : Enum
     where RegionsEnum : Enum
    {

        public List<NEWSupportChange<PlayersEnum, IssuesEnum>> IssueChanges { get; set; }
        public List<NEWSupportChange<PlayersEnum, StatesEnum>> StateChanges { get; set; }
        public List<NEWSupportChange<PlayersEnum, RegionsEnum>> EndorsementChanges { get; set; }
        public List<NEWSupportChange<PlayersEnum, RegionsEnum>> MediaSupportChanges { get; set; }

        public List<NEWAccumulationChange<PlayersEnum>> MomentumChanges { get; set; }
        public List<NEWAccumulationChange<PlayersEnum>> RestChanges { get; set; }

        public List<NEWPlayerLocationChange<PlayersEnum, StatesEnum>> PlayerLocationChanges { get; set; }

        //Can these all be SupportChanges?  Hmm Might need a name change
        //Some concepts don't need a target.
        //Non support components are
        //Accumlations (momentum)--------
        //Cards (player, cardclass)
        //Ehxaustion (Player)
        //Player Location (Player State)-------
        //IssuePositioning. (issue)
        //Rest-------

        public List<IssuesEnum> NewIssuesOrder { get; set; }

        public NEWChangeBattery()
        {
            IssueChanges = [];
            StateChanges = [];
            EndorsementChanges = [];
            MediaSupportChanges = [];
            NewIssuesOrder = [];
            MomentumChanges = [];
            RestChanges = [];
        }

    }




    public class NEWSupportChange<PlayersEnum, TargetEnum>
        (PlayersEnum player, TargetEnum target, NEWChangeDirection gainOrLoss, int change)
        where PlayersEnum : Enum
        where TargetEnum : Enum
    {
        public PlayersEnum Player { get; init; } = player;

        public TargetEnum Target { get; init; } = target;

        public NEWChangeDirection GainOrLoss { get; init; } = gainOrLoss;

        //This is not init because we will want to deduct these after support checks.
        public int Change { get; set; } = change;
    }

    public class NEWAccumulationChange<PlayersEnum>
        (PlayersEnum player, NEWChangeDirection gainOrLoss, int change)
        where PlayersEnum : Enum
    {
        public PlayersEnum Player { get; init; } = player;

        public NEWChangeDirection GainOrLoss { get; init; } = gainOrLoss;

        //This is not init because we will want to deduct these after support checks.
        public int Change { get; set; } = change;
    }


    public class NEWPlayerLocationChange<PlayersEnum, StatesEnum>(PlayersEnum player, StatesEnum state) 
    {
        public PlayersEnum Player { get; init; } = player;

        public StatesEnum State { get; init; } = state;

    }

    public class NewExhaustionStateChange<PlayersEnum>(PlayersEnum player, NEWExhaustionMode mode) 
    {
        public PlayersEnum Player { get; init; } = player;

        public NEWExhaustionMode State { get; init; } = mode;
    }

    public enum NEWChangeDirection 
    {
        Gain,
        Loss
    }

    public enum NEWExhaustionMode 
    {
        Exhaust,
        Unexhaust
    }
    

}
