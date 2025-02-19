using Monkeys;
using Balloons;
using Rounds;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;
Monkey pilApa = new Monkey("Pilapa", 3, 1, 2);
MultiAttMonkey kanon = new MultiAttMonkey("kanon", 1, 1, 3, 5);
SlowMoney isApa = new SlowMoney("IsApa", 0, 1, 2, 4);
Monkey stålApa = new Monkey("StålApa", 8, 2, 10);

List<Monkey> AllMonkeys = new(){
    pilApa,
    kanon,
    isApa,
    stålApa
};


int money = 10;
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
        Round r = new Round(i + 2, i * 2, i, i, i - 1, i - 3);
        rounds.Add(r);
    }
    Console.ReadLine();
    List<Monkey> MyMonkeys = new List<Monkey>(){
        pilApa, pilApa, pilApa
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
                    Console.WriteLine($"Who should {m.GetName()} attack? (write the number)");
                    response = Console.ReadLine() ?? "";

                    if (int.TryParse(response, out int x))
                    {
                        if (Convert.ToInt32(response) <= attackingBloons.Count && Convert.ToInt32(response) > 0)
                        {
                            break;
                        }
                    }
                }

                Bloon chosen = attackingBloons[Convert.ToInt32(response)];
                chosen.health -= m.Attack();
                chosen.speed += m.ApplyEffect();

                if (chosen.health <= 0)
                {
                    attackingBloons.RemoveAt(Convert.ToInt32(response));
                }


            }







            Console.ReadLine();


            if (attackingBloons[0] == null)
            {
                break;
            }

            for (int i = 0; i < attackingBloons.Count; i++)
            {

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


            MyMonkeys.Add(ShopSystem());
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


Monkey ShopSystem()
{

    Console.Clear();

    List<string> choices = new List<string>() { "1", "2", "3", "4", "5" };
    Console.WriteLine(@"MONKEY - SHOP");
    Console.WriteLine("What monkey would u like?");
    Console.WriteLine($"Your Money: {money}");
    Console.WriteLine();

    for (int i = 0; i < AllMonkeys.Count; i++)
    {
        Console.WriteLine(i);
        AllMonkeys[i].Showstats();
        Console.WriteLine("------------------------");
    }
    string answer;
    do
    {
        Console.WriteLine("please enter a valid choice that u can afford!:");
        answer = Console.ReadLine() ?? "";

    } while (!choices.Contains(answer) && AllMonkeys[Convert.ToInt32(answer)].Cost <= money);

    Monkey chosen = AllMonkeys[Convert.ToInt32(answer)];

    money -= AllMonkeys[Convert.ToInt32(answer)].Cost;
    return chosen;
}
string answerChecker(List<string> Answers)
{

    string input;
    do
    {

        Console.Write("Please type a valid argument");
        input = Console.ReadLine() ?? "";
    } while (!Answers.Contains(input));

    return input;
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
        Bloon YellowBloon = new Bloon("Yellow", 1, 3, 1, "Y");
        bloons.Add(YellowBloon);
    }
    for (int i = 0; i < round.blackCount; i++)
    {
        Bloon BlackBloon = new Bloon("Black", 3, 3, 4, "B");
        bloons.Add(BlackBloon);
    }
    for (int i = 0; i < round.whiteCount; i++)
    {
        Bloon WhiteBloon = new Bloon("White", 3, 4, 3, "W");
        bloons.Add(WhiteBloon);
    }
    for (int i = 0; i < round.rainbowCount; i++)
    {
        Bloon RainbowBloon = new Bloon("Rainbow", 6, 7, 12, "RB");
        bloons.Add(RainbowBloon);
    }
    for (int i = 0; i < round.blueMCount; i++)
    {
        Bloon Moab = new Bloon("Moab", 20, 10, 20, "M");
        bloons.Add(Moab);
    }

    return bloons;
}