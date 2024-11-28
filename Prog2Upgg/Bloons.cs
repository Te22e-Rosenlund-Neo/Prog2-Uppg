namespace Balloons{


    public class Bloon{


        private string name;
        private int damage;
        private int speed;
        public int health;

        public Bloon(string name, int damage, int speed, int health){
            this.name = name;  
            this.damage = damage;   
            this.speed = speed; 
            this.health = health;  
        }


    public int ReduceCooldown(){
        return speed;
    }
    public int attack(){
        return damage;
    }
    }


    public class MOAB : Bloon{
        public MOAB(string name, int damage, int speed, int health) : base(name, damage, speed, health) {}

        }

    }

