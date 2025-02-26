using Monkeys;
using Balloons;
using Rounds;

//creates different monkeys player can use
Monkey pilApa = new Monkey("Pilapa", 1, 1, 2, "Single target killing");
MultiAttMonkey kanon = new MultiAttMonkey("kanon", 1, 1, 5, "Attacks 2 enemies at once");
SlowMoney isApa = new SlowMoney("IsApa", 0, 1, 2, 4, "Slows enemy bloons down");
Monkey stålApa = new Monkey("StålApa", 8, 2, 10, "High damage single target");

float money = 10f;
int health = 10;

List<Monkey> allMonkeys = new(){
    pilApa,
    kanon,
    isApa,
    stålApa
};

bool gameOn = true;

//gameloop so game can be reset without restarting app
while (gameOn)
{


    WriteColoredText(@"Welcome to Monkeys Console Attack", ConsoleColor.Red);
    Console.WriteLine("How many waves will u take on?");

    //Creates rounds based on input
    List<Round> rounds = Round.MakeRounds();

    //players towers
    List<Monkey> myMonkeys = new List<Monkey>(){
        pilApa, kanon, isApa
    };

    int currentRound = 0;
    Console.Clear();
    Console.WriteLine($"Your goal is to protect your base from getting destroyed, u start with {health} health");
    WriteColoredText("Each wave, a set amount of balloons will spawn, if any of these balloons reach u, ur base take damage\n", ConsoleColor.Green);
    Console.WriteLine("To defend yourself, u have monkeys, each monkey attacks in different ways, and u choose what balloon to attack");
    WriteColoredText("DONT LET THE BALLOONS REACH YOU!!!", ConsoleColor.Red);

    Console.WriteLine("\n press enter to start");
    Console.ReadLine();

    //each round works here
    while (currentRound < rounds.Count)
    {
        Console.Clear();
        List<Bloon> attackingBloons = GenerateBloons(rounds[currentRound]);

        while (true)
        {
            //the game loop:
            // u get shown all enemy that are attacking, amongst with their base stats
            //for each monkey u got, u can attack. U may also apply effects to the enemy if thats the attack.
            //u get to buy more towers
            Console.Clear();
            WriteColoredText($"Your base has {health} health left", ConsoleColor.Green);

            Console.Write($"Your towers: ");
            foreach (Monkey m in myMonkeys)
            {
                m.GetName(1);
            }
            Console.WriteLine("\n---------------------------------------------");

            // for every monkey u got, u first get displayed the enemies, so u can choose who to attack
            int enemies = attackingBloons.Count;
            foreach (Monkey m in myMonkeys)
            {
                //displays all bloons
                foreach (Bloon blo in attackingBloons)
                {
                    blo.ShortDisplay();

                }

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
                        attackingBloons = Bloon.CheckForRemoval(chosen2, attackingBloons);

                    }
                    else
                    {
                        chosen2 = attackingBloons[attackingBloons.IndexOf(chosen) - 1];
                        m.Attack(chosen, chosen2);
                        m.ApplyEffect(chosen);
                        attackingBloons = Bloon.CheckForRemoval(chosen2, attackingBloons);
                    }
                }
                //checks if player killed the balloon
                attackingBloons = Bloon.CheckForRemoval(chosen, attackingBloons);
                if (attackingBloons.Count == 0)
                {
                    break;
                }

                Console.Clear();
            }
            //we see if number of enemies has decresed, and increase money per kileld bloon
            int amount = enemies - attackingBloons.Count;
            money += amount * 1f;

            if (attackingBloons.Count == 0)
            {
                //if player killed all the balloons, the round ends and it breaks this rounds loop
                break;
            }
            //all balloons walk closer to the players base, if they reach, they do damage
            //we add each bloon to die to a list, so removing wont change the order
            List<Bloon> removableBloons = new();
            Console.WriteLine("All bloons walked a little closer");
            for (int i = 0; i < attackingBloons.Count; i++)
            {


                Bloon bl = attackingBloons[i];
                bl.ChangeCoolDown(-1);

                //damages base
                if (bl.GiveSpeed() <= 0)
                {
                    Console.WriteLine($"A bloon of type {bl.GetName()} hit u, u took {bl.Attack()} amount of damage");
                    health -= bl.Attack();
                    removableBloons.Add(bl);
                }
            }

            Console.WriteLine("Press enter to continue");
            Console.ReadLine();

            //destroys killed bloons
            foreach (Bloon b in removableBloons)
            {
                attackingBloons.RemoveAt(attackingBloons.IndexOf(b));
            }
            removableBloons.Clear();
            //player dies
            if (health <= 0)
            {
                Console.WriteLine("You died");
                Console.ReadLine();
                break;
            }



            //calls the shop where player may buy one monkey if they have enough money
            var shopresult = ShopSystem(allMonkeys);

            if (shopresult != null)
            {
                myMonkeys.Add(shopresult);
            }
        }

        Console.WriteLine("Round:   " + (currentRound + 1) + " Finished");
        Console.ReadLine();
        currentRound += 1;

        if(health <= 0){
            break;
        }
        if (currentRound > rounds.Count - 1)
        {
            Console.WriteLine("YOU WON!!!");
            break;
        }
    }

    
    Console.ReadLine();
    Console.Clear();
}

//You can buy a monkey with the money u have, or u can chose not to (returns null), returns chosen monkey
Monkey? ShopSystem(List<Monkey> allMonkeys)
{

    Console.Clear();

    List<string> choices = new List<string>() { "0", "1", "2", "3", "4", "n" };
    Console.WriteLine(@"MONKEY - SHOP");
    Console.WriteLine("What monkey would u like?");
    Console.WriteLine($"Your Money: {money}");
    Console.WriteLine();
    //displays all monkeys
    for (int i = 0; i < allMonkeys.Count; i++)
    {
        Console.WriteLine("Index:" + (i + 1));
        allMonkeys[i].ShowStats();
        Console.WriteLine("------------------------");
    }
    string answer;
    //player can chose to buy or not to buy, 
    while (true)
    {
        Console.WriteLine("please enter a valid choice that u can afford!: (or n for none)");
        answer = Console.ReadLine() ?? "";

        if (answer.ToLower() == "n")
        {
            return null;
        }
        else if (choices.Contains(answer.ToLower()))
        {
            if (allMonkeys[Convert.ToInt32(answer) - 1].cost <= money)
            {
                money -= allMonkeys[Convert.ToInt32(answer) - 1].cost;
                Monkey chosen = allMonkeys[Convert.ToInt32(answer) - 1];
                return chosen;
            }
        }

    }
}





//generates bloons bases on how many of each that specific round has
List<Bloon> GenerateBloons(Round round)
{
    List<Bloon> bloons = new List<Bloon>();

    for (int i = 0; i < round.redCount; i++)
    {
        Bloon redBloon = new Bloon("RedBloon", 20, 1, 2, ConsoleColor.Red);
        bloons.Add(redBloon);
    }
    for (int i = 0; i < round.yellowCount; i++)
    {
        Bloon yellowBloon = new Bloon("YellowBloon", 1, 1, 3, ConsoleColor.Yellow);
        bloons.Add(yellowBloon);
    }
    for (int i = 0; i < round.blackCount; i++)
    {
        Bloon blackBloon = new Bloon("BlackBloon", 3, 3, 4, ConsoleColor.Black);
        bloons.Add(blackBloon);
    }
    for (int i = 0; i < round.whiteCount; i++)
    {
        Bloon whiteBloon = new Bloon("WhiteBloon", 3, 4, 5, ConsoleColor.White);
        bloons.Add(whiteBloon);
    }
    for (int i = 0; i < round.rainbowCount; i++)
    {
        Bloon rainbowBloon = new Bloon("RainbowBloon", 6, 7, 20, ConsoleColor.DarkMagenta);
        bloons.Add(rainbowBloon);
    }
    for (int i = 0; i < round.blueMCount; i++)
    {
        Bloon MOAB = new Bloon("Moab", 20, 10, 40, ConsoleColor.DarkBlue);
        bloons.Add(MOAB);
    }

    return bloons;
}
//turns written text into colored text
void WriteColoredText(string message, ConsoleColor color)
{

    Console.ForegroundColor = color;
    Console.WriteLine(message);
    Console.ResetColor();
}