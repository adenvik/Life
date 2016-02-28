using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Herbivorous : Animal
    {
        public Herbivorous(double age, bool sex)
            : base(age, sex)
        {

        }

        public void eat(WorldObject wo)
        {
            if (wo is Plant)
            {
                wo.health = 0;
                this.hunger = 0.0;
                this.health = this.health + wo.health / 10;
                this.health = (this.health > 100) ? 100 : this.health;
            }
        }
    }
}
