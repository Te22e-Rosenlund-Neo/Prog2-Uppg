using Monkeys;
namespace Upgrade
{

    //another item for the shop, it upgrades existing monkeys abilites
    class Upgrades : IBuyable
    {

        private string name;
        private int damageIncrease;
        private int effectIncrease;
        public int Cost { get; set; }

        public Upgrades(string name, int dmgIncrease, int effIncrease, int cost)
        {
            this.name = name;
            damageIncrease = dmgIncrease;
            effectIncrease = effIncrease;
            this.Cost = cost;
        }


        //displays information about it (a must bc of the Ibuyable interface)
        public void ShowStats()
        {
            Console.WriteLine($"UpgradeType: {name}");
            Console.WriteLine($"Damage increase amount: {damageIncrease}");
            Console.WriteLine($"Effect increase amount: {effectIncrease}");
            Console.WriteLine($"Cost: {Cost}");
        }

        //method that takes in all player monkeys, allows them to select which one to use the upgrade on
        public void ApplyUpgrade(List<Monkey> monkeys)
        {
            Console.Clear();
            Console.WriteLine("Please select the monkey u wanna upgrade by typing the index;");

            //displays all monkeys
            for (int i = 0; i < monkeys.Count; i++)
            {
                Console.WriteLine($"{i + 1}: ");
                monkeys[i].GetName(1);
                Console.WriteLine();
            }

            while (true)
            {
                //checks so answer is an int, and is within the monkey list perimiters
                Console.WriteLine("Type a appropriate value");

                string answer = Console.ReadLine() ?? "";

                if (int.TryParse(answer, out int intAnswer))
                {
                    int intAnsCorrected = intAnswer - 1;
                    if (intAnsCorrected >= 0 && intAnsCorrected < monkeys.Count)
                    {
                        //UGLY CODE: this checks what monkey type has been chosen, as only certain monkeys have a effect that can be upgraded
                        if (monkeys[intAnsCorrected] is SlowMoney effMonkey)
                        {
                            //casts choen monkey to effmonkey and applies changes
                            effMonkey.attackDamage += damageIncrease;
                            effMonkey.effectAmount += effectIncrease;
                            break;
                        }
                        else
                        {
                            //all other monkeys cant have an effect applied to it, if player chose invalid to upgrade, you just loose the ability.
                            monkeys[intAnsCorrected].attackDamage += damageIncrease;
                        }
                    }

                }
            }

        }
    }
}