using Balloons;

namespace Monkeys
{

    class Monkey : IBuyable
    {

        private string name;
        public int attackDamage { get; set; }
        private int attackSpeed;
        public int cost { get; set; }
        public string description { get; set; }
        public Bloon användsInteMenÄrHärInSpirit = new Bloon("test", 1, 1, 1, ConsoleColor.Black);

        public Monkey(string name, int attackDamage, int attackSpeed, int cost, string description)
        {

            this.name = name;
            this.attackSpeed = attackSpeed;
            this.attackDamage = attackDamage;
            this.cost = cost;
            this.description = description;

        }
        //shows default information about the monkey for the shop
        public virtual void ShowStats()
        {
            Console.WriteLine($"Monkey Type: {name}");
            Console.WriteLine($"Damage: {attackDamage}");
            Console.WriteLine(description);
            Console.WriteLine($"Cost: {cost}");
        }
        //damages the enemy bloon, second parameter is just there for the override
        public virtual void Attack(Bloon target, Bloon target2)
        {
            target.TakeDamage(attackDamage);
        }

        //Displays either just name, or the info given when the monkey is attacking. 
        public virtual void GetName(int option)
        {
            if (option == 1)
            {
                Console.Write($"{name} , ");
            }
            else
            {
                Console.WriteLine($"Who should {name} attack? (damage: {attackDamage}) (write the number)");
            }
        }

        //base method that overrides later
        public virtual void ApplyEffect(Bloon target)
        {
        }

    }
    //Class used to change colors of monkeys that dont just simply attack, but has an effect
    class EffMonkey : Monkey
    {
        private string name;
        private int attackDamage;
        public EffMonkey(string name, int attackDamage, int attackSpeed, int cost, string description) : base(name, attackDamage, attackSpeed, cost, description)
        {
            this.name = name;
            this.attackDamage = attackDamage;
        }
        //shows simple stats in special color
        public override void ShowStats()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Type: {name}");
            Console.WriteLine($"Damage: {attackDamage}");
            Console.WriteLine(description);
            Console.WriteLine($"Cost: {cost}");
            Console.ResetColor();
        }
        //displays name in a color
        public override void GetName(int option)
        {
            if (option == 1)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{name} , ");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine($"Who should {name} attack? (damage: {attackDamage}) (write the number)");
            }
        }
    }

    //class which allows enemy to target 2 bloons at once
    class MultiAttMonkey : EffMonkey
    {

        public MultiAttMonkey(string name, int attackDamage, int attackSpeed, int cost, string Description) : base(name, attackDamage, attackSpeed, cost, Description)
        {

        }

        //overrides base method, attacks twice, split between 2 targets if 2 exists, otherwise attacks same target twice
        public override void Attack(Bloon target, Bloon target2)
        {
            base.Attack(target, target2);
            base.Attack(target2, target);


        }
    }

    //monkey that has purpose of slowing down enemy
    class SlowMoney : EffMonkey
    {
        private int effectAmount;
        public SlowMoney(string name, int attackDamage, int attackSpeed, int effectC, int cost, string description) : base(name, attackDamage, attackSpeed, cost, description)
        {
            effectAmount = effectC;
        }

        public void ChangeEffAmount(int difference)
        {
            effectAmount += difference;
        }
        //makes enemy bloon take longer to reach its destination (before it can hurt the player)
        public override void ApplyEffect(Bloon target)
        {
            target.ChangeCoolDown(effectAmount);

        }

    }
}