using PresidentialGameEngine.ClassLibrary.Enums;

namespace PresidentialGameEngine.ClassLibrary.Data
{
    public class Card(int index, string title, string eventText, int campaignPoints, Issue issue, Candidate candidate, State state)
    {
        public int Index { get; internal set; } = index;

        public string Title { get; internal set; } = title;

        public string Text { get; internal set; } = eventText;

        public int CampaignPoints { get; internal set; } = campaignPoints;

        public int RestCubes
        {
            get { return 4 - CampaignPoints; }
        }

        public Issue Issue { get; internal set; } = issue;

        public Candidate Candidate { get; internal set; } = candidate;

        public State State { get; internal set; } = state;

        //public delegate PresidentialGameEngine GameAction(PresidentialGameEngine sender);
        //https://medium.com/@lemapp09/beginning-game-development-delegates-and-events-e23530caaba5

        //public delegate void GameAction(PresidentialGameEngine sender);

        //https://medium.com/@jepozdemir/delegates-in-c-155f062d0f6d
        //Action?

        //public Action GameAction(PresidentialGameEngine sender) 
        //{

        //}

        //https://medium.com/@jepozdemir/func-vs-predicate-vs-expression-in-c-net-0bfff5caefcf
        /*
            Prints "Print action called!" to the console
            Action<string> print = message => Console.WriteLine(message);
            print("Print action called!");
         */

        public Action<NineteenSixtyGameEngine, Player> Event;// = engine => Console.WriteLine(engine);

        //What do we need to know besides the SOURCE PLAYER?
        //We could universalize it into a text input?
        //Which would be the encoder.

        public override string ToString()
        {
            return $"{Title} [{Index}]";
        }

    }

}
