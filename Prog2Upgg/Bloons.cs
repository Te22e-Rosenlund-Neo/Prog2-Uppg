namespace Balloons
{
    //redbloon, yellow bloon, black bloon, white bloon, rainbow bloon, blue moab, DDT

    public class Bloon
    {


        private string name;
        private int damage;
        private int speed;
        public int health;
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
        public static List<Bloon> CheckForRemoval(Bloon checker, List<Bloon> AllBloons){
            
            if(checker.health <= 0){
                AllBloons.RemoveAt(AllBloons.IndexOf(checker));
                Console.WriteLine("You killed a bloon, press enter to confirm");
                Console.ReadLine();
            }

            return AllBloons;
        }
        
    }
}

