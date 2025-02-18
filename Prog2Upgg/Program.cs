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
        Round r = new Round(i+Random.Shared.Next(1,4), i*2, i, i, i-1, i-3, i-4);
    }
    Console.ReadLine();
    List<Monkey> MyMonkeys = new List<Monkey>(){
        pilApa, pilApa, pilApa
    };
    int currentround = 0;
    while (currentround < amountOfRounds)
    {
        Console.Clear();
        List<Bloon> attackingBloons = new();
        attackingBloons = GenerateBloons(rounds[currentround]);

        while (true)
        {
            //opens shop
            // Displays bloons plus duration
            // choose who to attack with each monkey
            // 

            MyMonkeys.Add(ShopSystem());

            Console.Clear();

            Console.WriteLine(attackingBloons.Count);
        
            // foreach(Bloon b in attackingBloons){
            //     b.DisplayBaseStats();
            // }
            Console.WriteLine("Your monkeys attack");



            Console.ReadLine();





        }
        currentround += 1;
    }
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
        Console.WriteLine("please enter a valid choice:");
        answer = Console.ReadLine() ?? "";

    } while (!choices.Contains(answer));

    Monkey chosen = AllMonkeys[Convert.ToInt32(answer)];


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
        Bloon RedBloon = new Bloon("Red", 1, 1, 2, "R");
        bloons.Add(RedBloon);
    }
    for (int i = 0; i < round.yellowCount; i++)
    {
        Bloon YellowBloon = new Bloon("Yellow", 1, 2, 1, "Y");
        bloons.Add(YellowBloon);
    }
    for (int i = 0; i < round.redCount; i++)
    {
        Bloon BlackBloon = new Bloon("Black", 3, 1, 4, "B");
        bloons.Add(BlackBloon);
    }
    for (int i = 0; i < round.redCount; i++)
    {
        Bloon WhiteBloon = new Bloon("White", 3, 2, 3, "W");
        bloons.Add(WhiteBloon);
    }
    for (int i = 0; i < round.redCount; i++)
    {
        Bloon RainbowBloon = new Bloon("Rainbow", 6, 1, 8, "RB");
        bloons.Add(RainbowBloon);
    }
    for (int i = 0; i < round.redCount; i++)
    {
        Bloon Moab = new Bloon("Moab", 20, 1, 15, "M");
        bloons.Add(Moab);
    }

    return bloons;
}