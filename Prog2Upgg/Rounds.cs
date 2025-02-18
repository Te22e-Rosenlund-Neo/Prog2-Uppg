using Balloons;
namespace Rounds
{
    class Round
    {

        //redbloon, yellow bloon, black bloon, white bloon, rainbow bloon, blue moab, DDT
        private int _redCount = 0;
        public int BloonTypes = 6;
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
        public int DDTCount { get; set; }

        public Round(int red, int yellow, int black, int white, int rainbow, int blueM, int DDT)
        {
            redCount = red;
            yellowCount = Math.Max(0, yellow);
            blackCount = Math.Max(0, black);
            whiteCount = Math.Max(0, white);
            rainbowCount = Math.Max(0, rainbow);
            blueMCount = Math.Max(0, blueM);
            DDTCount = Math.Max(0, DDT);
        }

        public static int MakeRounds()
        {
            string response = "";
            while (true)
            {
                Console.WriteLine("enter a number between 1 and 20");
                response = Console.ReadLine() ?? "";

                if (int.TryParse(response, out int x))
                {
                    Console.WriteLine("Was number");
                    if (Convert.ToInt32(response) > 0 && Convert.ToInt32(response) < 20)
                    {
                        Console.WriteLine("Is between");
                        return Convert.ToInt32(response);
                    }
                }

            }
        }

    }
}