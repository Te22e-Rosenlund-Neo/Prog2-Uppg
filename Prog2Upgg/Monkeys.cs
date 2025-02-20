using System.Security.Cryptography.X509Certificates;
using Balloons;

namespace Monkeys
{

    class Monkey
    {

        public string Name;
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
//shows default information about the monkey
        public void Showstats()
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
        public int getdamage(){
            return attackDamage;
        }
        public string GetName()
        {
            return Name;
        }

        public virtual void ApplyEffect(Bloon target){
//base method to override
        }

    }
//useless parenting, here more for display and organizing
    class EffMonkey : Monkey
    {
        public EffMonkey(string name, int AttackDamage, int AttackSpeed, int cost, string Description) : base(name, AttackDamage, AttackSpeed, cost, Description) { }
    }

    class MultiAttMonkey : EffMonkey
    {
        int attackAmount;
        public MultiAttMonkey(string name, int AttackDamage, int AttackSpeed, int attackAmount, int cost, string Description) : base(name, AttackDamage, AttackSpeed, cost, Description)
        {
            this.attackAmount = attackAmount;
        }

//overrides base method, attacks twice, split between 2 targets if 2 exists
         public override void Attack(Bloon target, Bloon target2)
        {
            if(target == target2){
                base.Attack(target, target2);
            }else{
                base.Attack(target, target2);
                base.Attack(target2, target);
            }
        }
    }

    class SlowMoney : EffMonkey
    {
        int slowAmount;
        public SlowMoney(string name, int AttackDamage, int AttackSpeed, int Slow, int cost, string Description) : base(name, AttackDamage, AttackSpeed, cost, Description)
        {
            slowAmount = Slow;
        }
//makes enemy bloon take longer to reach its destination
        public override void ApplyEffect(Bloon target)
        {
            target.ChangeCoolDown(slowAmount);
            
        }

    }
}