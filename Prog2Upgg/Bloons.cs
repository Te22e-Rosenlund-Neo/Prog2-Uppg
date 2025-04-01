namespace Balloons
{
    //redbloon, yellow bloon, black bloon, white bloon, rainbow bloon, blue moab, DDT

    public class Bloon
    {

        
        private string name;
        private int damage;
        private int speed;
        int health { get; set;}
        private ConsoleColor color;

        public Bloon(string name, int damage, int speed, int health, ConsoleColor color)
        {
            this.name = name;
            this.damage = damage;
            this.speed = speed;
            this.health = health;
            this.color = color;
        }
//displays speed
        public int GiveSpeed()
        {
            return speed;
        }
//returns attack damage
        public int Attack()
        {
            return damage;
        }
//takes damage based on input
        public void TakeDamage(int amount)
        {
            health -= amount;
        }
//displays attack text in color
        public void ShortDisplay(){
            Console.ForegroundColor = color;
            Console.WriteLine($"{GetName()}: hp: {health}: attacks in: {GiveSpeed()} rounds");
            Console.ResetColor();
        }
//increases time until it reaches target
        public void ChangeCoolDown(int amount)
        {
            speed += amount;

        }
//displays name
        public string GetName(){
            return name;
        }
//checks if a bloon health is less that 0, if so removes itself from list
        public virtual List<Bloon> CheckForRemoval( List<Bloon> AllBloons){
            
            if(health <= 0){
                AllBloons.RemoveAt(AllBloons.IndexOf(this));
                Console.WriteLine("You killed a bloon, press enter to confirm");
                Console.ReadLine();
            }
            return AllBloons;
        }
        
    }

    public class Moab : Bloon{

        private int health;
        public Moab(string name, int damage, int speed, int health, ConsoleColor color): base(name, damage, speed, health, color){
            this.health = health;

        }

        public override List<Bloon> CheckForRemoval(List<Bloon> AllBloons)
        {
            if(health <= 0){
                AllBloons.RemoveAt(AllBloons.IndexOf(this));
                Console.WriteLine("You killed the moab, however it spawned two bloons upon defeat");
                Bloon redBloon = new Bloon("RedBloon", 20, 1, 2, ConsoleColor.Red);
                Bloon yellowBloon = new Bloon("YellowBloon", 1, 1, 3, ConsoleColor.Yellow);
                AllBloons.Add(redBloon);
                AllBloons.Add(yellowBloon);
            }   

            return AllBloons;
        }
        

    }
}

