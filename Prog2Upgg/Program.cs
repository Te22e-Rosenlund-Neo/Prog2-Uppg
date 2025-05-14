using Monkeys;
using Balloons;
using Rounds;
using Shops;
using Upgrade;

//creates different monkeys player can use
Monkey pilApa = new Monkey("Pilapa", 1, 1, 2, "Single target killing");
Monkey kanon = new MultiAttMonkey("kanon", 1, 1, 5, "Attacks 2 enemies at once");
EffMonkey isApa = new SlowMoney("IsApa", 0, 1, 2, 4, "Slows enemy bloons down");
Monkey stålApa = new Monkey("StålApa", 8, 2, 10, "High damage single target");
Upgrades strengthUp = new Upgrades("Strength - upgrade", 1, 0, 2);
Upgrades effectUp = new Upgrades("Effect - upgrade", 0, 2, 2);



List<IBuyable> allMonkeys = new(){
    pilApa,
    kanon,
    isApa,
    stålApa
};

//instantiates a shop using an interface and adds items to the list of sellable items inside the shop
Shop<IBuyable> shop = new Shop<IBuyable>(10);
shop.AddItemList(allMonkeys);
shop.AddItemList(new List<IBuyable>() { strengthUp, effectUp });
int health = 10;

bool gameOn = true;




//gameloop so game can be reset without restarting app
while (gameOn)
{
    //displays basic info and creates the amount of rounds a player chose.
    List<Round> rounds = StartGameText();
    int currentRound = 0;
    //Creates rounds based on input


    //players towers
    List<Monkey> myMonkeys = new List<Monkey>(){
        pilApa, kanon, isApa, kanon, isApa
    };

    //each round works here
    while (currentRound < rounds.Count)
    {
        Console.Clear();
        List<Bloon> attackingBloons = rounds[currentRound].GenerateBloons();

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

            //keeps track of the enemy count before the attack phase
            int enemies = attackingBloons.Count;

            //enters attack phase where player attack the enemy bloons
            var result = rounds[currentRound].attackPhase(myMonkeys, attackingBloons);
            myMonkeys = result.Item1;
            attackingBloons = result.Item2;



            //if the enemy count is different after than before round, then we give money to  playwr
            int amount = enemies - attackingBloons.Count;
            shop.moneyChange(amount * shop.moneyPerBloon);

            if (attackingBloons.Count == 0)
            {
                //if player killed all the balloons, the round ends and it breaks this rounds loop
                break;
            }
            //all balloons walk closer to the players base, if they reach, they do damage
            //we add each bloon to die to a list, so removing wont change the order
            List<Bloon> removableBloons = rounds[currentRound].BloonAttackPhase(attackingBloons);
            
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();

            //destroys killed bloons
            foreach (Bloon b in removableBloons)
            {
                attackingBloons.RemoveAt(attackingBloons.IndexOf(b));
                health -= b.Attack();
            }
            removableBloons.Clear();
            //player dies
            if (health <= 0)
            {
                Console.WriteLine("You died");
                Console.ReadLine();
                break;
            }


            //loop that calls the shop everytime the player says yes, which allows player to buy more than 1 item
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Would you like to buy an item? (y for yes, any other key for no)");
                string answer = Console.ReadLine() ?? "";
                if (answer.ToLower() == "y")
                {
                    //returns an item from shop
                    var shopresult = shop.BuyItem();
                    //checks what item was bought, if any, and applies it correctly
                    if (shopresult != null)
                    {
                        if (shopresult is Monkey monkey)
                        {
                            myMonkeys.Add(monkey.CloneMe());
                        }
                        if (shopresult is Upgrades upgrade)
                        {
                            upgrade.ApplyUpgrade(myMonkeys);
                        }
                    }
                }
                else
                {
                    break;
                }
            }
        }
        //gives feedback for winning and checks if we should have died
        Console.WriteLine("Round:   " + (currentRound + 1) + " Finished");
        Console.ReadLine();
        currentRound += 1;
        //death scnario
        if (health <= 0)
        {
            break;
        }
        //win scenario
        if (currentRound > rounds.Count - 1)
        {
            Console.WriteLine("YOU WON!!!");
            break;
        }
    }


    Console.ReadLine();
    Console.Clear();
}
//--------------------------------------------------------------------------------------------------------------------------------------------------------------
//turns written text into colored text
void WriteColoredText(string message, ConsoleColor color)
{

    Console.ForegroundColor = color;
    Console.WriteLine(message);
    Console.ResetColor();
}


//--------------------------------------------------------------------------------------------------------------------------------------------------------------

List<Round> StartGameText()
{
    Console.Clear();
    WriteColoredText(@"Welcome to Monkeys Console Attack", ConsoleColor.Red);
    Console.WriteLine("How many waves will u take on?");

    List<Round> rounds = Round.MakeRounds();
    //basic info on how to play the game

    Console.Clear();
    Console.WriteLine($"Your goal is to protect your base from getting destroyed, u start with {health} health");
    WriteColoredText("Each wave, a set amount of balloons will spawn, if any of these balloons reach u, ur base take damage\n", ConsoleColor.Green);
    Console.WriteLine("To defend yourself, u have monkeys, each monkey attacks in different ways, and u choose what balloon to attack");
    WriteColoredText("DONT LET THE BALLOONS REACH YOU!!!", ConsoleColor.Red);

    Console.WriteLine("\n press enter to start");
    Console.ReadLine();

    return rounds;
}