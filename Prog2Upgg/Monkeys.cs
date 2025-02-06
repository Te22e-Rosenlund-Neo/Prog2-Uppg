using Balloons;

namespace Monkeys
{

    class Monkey
    {

        private string Name;
        private int attackDamage;
        private int attackSpeed;
        public int Cost;
        public string type;

        public Monkey(string name, int AttackDamage, int AttackSpeed, int Cost)
        {

            Name = name;
            this.attackSpeed = AttackSpeed;
            this.attackDamage = AttackDamage;
            this.Cost = Cost;

        }

        public void Showstats()
        {
            Console.WriteLine($"Type: {Name}");
            Console.WriteLine($"Damage: {attackDamage}");
            Console.WriteLine($"Rounds between attacks: {attackSpeed}");
        }
        public int attack()
        {
            return attackDamage;
        }


    }

    class EffMonkey : Monkey
    {
        public EffMonkey(string name, int AttackDamage, int AttackSpeed, int cost) : base(name, AttackDamage, AttackSpeed, cost) { }
    }

    class MultiAttMonkey : EffMonkey
    {
        int attackAmount;
        public MultiAttMonkey(string name, int AttackDamage, int AttackSpeed, int attackAmount, int cost) : base(name, AttackDamage, AttackSpeed, cost)
        {
            this.attackAmount = attackAmount;
        }

    }

    class SlowMoney : EffMonkey
    {
        int slowAmount;
        public SlowMoney(string name, int AttackDamage, int AttackSpeed, int Slow, int cost) : base(name, AttackDamage, AttackSpeed, cost)
        {
            slowAmount = Slow;
        }

        private int SlowBloon()
        {
            return slowAmount;
        }

    }
}