using Monkeys;
namespace Upgrade
{

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



        public void ShowStats()
        {
            Console.WriteLine($"UpgradeType: {name}");
            Console.WriteLine($"Damage increase amount: {damageIncrease}");
            Console.WriteLine($"Effect increase amount: {effectIncrease}");
            Console.WriteLine($"Cost: {Cost}");
        }

        public void ApplyUpgrade(List<Monkey> monkeys)
        {
            Console.Clear();
            Console.WriteLine("Please select the monkey u wanna upgrade by typing the index;");

            for (int i = 0; i < monkeys.Count; i++)
            {
                Console.WriteLine($"{i + 1}: ");
                monkeys[i].GetName(1);
                Console.WriteLine();
            }

            while (true)
            {
                Console.WriteLine("Type a appropriate value");

                string answer = Console.ReadLine() ?? "";

                if (int.TryParse(answer, out int intAnswer))
                {
                    int intAnsCorrected = intAnswer - 1;
                    if (intAnsCorrected >= 0 && intAnsCorrected < monkeys.Count)
                    {
                        if (monkeys[intAnsCorrected] is SlowMoney effMonkey)
                        {
                            effMonkey.attackDamage += damageIncrease;
                            effMonkey.effectAmount += effectIncrease;
                            break;
                        }
                        else if (monkeys[intAnsCorrected] is Monkey normalMonkey)
                        {
                            normalMonkey.attackDamage += damageIncrease;
                            break;

                        }
                        else if (monkeys[intAnsCorrected] is MultiAttMonkey multMonkey)
                        {
                            multMonkey.attackDamage += damageIncrease;
                            break;
                        }
                    }
                }

            }
        }

    }
}