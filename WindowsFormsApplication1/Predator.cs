using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Predator : Animal
    {
        public Predator(double age, double maxAge, bool sex, int type)
            : base(age, maxAge, sex, type)
        {

        } 

        public override void eat(WorldObject wo)
        {
            this.hunger = 0.0;
            this.health = this.health + wo.health / 10;
            this.health = (this.health > 100) ? 100 : this.health;
        }

        public override void growthOfHunger()
        {
            if (type == 1)
            {
                this.hunger += 6.4;
                this.health -= 7;
            }
            if (type == 2)
            {
                this.hunger += 5.7;
                this.health -= 5;
            }
            if (type > 2)
            {
                this.hunger += type + 2.13;
                this.health -= type + 2.21;
            }
        }
    }
}
