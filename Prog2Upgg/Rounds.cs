using Balloons;
using Monkeys;
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
                //checks if input was within given range (1-20)
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

        //method that generates bloons:
        public List<Bloon> GenerateBloons()
        {

            List<Bloon> bloons = new List<Bloon>();

            //checks how many of each type it should have
            //creates amount of bloons of each type
            //adds them to the total bloon list of that round
            for (int i = 0; i < redCount; i++)
            {
                Bloon redBloon = new Bloon("RedBloon", 2, 1, 2, ConsoleColor.Red);
                bloons.Add(redBloon);
            }
            for (int i = 0; i < yellowCount; i++)
            {
                Bloon yellowBloon = new Bloon("YellowBloon", 1, 2, 3, ConsoleColor.Yellow);
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
                Moab MOAB = new Moab("Moab", 20, 10, 40, ConsoleColor.DarkBlue);
                bloons.Add(MOAB);
            }
            return bloons;
        }

        //this method handles checking for input, killing enemy bloons.
        public (List<Monkey>, List<Bloon>) attackPhase(List<Monkey> myMonkeys, List<Bloon> attackingBloons)
        {
            foreach (Monkey m in myMonkeys)
            {
                //displays all bloons
                foreach (Bloon blo in attackingBloons)
                {
                    blo.ShortDisplay();

                }
                Console.WriteLine("--------------------------------------------");
                string response;

                //Checks for input on who to attack
                while (true)
                {
                    m.GetName(2);
                    response = Console.ReadLine() ?? "";

                    if (int.TryParse(response, out int x))
                    {
                        if (Convert.ToInt32(response) <= attackingBloons.Count && Convert.ToInt32(response) > 0)
                        {
                            break;
                        }
                    }
                }
                Bloon chosen = attackingBloons[Convert.ToInt32(response) - 1];
                Bloon chosen2 = chosen;

                //multi attack monkeys can only attack multiple if two or more enemies exist, here we check for that
                if (attackingBloons.Count <= 1)
                {
                    m.Attack(chosen, chosen2);
                    m.ApplyEffect(chosen);
                }
                else
                {
                    //if there is more than one, we check which the second target should be
                    if (attackingBloons.ElementAtOrDefault(attackingBloons.IndexOf(chosen) + 1) != null)
                    {
                        chosen2 = attackingBloons[attackingBloons.IndexOf(chosen) + 1];
                        m.Attack(chosen, chosen2);
                        m.ApplyEffect(chosen);
                        attackingBloons = chosen2.CheckForRemoval(attackingBloons);

                    }
                    else
                    {
                        chosen2 = attackingBloons[attackingBloons.IndexOf(chosen) - 1];
                        m.Attack(chosen, chosen2);
                        m.ApplyEffect(chosen);
                        attackingBloons = chosen2.CheckForRemoval(attackingBloons);
                    }
                }
                //checks if player killed the balloon
                attackingBloons = chosen.CheckForRemoval(attackingBloons);
                if (attackingBloons.Count == 0)
                {
                    break;
                }

                Console.Clear();
            }

            return (myMonkeys, attackingBloons);
        }

    
//method that handles what bloons reached our base (speed = 0)
    public List<Bloon> BloonAttackPhase(List<Bloon> attackingBloons)
        {
            //a list that has all the bloons that we need to remove
            List<Bloon> removableBloons = new();
            Console.WriteLine("All bloons walked a little closer");

            //runs through all bloons and adds reduces their cooldown, then checks if its at the bsae, if so we add it to the removebloon list
            for (int i = 0; i < attackingBloons.Count; i++)
            {


                Bloon bl = attackingBloons[i];
                bl.ChangeCoolDown(-1);

                //damages player
                if (bl.GiveSpeed() <= 0)
                {
                    Console.WriteLine($"A bloon of type {bl.GetName()} hit u, u took {bl.Attack()} amount of damage");
                    removableBloons.Add(bl);
                }

            }
            return removableBloons;
        }
    }
}