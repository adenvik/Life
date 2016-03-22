using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    abstract class Animal : WorldObject
    {
        public bool sex { set; get; }               //0 - Female 1 - Male
        public double hunger { set; get; }          //Показатель голода животного
        public int type { set; get; }               //Тип животного

        public Animal(double age, double maxAge, bool sex, int type)
            : base(age, maxAge)
        {
            this.hunger = 0.1;
            this.sex = sex;
            this.type = type;
        }

        public override string ToString()
        {
            return this.GetType().ToString().Substring(this.GetType().ToString().LastIndexOf('.')) 
                + " sex: " + sex.ToString() + " hunger: " + hunger.ToString() + " type: " + type.ToString() 
                + " health: " + health.ToString() + " age: " + age.ToString() + " maxAge: " + maxAge.ToString();  
        }

        abstract public void eat(WorldObject wo);
        abstract public void growthOfHunger();
    }
}
