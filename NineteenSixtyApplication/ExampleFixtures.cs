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

public static class ExampleFixtures
{
    
    public static GameState GetFakeGameState()
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
                {
                    Player.Kennedy, 6
                },
                {
                    Player.Nixon, 3
                },
            },
            RestCubes = new Dictionary<Player, int>()
            {
                {
                    Player.Kennedy, 41
                },
                {
                    Player.Nixon, 10
                },
            },
            Endorsements = new Dictionary<Region, SupportContest<Leader>>()
            {
                {
                    Region.East, new SupportContest<Leader>()
                    {
                        Amount = 2,
                        Leader = Leader.Kennedy
                    }
                },
                {
                    Region.Midwest, new SupportContest<Leader>()
                    {
                        Amount = 7,
                        Leader = Leader.Kennedy
                    }
                },
                {
                    Region.South, new SupportContest<Leader>()
                    {
                        Amount = 2,
                        Leader = Leader.Nixon
                    }
                },
                {
                    Region.West, new SupportContest<Leader>()
                    {
                        Amount = 0,
                        Leader = Leader.None
                    }
                }
            },
            MediaSupportLevels = new Dictionary<Region, SupportContest<Leader>>()
            {
                {
                    Region.East, new SupportContest<Leader>()
                    {
                        Amount = 0,
                        Leader = Leader.None
                    }
                },
                {
                    Region.Midwest, new SupportContest<Leader>()
                    {
                        Amount = 3,
                        Leader = Leader.Nixon
                    }
                },
                {
                    Region.South, new SupportContest<Leader>()
                    {
                        Amount = 5,
                        Leader = Leader.Nixon
                    }
                },
                {
                    Region.West, new SupportContest<Leader>()
                    {
                        Amount = 1,
                        Leader = Leader.Kennedy
                    }
                }
            },
            PlayerLocations = new Dictionary<Player, State>()
            {
                {
                    Player.Kennedy, State.RI
                },
                {
                    Player.Nixon, State.OR
                },
            },
            PlayerStatuses = new Dictionary<Player, Status>()
            {
                {
                    Player.Kennedy, Status.Exhausted
                },
                {
                    Player.Nixon, Status.Exhausted
                },
            },
            StateContests = new Dictionary<State, SupportContest<Leader>>()
            {
                {
                    State.AK, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy,
                        Amount = 1
                    }
                },
                {
                    State.AL, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy,
                        Amount = 1
                    }
                },
                {
                    State.AR, new SupportContest<Leader>()
                    {
                        Leader = Leader.None,
                        Amount = 0
                    }
                },
                {
                    State.AZ, new SupportContest<Leader>()
                    {
                        Leader = Leader.Nixon,
                        Amount = 3
                    }
                },
                {
                    State.CA, new SupportContest<Leader>()
                    {
                        Leader = Leader.None,
                        Amount = 0
                    }
                },
                {
                    State.CO, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy,
                        Amount = 3
                    }
                },
                {
                    State.CT, new SupportContest<Leader>()
                    {
                        Leader = Leader.Nixon,
                        Amount = 4
                    }
                },
                {
                    State.DE, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy,
                        Amount = 1
                    }
                },
                {
                    State.FL, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy,
                        Amount = 1
                    }
                },
                {
                    State.GA, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy,
                        Amount = 1
                    }
                },
                {
                    State.HI, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy,
                        Amount = 1
                    }
                },
                {
                    State.IA, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy,
                        Amount = 1
                    }
                },
                {
                    State.ID, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy,
                        Amount = 1
                    }
                },
                {
                    State.IL, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy,
                        Amount = 1
                    }
                },
                {
                    State.IN, new SupportContest<Leader>()
                    {
                        Leader = Leader.None,
                        Amount = 0
                    }
                },
                {
                    State.KS, new SupportContest<Leader>()
                    {
                        Leader = Leader.Nixon,
                        Amount = 3
                    }
                },
                {
                    State.KY, new SupportContest<Leader>()
                    {
                        Leader = Leader.None,
                        Amount = 0
                    }
                },
                {
                    State.LA, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy,
                        Amount = 3
                    }
                },
                {
                    State.MA, new SupportContest<Leader>()
                    {
                        Leader = Leader.Nixon,
                        Amount = 4
                    }
                },
                {
                    State.MD, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy,
                        Amount = 1
                    }
                },
                {
                    State.ME, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy,
                        Amount = 1
                    }
                },
                {
                    State.MI, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy,
                        Amount = 1
                    }
                },
                {
                    State.MN, new SupportContest<Leader>()
                    {
                        Leader = Leader.None,
                        Amount = 0
                    }
                },
                {
                    State.MO, new SupportContest<Leader>()
                    {
                        Leader = Leader.Nixon,
                        Amount = 3
                    }
                },
                {
                    State.MS, new SupportContest<Leader>()
                    {
                        Leader = Leader.None,
                        Amount = 0
                    }
                },
                {
                    State.MT, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy,
                        Amount = 3
                    }
                },
                {
                    State.NC, new SupportContest<Leader>()
                    {
                        Leader = Leader.Nixon,
                        Amount = 4
                    }
                },
                {
                    State.ND, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy,
                        Amount = 1
                    }
                },
                {
                    State.NE, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy,
                        Amount = 1
                    }
                },
                {
                    State.NH, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy,
                        Amount = 1
                    }
                },
                {
                    State.NJ, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy,
                        Amount = 1
                    }
                },
                {
                    State.NM, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy,
                        Amount = 1
                    }
                },
                {
                    State.NV, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy,
                        Amount = 1
                    }
                },
                {
                    State.NY, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy,
                        Amount = 1
                    }
                },
                {
                    State.OH, new SupportContest<Leader>()
                    {
                        Leader = Leader.None,
                        Amount = 0
                    }
                },
                {
                    State.OK, new SupportContest<Leader>()
                    {
                        Leader = Leader.Nixon,
                        Amount = 3
                    }
                },
                {
                    State.OR, new SupportContest<Leader>()
                    {
                        Leader = Leader.None,
                        Amount = 0
                    }
                },
                {
                    State.PA, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy,
                        Amount = 3
                    }
                },
                {
                    State.RI, new SupportContest<Leader>()
                    {
                        Leader = Leader.Nixon,
                        Amount = 4
                    }
                },
                {
                    State.SC, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy,
                        Amount = 1
                    }
                },
                {
                    State.SD, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy,
                        Amount = 1
                    }
                },
                {
                    State.TN, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy,
                        Amount = 1
                    }
                },
                {
                    State.TX, new SupportContest<Leader>()
                    {
                        Leader = Leader.None,
                        Amount = 0
                    }
                },
                {
                    State.UT, new SupportContest<Leader>()
                    {
                        Leader = Leader.Nixon,
                        Amount = 3
                    }
                },
                {
                    State.VA, new SupportContest<Leader>()
                    {
                        Leader = Leader.None,
                        Amount = 0
                    }
                },
                {
                    State.VT, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy,
                        Amount = 3
                    }
                },
                {
                    State.WA, new SupportContest<Leader>()
                    {
                        Leader = Leader.Nixon,
                        Amount = 4
                    }
                },
                {
                    State.WI, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy,
                        Amount = 1
                    }
                },
                {
                    State.WV, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy,
                        Amount = 1
                    }
                },
                {
                    State.WY, new SupportContest<Leader>()
                    {
                        Leader = Leader.Kennedy,
                        Amount = 1
                    }
                },
            },
            NumberOfCardsInPlayerHands = new Dictionary<Player, int>()
            {
                { Player.Kennedy, 2 },
                { Player.Nixon, 15 },
            },
        };
        


    }


}