using Balloons;

namespace Monkeys
{

    class Monkey
    {

        private string Name;
        private int attackDamage;
        private int attackSpeed;
        public int Cost;
        public string Description;

        public Monkey(string name, int AttackDamage, int AttackSpeed, int Cost, string Description)
        {

            this.Name = name;
            this.attackSpeed = AttackSpeed;
            this.attackDamage = AttackDamage;
            this.Cost = Cost;
            this.Description = Description;

        }
        //shows default information about the monkey for the shop
        public virtual void Showstats()
        {
            Console.WriteLine($"Type: {Name}");
            Console.WriteLine($"Damage: {attackDamage}");
            Console.WriteLine(Description);
            Console.WriteLine($"Cost: {Cost}");
        }
        //damages the enemy bloon, second parameter is just there for the override
        public virtual void Attack(Bloon target, Bloon target2)
        {
            target.takeDamage(attackDamage);
        }

        //Displays either just name, or the info given when the monkey is attacking. 
        public virtual void GetName(int option)
        {
            if (option == 1)
            {
                Console.Write($"{Name} , ");
            }
            else
            {
                Console.WriteLine($"Who should {Name} attack? (damage: {attackDamage}) (write the number)");
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
        string name = "";
        int attackDamage;
        public EffMonkey(string name, int AttackDamage, int AttackSpeed, int cost, string Description) : base(name, AttackDamage, AttackSpeed, cost, Description)
        {
            this.name = name;
            this.attackDamage = AttackDamage;
        }
//shows simple stats in special color
        public override void Showstats()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Type: {name}");
            Console.WriteLine($"Damage: {attackDamage}");
            Console.WriteLine(Description);
            Console.WriteLine($"Cost: {Cost}");
            Console.ResetColor();
        }
//displays name in a colro
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

    class MultiAttMonkey : EffMonkey
    {
        int attackAmount;
        public MultiAttMonkey(string name, int AttackDamage, int AttackSpeed, int attackAmount, int cost, string Description) : base(name, AttackDamage, AttackSpeed, cost, Description)
        {
            this.attackAmount = attackAmount;
        }

        //overrides base method, attacks twice, split between 2 targets if 2 exists, otherwise attacks same target twice
        public override void Attack(Bloon target, Bloon target2)
        {
            base.Attack(target, target2);
            base.Attack(target2, target);


        }
    }

    class SlowMoney : EffMonkey
    {
        int slowAmount;
        public SlowMoney(string name, int AttackDamage, int AttackSpeed, int Slow, int cost, string Description) : base(name, AttackDamage, AttackSpeed, cost, Description)
        {
            slowAmount = Slow;
        }
        //makes enemy bloon take longer to reach its destination (before it can hurt the player)
        public override void ApplyEffect(Bloon target)
        {
            target.ChangeCoolDown(slowAmount);

        }

    }
}