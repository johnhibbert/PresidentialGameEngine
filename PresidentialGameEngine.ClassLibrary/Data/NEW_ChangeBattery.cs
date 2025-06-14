using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresidentialGameEngine.ClassLibrary.Data
{
    public class NEW_ChangeBattery<PlayersEnum, IssuesEnum, StatesEnum, RegionsEnum>
     where PlayersEnum : Enum
     where IssuesEnum : Enum
     where StatesEnum : Enum
     where RegionsEnum : Enum
    {

        public List<NEW_SupportChange<PlayersEnum, IssuesEnum>> IssueChanges { get; set; }
        public List<NEW_SupportChange<PlayersEnum, StatesEnum>> StateChanges { get; set; }
        public List<NEW_SupportChange<PlayersEnum, RegionsEnum>> EndorsementChanges { get; set; }
        public List<NEW_SupportChange<PlayersEnum, RegionsEnum>> MediaSupportChanges { get; set; }

        public List<NEW_AccumulationChange<PlayersEnum>> MomentumChanges { get; set; }
        public List<NEW_AccumulationChange<PlayersEnum>> RestChanges { get; set; }

        public List<NEW_PlayerLocationChange<PlayersEnum, StatesEnum>> PlayerLocationChanges { get; set; }

        public List<IssuesEnum> NewIssuesOrder { get; set; }

        public NEW_ChangeBattery()
        {
            IssueChanges = [];
            StateChanges = [];
            EndorsementChanges = [];
            MediaSupportChanges = [];
            NewIssuesOrder = [];
            MomentumChanges = [];
            RestChanges = [];
            PlayerLocationChanges = [];
        }

    }




    public class NEW_SupportChange<PlayersEnum, TargetEnum>
        (PlayersEnum player, TargetEnum target, NEW_ChangeDirection gainOrLoss, int change)
        where PlayersEnum : Enum
        where TargetEnum : Enum
    {
        public PlayersEnum Player { get; init; } = player;

        public TargetEnum Target { get; init; } = target;

        public NEW_ChangeDirection GainOrLoss { get; init; } = gainOrLoss;

        //This is not init because we will want to deduct these after support checks.
        public int Change { get; set; } = change;
    }

    public class NEW_AccumulationChange<PlayersEnum>
        (PlayersEnum player, NEW_ChangeDirection gainOrLoss, int change)
        where PlayersEnum : Enum
    {
        public PlayersEnum Player { get; init; } = player;

        public NEW_ChangeDirection GainOrLoss { get; init; } = gainOrLoss;

        //This is not init because we will want to deduct these after support checks.
        public int Change { get; set; } = change;
    }


    public class NEW_PlayerLocationChange<PlayersEnum, StatesEnum>(PlayersEnum player, StatesEnum state) 
    {
        public PlayersEnum Player { get; init; } = player;

        public StatesEnum State { get; init; } = state;

    }

    public class NEW_ExhaustionStateChange<PlayersEnum>(PlayersEnum player, NEW_ExhaustionMode mode) 
    {
        public PlayersEnum Player { get; init; } = player;

        public NEW_ExhaustionMode State { get; init; } = mode;
    }

    public enum NEW_ChangeDirection 
    {
        Gain,
        Loss
    }

    public enum NEW_ExhaustionMode 
    {
        Exhaust,
        Unexhaust
    }
    

}
