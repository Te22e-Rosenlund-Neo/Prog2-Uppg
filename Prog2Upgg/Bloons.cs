using Monkeys;
namespace Balloons{
//redbloon, yellow bloon, black bloon, white bloon, rainbow bloon, blue moab, DDT

    public class Bloon{


        private string name;
        private int damage;
        private int speed;
        public int health;
        // variable thats into the class and not a object, cant be changed via object referencem
        public static int enemyCount;

        public Bloon(string name, int damage, int speed, int health){
            this.name = name;  
            this.damage = damage;   
            this.speed = speed; 
            this.health = health;  
            enemyCount ++;
        }

    public int ReduceCooldown(){
        return speed;
    }
    public int attack(){
        return damage;
    }
    }


    public class SpecialBloon : Bloon{

        private string weakness {set; get;}
        public SpecialBloon(string name, int damage, int speed, int health, string weakness) : base(name, damage, speed, health){
            this.weakness = weakness;
        }
        
    }
    public class MOAB : Bloon{
        public MOAB(string name, int damage, int speed, int health) : base(name, damage, speed, health) {}
            
        }

    }

