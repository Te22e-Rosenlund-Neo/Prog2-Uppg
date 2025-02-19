using Monkeys;
namespace Balloons
{
    //redbloon, yellow bloon, black bloon, white bloon, rainbow bloon, blue moab, DDT

    public class Bloon
    {


        private string name;
        private int damage;
        public int speed;
        private string symbol;
        public int health;
        // variable thats into the class and not a object, cant be changed via object referencem
        public static int enemyCount;

        public Bloon(string name, int damage, int speed, int health, string symbol)
        {
            this.name = name;
            this.damage = damage;
            this.speed = speed;
            this.health = health;
            this.symbol = symbol;
            enemyCount++;
        }

        public int ReduceCooldown()
        {
            return speed;
        }
        public int GiveSpeed(){
            return speed;
        }
        public int attack()
        {
            return damage;
        }
        public void DisplayBaseStats()
        {
            Console.Write($"Name{name}  ");
            Console.Write($"Hp{health}");
        }
        public string Symbol()
        {
            return symbol;
        }
    }


    public class SpecialBloon : Bloon
    {

        private string weakness { set; get; }
        public SpecialBloon(string name, int damage, int speed, int health, string weakness, string symbol) : base(name, damage, speed, health, symbol)
        {
            this.weakness = weakness;
        }

    }
    public class MOAB : Bloon
    {
        public MOAB(string name, int damage, int speed, int health, string symbol) : base(name, damage, speed, health, symbol) { }

    }

}

