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


    WriteColoredText(@"Welcome to Monkeys Console Attack", ConsoleColor.Red);
    Console.WriteLine("How many waves will u take on?");

    //Creates rounds based on input
    List<Round> rounds = Round.MakeRounds();

    //players towers
    List<Monkey> myMonkeys = new List<Monkey>(){
        pilApa, kanon, isApa
    };

    //basic info on how to play the game
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
                attackingBloons = attackingBloons = chosen.CheckForRemoval(attackingBloons);
                if (attackingBloons.Count == 0)
                {
                    break;
                }

                Console.Clear();
            }
            //we see if number of enemies has decresed, and increase money per kileld bloon
            int amount = enemies - attackingBloons.Count;
            shop.moneyChange(amount * shop.moneyPerBloon);

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

                //damages player
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


            while (true)
            {
                Console.Clear();
                Console.WriteLine("Would you like to buy an item? (y for yes, any other key for no)");
                string answer = Console.ReadLine() ?? "";
                if (answer.ToLower() == "y")
                {
                    var shopresult = shop.BuyItem();

                    if (shopresult != null)
                    {
                        if (shopresult is Monkey monkey)
                        {
                            myMonkeys.Add(monkey);
                        }
                        if (shopresult is Upgrades upgrade)
                        {
                            upgrade.ApplyUpgrade(myMonkeys);
                        }
                    }
                }else{
                    break;
                }
            }
        }
        //gives feedback for winning and checks if we should have died
        Console.WriteLine("Round:   " + (currentRound + 1) + " Finished");
        Console.ReadLine();
        currentRound += 1;

        if (health <= 0)
        {
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

//turns written text into colored text
void WriteColoredText(string message, ConsoleColor color)
{

    Console.ForegroundColor = color;
    Console.WriteLine(message);
    Console.ResetColor();
}