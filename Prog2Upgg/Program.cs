using Monkeys;
using Balloons;
using Rounds;
Monkey pilApa = new Monkey("Pilapa", 3, 1, 2);
MultiAttMonkey kanon = new MultiAttMonkey("kanon", 1, 1, 3, 5);
SlowMoney isApa = new SlowMoney("IsApa", 0, 1, 2, 4);
Monkey stålApa = new Monkey("StålApa", 8, 2, 10);


int amountOfRounds = 5;
int money = 10;

List<Round> rounds = new();
for (int i = 0; i <= amountOfRounds; i++)
{
    rounds.Add(new Round(i + Random.Shared.Next(1, 5), i * 2, i, i, i - 1, i - 3, i - 4));
}

int currentround = 0;
while(currentround <= amountOfRounds){




}


