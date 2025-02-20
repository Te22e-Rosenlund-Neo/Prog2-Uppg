using Monkeys;
namespace Balloons
{
    //redbloon, yellow bloon, black bloon, white bloon, rainbow bloon, blue moab, DDT

    public class Bloon
    {


        private string name;
        private int damage;
        private int speed;
        private string symbol;
        public int health;

        public Bloon(string name, int damage, int speed, int health, string symbol)
        {
            this.name = name;
            this.damage = damage;
            this.speed = speed;
            this.health = health;
            this.symbol = symbol;
        }

        public int GiveSpeed(){
            return speed;
        }
        
        public int attack()
        {
            return damage;
        }
        public void takeDamage(int amount){
            health -= amount;
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
        public void ChangeCoolDown(int amount){
            speed += amount;
            
        }
    }

//technically useless inheritance, just here for display and sorting if more bloons are added with more features.
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

