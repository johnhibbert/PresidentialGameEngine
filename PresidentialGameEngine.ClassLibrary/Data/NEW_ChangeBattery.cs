using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresidentialGameEngine.ClassLibrary.Data
{
    public class NEW_ChangeBattery<TPlayer, TIssue, TState, TRegion>
     where TPlayer : Enum
     where TIssue : Enum
     where TState : Enum
     where TRegion : Enum
    {

        public List<NEW_SupportChange<TPlayer, TIssue>> IssueChanges { get; set; }
        public List<NEW_SupportChange<TPlayer, TState>> StateChanges { get; set; }
        public List<NEW_SupportChange<TPlayer, TRegion>> EndorsementChanges { get; set; }
        public List<NEW_SupportChange<TPlayer, TRegion>> MediaSupportChanges { get; set; }

        public List<NEW_AccumulationChange<TPlayer>> MomentumChanges { get; set; }
        public List<NEW_AccumulationChange<TPlayer>> RestChanges { get; set; }

        public List<NEW_PlayerLocationChange<TPlayer, TState>> PlayerLocationChanges { get; set; }

        public List<TIssue> NewIssuesOrder { get; set; }

        public TIssue IssueToElevate { get; set; }

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
            IssueToElevate = (TIssue)Enum.ToObject(typeof(TIssue), 0);
        }

    }




    public class NEW_SupportChange<TPlayer, TTarget>
        (TPlayer player, TTarget target, NEW_ChangeDirection gainOrLoss, int change)
        where TPlayer : Enum
        where TTarget : Enum
    {
        public TPlayer Player { get; init; } = player;

        public TTarget Target { get; init; } = target;

        public NEW_ChangeDirection GainOrLoss { get; init; } = gainOrLoss;

        //This is not init because we will want to deduct these after support checks.
        public int Change { get; set; } = change;
    }

    public class NEW_AccumulationChange<TPlayer>
        (TPlayer player, NEW_ChangeDirection gainOrLoss, int change)
        where TPlayer : Enum
    {
        public TPlayer Player { get; init; } = player;

        public NEW_ChangeDirection GainOrLoss { get; init; } = gainOrLoss;

        //This is not init because we will want to deduct these after support checks.
        public int Change { get; set; } = change;
    }


    public class NEW_PlayerLocationChange<TPlayer, TState>(TPlayer player, TState state) 
    {
        public TPlayer Player { get; init; } = player;

        public TState State { get; init; } = state;

    }

    public class NEW_ExhaustionStateChange<TPlayer>(TPlayer player, NEW_ExhaustionMode mode) 
    {
        public TPlayer Player { get; init; } = player;

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
