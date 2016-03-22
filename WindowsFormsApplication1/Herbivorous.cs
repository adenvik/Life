using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Herbivorous : Animal
    {
        public Herbivorous(double age, double maxAge, bool sex, int type)
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
                this.hunger += 4;
                this.health -= 4;
            }
            if (type == 2)
            {
                this.hunger += 3.3;
                this.health -= 3;
            }
            if (type > 2)
            {
                this.hunger += type + 0.23;
                this.health -= type + 0.23;
            }
        }
    }
}
