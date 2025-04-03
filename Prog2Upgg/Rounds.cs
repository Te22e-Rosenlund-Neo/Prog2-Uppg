using Balloons;
namespace Rounds
{
    class Round
    {

        //redbloon, yellow bloon, black bloon, white bloon, rainbow bloon, blue moab
        private int _redCount = 0;
        public int redCount
        {
            get => _redCount;
            set
            {
                _redCount = Math.Max(0, value);
            }
        }
        public int yellowCount { get; set; }
        public int blackCount { get; set; }
        public int whiteCount { get; set; }
        public int rainbowCount { get; set; }
        public int blueMCount { get; set; }

        //how many of each bloon that exists. Math.max ensures we cant have below 0 of them
        public Round(int red, int yellow, int black, int white, int rainbow, int blueM)
        {
            redCount = red;
            yellowCount = Math.Max(0, yellow);
            blackCount = Math.Max(0, black);
            whiteCount = Math.Max(0, white);
            rainbowCount = Math.Max(0, rainbow);
            blueMCount = Math.Max(0, blueM);
        }
        //user puts in how many rounds they want, cant be more than max allowed or below 0
        //returns a list of all rounds made
        public static List<Round> MakeRounds()
        {
            string response = "";
            int roundCount;
            List<Round> allRounds = new();
            while (true)
            {
                Console.WriteLine("enter a number between 1 and 20");
                response = Console.ReadLine() ?? "";

                if (int.TryParse(response, out int x))
                {
                    if (Convert.ToInt32(response) > 0 && Convert.ToInt32(response) < 20)
                    {
                        roundCount = Convert.ToInt32(response);
                        break;
                    }
                }

            }

            //creates the rounds objects 
            for (int i = 0; i < roundCount; i++)
            {
                Round r = new Round(i + 3, i * 2, i, i, i - 1, i - 3);
                allRounds.Add(r);
            }

            return allRounds;
        }

        public List<Bloon> GenerateBloons()
        {

            List<Bloon> bloons = new List<Bloon>();
            for (int i = 0; i < redCount; i++)
            {
                Bloon redBloon = new Bloon("RedBloon", 2, 1, 2, ConsoleColor.Red);
                bloons.Add(redBloon);
            }
            for (int i = 0; i < yellowCount; i++)
            {
                Bloon yellowBloon = new Bloon("YellowBloon", 1, 1, 3, ConsoleColor.Yellow);
                bloons.Add(yellowBloon);
            }
            for (int i = 0; i < blackCount; i++)
            {
                Bloon blackBloon = new Bloon("BlackBloon", 3, 3, 4, ConsoleColor.Black);
                bloons.Add(blackBloon);
            }
            for (int i = 0; i < whiteCount; i++)
            {
                Bloon whiteBloon = new Bloon("WhiteBloon", 3, 4, 5, ConsoleColor.White);
                bloons.Add(whiteBloon);
            }
            for (int i = 0; i < rainbowCount; i++)
            {
                Bloon rainbowBloon = new Bloon("RainbowBloon", 6, 7, 20, ConsoleColor.DarkMagenta);
                bloons.Add(rainbowBloon);
            }
            for (int i = 0; i < blueMCount; i++)
            {
                Bloon MOAB = new Bloon("Moab", 20, 10, 40, ConsoleColor.DarkBlue);
                bloons.Add(MOAB);
            }
            return bloons;
        }


    }
}