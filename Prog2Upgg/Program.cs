using Monkeys;
using Balloons;
using Rounds;

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

    Console.WriteLine("Welcome to Monkeys Console Attack");
    Console.WriteLine("How many rounds will u take on?");

    string response = "";
    int x;

    while (true)
    {
        Console.WriteLine("enter a number between 1 and 20");
        response = Console.ReadLine() ?? "";

        if (int.TryParse(response, out x))
        {
            if (Convert.ToInt32(response) > 0 && Convert.ToInt32(response) < 20)
            {
                break;
            }
        }
    }

    int amountOfRounds = Convert.ToInt32(response);

    List<Round> rounds = new();
    for (int i = 0; i <= amountOfRounds; i++)
    {
        rounds.Add(new Round(i + Random.Shared.Next(1, 5), i * 2, i, i, i - 1, i - 3, i - 4));
    }

    int currentround = 0;
    while (currentround <= amountOfRounds)
    {





        currentround += 1;
    }
}


Monkey ShopSystem()
{

    Console.Clear();

    List<string> choices = new List<string>() { "1", "2", "3", "4" };
    Console.WriteLine("What monkey would u like?");
    Console.WriteLine($"Your Money: {money}");
    foreach (Monkey monkey in AllMonkeys)
    {
        monkey.Showstats();
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


