using Monkeys;
using Balloons;
using Rounds;
using System.Linq;
using System.Text;

//creates different monkeys player can use
Monkey pilApa = new Monkey("Pilapa", 1, 1, 2, "Single target killing");
MultiAttMonkey kanon = new MultiAttMonkey("kanon", 1, 1, 3, 5, "Attacks 2 enemies at once");
SlowMoney isApa = new SlowMoney("IsApa", 0, 1, 2, 4, "Slows enemy bloons down");
Monkey stålApa = new Monkey("StålApa", 8, 2, 10, "High damage single target");

float money = 10f;
int health = 10;

List<Monkey> AllMonkeys = new(){
    pilApa,
    kanon,
    isApa,
    stålApa
};

bool GameOn = true;

while (GameOn)
{


    WriteColoredText(@"Welcome to Monkeys Console Attack", "red");
    Console.WriteLine("How many waves will u take on?");
    int amountOfRounds = Round.MakeRounds();

    //depending on how many rounds player chose, creates that many instances of rounds.
    List<Round> rounds = new();

    //each round has scaling difficulty, amount of enemies of each type is indicated by i
    for (int i = 0; i < amountOfRounds; i++)
    {
        Round r = new Round(i + 3, i * 2, i, i, i - 1, i - 3);
        rounds.Add(r);
    }
    //players towers
    List<Monkey> MyMonkeys = new List<Monkey>(){
        pilApa, kanon, isApa
    };
    int currentround = 0;

    Console.WriteLine($"Your goal is to protect your base from getting destroyed, u start with {health} health");
    WriteColoredText("Each wave, a set amount of balloons will spawn, if any of these balloons reach u, ur base take damage\n", "green");
    Console.WriteLine("To defend yourself, u have monkeys, each monkey attacks in different ways, and u choose what balloon to attack");
    WriteColoredText("DONT LET THE BALLOONS REACH YOU!!!", "red");

    Console.WriteLine("\n press enter to start");
    Console.ReadLine();
    while (currentround < amountOfRounds)
    {
        Console.Clear();
        List<Bloon> attackingBloons = GenerateBloons(rounds[currentround]);

        while (true)
        {
            //the game loop:
            // u get shown all enemy that are attacking, amongst with their base stats
            //for each monkey u got, u can attack. U may also apply effects to the enemy if thats the attack.
            Console.Clear();
            WriteColoredText($"Your base has {health} health left", "green");

            // for every monkey u got, u first get displayed the enemies, so u can choose who to attack
            foreach (Monkey m in MyMonkeys)
            {
                //displays all bloons
                foreach (Bloon blo in attackingBloons)
                {
                    Console.WriteLine($"{blo.Symbol()}: hp: {blo.health}: attacks in: {blo.GiveSpeed()} rounds");
                }

                string response;
                //makes player choose what to attack
                while (true)
                {
                    Console.WriteLine($"Who should {m.GetName()} attack? (damage: {m.getdamage()}) (write the number)");
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
                //this check is to see if the multiattack monkeys can attack multiple bloons at oncww
                if (attackingBloons.Count <= 1)
                {
                    m.Attack(chosen, chosen2);
                    m.ApplyEffect(chosen);
                }
                else
                {

                    if (attackingBloons.ElementAtOrDefault(attackingBloons.IndexOf(chosen) + 1) != null)
                    {
                        chosen2 = attackingBloons[attackingBloons.IndexOf(chosen) + 1];
                        m.Attack(chosen, chosen2);
                        m.ApplyEffect(chosen);
                        //borde göras till funktion
                        if (chosen2.health <= 0)
                        {
                            money += 0.5f;
                            Console.WriteLine("You killed a balloon, press enter to continue");
                            Console.ReadLine();
                            attackingBloons.RemoveAt(attackingBloons.IndexOf(chosen2));
                        }
                    }
                    else
                    {
                        chosen2 = attackingBloons[attackingBloons.IndexOf(chosen) - 1];
                        m.Attack(chosen, chosen2);
                        m.ApplyEffect(chosen);

                        if (chosen2.health <= 0)
                        {
                            money += 0.5f;
                            Console.WriteLine("You killed a balloon, press enter to continue");
                            Console.ReadLine();
                            attackingBloons.RemoveAt(attackingBloons.IndexOf(chosen2));
                        }
                    }
                }
                //checks if player killed the balloon
                if (chosen.health <= 0)
                {
                    money += 0.5f;
                    Console.WriteLine("You killed a balloon, press enter to continue");
                    Console.ReadLine();
                    attackingBloons.RemoveAt(attackingBloons.IndexOf(chosen));
                }
                if (attackingBloons.Count == 0)
                {
                    break;
                }

                Console.Clear();
            }







            Console.ReadLine();


            if (attackingBloons.Count == 0)
            {
                //if player killed all the balloons, the round ends and it breaks this rounds loop
                break;
            }
            //all balloons walk closer to the players base, if they reach, they do damage
            List<int> RemovableBloons = new();
            Console.WriteLine("All bloons walked a little closer");
            for (int i = 0; i < attackingBloons.Count; i++)
            {


                Bloon bl = attackingBloons[i];
                bl.ChangeCoolDown(-1);
                if (bl.GiveSpeed() <= 0)
                {
                    Console.WriteLine($"A bloon of type {bl.GetType()} hit u, u took {bl.attack()} amount of damage");
                    health -= bl.attack();
                    RemovableBloons.Add(i);
                }
            }
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
            foreach (int x in RemovableBloons)
            {
                attackingBloons.RemoveAt(x);
            }
            RemovableBloons.Clear();
            //spelaren dör
            if (health <= 0)
            {
                Console.WriteLine("You died");
                Console.ReadLine();
                break;
            }

            //calls the shop where player may buy one monkey if they have enough money
            var shopresult = ShopSystem();

            if (shopresult != null)
            {
                MyMonkeys.Add(shopresult);
            }
        }
        if (health <= 0)
        {
            break;
        }
        Console.WriteLine("Round:   " + currentround + 1 + " Finished");
        Console.ReadLine();
        currentround += 1;
        if (currentround > amountOfRounds)
        {
            break;
        }
    }
    if (health <= 0)
    {
        break;
    }
    Console.WriteLine("YOU WON!!!");
}

//You can buy a monkey with the money u have, or u can chose not to
Monkey? ShopSystem()
{

    Console.Clear();

    List<string> choices = new List<string>() { "0", "1", "2", "3", "4", "n" };
    Console.WriteLine(@"MONKEY - SHOP");
    Console.WriteLine("What monkey would u like?");
    Console.WriteLine($"Your Money: {money}");
    Console.WriteLine();
    //displays all monkeys
    for (int i = 0; i < AllMonkeys.Count; i++)
    {
        Console.WriteLine("Index:" + (i + 1));
        AllMonkeys[i].Showstats();
        Console.WriteLine("------------------------");
    }
    string answer;
    //player can chose to buy or not to buy
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
            if (AllMonkeys[Convert.ToInt32(answer) - 1].Cost <= money)
            {
                money -= AllMonkeys[Convert.ToInt32(answer) - 1].Cost;
                Monkey chosen = AllMonkeys[Convert.ToInt32(answer) - 1];
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
        Bloon RedBloon = new Bloon("Red", 1, 2, 2, "R");
        bloons.Add(RedBloon);
    }
    for (int i = 0; i < round.yellowCount; i++)
    {
        Bloon YellowBloon = new Bloon("Yellow", 1, 1, 3, "Y");
        bloons.Add(YellowBloon);
    }
    for (int i = 0; i < round.blackCount; i++)
    {
        Bloon BlackBloon = new Bloon("Black", 3, 3, 4, "B");
        bloons.Add(BlackBloon);
    }
    for (int i = 0; i < round.whiteCount; i++)
    {
        Bloon WhiteBloon = new Bloon("White", 3, 4, 5, "W");
        bloons.Add(WhiteBloon);
    }
    for (int i = 0; i < round.rainbowCount; i++)
    {
        Bloon RainbowBloon = new Bloon("Rainbow", 6, 7, 20, "RB");
        bloons.Add(RainbowBloon);
    }
    for (int i = 0; i < round.blueMCount; i++)
    {
        Bloon Moab = new Bloon("Moab", 20, 10, 40, "M");
        bloons.Add(Moab);
    }

    return bloons;
}
//turns written text into colored text
void WriteColoredText(string message, string color)
{
    if (color == "red")
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }
    if (color == "green")
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}