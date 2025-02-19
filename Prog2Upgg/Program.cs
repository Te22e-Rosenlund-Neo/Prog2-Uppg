using Monkeys;
using Balloons;
using Rounds;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;
Monkey pilApa = new Monkey("Pilapa", 1, 1, 2);
MultiAttMonkey kanon = new MultiAttMonkey("kanon", 1, 1, 3, 5);
SlowMoney isApa = new SlowMoney("IsApa", 0, 1, 2, 4);
Monkey stålApa = new Monkey("StålApa", 8, 2, 10);

List<Monkey> AllMonkeys = new(){
    pilApa,
    kanon,
    isApa,
    stålApa
};


float money = 10f;
int health = 10;


bool GameOn = true;

while (GameOn)
{


    Console.WriteLine(@"Welcome to Monkeys Console Attack");
    Console.WriteLine("How many rounds will u take on?");
    int amountOfRounds = Round.MakeRounds();
    Console.WriteLine(amountOfRounds);

    List<Round> rounds = new();
    for (int i = 0; i < amountOfRounds; i++)
    {
        //rounds 1 range: 
        Round r = new Round(i + 3, i * 2, i, i, i - 1, i - 3);
        rounds.Add(r);
    }
    Console.ReadLine();
    List<Monkey> MyMonkeys = new List<Monkey>(){
        isApa, pilApa, pilApa
    };
    int currentround = 0;
    while (currentround < amountOfRounds)
    {
        Console.Clear();
        List<Bloon> attackingBloons = GenerateBloons(rounds[currentround]);


        while (true)
        {
            Console.Clear();
            Console.WriteLine($"Your base has {health} left");
            //opens shop
            // Displays bloons plus duration
            // choose who to attack with each monkey
            // 


            foreach (Monkey m in MyMonkeys)
            {

                foreach (Bloon blo in attackingBloons)
                {
                    Console.WriteLine($"{blo.Symbol()}: hp: {blo.health}: RoundsLeft: {blo.GiveSpeed()}");
                }

                string response;

                while (true)
                {
                    Console.WriteLine($"Who should {m.GetName()} attack? (damage: {m.Attack()}) (write the number)");
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
                chosen.health -= m.Attack();
                chosen.speed += m.ApplyEffect();

                if (chosen.health <= 0)
                {
                    money += 0.5f;
                    Console.WriteLine("You killed a balloon");
                    attackingBloons.RemoveAt(Convert.ToInt32(response) - 1);
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
                break;
            }

            for (int i = 0; i < attackingBloons.Count; i++)
            {
                Console.WriteLine("All bloons walked a little closer");

                Bloon bl = attackingBloons[i];
                bl.speed -= 1;
                if (bl.speed <= 0)
                {
                    Console.WriteLine($"A bloon of type {bl.GetType()} hit u, u took {bl.attack()} amount of damage");
                    health -= bl.attack();
                    attackingBloons.RemoveAt(i);
                }
            }

            if (health <= 0)
            {
                Console.WriteLine("You died");
                Console.ReadLine();
                break;
            }


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


Monkey? ShopSystem()
{

    Console.Clear();

    List<string> choices = new List<string>() { "0", "1", "2", "3", "4", "n" };
    Console.WriteLine(@"MONKEY - SHOP");
    Console.WriteLine("What monkey would u like?");
    Console.WriteLine($"Your Money: {money}");
    Console.WriteLine();

    for (int i = 0; i < AllMonkeys.Count; i++)
    {
        Console.WriteLine("Index:" + (i + 1));
        AllMonkeys[i].Showstats();
        Console.WriteLine("------------------------");
    }
    string answer;

    while (true)
    {
        Console.WriteLine("please enter a valid choice that u can afford!: (or n for none)");
        answer = Console.ReadLine();

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


void attack(Monkey attacker, Bloon defender)
{
    defender.health -= attacker.Attack();
    Console.Clear();
    defender.DisplayBaseStats();
}



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